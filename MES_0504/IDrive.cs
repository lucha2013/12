using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MES_0504
{
    [StructLayout(LayoutKind.Sequential)]
    public struct DeviceAddress : IComparable<DeviceAddress>
    {
        public int Area;
        public int Start;
        public ushort DBNumber;
        public ushort DataSize;
        public ushort CacheIndex;
        public byte Bit;
        public DataType VarType;
        public ByteOrder ByteOrder;

        public DeviceAddress(int area, ushort dbnumber, ushort cIndex, int start, ushort size, byte bit, DataType type, ByteOrder order = ByteOrder.None)
        {
            Area = area;
            DBNumber = dbnumber;
            CacheIndex = cIndex;
            Start = start;
            DataSize = size;
            Bit = bit;
            VarType = type;
            ByteOrder = order;
        }

        public static readonly DeviceAddress Empty = new DeviceAddress(0, 0, 0, 0, 0, 0, DataType.NONE);

        public int CompareTo(DeviceAddress other)
        {
            return this.Area > other.Area ? 1 :
                this.Area < other.Area ? -1 :
                this.DBNumber > other.DBNumber ? 1 :
                this.DBNumber < other.DBNumber ? -1 :
                this.Start > other.Start ? 1 :
                this.Start < other.Start ? -1 :
                this.Bit > other.Bit ? 1 :
                this.Bit < other.Bit ? -1 : 0;
        }
    }
    public interface IReaderWriter
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
    public interface IMultiReadWrite
    {
        int Limit { get; }
        ItemData<Storage>[] ReadMultiple(DeviceAddress[] addrsArr);
        int WriteMultiple(DeviceAddress[] addrArr, object[] buffer);
    }

    public interface IDriver : IDisposable
    {
        short ID { get; }
        string Name { get; }
        string ServerName { get; set; }//可以考虑增加一个附加参数，Sever只定义本机名
        bool IsClosed { get; }
        int TimeOut { get; set; }
        IEnumerable<IGroup> Groups { get; }
        IDataServer Parent { get; }
        bool Connect();
        IGroup AddGroup(string name, short id, int updateRate, float deadBand = 0f, bool active = false);
        bool RemoveGroup(IGroup group);
        event IOErrorEventHandler OnError;
    }

    public interface IPLCDriver : IDriver, IReaderWriter
    {
        int PDU { get; }
        DeviceAddress GetDeviceAddress(string address);
        string GetAddress(DeviceAddress address);
    }

    public interface IFileDriver : IDriver, IReaderWriter
    {
        string FileName { get; set; }
        FileData[] ReadAll(short groupId);
        //bool RecieveData(string data);
    }

}
