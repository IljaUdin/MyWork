using Modbus.Device;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ReaderPorts
{
    internal class ModBus
    {
        CommPort commPort;
        ModbusSerialMaster master;

        byte slaveID;
        ushort startAddress, numOfPoints;
        List<int> massDataAsync;

        public ModBus(CommPort commPort, byte Addr, byte Begin, byte Qty)
        {
            this.commPort = commPort;
            slaveID = Addr;
            startAddress = Begin;
            numOfPoints = Qty;
            master = ModbusSerialMaster.CreateRtu(commPort.serialPortInfo);
        }
        public void ConnectModBus_Read(ref List<int> massData)
        {
            //Добавлен try catch чтобы программа не висла при отключении порта
            try
            {
                {
                    master.Transport.ReadTimeout = 100;
                    var holding_register = master.ReadHoldingRegisters(slaveID, startAddress, numOfPoints);

                    foreach (var num in holding_register)
                    {
                        Convert.ToInt32(num);
                        massData.Add(num);
                    }
                }
            }
            catch { }
        }
        public async Task<List<int>> ConnectModBus_ReadAsync()
        {
            massDataAsync = new List<int>();
            //Добавлен try catch чтобы программа не висла при отключении порта
            try
            {
                master.Transport.ReadTimeout = 100;
                var holding_register = await master.ReadHoldingRegistersAsync(slaveID, startAddress, numOfPoints);

                foreach (var num in holding_register)
                {
                    Convert.ToInt32(num);
                    massDataAsync.Add(num);
                }

            }
            catch { }
                
            return massDataAsync;
        }
        public void ConnectModBus_Write(byte slaveIDWrite, byte startAddressWrite, ushort[] resisterListWrite)
        {
            if (!commPort.SerialPortIsOpen())
                commPort.SerialPortOpen();

            byte slaveID = slaveIDWrite;
            ushort startAddress = startAddressWrite;
            ushort[] resister = resisterListWrite.ToArray();

            // try
            {
                master.WriteMultipleRegisters(slaveID, startAddress, resister);
            }
            //catch { }
        }
    }
}

