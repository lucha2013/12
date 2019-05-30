using System;


namespace ClassLibrary1
{
    [Flags]
    public enum ByteOrder : byte
    {
        None = 0,
        BigEndian = 1,
        LittleEndian = 2,
        Network = 4,
        Host = 8
    }
    public enum DataType : byte
    {
        NONE = 0,
        BOOL = 1,
        BYTE = 3,
        SHORT = 4,
        WORD = 5,
        DWORD = 6,
        INT = 7,
        FLOAT = 8,
        SYS = 9,
        STR = 11
    }
}
