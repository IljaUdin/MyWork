using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReaderPorts
{
    internal class Crc16
    {
        public static ushort Crc16_(ref List<byte> pcBlock, int len)
        {
            ushort crc = 0xFFFF;

            for (int j = 0; j < len; j++)
            {
                //У Ивана было pcBlock[j]++ но из-за этого увеличиваются все значения
                crc ^= pcBlock[j];

                for (int i = 0; i < 8; i++)
                    crc = (ushort)((crc & 0x1) != 0 ? (crc >> 1) ^ 0xA001 : crc >> 1);
            }

            return crc;
        }
        public static void Crc16_next(ref List<byte> pcBlock, ref ushort storage)
        {
            ushort crc = storage;

            for (int j = 0; j < pcBlock.Count; j++)
            {
                crc ^= pcBlock[j];

                for (int i = 0; i < 8; i++)
                    crc = (ushort)((crc & 0x1) != 0 ? (crc >> 1) ^ 0xA001 : crc >> 1);

                storage = crc;
            }
        }
    }
}
