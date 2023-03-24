using System;
using System.Collections.Generic;

namespace ReaderPorts
{
    internal class CreateNewConnect
    {
        CommPort commPort;
        DeviceCtl deviceCtl;
        ModBus modBus;

        string portName;
        int baudRate;
        string typeProtocol;
        byte Addr;
        byte begin;
        byte Qty;
        string errorGetMassData;

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
        public string TypeProtocol
        {
            get { return typeProtocol; }
        }
        public int Begin
        {
            get { return begin; }
            set { begin = (byte)value; }
        }
        public int QtyRows
        {
            get { return Qty; }
            set { Qty = (byte)value; }
        }
        public CreateNewConnect(string portName, int baudRate, string typeProtocol, byte Addr, byte Begin, byte Qty)
        {
            this.portName = portName;
            this.baudRate = baudRate;
            this.typeProtocol = typeProtocol;
            this.Addr = Addr;
            this.begin = Begin;
            this.Qty = Qty;

            commPort = new CommPort(portName, baudRate, typeProtocol);
            commPort.SerialPortOpen();
        }
        public bool ConnectIsOpen()
        {
            return commPort.SerialPortIsOpen();
        }
        public void ConnectClose()
        {
            commPort.SerialPortClose();
        }

        public List<string> massSellStr;
        public void GetMassSellStr()
        {
            deviceCtl = new DeviceCtl(commPort);
            massSellStr = new List<string>();

            for (byte i = 0; i < Qty; i++)
            {
                List<string> cellstrList;
                deviceCtl.GetCellStr(Addr, begin, (byte)(i + begin), Qty, out cellstrList);
                massSellStr.AddRange(cellstrList);
            }
        }
        public List<int> GetMassData()
        {
            List<int> massData = new List<int>();

            switch (typeProtocol)
            {

                case "SLIP":
                    {
                        deviceCtl = new DeviceCtl(commPort);

                        deviceCtl.ConnectSLIP_Read(Addr, begin, Qty, ref massData);

                        if (massData.Count == 0)
                        {
                            errorGetMassData = $"Ошибка обмена данных {PortName}";
                        }
                        break;
                    }
                case "ModBus":
                    {
                        modBus = new ModBus(commPort, Addr, begin, Qty);
                        modBus.ConnectModBus_Read(ref massData);

                        if (massData.Count == 0)
                        {
                            errorGetMassData = $"Ошибка обмена данных {PortName}";
                        }
                        break;
                    }
            }
            return massData;
        }
        public void PutData(byte BeginPut, int numChng, string typeProtocolPut)
        {
            byte QtyPut = 1;
            byte BeginPutUpdate = Convert.ToByte(BeginPut + begin);

            switch (typeProtocolPut)
            {
                case "SLIP":
                    {
                        deviceCtl.PutData(Addr, BeginPutUpdate, QtyPut, numChng);
                        break;
                    }
                case "ModBus":
                    {
                        ushort[] massNunChng = new ushort[] { (ushort)numChng };
                        modBus.ConnectModBus_Write(Addr, BeginPutUpdate, massNunChng);
                        break;
                    }
            }
        }
    }
}
