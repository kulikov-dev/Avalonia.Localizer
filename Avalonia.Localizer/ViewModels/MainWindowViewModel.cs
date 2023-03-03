using System;
using System.Collections.Generic;
using System.Linq;
using ReactiveUI;

namespace Avalonia.Localizer.ViewModels
{
    /// <summary>
    /// Main window view model
    /// </summary>
    internal class MainWindowViewModel : ReactiveObject
    {
        /// <summary>
        /// List of samples
        /// </summary>
        public List<SampleEnum> Samples => Enum.GetValues<SampleEnum>().ToList();

        /// <summary>
        /// Selected sample
        /// </summary>
        public SampleEnum SelectedSample { get; set; }
    }
}
