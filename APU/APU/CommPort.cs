using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace APU
{
    internal class CommPort
    {
        SerialPort serialPort;

        string portName;
        int baudRate;
        public string PortName
        {
            get { return portName; }
        }
        public int BaudRate
        {
            get { return baudRate; }
        }
        public SerialPort serialPortInfo
        {
            get { return serialPort; }
        }

        public CommPort(string portName, int baudRate)
        {
            this.portName = portName;
            this.baudRate = baudRate;
            try
            {
                serialPort = new SerialPort(portName, baudRate, Parity.None, 8, StopBits.One);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void SerialPortOpen()
        {
            if (!serialPort.IsOpen)
                serialPort.Open();
        }
        public bool SerialPortIsOpen()
        {
            return serialPort.IsOpen;
        }
        public void SerialPortClose()
        {
            serialPort.Close();
        }
        public bool Write(byte[] buffer, int offset, int count)
        {
            if (serialPort.IsOpen)
            {
                serialPort.Write(buffer, offset, count);
                return true;
            }
            return false;
        }
        public bool Read(byte[] buffer, int offset, int count)
        {
            if (serialPort.IsOpen)
            {
                serialPort.Read(buffer, offset, count);
                return true;
            }
            return false;
        }
        public int BytesToRead()
        {
            return serialPort.BytesToRead;
        }
    }
}
