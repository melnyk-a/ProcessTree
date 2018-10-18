using ProcessTree.Utilities.Marshaling;
using System.Collections.Generic;

namespace ProcessTree.DomainModels
{
    public sealed class ExtendProcess
    {
        private readonly ICollection<ExtendProcess> children = new List<ExtendProcess>();
        private readonly string name;
        private readonly int parentId;
        private readonly int processId;

        public ExtendProcess(ProcessEntry entry)
        {
            name = entry.Name;
            parentId = entry.ParentId;
            processId = entry.ProcessId;
        }

        public ICollection<ExtendProcess> Children => children;

        public string Name => name;

        public int ParentId => parentId;

        public int ProcessId => processId;

        public void AddChild(ExtendProcess child)
        {
            children.Add(child);
        }
    }
}