using Avalonia.Controls;
using Avalonia.Localizer.Core;
using Avalonia.Localizer.ViewModels;
using Avalonia.Markup.Xaml;

namespace Avalonia.Localizer
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            DataContext = new MainWindowViewModel();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        private void OnLanguageChanged(object sender, SelectionChangedEventArgs args)
        {
            var cb = sender as ComboBox;

            _ = ProgramCore.Localizer.SwitchLanguage(ProgramCore.Localizer.AvailableLanguages[cb.SelectedIndex]);
        }
    }
}
