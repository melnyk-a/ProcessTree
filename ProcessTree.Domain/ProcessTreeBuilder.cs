using ProcessTree.DomainModels;
using ProcessTree.Utilities.Marshaling;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace ProcessTree.Domain
{
    public sealed class ProcessTreeBuilder
    {
        private readonly IList<ExtendProcess> orderedNodes = new List<ExtendProcess>();
        private readonly IList<ExtendProcess> unorderedNodes = new List<ExtendProcess>();

        public ProcessTreeBuilder()
        {
            int snapshot = NativeMethods.CreateSnapshot(SnapshotFlag.TH32CS_SNAPPROCESS, 0);

            ProcessEntry entry = new ProcessEntry()
            {
                Size = Marshal.SizeOf<ProcessEntry>()
            };

            if (NativeMethods.GetFirstProcess(snapshot, ref entry))
            {
                do
                {
                    unorderedNodes.Add(new ExtendProcess(entry));
                } while (NativeMethods.GetNextProcess(snapshot, ref entry));
            }
            NativeMethods.CloseHandle(snapshot);
        }

        public IEnumerable<ExtendProcess> Build()
        {
            var sortedNodesByParentId = unorderedNodes.OrderByDescending(node => node.ParentId).ToArray();

            for (int i = 0; i < sortedNodesByParentId.Length; ++i)
            {
                ExtendProcess parent = FindInTree(sortedNodesByParentId[i].ParentId);
                if (parent != null)
                {
                    parent.Children.Add(sortedNodesByParentId[i]);
                }
                else
                {
                    orderedNodes.Add(sortedNodesByParentId[i]);
                }
            }

            return orderedNodes;
        }


        private ExtendProcess FindInTree(int itemId)
        {
            ExtendProcess finded = null;
            foreach (ExtendProcess node in unorderedNodes)
            {
                if (node.ProcessId.Equals(itemId))
                {
                    finded = node;
                    break;
                }
                else if (node.Children.Count != 0)
                {
                    finded = FindInNode(node, itemId);
                    if (finded != null)
                    {
                        break;
                    }
                }
            }
            return finded;
        }

        private ExtendProcess FindInNode(ExtendProcess node, int itemId)
        {
            ExtendProcess finded = null;
            foreach (ExtendProcess child in node.Children)
            {
                if (child.ProcessId.Equals(itemId))
                {
                    finded = child;
                    break;
                }
                else if (child.Children.Count != 0)
                {
                    finded = FindInNode(child, itemId);
                    if (finded != null)
                    {
                        break;
                    }
                }
            }
            return finded;
        }
    }
}