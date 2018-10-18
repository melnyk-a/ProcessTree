using ProcessTree.DomainModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace ProcessTree.Domain
{
    public sealed class ProcessManager : IProcessManager
    {
        public event EventHandler<ErrorEventArgs> ErrorErised;

        public void CloseProcess(int processId)
        {
            Process process = Process.GetProcessById(processId);
            process.Kill();
        }

        public IEnumerable<ExtendProcess> GetProcessTree()
        {
            ProcessTreeBuilder tree = new ProcessTreeBuilder();
            return tree.Build();
        }

        public void StartProcess(string name)
        {
            try
            {
                Process.Start(name);
            }
            catch (System.ComponentModel.Win32Exception e)
            {
                OnErrorErised(new ErrorEventArgs(e.Message));
            }
        }

        private void OnErrorErised(ErrorEventArgs e)
        {
            ErrorErised?.Invoke(this, e);
        }
    }
}