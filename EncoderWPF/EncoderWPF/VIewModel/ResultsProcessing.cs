using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Threading;
using static System.Net.Mime.MediaTypeNames;

namespace EncoderWPF
{
    internal class ResultsProcessing
    {
        MainViewModel mainViewModel;
        CreateNewConnect newConnect;
        ParametersWindow parametersWindow;
        ParametersViewModel parametersViewModel;

        bool _stopRequested = false;
        string _currentNamePort;
        DispatcherTimer timerUpdateDate;
        int timerUpdateMilliseconds = 100;
        int numberOfIterations;
        public string DeviceInformation
        {
            get
            {
                if (newConnect != null)
                    return ErrorsAndDiagnostics.DeviceInformation;
                else
                    return " ";
            }
        }
        public ResultsProcessing(MainViewModel mainViewModel)
        {
            timerUpdateDate = new DispatcherTimer();
            timerUpdateDate.Interval = TimeSpan.FromMilliseconds(timerUpdateMilliseconds);
            timerUpdateDate.Tick += Timer_Tick;

            this.mainViewModel = mainViewModel;
        }
        /// <summary>
        /// Create New Connect COM_Port
        /// </summary>
        public async void Connection()
        {
            List<int> requestConnectToMast = new List<int>();
            byte begin = 4;

            if (!string.IsNullOrEmpty(mainViewModel.PortsComboBoxText) && !string.IsNullOrEmpty(mainViewModel.SpeedComboBoxPropertyText))
            {
                if (mainViewModel.SpeedComboBoxPropertyText == "Автопоиск")
                {
                    if (mainViewModel.StateConnectionButton == "Подключить")
                    {
                        mainViewModel.StateConnectionButton = "Отмена";
                        try
                        {
                            for (int i = 0; i < newConnect.SpeedConnectionList.Count; i++)
                            {
                                mainViewModel.ExchngSpdInWorkComboBoxText = newConnect.SpeedConnectionList[i].ToString();
                                newConnect = new CreateNewConnect(mainViewModel.PortsComboBoxText, newConnect.SpeedConnectionList[i]);

                                for (int j = 0; j < newConnect.AddressDeviseList.Count; j++)
                                {
                                    mainViewModel.AddressDeviceNumericUpDownValue = newConnect.AddressDeviseList[j];

                                    await Task.Run(() =>
                                    {
                                        if (newConnect == null) { return; }
                                        requestConnectToMast = newConnect.SingleСellRequest((byte)newConnect.AddressDeviseList[j], begin);
                                    });

                                    if (_stopRequested)
                                    {
                                        Thread.Sleep(100);
                                        newConnect.ConnectClose();
                                        _stopRequested = false;
                                        mainViewModel.StateConnectionButton = "Подключить";
                                        return;
                                    }
                                    if (requestConnectToMast.Count > 0)
                                    {
                                        _currentNamePort = mainViewModel.PortsComboBoxText;
                                        mainViewModel.StateConnectionButton = "Отключить";
                                        mainViewModel.PortsComboBoxEnabled = false;
                                        mainViewModel.SpeedComboBoxEnabled = false;
                                        mainViewModel.AddressDeviceNumericUpDownEnabled = false;
                                        timerUpdateDate.Start();
                                        mainViewModel.ErrorMessageListBoxText += $"{DateTime.Now} Подключение к {mainViewModel.PortsComboBoxText} со скоростью {mainViewModel.SpeedComboBoxPropertyText} и устройству с ID = {mainViewModel.AddressDeviceNumericUpDownValue}\n";

                                        ErrorsAndDiagnostics.DeviceIdentification(newConnect);

                                        Thread.Sleep(100);
                                        
                                        DeterminationMastType(begin);

                                        return;
                                    }
                                }
                                if (requestConnectToMast.Count == 0)
                                {
                                    newConnect.ConnectClose();
                                    newConnect = null;
                                }
                            }

                            if (!newConnect.ConnectIsOpen(begin))
                                mainViewModel.ErrorMessageListBoxText += $"{DateTime.Now} Ошибка подключения COM-порта\n";
                        }
                        catch { }
                    }
                    else if (mainViewModel.StateConnectionButton == "Отключить")
                    {
                        timerUpdateDate.Stop();
                        Thread.Sleep(100);
                        mainViewModel.StateConnectionButton = "";
                        newConnect.ConnectClose();
                        newConnect = null;
                        mainViewModel.StateConnectionButton = "Подключить";
                        mainViewModel.PortsComboBoxEnabled = true;
                        mainViewModel.SpeedComboBoxEnabled = true;
                        mainViewModel.AddressDeviceNumericUpDownEnabled = true;
                    }
                    else
                    {
                        _stopRequested = true;
                    }
                }
                else
                {
                    if (mainViewModel.StateConnectionButton == "Подключить")
                    {
                        try
                        {
                            newConnect = new CreateNewConnect(mainViewModel.PortsComboBoxText, Convert.ToInt32(mainViewModel.SpeedComboBoxPropertyText));

                            if (newConnect == null) { return; }

                            requestConnectToMast = newConnect.SingleСellRequest(Convert.ToByte(mainViewModel.AddressDeviceNumericUpDownValue.ToString()), begin);

                            int num = mainViewModel.AddressDeviceNumericUpDownValue;

                            if (requestConnectToMast.Count > 0)
                            {
                                _currentNamePort = mainViewModel.PortsComboBoxText;
                                mainViewModel.StateConnectionButton = "Отключить";
                                mainViewModel.PortsComboBoxEnabled = false;
                                mainViewModel.SpeedComboBoxEnabled = false;
                                mainViewModel.AddressDeviceNumericUpDownEnabled = false;
                                timerUpdateDate.Start();
                                mainViewModel.ErrorMessageListBoxText += $"{DateTime.Now} Подключение к {mainViewModel.PortsComboBoxText} со скоростью {mainViewModel.SpeedComboBoxPropertyText} и устройству с ID = {mainViewModel.AddressDeviceNumericUpDownValue}\n";
                                
                                ErrorsAndDiagnostics.DeviceIdentification(newConnect);

                                Thread.Sleep(100);
                                
                                DeterminationMastType(begin);

                                return;
                            }
                            else
                            {
                                newConnect.ConnectClose();
                                newConnect = null;
                                mainViewModel.ErrorMessageListBoxText += $"{DateTime.Now} Ошибка подключения COM-порта\n";
                            }
                        }
                        catch { }
                    }
                    else if (mainViewModel.StateConnectionButton == "Отключить")
                    {
                        timerUpdateDate.Stop();
                        Thread.Sleep(100);
                        mainViewModel.StateConnectionButton = "";
                        newConnect.ConnectClose();
                        newConnect = null;
                        mainViewModel.StateConnectionButton = "Подключить";
                        mainViewModel.PortsComboBoxEnabled = true;
                        mainViewModel.SpeedComboBoxEnabled = true;
                        mainViewModel.AddressDeviceNumericUpDownEnabled = true;
                    }
                }
            }
        }
        /// <summary>
        /// Load Second Window With Parameters
        /// </summary>
        public void LoadParametersWindow()
        {
            if (newConnect != null)
            {
                parametersWindow = new ParametersWindow();

                parametersViewModel = new ParametersViewModel();

                parametersWindow.DataContext = parametersViewModel;

                parametersWindow.Show();
            }
        }


        /// <summary>
        /// Get status information
        /// </summary>
        public void Diagnostics()
        {
            if (newConnect != null)
            {
                timerUpdateDate.Stop();
                Thread.Sleep(100);

                byte beginNum = 0X0012;
                int numChng = 1234;
                newConnect.PutData(beginNum, numChng);

                byte rowEncoder = 0X0019;

                for (int i = 1; i < 4; i++)
                {
                    beginNum = 0X0017;
                    numChng = i;
                    newConnect.PutData(beginNum, numChng);

                    byte addr = (byte)mainViewModel.AddressDeviceNumericUpDownValue;
                    byte begin = 0X0018;
                    List<int> resultDiagnost = newConnect.SingleСellRequest(addr, begin);

                    mainViewModel.ErrorMessageListBoxText += $"Статусный регистр энкодера {i}:\n";
                    ErrorsAndDiagnostics.DiagnostEncoder(resultDiagnost, mainViewModel);


                    resultDiagnost = newConnect.SingleСellRequest(addr, rowEncoder);
                    if (resultDiagnost.Count > 0)
                        mainViewModel.ErrorMessageListBoxText += $"Абсолютное угловое положение {i} энкодера = {resultDiagnost[0]}\n";
                    rowEncoder++;
                }

                beginNum = 0X0012;

                newConnect.PutData(beginNum, 0);
                timerUpdateDate.Start();
            }
        }
        public void UpdateFirmware()
        {
            if (mainViewModel.StateConnectionButton == "Отключить")
            {
                timerUpdateDate.Stop();

                OpenFileDialog openFileDialog = new OpenFileDialog();

                if (openFileDialog.ShowDialog() == true)
                {
                    newConnect.PutData(71, 0xFF80);
                    newConnect.ConnectClose();

                    string fileName = Path.GetFileName(openFileDialog.FileName);

                    ProcessStartInfo startInfo = new ProcessStartInfo();
                    startInfo.FileName = "cmd.exe";
                    startInfo.Arguments = $"/K UpdateFirmware.exe {_currentNamePort} {fileName}";
                    Process process = Process.Start(startInfo);
                    process.WaitForExit();

                    ErrorsAndDiagnostics.DeviceIdentification(newConnect);
                }

                if (!newConnect.ConnectIsOpen())
                    newConnect = new CreateNewConnect(_currentNamePort, Convert.ToInt32(mainViewModel.SpeedComboBoxPropertyText), (byte)mainViewModel.AddressDeviceNumericUpDownValue);

                timerUpdateDate.Start();
            }
        }
        /// <summary>
        /// Get Information About Select Type of Mast
        /// </summary>
        /// <param name="addr"></param>
        /// <param name="begin"></param>
        public void DeterminationMastType(byte begin)
        {
            byte addr = Convert.ToByte(mainViewModel.AddressDeviceNumericUpDownValue);

            List<int> requestValidationResult = new List<int>();

            requestValidationResult = newConnect.SingleСellRequest(addr, begin);

            ErrorsAndDiagnostics.DeterminationOfMastTypeAnswer(requestValidationResult, mainViewModel);
        }
        /// <summary>
        /// Set zero position
        /// </summary>
        public void ChangeZeroPositionValue()
        {
            if (newConnect != null && mainViewModel.StateConnectionButton == "Отключить")
            {
                List<int> requestValidationResult = new List<int>();
                byte begin = 8;
                int zeroPositionCalibration = 1;
                byte addr = newConnect.Addr;

                newConnect.PutData(begin, zeroPositionCalibration);

                Thread.Sleep(100);

                requestValidationResult = newConnect.SingleСellRequest(addr, begin);

                mainViewModel.ErrorMessageListBoxText += ErrorsAndDiagnostics.ChangeZeroPositionValueAnswer(requestValidationResult);

                RecordingDeviceReadings();
            }
        }
        List<int> RecordingResultOfChanges(byte begin, int speed, byte addr)
        {
            newConnect.ConnectClose();

            newConnect = new CreateNewConnect(_currentNamePort, speed);

            mainViewModel.PortsComboBoxText = _currentNamePort;

            if (newConnect == null) { return new List<int>(); }

            Thread.Sleep(100);

            return newConnect.SingleСellRequest(addr, begin);
        }

        List<int> requestMassDataMast;
        bool startMassDataMast;
        private object positionLock = new object();
        void MassDataMast()
        {
            lock (positionLock)
            {
                startMassDataMast = true;

                requestMassDataMast = new List<int>();

                byte begin = 0x0003;

                requestMassDataMast = newConnect.GetMassData(begin);

                if (requestMassDataMast.Count > 0)
                {
                    ErrorsAndDiagnostics.GetTypeError(requestMassDataMast, mainViewModel);
                    RecordingDeviceReadings();
                    numberOfIterations = 0;
                }

                if (requestMassDataMast.Count == 0)
                    if (numberOfIterations > 15)
                    {
                        numberOfIterations = 0;
                        mainViewModel.ErrorMessageListBoxText += $"{DateTime.Now} Ошибка запроса данных\n";
                    }
                    else
                    {
                        numberOfIterations++;
                    }

                startMassDataMast = false;
            }
        }
        private async void Timer_Tick(object sender, EventArgs e)
        {
            await Task.Run(() =>
            {
                if (!startMassDataMast)
                    MassDataMast();
            });

        }
        /// <summary>
        /// Write Data To ProgressBar And Parameters Form 
        /// </summary>
        void RecordingDeviceReadings()
        {
            if (requestMassDataMast[4] != 0)
            {
                //Записываем положение мачты
                mainViewModel.MastPositionTextBoxPropertyText = requestMassDataMast[4].ToString();

                //Обновление ProgressBar
                int minProgressBar = Convert.ToInt32(mainViewModel.MastPositionProgressBarMinimum);
                int maxProgressBar = 8010;
                if (requestMassDataMast[4] >= minProgressBar && requestMassDataMast[4] <= maxProgressBar)
                {
                    mainViewModel.MastPositionProgressBarValue = requestMassDataMast[4].ToString();
                    float mastPositionPersent = (requestMassDataMast[4] - minProgressBar) * 100 / (maxProgressBar - minProgressBar);
                    mainViewModel.MastPositionLabelContent = mastPositionPersent.ToString() + " % Положения мачты";
                }

                //Записываем в новое окно параметры
                if (parametersWindow != null)
                {
                    parametersViewModel.Voltage3VTextBoxText = requestMassDataMast[11];
                    parametersViewModel.Voltage5VTextBoxText = requestMassDataMast[12];
                    parametersViewModel.Voltage27VTextBoxText = requestMassDataMast[13];
                    parametersViewModel.TemperaturePlateTextBoxText = requestMassDataMast[14];
                }
            }
        }
        public void MystTypeChange()
        {
            if (newConnect != null && mainViewModel.StateConnectionButton == "Отключить" && mainViewModel.MystTypeComboBoxText != "")
            {
                byte begin = 4;
                string mastType = mainViewModel.MystTypeComboBoxText;
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

                DeterminationMastType(begin);
            }
        }
        public void SpeedChange()
        {
            if (newConnect != null && mainViewModel.StateConnectionButton == "Отключить" && mainViewModel.ExchngSpdInWorkComboBoxText != "")
            {
                List<int> requestValidationResultSpeedChange = new List<int>();
                byte begin = 10;
                byte addr = newConnect.Addr;
                string newSpeed = mainViewModel.ExchngSpdInWorkComboBoxText;

                newConnect.PutData(begin, Convert.ToInt32(newSpeed.Substring(0, newSpeed.Length - 2)));

                requestValidationResultSpeedChange = RecordingResultOfChanges(begin, Convert.ToInt32(newSpeed), addr);

                mainViewModel.ErrorMessageListBoxText += ErrorsAndDiagnostics.SpeedChangeAnswer(requestValidationResultSpeedChange, mainViewModel, newSpeed);
            }
        }
        public void AddressChange()
        {
            if (newConnect != null && mainViewModel.StateConnectionButton == "Отключить")
            {
                string newAddrText = mainViewModel.AddrssDvsInWorkTextBoxValue.ToString();

                if (string.IsNullOrEmpty(newAddrText)) { return; }

                if (int.TryParse(newAddrText, out int value) && value >= 1 && value <= 247)
                {
                    List<int> requestValidationResult = new List<int>();
                    byte begin = 12;
                    byte newAddr = Convert.ToByte(newAddrText);
                    int speed = newConnect.BaudRate;

                    newConnect.PutData(begin, newAddr);

                    requestValidationResult = RecordingResultOfChanges(begin, speed, newAddr);

                    mainViewModel.ErrorMessageListBoxText += ErrorsAndDiagnostics.ChangeAddressAnswer(requestValidationResult, mainViewModel, newAddr);
                }
                else
                {
                    MessageBox.Show("Значение должно быть в диапазоне от 1 до 247.");
                }
            }
        }
    }
}
