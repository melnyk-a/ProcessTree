using System;
using System.Diagnostics;

namespace ProcessTree.DomainModels
{
    internal sealed class ExtendProcess
    {
        private readonly Process process;

        public ExtendProcess(Process process)
        {
            this.process = process;
        }

        public Process ParentProcess => GetParentProcess();

        public Process Process => process;

        private Process GetParentProcess()
        {
            int parentId = 0;
            return Process.GetProcessById(parentId);
        }
    }
}