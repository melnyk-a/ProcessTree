using System;
using System.Runtime.InteropServices;

namespace ProcessTree.Utilities.Marshaling
{
    public struct ProcessEntry 
    {
        private int dwSize;
        private int cntUsage;
        private int th32ProcessID;
        private IntPtr th32DefaultHeapID;
        private int th32ModuleID;
        private int cntThreads;
        private int th32ParentProcessID;
        private int pcPriClassBase;
        private int dwFlags;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
        private  string szExeFile;

        public string Name => szExeFile;

        public int ParentId => th32ParentProcessID;

        public int ProcessId => th32ProcessID;

        public int Size 
        {
            get => dwSize;
            set => dwSize = value;
        }
    }
}