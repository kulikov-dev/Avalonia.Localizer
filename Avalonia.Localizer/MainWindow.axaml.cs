using System.Linq;
using Avalonia.Controls;
using Avalonia.Localizer.Core;
using Avalonia.Localizer.ViewModels;
using Avalonia.Markup.Xaml;
using Avalonia.VisualTree;

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
            var cbLanguage = sender as ComboBox;

            _ = ProgramCore.Localizer.SwitchLanguage(ProgramCore.Localizer.AvailableLanguages[cbLanguage.SelectedIndex]);

            RefreshComboBoxes(cbLanguage);
        }

        private void RefreshComboBoxes(ComboBox owner)
        {
            //// Workaround due to avalonia issue connected with SelectedItem

            foreach (var comboBox in this.GetVisualDescendants().OfType<ComboBox>())
            {
                if (comboBox.SelectedIndex < 0 || comboBox == owner)
                {
                    continue;
                }

                var temp = comboBox.SelectedIndex;
                comboBox.SelectedIndex = -1;
                comboBox.SelectedIndex = temp;
            }
        }
    }
}
