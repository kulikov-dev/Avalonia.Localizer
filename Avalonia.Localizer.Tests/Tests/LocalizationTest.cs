using Avalonia.Localizer.Tests.Fixtures;

namespace Avalonia.Localizer.Tests.Tests
{
    /// <summary>
    /// Localization service tests
    /// </summary>
    public class LocalizationTest : IClassFixture<LocalizationFixture>
    {
        private readonly LocalizationFixture _fixture;

        public LocalizationTest(LocalizationFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact(DisplayName = "Check local packages file structure.")]
        public void ValidatePackagesLoading()
        {
            Assert.NotNull(_fixture.SlPackage);
            Assert.NotNull(_fixture.EnPackage);
        }

        [Fact(DisplayName = "Check local packages composition.")]
        public void ValidatePackagesComposition()
        {
            _ = AvaloniaApp.GetApp();
            var languages = Core.ProgramCore.Localizer.AvailableLanguages;

            Assert.Equal(2, languages.Count);
            Assert.Contains("en-US", languages);
            Assert.Contains("sl-SL", languages);
        }

        [Fact(DisplayName = "Check sl-SL local package.")]
        public void ValidateSlLocalization()
        {
            _ = AvaloniaApp.GetApp();

            Core.ProgramCore.Localizer.SwitchLanguage("sl-SL");
            foreach (var item in _fixture.SlPackage!)
            {
                Assert.Equal(item.Value, Core.ProgramCore.Localizer[item.Key]);
            }
        }

        [Fact(DisplayName = "Check en-US local package.")]
        public void ValidateEnLocalization()
        {
            _ = AvaloniaApp.GetApp();

            Core.ProgramCore.Localizer.SwitchLanguage("en-US");
            foreach (var item in _fixture.EnPackage!)
            {
                Assert.Equal(item.Value, Core.ProgramCore.Localizer[item.Key]);
            }
        }

        [Fact(DisplayName = "Check language switching in code.")]
        public void CheckSwitchLanguage()
        {
            _ = AvaloniaApp.GetApp();

            Core.ProgramCore.Localizer.SwitchLanguage("en-US");

            var firstItem = _fixture.EnPackage?.FirstOrDefault();

            Assert.NotNull(firstItem);
            Assert.Equal(firstItem!.Value.Value, Core.ProgramCore.Localizer[firstItem.Value.Key]);

            Core.ProgramCore.Localizer.SwitchLanguage("sl-SL");

            firstItem = _fixture.SlPackage?.FirstOrDefault();

            Assert.NotNull(firstItem);
            Assert.Equal(firstItem!.Value.Value, Core.ProgramCore.Localizer[firstItem.Value.Key]);
        }
    }
}
