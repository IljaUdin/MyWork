using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using static ReaderPorts.GetDataPacket;

namespace ReaderPorts
{
    internal class DeviceCtl
    {
        Transport transport;
        List<byte> mass_pRx;
        public DeviceCtl(CommPort commPort)
        {
            transport = new Transport(commPort);
        }

        int NmbRequest(byte retry)
        {
            decimal maxSizeLines = 33m;
            int sizeRequest = (int)Math.Round(retry / maxSizeLines, 2) + 1;
            return sizeRequest;
        }

        // Метод обработки строк, если их количество превышает 32шт в запросе - это число заложено Иваном в его алгоритме
        void ProcessRequest(ref Tx Tx, ref int step, ref byte calcQty)
        {
            //calcQty = Tx.Qty;
            if (step == 0 && Tx.Qty <= 32)
            {
                return;
            }
            else if (step == 0 && Tx.Qty > 32)
            {
                //Tx.Begin = 0;
                Tx.Qty = 32;
                calcQty = (byte)(calcQty - Tx.Qty);
                step++;
            }
            else
            {
                Tx.Begin = (byte)(Tx.Begin + 32);
                if (calcQty > 32)
                {
                    Tx.Qty = 32;
                    calcQty = (byte)(calcQty - Tx.Qty);
                }
                else
                {
                    Tx.Qty = calcQty;
                }
                step++;
            }
        }
        public bool ConnectSLIP_Read(byte DevAddr, byte Begin, byte Qty, ref List<int> data)
        {
            // Буфер отправки
            GetDataPacket.Tx Tx = new GetDataPacket.Tx();
            Tx.Addr = DevAddr;
            Tx.Cmd = 1;
            Tx.Size = 2;
            Tx.Begin = Begin;
            Tx.Qty = Qty;
            // Указатель на буфер приемника
            GetDataPacket.Rx pRx = new GetDataPacket.Rx();
            mass_pRx = new List<byte>();

            //          Запрос
            // Определяем кол-во запросов
            int retry = NmbRequest(Tx.Qty);
            int step = 0;
            byte calcQty = Tx.Qty;
            while (retry-- > 0)
            {
                ProcessRequest(ref Tx, ref step, ref calcQty);

                List<byte> massDataPush = new List<byte> { Tx.Addr, Tx.Cmd, Tx.Size, Tx.Begin, Tx.Qty };

                if (!transport.Request(massDataPush, ref mass_pRx))
                {
                    return false;
                }

                pRx.Size = mass_pRx[2];
                pRx.Begin = mass_pRx[3];
                pRx.Qty = mass_pRx[4];

                // Проверка размера (минимум)
                if (pRx.Size < 2) return false;
                // Проверка адреса и диапазона
                if (Tx.Begin != pRx.Begin) return false;
                if (Tx.Qty != pRx.Qty) return false;
                if (pRx.Size < pRx.Qty * 2 + 2) return false;
                // Передаем ссылку на указатель (для копирования снаружи)
                for (int i = 5; i < mass_pRx.Count; i = i + 2)
                {
                    data.Add((mass_pRx[i + 1] << 8 | mass_pRx[i]));
                }
            }
            return true;
        }
        public bool PutData(byte Addr, byte Begin, byte Qty, int numChng)
        {
            // Указатель на буфер Tx
            PutDataPacket.Tx pTx = new PutDataPacket.Tx();
            // Поля в запросе
            pTx.Addr = Addr;
            pTx.Cmd = 2;
            pTx.Size = (byte)(2 + Qty * 2);
            pTx.Begin = Begin;
            pTx.Qty = Qty;
            byte numChng_l_Calc = Convert.ToByte(numChng >> 8);
            byte numChng_h_Calc = Convert.ToByte(numChng & 0xFF);

            List<byte> massData = new List<byte> { pTx.Addr, pTx.Cmd, pTx.Size, pTx.Begin, pTx.Qty, numChng_h_Calc, numChng_l_Calc };

            // Указатель на буфер Rx
            PutDataPacket.Rx pRx = new PutDataPacket.Rx();
            // Запрос
            mass_pRx = new List<byte>();
            if (!transport.Request(massData, ref mass_pRx))
            {
                return false;
            }

            pRx.Size = mass_pRx[2];
            pRx.Begin = mass_pRx[3];
            pRx.Qty = mass_pRx[4];

            // Проверка размера
            if (pRx.Size != 2) return false;
            // Проверка адреса и диапазона
            if (pTx.Begin != pRx.Begin) return false;
            if (pTx.Qty != pRx.Qty) return false;

            return true;
        }

        public bool GetCellStr(byte Addr, byte Begin, byte Cell, byte Qty, out List<string> cellstrList)
        {
            cellstrList = new List<string>();
            // Буфер отправки
            GetCellStrPacket.Tx Tx = new GetCellStrPacket.Tx();
            Tx.Addr = Addr;
            Tx.Cmd = 3;
            Tx.Size = 1;
            Tx.Cell = Cell;

            // Указатель на буфер приемника
            GetCellStrPacket.Rx pRx = new GetCellStrPacket.Rx();

            //          Запрос
            {
                List<byte> massDataPush = new List<byte> { Tx.Addr, Tx.Cmd, Tx.Size, Tx.Cell };

                if (!transport.Request(massDataPush, ref mass_pRx))
                {
                    return false;
                }

                pRx.Size = mass_pRx[2];
                pRx.Cell = mass_pRx[3];

                // Проверка адреса ячейки
                if (Tx.Cell != pRx.Cell) return false;
                // Все принято
                // Очистка
                cellstrList.Clear();
                // Проверка размера
                if (pRx.Size > 1)
                {
                    List<byte> getNameRow = new List<byte>();
                    // Передаем ссылку на указатель (для копирования снаружи)
                    for (int i = 4; i < mass_pRx.Count - 1; i++)
                    {
                        getNameRow.Add(mass_pRx[i]);
                    }

                    cellstrList.Add(Encoding.Default.GetString(getNameRow.ToArray()));
                }
            }
            return true;
        }
    }
}
