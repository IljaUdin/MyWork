using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EncoderWPF
{
    internal class CreateNewConnect
    {
        CommPort commPort;
        ModBus modBus;

        string portName;
        int baudRate;
        byte addr;
        byte begin = 0;
        byte Qty = 15;
        string _errorGetMassData;
        List<int> _speedConnectionList = new List<int>() { 115200, 57600, 56000, 38400, 19200, 14400, 9600 };
        List<int> _addressDvsList = Enumerable.Range(1, 247).ToList();
        public List<int> SpeedConnectionList
        {
            get { return _speedConnectionList; }
        }
        public List<int> AddressDeviseList
        {
            get { return _addressDvsList;}
        }
        public string ErrorGetMassData
        {
            get { return _errorGetMassData; }
            set { _errorGetMassData = value; }
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
        public bool ConnectIsOpen()
        {
            return commPort.SerialPortIsOpen();
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
        /// <summary>
        /// Close Current Connect
        /// </summary>
        public void ConnectClose()
        {
            commPort.SerialPortClose();
        }
        /// <summary>
        /// Get Request 15 Cell
        /// </summary>
        /// <param name="begin"></param>
        /// <returns></returns>
        public List<int> GetMassData(byte begin)
        {
            List<int> massData = new List<int>();

            modBus = new ModBus(commPort, addr, begin, Qty);
            modBus.ConnectModBus_Read(ref massData);

            if (massData.Count == 0)
            {
                _errorGetMassData = $"Ошибка обмена данных {PortName}";
            }
            else
            {
                _errorGetMassData = null;
            }

            return massData;
        }
        /// <summary>
        /// Get Request Only One Cell
        /// </summary>
        /// <param name="addr"></param>
        /// <param name="begin"></param>
        /// <returns></returns>
        public List<int> SingleСellRequest(byte addr, byte begin)
        {
            this.addr = addr;
            byte QtyForRequest = 1;
            List<int> massData = new List<int>();

            modBus = new ModBus(commPort, addr, begin, QtyForRequest);
            modBus.ConnectModBus_Read(ref massData);

            if (massData.Count == 0)
            {
                _errorGetMassData = $"Ошибка обмена данных {PortName}";
            }
            else
            {
                _errorGetMassData = null;
            }

            return massData;
        }
        /// <summary>
        /// Write Data To Device
        /// </summary>
        /// <param name="BeginPut"></param>
        /// <param name="numChng"></param>
        public void PutData(byte BeginPut, int numChng)
        {
            byte BeginPutUpdate = Convert.ToByte(BeginPut + begin);

            ushort[] massNunChng = new ushort[] { (ushort)numChng };
            if (modBus != null)
                modBus.ConnectModBus_Write(Addr, BeginPutUpdate, massNunChng);

        }
    }
}
