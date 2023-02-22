using System;
using System.Collections.Generic;
using System.Linq;

namespace Avalonia.Localizer.ViewModels
{
    internal class MainWindowViewModel
    {
        public List<SampleEnum> Samples => Enum.GetValues<SampleEnum>().Cast<SampleEnum>().ToList();

        public SampleEnum SelectedSample { get; set; }
    }
}
