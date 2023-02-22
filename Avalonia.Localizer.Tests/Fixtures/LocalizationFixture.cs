using System.Collections.Concurrent;
using System.Globalization;
using Avalonia.Localizer.Tests.Utils;
using Newtonsoft.Json;

namespace Avalonia.Localizer.Tests.Fixtures
{
    /// <summary>
    /// Fixture to load localization packages in the Test Environment
    /// </summary>
    public class LocalizationFixture : IDisposable
    {
        /// <summary>
        /// Localization package for ru-Ru locale
        /// </summary>
        public ConcurrentDictionary<string, string>? SlPackage { get; }

        /// <summary>
        /// Localization package for en-US locale
        /// </summary>
        public ConcurrentDictionary<string, string>? EnPackage { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="LocalizationFixture"/> class.
        /// </summary>
        /// <exception cref="CultureNotFoundException"> Missing localization packages in the Test Environment </exception>
        public LocalizationFixture()
        {
            var ruSourceStr = ResourceHelper.ReadResourceAsString(System.Reflection.Assembly.GetExecutingAssembly(), "Assets.Localization", "ru-RU.json");

            if (ruSourceStr == null)
            {
                throw new CultureNotFoundException("Missing ru-Ru localization package in the Test environment.");
            }

            SlPackage = JsonConvert.DeserializeObject<ConcurrentDictionary<string, string>>(ruSourceStr);

            var enSourceStr = ResourceHelper.ReadResourceAsString(System.Reflection.Assembly.GetExecutingAssembly(), "Assets.Localization", "en-US.json");

            if (enSourceStr == null)
            {
                throw new CultureNotFoundException("Missing en-US localization package in the Test environment.");
            }

            EnPackage = JsonConvert.DeserializeObject<ConcurrentDictionary<string, string>>(enSourceStr);
        }

        /// <inheritdoc/>
        public void Dispose()
        {
        }
    }
}
