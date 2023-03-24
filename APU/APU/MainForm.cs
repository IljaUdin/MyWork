using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace APU
{
    public partial class MainForm : Form
    {
        CreateNewConnect newConnect;
        List<string> portsNames = new List<string>();
        List<int> speedConnecntList = new List<int>() { 115200, 57600, 56000, 38400, 19200, 14400, 9600 };
        List<int> addressDvsList = Enumerable.Range(1, 247).ToList();

        bool stopRequested = false;
        string namePort;

        public MainForm()
        {
            InitializeComponent();
        }


        private void bnUpdate_Click(object sender, EventArgs e)
        {
            portsNames = SerialPort.GetPortNames().ToList();

            cmbBxPrts.Text = "";
            cmbBxPrts.Items.Clear();
            if (portsNames.Count != 0)
            {
                cmbBxPrts.Items.AddRange(portsNames.ToArray());
                cmbBxPrts.SelectedIndex = 0;
            }
        }
        private async void bnConnection_Click(object sender, EventArgs e)
        {
            List<int> requestMastType = new List<int>();
            byte begin = 4;

            if (cmbBxPrts.Text != "" && cmbBxExchngSpd.Text != "")
                if (cmbBxExchngSpd.Text == "Автопоиск")
                {
                    if (bnConnection.Text == "Подключить")
                    {
                        bnConnection.Text = "Поиск";
                        try
                        {
                            for (int i = 0; i < speedConnecntList.Count; i++)
                            {
                                cmbBxExchngSpdInWork.Text = speedConnecntList[i].ToString();
                                newConnect = new CreateNewConnect(cmbBxPrts.Text, speedConnecntList[i]);

                                for (int j = 0; j < addressDvsList.Count; j++)
                                {
                                    nmrcAddrssDvs.Value = addressDvsList[j];

                                    await Task.Run(() =>
                                    {
                                        if (newConnect == null) { return; }
                                        requestMastType = newConnect.RequestToExchangeData((byte)addressDvsList[j], begin);

                                    });

                                    if (stopRequested)
                                    {
                                        Thread.Sleep(100);
                                        newConnect.ConnectClose();
                                        stopRequested = false;
                                        bnConnection.Text = "Подключить";
                                        return;
                                    }

                                    if (requestMastType.Count > 0)
                                    {
                                        namePort = cmbBxPrts.Text;
                                        cmbBxExchngSpd.Text = speedConnecntList[i].ToString();
                                        bnConnection.Text = "Отключить";
                                        cmbBxPrts.Enabled = false;
                                        cmbBxExchngSpd.Enabled = false;
                                        nmrcAddrssDvs.Enabled = false;
                                        timerUpdateDate.Enabled = true;
                                        listBoxErrorMessage.Items.Add($"{DateTime.Now} Подключение к {newConnect.PortName} со скоростью {newConnect.BaudRate} и устройству с ID = {newConnect.Addr}");
                                        listBoxErrorMessage.TopIndex = listBoxErrorMessage.Items.Count - 1;
                                        //MessageBox.Show($"Подключение к {newConnect.PortName} со скоростью {newConnect.BaudRate} и устройству с ID = {newConnect.Addr}");
                                        return;
                                    }
                                }
                                if (requestMastType.Count == 0)
                                {
                                    newConnect.ConnectClose();
                                }
                            }

                            if (!newConnect.ConnectIsOpen(begin))
                                listBoxErrorMessage.Items.Add($"{DateTime.Now} Ошибка подключения COM-порта");
                            //MessageBox.Show(DateTime.Now + " " + "Ошибка подключения COM-порта");
                        }
                        catch { }
                    }
                    else if (bnConnection.Text == "Отключить")
                    {
                        //do
                        {
                            timerUpdateDate.Enabled = false;
                            Thread.Sleep(100);
                            namePort = "";
                            newConnect.ConnectClose();
                            bnConnection.Text = "Подключить";
                            cmbBxPrts.Enabled = true;
                            cmbBxExchngSpd.Enabled = true;
                            nmrcAddrssDvs.Enabled = true;
                        }
                        //while (tickLaunchMethod != 0);
                    }
                    else
                    {
                        stopRequested = true;
                    }
                }
                else
                {
                    if (bnConnection.Text == "Подключить")
                    {
                        try
                        {
                            newConnect = new CreateNewConnect(cmbBxPrts.Text, Convert.ToInt32(cmbBxExchngSpd.SelectedItem.ToString()));

                            if (newConnect == null) { return; }
                            requestMastType = newConnect.RequestToExchangeData((byte)nmrcAddrssDvs.Value, begin);

                            if (requestMastType.Count > 0)
                            {
                                namePort = cmbBxPrts.Text;
                                bnConnection.Text = "Отключить";
                                cmbBxPrts.Enabled = false;
                                cmbBxExchngSpd.Enabled = false;
                                nmrcAddrssDvs.Enabled = false;
                                timerUpdateDate.Enabled = true;
                                //MessageBox.Show($"Подключение к {newConnect.PortName} со скоростью {newConnect.BaudRate} и устройству с ID = {newConnect.Addr}");
                                listBoxErrorMessage.Items.Add($"{DateTime.Now} Подключение к {newConnect.PortName} со скоростью {newConnect.BaudRate} и устройству с ID = {newConnect.Addr}");
                                listBoxErrorMessage.TopIndex = listBoxErrorMessage.Items.Count - 1;
                                return;
                            }
                            else
                            {
                                newConnect.ConnectClose();
                                //MessageBox.Show(DateTime.Now + " " + "Ошибка подключения COM-порта");
                                listBoxErrorMessage.Items.Add($"{DateTime.Now} Ошибка подключения COM-порта");
                                listBoxErrorMessage.TopIndex = listBoxErrorMessage.Items.Count - 1;
                            }
                        }
                        catch { }
                    }
                    else if (bnConnection.Text == "Отключить")
                    {
                        //do
                        {
                            timerUpdateDate.Enabled = false;
                            Thread.Sleep(100);
                            namePort = "";
                            newConnect.ConnectClose();
                            bnConnection.Text = "Подключить";
                            cmbBxPrts.Enabled = true;
                            cmbBxExchngSpd.Enabled = true;
                            nmrcAddrssDvs.Enabled = true;
                        }
                        //while (tickLaunchMethod != 0);
                    }
                }
        }
        private void qToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            bnUpdate_Click(sender, e);
        }

        private void bnChngSpeed_Click(object sender, EventArgs e)
        {
            if (newConnect != null && bnConnection.Text == "Отключить" && cmbBxExchngSpdInWork.Text != "")
            {
                List<int> requestValidationResult = new List<int>();
                byte begin = 10;
                byte addr = newConnect.Addr;
                string newSpeed = cmbBxExchngSpdInWork.SelectedItem.ToString();

                newConnect.PutData(begin, Convert.ToInt32(newSpeed.Substring(0, newSpeed.Length - 2)));

                requestValidationResult = RecordingResultOfChanges(begin, Convert.ToInt32(newSpeed), addr);

                if (requestValidationResult.Count > 0)
                {
                    if (requestValidationResult[0].ToString() == "1")
                    {
                        cmbBxExchngSpd.Text = newSpeed.ToString();
                        listBoxErrorMessage.Items.Add($"{DateTime.Now} Скорость успешно изменена");
                        listBoxErrorMessage.TopIndex = listBoxErrorMessage.Items.Count - 1;
                        //MessageBox.Show("Скорость успешно изменена");
                    }
                    else if (requestValidationResult[0].ToString() == "4")
                    {
                        listBoxErrorMessage.Items.Add($"{DateTime.Now} Некорректная скорость");
                        listBoxErrorMessage.TopIndex = listBoxErrorMessage.Items.Count - 1;
                        //MessageBox.Show("Некорректная скорость");
                    }
                    else if (requestValidationResult[0].ToString() == "5")
                    {
                        listBoxErrorMessage.Items.Add($"{DateTime.Now} Ошибка сохранения в EEPROM");
                        listBoxErrorMessage.TopIndex = listBoxErrorMessage.Items.Count - 1;
                        //MessageBox.Show("Ошибка сохранения в EEPROM");
                    }
                }
                else
                {
                    listBoxErrorMessage.Items.Add($"{DateTime.Now} Ошибка запроса данных после изменения скорости");
                    listBoxErrorMessage.TopIndex = listBoxErrorMessage.Items.Count - 1;
                }
            }
        }

        private void clearErrorListBoxToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listBoxErrorMessage.Items.Clear();
        }

        private void bnChngAddr_Click(object sender, EventArgs e)
        {
            if (newConnect != null && bnConnection.Text == "Отключить")
            {
                List<int> requestValidationResult = new List<int>();
                byte begin = 12;
                byte newAddr = (byte)nmrcAddrssDvsInWork.Value;
                int speed = newConnect.BaudRate;

                newConnect.PutData(begin, newAddr);

                requestValidationResult = RecordingResultOfChanges(begin, speed, newAddr);

                if (requestValidationResult.Count > 0)
                {
                    if (requestValidationResult[0].ToString() == "0")
                    {
                        nmrcAddrssDvs.Text = newAddr.ToString();
                        listBoxErrorMessage.Items.Add($"{DateTime.Now} Адрес устройства успешно изменен");
                        listBoxErrorMessage.TopIndex = listBoxErrorMessage.Items.Count - 1;
                        //MessageBox.Show("Скорость успешно изменена");
                    }
                    else if (requestValidationResult[0].ToString() == "250")
                    {
                        listBoxErrorMessage.Items.Add($"{DateTime.Now} Адрес не установлен");
                        listBoxErrorMessage.TopIndex = listBoxErrorMessage.Items.Count - 1;
                        //MessageBox.Show("Адрес не установлен");
                    }
                    else if (requestValidationResult[0].ToString() == "251")
                    {
                        listBoxErrorMessage.Items.Add($"{DateTime.Now} Некорректный адрес");
                        listBoxErrorMessage.TopIndex = listBoxErrorMessage.Items.Count - 1;
                        //MessageBox.Show("Некорректный адрес");
                    }
                    else if (requestValidationResult[0].ToString() == "252")
                    {
                        listBoxErrorMessage.Items.Add($"{DateTime.Now} Ошибка сохранения в EEPROM");
                        listBoxErrorMessage.TopIndex = listBoxErrorMessage.Items.Count - 1;
                        //MessageBox.Show("Ошибка сохранения в EEPROM");
                    }
                }
                else
                {
                    listBoxErrorMessage.Items.Add($"{DateTime.Now} Ошибка запроса данных после изменения адреса устройства");
                    listBoxErrorMessage.TopIndex = listBoxErrorMessage.Items.Count - 1;
                }

            }
        }
        private void bnChngMystType_Click(object sender, EventArgs e)
        {
            if (newConnect != null && bnConnection.Text == "Отключить" && cmbBxMystType.Text != "")
            {
                List<int> requestValidationResult = new List<int>();
                byte begin = 4;
                string mastType = cmbBxMystType.SelectedItem.ToString();
                byte addr = newConnect.Addr;
                int speed = newConnect.BaudRate;
                int mastTypeConvert = 0;


                switch (mastType)
                {
                    case "ПМ-1":
                        {
                            mastTypeConvert = 1;
                            break;
                        }
                    case "ПМ-2":
                        {
                            mastTypeConvert = 2;
                            break;
                        }
                    case "ПМ-3":
                        {
                            mastTypeConvert = 3;
                            break;
                        }
                }

                newConnect.PutData(begin, mastTypeConvert);

                requestValidationResult = newConnect.RequestToExchangeData(addr, begin);

                if (requestValidationResult.Count > 0)
                {
                    if (requestValidationResult[0].ToString() == "1")
                    {
                        listBoxErrorMessage.Items.Add($"{DateTime.Now} Установлен тип мачты: ПМ-1");
                        listBoxErrorMessage.TopIndex = listBoxErrorMessage.Items.Count - 1;
                        //MessageBox.Show("Скорость успешно изменена");
                    }
                    else if (requestValidationResult[0].ToString() == "2")
                    {
                        listBoxErrorMessage.Items.Add($"{DateTime.Now} Установлен тип мачты: ПМ-2");
                        listBoxErrorMessage.TopIndex = listBoxErrorMessage.Items.Count - 1;
                        //MessageBox.Show("Тип мачты: ПМ-2");
                    }
                    else if (requestValidationResult[0].ToString() == "3")
                    {
                        listBoxErrorMessage.Items.Add($"{DateTime.Now} Установлен тип мачты: ПМ-3");
                        listBoxErrorMessage.TopIndex = listBoxErrorMessage.Items.Count - 1;
                        //MessageBox.Show("Тип мачты: ПМ-3");
                    }
                }
                else
                {
                    listBoxErrorMessage.Items.Add($"{DateTime.Now} Ошибка запроса данных после изменения типа мачты");
                    listBoxErrorMessage.TopIndex = listBoxErrorMessage.Items.Count - 1;
                }

            }
        }
        private void bnChngZeroPositionValue_Click(object sender, EventArgs e)
        {
            if (newConnect != null && bnConnection.Text == "Отключить")
            {
                List<int> requestValidationResult = new List<int>();
                byte begin = 8;
                int zeroPositionCalibration = 1;
                byte addr = newConnect.Addr;

                newConnect.PutData(begin, zeroPositionCalibration);

                Thread.Sleep(100);

                requestValidationResult = newConnect.RequestToExchangeData(addr, begin);

                if (requestValidationResult.Count > 0)
                {
                    if (requestValidationResult[0].ToString() == "201")
                    {
                        listBoxErrorMessage.Items.Add($"{DateTime.Now} Успешная установка нулевого положения");
                        listBoxErrorMessage.TopIndex = listBoxErrorMessage.Items.Count - 1;
                    }
                    else if (requestValidationResult[0].ToString() == "243")
                    {
                        listBoxErrorMessage.Items.Add($"{DateTime.Now} Ошибка установки нулевого положения мачты");
                        listBoxErrorMessage.TopIndex = listBoxErrorMessage.Items.Count - 1;
                    }
                    else
                    {
                        listBoxErrorMessage.Items.Add($"{DateTime.Now} Неизвестная ошибка");
                        listBoxErrorMessage.TopIndex = listBoxErrorMessage.Items.Count - 1;
                    }
                }
                else
                {
                    listBoxErrorMessage.Items.Add($"{DateTime.Now} Ошибка запроса данных после установки нулевого положения");
                    listBoxErrorMessage.TopIndex = listBoxErrorMessage.Items.Count - 1;
                }
            }
        }
        private void bnChngZeroPositionValueBreak_Click(object sender, EventArgs e)
        {
            if (newConnect != null && bnConnection.Text == "Отключить")
            {
                List<int> requestValidationResult = new List<int>();
                byte begin = 8;
                int zeroPositionCalibration = 2;
                byte addr = newConnect.Addr;

                newConnect.PutData(begin, zeroPositionCalibration);

                Thread.Sleep(100);

                requestValidationResult = newConnect.RequestToExchangeData(addr, begin);

                if (requestValidationResult.Count > 0)
                {
                    if (requestValidationResult[0].ToString() == "202")
                    {
                        listBoxErrorMessage.Items.Add($"{DateTime.Now} Успешный сброс нулевого положения");
                        listBoxErrorMessage.TopIndex = listBoxErrorMessage.Items.Count - 1;
                    }
                    else if (requestValidationResult[0].ToString() == "243")
                    {
                        listBoxErrorMessage.Items.Add($"{DateTime.Now} Ошибка установки нулевого положения мачты");
                        listBoxErrorMessage.TopIndex = listBoxErrorMessage.Items.Count - 1;
                    }
                    else
                    {
                        listBoxErrorMessage.Items.Add($"{DateTime.Now} Неизвестная ошибка");
                        listBoxErrorMessage.TopIndex = listBoxErrorMessage.Items.Count - 1;
                    }
                }
                else
                {
                    listBoxErrorMessage.Items.Add($"{DateTime.Now} Ошибка запроса данных после установки нулевого положения");
                    listBoxErrorMessage.TopIndex = listBoxErrorMessage.Items.Count - 1;
                }
            }
        }
        List<int> RecordingResultOfChanges(byte begin, int speed, byte addr)
        {
            newConnect.ConnectClose();

            newConnect = new CreateNewConnect(namePort, speed);

            cmbBxPrts.Text = namePort;

            if (newConnect == null) { return new List<int>(); }

            Thread.Sleep(100);

            return newConnect.RequestToExchangeData(addr, begin);
        }

        List<int> requestMastPositionList;
        bool startMethodMastPosition;
        private object mastPositionLock = new object();
        void MastPosition()
        {
            lock (mastPositionLock)
            {
                startMethodMastPosition = true;

                requestMastPositionList = new List<int>();

                byte begin = 7;

                requestMastPositionList = newConnect.GetMassData(begin);

                startMethodMastPosition = false;
            }
        }
        private void updateFirmwareToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (namePort != "" && namePort != null)
            {
                newConnect.PutData(71, 0xFF80);

                timerUpdateDate.Enabled = false;

                newConnect.ConnectClose();

                OpenFileDialog openFileDialog = new OpenFileDialog();

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string fileName = Path.GetFileName(openFileDialog.FileName);

                    ProcessStartInfo startInfo = new ProcessStartInfo();
                    startInfo.FileName = "cmd.exe";
                    startInfo.Arguments = $"/K UpdateFirmware.exe {namePort} {fileName}";
                    Process process = Process.Start(startInfo);
                    process.WaitForExit();
                }
                newConnect = new CreateNewConnect(namePort, Convert.ToInt32(cmbBxExchngSpd.Text), (byte)nmrcAddrssDvs.Value);

                timerUpdateDate.Enabled = true;
            }
        }
        private async void timerUpdateDate_Tick(object sender, EventArgs e)
        {
            await Task.Run(() =>
            {
                if (!startMethodMastPosition)
                    MastPosition();
            });

            if (requestMastPositionList.Count > 0)
                txtBoxMastPosition.Text = requestMastPositionList[0].ToString();
        }

    }
}
