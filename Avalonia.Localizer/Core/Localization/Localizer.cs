using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using Avalonia.Localizer.Core.Interfaces;
using Avalonia.Platform;
using Newtonsoft.Json;

namespace Avalonia.Localizer.Core.Localization
{
    /// <summary>
    /// Localization service
    /// </summary>
    internal sealed class Localizer : INotifyPropertyChanged, ILocalizer
    {
        /// <summary>
        /// Path to localization assets
        /// </summary>
        private const string LocalizationPath = "avares://Avalonia.Localizer/Assets/Localization";

        /// <summary>
        /// Property name for changing notification
        /// </summary>
        private const string IndexerPropertyName = "Item";

        /// <summary>
        /// Property name for changing notification
        /// </summary>
        private const string IndexerPropertyArrayName = "Item[]";

        /// <summary>
        /// Dictionary to compare 'key' - 'localized sentence'
        /// </summary>
        private ConcurrentDictionary<string, string>? _dict;

        /// <summary>
        /// Initializes a new instance of the <see cref="Localizer"/> class.
        /// </summary>
        public Localizer()
        {
            LoadLocalizationPackages();
        }

        /// <summary>
        /// Event on the localization property changed
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged;

        /// <inheritdoc/>
        public string Language { get; private set; } = string.Empty;

        /// <inheritdoc/>
        public List<string> AvailableLanguages { get; private set; } = new();

        /// <inheritdoc/>
        public string this[string key]
        {
            get
            {
                if (_dict == null)
                {
                    throw new CultureNotFoundException("Localization packages don't initialized.");     // TODO учет кодов ошибок (возможно словарь с кодами и енамками)
                }

                if (string.IsNullOrWhiteSpace(Language))
                {
                    throw new CultureNotFoundException("Local language doesn't selected. Call to the 'SwitchLanguage' method.");
                }

                if (_dict.TryGetValue(key, out var res))
                {
                    return res.Replace("\\n", "\n");
                }

                return key;
            }
        }

        /// <summary>
        /// Switch current language of the program
        /// </summary>
        /// <param name="languageCode"> New language code in format: 'en-US' </param>
        /// <returns> True, if success </returns>
        /// <exception cref="CultureNotFoundException"> Missing a localization package for this language code </exception>
        public bool SwitchLanguage(string languageCode)
        {
            var assetsService = AvaloniaLocator.Current.GetService<IAssetLoader>();

            if (assetsService == null)
            {
                throw new AvaloniaInternalException("Avalonia core not initalized yet.");
            }

            var uri = new Uri($"{LocalizationPath}/{languageCode}.json");

            if (!assetsService.Exists(uri))
            {
                throw new CultureNotFoundException("Missing a localization package.");     // TODO учет кодов ошибок (возможно словарь с кодами и енамками)
            }

            using (var streamReader = new StreamReader(assetsService.Open(uri), Encoding.UTF8))
            {
                var packageData = streamReader.ReadToEnd();
                if (packageData == null)
                {
                    throw new DataException("Incorrect localization package format.");     // TODO учет кодов ошибок (возможно словарь с кодами и енамками)
                }

                var result = JsonConvert.DeserializeObject<ConcurrentDictionary<string, string>>(packageData);
                _dict = result ?? throw new DataException("Incorrect localization package format.");        // TODO учет кодов ошибок (возможно словарь с кодами и енамками)
            }

            Language = languageCode;

            OnPropertyChanged();
            return true;
        }

        /// <summary>
        /// Raise event on localization property changed
        /// </summary>
        private void OnPropertyChanged()
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(IndexerPropertyName));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(IndexerPropertyArrayName));
        }

        /// <summary>
        /// Load localization packages
        /// </summary>
        /// <exception cref="AvaloniaInternalException"> Service initialization called before Avalonia has been initialized </exception>
        /// <exception cref="CultureNotFoundException"> Missing localization packages </exception>
        private void LoadLocalizationPackages()
        {
            AvailableLanguages = new List<string>();

            var assetsService = AvaloniaLocator.Current.GetService<IAssetLoader>();

            if (assetsService == null)
            {
                throw new AvaloniaInternalException("Avalonia core not initalized yet.");
            }

            var dirUri = new Uri(LocalizationPath);
            var assets = assetsService.GetAssets(dirUri, null);

            foreach (var asset in assets)
            {
                var assetName = asset.Segments.LastOrDefault();

                if (string.IsNullOrWhiteSpace(assetName))
                {
                    continue;
                }

                var localeName = assetName[..assetName.LastIndexOf(".", StringComparison.Ordinal)];

                if (!string.IsNullOrWhiteSpace(localeName))
                {
                    AvailableLanguages.Add(localeName);
                }
            }

            if (AvailableLanguages.Count == 0)
            {
                throw new CultureNotFoundException("Missing localization packages");
            }
        }
    }
}
