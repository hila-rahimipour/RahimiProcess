using System;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace POC_NEW
{
    class IdealProcessor
    {
        const uint THREAD_QUERY_INFORMATION = 0x0040;

        [StructLayout(LayoutKind.Sequential)]
        struct PROCESSOR_RELATIONSHIP
        {
            public byte Flags;
            public byte EfficiencyClass;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] Reserved;
            public ushort GroupCount;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 128)]
            public GROUP_AFFINITY[] GroupMask;
        }

        [StructLayout(LayoutKind.Explicit)]
        struct GROUP_AFFINITY
        {
            [FieldOffset(0)]
            public ulong Mask;
            [FieldOffset(0)]
            public ushort Group;
            [FieldOffset(2)]
            public ushort Reserved;
        }

        [StructLayout(LayoutKind.Sequential)]
        struct PROCESSOR_NUMBER
        {
            public ushort Group;
            public byte Number;
            public byte Reserved;
        }
        [DllImport("kernel32.dll")]
        static extern IntPtr OpenThread(uint dwDesiredAccess, bool bInheritHandle, uint dwThreadId);

        [DllImport("kernel32.dll")]
        static extern bool GetThreadIdealProcessorEx(IntPtr hThread, out PROCESSOR_NUMBER lpIdealProcessor);

        static void MMain(string[] args)
        {
            uint threadId = 1032;
            // Thread ID of the thread you want to get the logical processor number for
            IntPtr thread = OpenThread(THREAD_QUERY_INFORMATION, false, threadId);
            PROCESSOR_NUMBER idealProcessor;
            if (thread != IntPtr.Zero)
            {
                if (GetThreadIdealProcessorEx(thread, out idealProcessor))
                {
                    byte processor = idealProcessor.Number;
                    string byteString = string.Format("{0:X2}", processor);
                    Console.WriteLine("Byte value: {0}", byteString);
                }
            }
            Console.ReadLine();
        }
    }
}
