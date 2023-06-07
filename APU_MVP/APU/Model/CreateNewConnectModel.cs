using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace APU
{
    internal class CreateNewConnectModel
    {
        CommPort commPort;
        ModBus modBus;
        
        string portName;
        int baudRate;
        byte addr;
        byte Qty = 50;
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
        public CreateNewConnectModel()
        {
            
        }
        public List<string> GetAvailablePorts()
        {
            string[] ports = SerialPort.GetPortNames();
            return new List<string>(ports);
        }
        /// <summary>
        /// Connect COM_Port
        /// </summary>
        /// <param name="portName"></param>
        /// <param name="baudRate"></param>
        /// <param name="addr"></param>
        /// <returns></returns>
        public bool Connect(string portName, int baudRate, byte addr)
        {
            this.portName = portName;
            this.baudRate = baudRate;
            this.addr = addr;

            commPort = new CommPort(portName, baudRate);
            commPort.SerialPortOpen();

            if (SingleСellRequest(addr, 20).Count == 0)
            {
                ConnectClose();
                return false;
            }
            
            WriteInitialParameters();
            return true;
        }
        /// <summary>
        /// Set Initial Values
        /// </summary>
        public void WriteInitialParameters()
        {
            byte begin = 11;
            int initialSpeed = 500;
            PutData(begin, initialSpeed);

            begin = 13;
            int initialCurrent = 6000;
            PutData(begin, initialCurrent);
        }
        public bool ConnectIsOpen()
        {
            byte begin = 0;

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
        public List<int> GetMassData(byte begin, byte Qty)
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
        public List<int> SingleСellRequest(byte addr, byte begin)
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
