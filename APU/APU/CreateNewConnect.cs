using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;

namespace APU
{
    internal class CreateNewConnect
    {
        CommPort commPort;
        ModBus modBus;

        string portName;
        int baudRate;
        byte addr;
        byte Qty = 1;
        string errorGetMassData;
        byte begin = 0;

        public string ErrorGetMassData
        {
            get { return errorGetMassData; }
            set { errorGetMassData = value; }
        }
        public string PortName
        {
            get { return portName; }
        }
        public int BaudRate
        {
            get { return baudRate; }
        }
        public byte Addr
        {
            get { return addr; }
        }
        public CreateNewConnect(string portName, int baudRate)
        {
            this.portName = portName;
            this.baudRate = baudRate;

            commPort = new CommPort(portName, baudRate);
            commPort.SerialPortOpen();
        }
        public CreateNewConnect(string portName, int baudRate, byte addr)
        {
            this.portName = portName;
            this.baudRate = baudRate;
            this.addr = addr;

            commPort = new CommPort(portName, baudRate);
            commPort.SerialPortOpen();
        }
        public bool ConnectIsOpen(byte begin)
        {
            if (GetMassData(begin).Count != 0)
            {
                return commPort.SerialPortIsOpen();
            }
            else
            {
                commPort.SerialPortClose();
                return false;
            }
        }
        public void ConnectClose()
        {
            commPort.SerialPortClose();
        }

        public List<int> GetMassData(byte begin)
        {
            List<int> massData = new List<int>();

            modBus = new ModBus(commPort, addr, begin, Qty);
            modBus.ConnectModBus_Read(ref massData);

            if (massData.Count == 0)
            {
                errorGetMassData = $"Ошибка обмена данных {PortName}";
            }
            else
            {
                errorGetMassData = null;
            }


            return massData;
        }

        public int countDataInList;
        public List<int> RequestToExchangeData(byte addr, byte begin)
        {
            this.addr = addr;
            byte QtyForRequest = 1;
            List<int> massData = new List<int>();

            modBus = new ModBus(commPort, addr, begin, QtyForRequest);
            modBus.ConnectModBus_Read(ref massData);

            if (massData.Count == 0)
            {
                errorGetMassData = $"Ошибка обмена данных {PortName}";
            }
            else
            {
                errorGetMassData = null;
            }

            countDataInList = massData.Count;

            return massData;
        }
        public void PutData(byte BeginPut, int numChng)
        {
            byte BeginPutUpdate = Convert.ToByte(BeginPut + begin);

            ushort[] massNunChng = new ushort[] { (ushort)numChng };
            if (modBus != null)
            modBus.ConnectModBus_Write(Addr, BeginPutUpdate, massNunChng);

        }
    }
}
