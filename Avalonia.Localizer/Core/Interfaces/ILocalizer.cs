using System.Collections.Generic;

namespace Avalonia.Localizer.Core.Interfaces
{
    /// <summary>
    /// Interface for localization service
    /// </summary>
    internal interface ILocalizer
    {
        /// <summary>
        /// Gets current language code in format: 'en-US'
        /// </summary>
        /// <value> Current language code </value>
        string Language { get; }

        /// <summary>
        /// Gets list of languages with available localization packages in format: 'en-US'
        /// </summary>
        /// <value> List of available languages </value>
        List<string> AvailableLanguages { get; }

        /// <summary>
        /// Localize to current language by key in the package
        /// </summary>
        /// <param name="key"> Key </param>
        /// <returns> Localized sentence </returns>
        string this[string key]
        {
            get;
        }

        /// <summary>
        /// Switch current language
        /// </summary>
        /// <param name="languageCode"> Language code in format: 'en-US' </param>
        /// <returns> True, if switched </returns>
        bool SwitchLanguage(string languageCode);
    }
}
