using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ReaderPorts
{
    internal class Transport
    {
        const byte SEND = 0xc0;
        const byte SESC = 0xdb;
        const byte SESC_END = 0xdc;
        const byte SESC_ESC = 0xdd;

        List<byte> TxBuf = new List<byte>();
        List<byte> RxBuf = new List<byte>();

        CommPort serialPort;

        public Transport(CommPort commPort)
        {
            serialPort = commPort;
        }

        public bool Request(List<byte> txbuf, ref List<byte> rxbuf)
        {
            if (!ProcessTx(ref txbuf))
            {
                return false;
            }
            Thread.Sleep(10);
            if (!ProcessRx(txbuf))
            {
                return false;

            }
            rxbuf = RxBuf;
            return true;
        }
        public void SLIP_char(ref List<byte> inputData)
        {
            //Проверка пакета на наличие спец символов
            for (int i = 0; i < inputData.Count; i++)
            {
                if (inputData[i] == SEND)
                {
                    inputData[i] = SESC;
                    inputData.Insert(i + 1, SESC_END);
                }
                else if (inputData[i] == SESC)
                {
                    inputData[i] = SESC;
                    inputData.Insert(i + 1, SESC_ESC);
                }
            }
            //Добавление спецсимвола в начало
            inputData.Insert(0, SEND);
        }

        //Передача пакета
        public bool ProcessTx(ref List<byte> txbuf)
        {
            //Очистка, если не пуст
            if (TxBuf.Count != 0) { TxBuf.Clear(); }
            //Обработка
            TxBuf = txbuf;

            ushort crc_storage = 0xFFFF;

            Crc16.Crc16_next(ref TxBuf, ref crc_storage);

            //Добавление CRC к концу массива, разбив 16-битный CRC на 2 8-битный
            byte crc_h = Convert.ToByte(crc_storage >> 8);
            byte crc_l = Convert.ToByte(crc_storage & 0xFF);
            TxBuf.Add(crc_l);
            TxBuf.Add(crc_h);

            SLIP_char(ref TxBuf);

            int wcnt = 0;
            bool wr_res = serialPort.Write(TxBuf.ToArray(), wcnt, TxBuf.Count);

            return wr_res;
        }

        //Прием пакета
        public bool ProcessRx(List<byte> txbuf)
        {
            //Очистка, если не пуст
            if (RxBuf.Count != 0) { RxBuf.Clear(); }
            // Маркер получен - начинаем прием заголовка, если функция выполнилась с ошибкой - выход
            if (!ReadSLIP()) return false;

            if (RxBuf.Count == 0) return false;
            // Указатель на буфер приемника
            if (RxBuf[1] == txbuf[1])
            {
                // бит ответа должен быть выставлен, бит ошибки не проверяем - это на след. уровне
                if ((RxBuf[2] & 0x7F) == (txbuf[2] | 0x40)) { }
            }

            //Рассчет CRC запрошенного массива
            RxBuf.Remove(RxBuf.First());

            byte crc_h_get = RxBuf[RxBuf.Count - 2];
            byte crc_l_get = RxBuf[RxBuf.Count - 1];

            RxBuf.RemoveRange(RxBuf.Count - 2, 2);

            ushort crcCalc = Crc16.Crc16_(ref RxBuf, RxBuf.Count);

            byte crc_l_Calc = Convert.ToByte(crcCalc >> 8);
            byte crc_h_Calc = Convert.ToByte(crcCalc & 0xFF);

            if (crc_h_Calc == crc_h_get && crc_l_Calc == crc_l_get)
            {
                return true;
            }
            return false;
        }
        // Чтение данных с распаковкой
        public bool ReadSLIP()
        {
            // Читаем буфер
            int getBytes = serialPort.BytesToRead();
            byte[] massGetBytes = new byte[getBytes];
            bool result = serialPort.Read(massGetBytes, 0, getBytes);

            // Меняем SESC_ESC на SESC и SESC_END на SEND
            RxBuf = massGetBytes.ToList();

            for (int i = 0; i < RxBuf.Count; i++)
            {
                if (RxBuf[i] == SESC)
                {
                    if (RxBuf[i + 1] == SESC_ESC)
                    {
                        RxBuf.RemoveAt(i);
                        RxBuf[i] = SESC;
                        break;
                    }
                    if (RxBuf[i + 1] == SESC_END)
                    {
                        RxBuf.RemoveAt(i);
                        RxBuf[i] = SEND;
                        break;
                    }
                    else
                //result = false;

                // После удаления можем ВНЕЗАПНО оказаться в конце при некорректных данных
                if (RxBuf[i] == RxBuf.Last())
                        break;
                }
            }
            return result;
        }
    }
}
