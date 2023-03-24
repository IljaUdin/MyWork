using System;
using System.IO.Ports;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ReaderPorts
{
    internal class CommPort
    {
        SerialPort serialPort;

        string portName;
        int baudRate;
        string typeProtocol;

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
        public SerialPort serialPortInfo
        {
            get { return serialPort; }
        }

        public CommPort(string portName, int baudRate, string typeProtocol)
        {
            this.portName = portName;
            this.baudRate = baudRate;
            this.typeProtocol = typeProtocol;
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
        public async Task<bool> WriteAsync(byte[] buffer, int offset, int count)
        {
            if (serialPort.IsOpen)
            {
                await serialPort.BaseStream.WriteAsync(buffer, offset, count);
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
        public async Task<bool> ReadAsync(byte[] buffer, int offset, int count)
        {
            if (serialPort.IsOpen)
            {
                await serialPort.BaseStream.ReadAsync(buffer, offset, count);
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
