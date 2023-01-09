#include <windows.h>
#include <stdio.h>
int main()
{
    typedef struct _PROCESSOR_RELATIONSHIP {
        BYTE Flags;
        BYTE EfficiencyClass;
        BYTE Reserved[20];
        WORD GroupCount;
        GROUP_AFFINITY GroupMask[ANYSIZE_ARRAY];
    } PROCESSOR_RELATIONSHIP, * PPROCESSOR_RELATIONSHIP;

    DWORD thread_id = 6740;  // Thread ID of the thread you want to get the logical processor number for
    HANDLE thread = OpenThread(THREAD_QUERY_INFORMATION, FALSE, thread_id);
    PROCESSOR_NUMBER ideal_processor;

    if (thread != NULL)
    {
        if (GetThreadIdealProcessorEx(thread, &ideal_processor))
        {
            SYSTEM_LOGICAL_PROCESSOR_INFORMATION_EX buffer[128];
            DWORD return_length = 0;

            BYTE processor = ideal_processor.Number;
            char byte_string[16];
            sprintf_s(byte_string, "%02X", processor);
            printf("Byte value: %s\n", byte_string);
        }
    }
}