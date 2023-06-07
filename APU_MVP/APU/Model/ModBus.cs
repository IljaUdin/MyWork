using Modbus.Device;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APU
{
    internal class ModBus
    {
        CommPort commPort;
        ModbusSerialMaster master;

        byte slaveID;
        ushort startAddress, numOfPoints;

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
                    master.Transport.ReadTimeout = 20;
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
        public void ConnectModBus_Write(byte slaveIDWrite, byte startAddressWrite, ushort[] resisterListWrite)
        {
            if (!commPort.SerialPortIsOpen())
                commPort.SerialPortOpen();

            byte slaveID = slaveIDWrite;
            ushort startAddress = startAddressWrite;
            ushort[] resister = resisterListWrite.ToArray();

            try
            {
                master.Transport.ReadTimeout = 100;
                master.WriteMultipleRegisters(slaveID, startAddress, resister);
            }
            catch { }
        }
    }
}
