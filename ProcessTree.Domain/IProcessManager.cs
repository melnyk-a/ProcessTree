using ProcessTree.DomainModels;
using System;
using System.Collections.Generic;

namespace ProcessTree.Domain
{
    public interface IProcessManager
    {
        event EventHandler<ErrorEventArgs> ErrorErised;

        void CloseProcess(int processId);

        IEnumerable<ExtendProcess> GetProcessTree();

        void StartProcess(string name);
    }
}