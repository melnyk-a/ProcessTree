using ProcessTree.DomainModels;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ProcessTree.Presentation.Wpf.ViewModels
{
    internal sealed class ProcessViewModel
    {
        private readonly ICollection<ProcessViewModel> innerProcesses = new ObservableCollection<ProcessViewModel>();
        private readonly ExtendProcess process;
        
        public ProcessViewModel(ExtendProcess process)
        {
            this.process = process;

            foreach (ExtendProcess child in process.Children)
            {
                innerProcesses.Add(new ProcessViewModel(child));
            }
        }

        public ICollection<ProcessViewModel> InnerProcesses => innerProcesses;

        public string Name => process.Name;

        public int ProcessId => process.ProcessId;
    }
}