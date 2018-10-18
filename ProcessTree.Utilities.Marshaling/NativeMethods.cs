using System.Runtime.InteropServices;

namespace ProcessTree.Utilities.Marshaling
{
    public static class NativeMethods
    {
        private const string KernelPath = "Kernel32.dll";

        [DllImport(KernelPath)]
        public static extern bool CloseHandle(int handle);

        [DllImport(KernelPath, EntryPoint = "CreateToolhelp32Snapshot")]
        public static extern int CreateSnapshot(SnapshotFlag snapshotFlags, int processId);

        [DllImport(KernelPath, EntryPoint = "Process32First")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetFirstProcess(int snapshotHandle, ref ProcessEntry processEntry);

        [DllImport(KernelPath, EntryPoint = "Process32Next")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetNextProcess(int snapshotHandle, ref ProcessEntry processEntry);
    }
}