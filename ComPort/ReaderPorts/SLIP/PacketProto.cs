using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReaderPorts
{
    internal class PacketProto
    {
        //public ushort HEADERSIZE;
        public byte Addr;
        public byte Cmd;
        public byte Size;
    }

    internal class GetCellStrPacket : PacketProto
    {
        internal class Tx : PacketProto
        {
            public byte Cell;
        }
        internal class Rx : PacketProto
        {
            public byte Cell;
        }
    }

    internal class GetDataPacket : PacketProto 
    { 
        internal class Tx : PacketProto
        {
            public byte Begin;
            public byte Qty;
        }
        internal class Rx : PacketProto
        {
            public byte Begin;
            public byte Qty;
            //public ushort[] Data;
        }
    }
    internal class PutDataPacket : PacketProto
    {
        internal class Tx : PacketProto
        {
            public byte Begin;
            public byte Qty;
            //public ushort[] Data;
        };
        internal class Rx : PacketProto
        {
            public byte Begin;
            public byte Qty;
        };
    };
}
