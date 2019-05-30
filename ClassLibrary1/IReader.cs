using System;
using System.Collections.Generic;

namespace ClassLibrary1
{
    public interface IReaderWrite
    {
        byte[] ReadBytes(DeviceAddress address, ushort size);
        ItemData<uint> ReadUInt32(DeviceAddress address);
        ItemData<int> ReadInt32(DeviceAddress address);
        ItemData<ushort> ReadUInt16(DeviceAddress address);
        ItemData<short> ReadInt16(DeviceAddress address);
        ItemData<byte> ReadByte(DeviceAddress address);
        ItemData<string> ReadString(DeviceAddress address, ushort size);
        ItemData<float> ReadFloat(DeviceAddress address);
        ItemData<bool> ReadBit(DeviceAddress address);
        ItemData<object> ReadValue(DeviceAddress address);

        int WriteBytes(DeviceAddress address, byte[] bit);
        int WriteBit(DeviceAddress address, bool bit);
        int WriteBits(DeviceAddress address, byte bits);
        int WriteInt16(DeviceAddress address, short value);
        int WriteUInt16(DeviceAddress address, ushort value);
        int WriteInt32(DeviceAddress address, int value);
        int WriteUInt32(DeviceAddress address, uint value);
        int WriteFloat(DeviceAddress address, float value);
        int WriteString(DeviceAddress address, string str);
        int WriteValue(DeviceAddress address, object value);
    }
    public interface IDriver : IDisposable
    {
        short ID { get; }
        string Name { get; }
        string ServerName { get; set; }
        bool IsClosed { get; }
        int TimeOut { get; set; }
        IEnumerable<IGroup> Groups { get; }
        IDataServer Parent { get; }
        bool Connect();
        IGroup AddGroup(string name, short id, int updateRate, float deadBand = 0f, bool active = false);
        bool RemoveGroup(IGroup group);
        event IOErrorEventHandler OnError;
    }
    public interface IPLCDrive : IDriver, IReaderWrite
    {
        int PDU { get; }
        DeviceAddress GetDeviceAddress(string address);
        string GetAddress(DeviceAddress address);
    }
}
