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
        MainViewModel _mainViewModel;
        CreateNewConnect _newConnect;
        ParametersWindow parametersWindow;

        string _deviceInformation;
        List<int> _speedConnecntList = new List<int>() { 115200, 57600, 56000, 38400, 19200, 14400, 9600 };
        List<int> _addressDvsList = Enumerable.Range(1, 247).ToList();
        bool _stopRequested = false;
        string _currentNamePort;
        DispatcherTimer timerUpdateDate;
        int numberOfIterations;
        public string DeviceInformation
        {
            get { return _deviceInformation; }
        }
        public ResultsProcessing(MainViewModel mainViewModel)
        {
            timerUpdateDate = new DispatcherTimer();
            timerUpdateDate.Interval = TimeSpan.FromMilliseconds(100);
            timerUpdateDate.Tick += Timer_Tick;

            _mainViewModel = mainViewModel;
        }
        private async void Timer_Tick(object sender, EventArgs e)
        {
            await Task.Run(() =>
            {
                if (!startMethodMastPosition)
                    MassDataMast();
            });

            if (requestMassDataMast.Count > 5)
            {
                ErrorsAndDiagnostics.GetTypeError(requestMassDataMast, _mainViewModel);
                RecordingDeviceReadings();
                numberOfIterations = 0;
            }

            if (requestMassDataMast.Count == 0)
                if (numberOfIterations > 80)
                {
                    numberOfIterations = 0;
                    _mainViewModel.ErrorMessageListBoxText += $"{DateTime.Now} Ошибка запроса данных\n";
                }
                else
                {
                    numberOfIterations++;
                }
        }
        /// <summary>
        /// Create New Connect COM-Port
        /// </summary>
        public async void Connection()
        {
            List<int> requestMastType = new List<int>();
            byte begin = 4;

            if (!string.IsNullOrEmpty(_mainViewModel.PortsComboBoxText) && !string.IsNullOrEmpty(_mainViewModel.SpeedComboBoxPropertyText))
            {
                if (_mainViewModel.SpeedComboBoxPropertyText == "Автопоиск")
                {
                    if (_mainViewModel.StateConnectionButton == "Подключить")
                    {
                        _mainViewModel.StateConnectionButton = "Отмена";
                        try
                        {
                            for (int i = 0; i < _speedConnecntList.Count; i++)
                            {
                                _mainViewModel.ExchngSpdInWorkComboBoxText = _speedConnecntList[i].ToString();
                                _newConnect = new CreateNewConnect(_mainViewModel.PortsComboBoxText, _speedConnecntList[i]);

                                for (int j = 0; j < _addressDvsList.Count; j++)
                                {
                                    _mainViewModel.AddressDeviceNumericUpDownValue = _addressDvsList[j];

                                    await Task.Run(() =>
                                    {
                                        if (_newConnect == null) { return; }
                                        requestMastType = _newConnect.RequestToExchangeData((byte)_addressDvsList[j], begin);
                                    });

                                    if (_stopRequested)
                                    {
                                        Thread.Sleep(100);
                                        _newConnect.ConnectClose();
                                        _stopRequested = false;
                                        _mainViewModel.StateConnectionButton = "Подключить";
                                        return;
                                    }
                                    if (requestMastType.Count > 0)
                                    {
                                        _currentNamePort = _mainViewModel.PortsComboBoxText;
                                        _mainViewModel.StateConnectionButton = "Отключить";
                                        _mainViewModel.PortsComboBoxEnabled = false;
                                        _mainViewModel.SpeedComboBoxEnabled = false;
                                        _mainViewModel.AddressDeviceNumericUpDownEnabled = false;
                                        timerUpdateDate.Start();
                                        _mainViewModel.ErrorMessageListBoxText += $"{DateTime.Now} Подключение к {_mainViewModel.PortsComboBoxText} со скоростью {_mainViewModel.SpeedComboBoxPropertyText} и устройству с ID = {_mainViewModel.AddressDeviceNumericUpDownValue}\n";

                                        begin = 0;
                                        DeviceIdentification(begin);

                                        Thread.Sleep(100);
                                        begin = 4;
                                        byte addr = Convert.ToByte(_mainViewModel.AddressDeviceNumericUpDownValue);
                                        DeterminationOfMastType(addr, begin);
                                        return;
                                    }
                                }
                                if (requestMastType.Count == 0)
                                {
                                    _newConnect.ConnectClose();
                                }
                            }

                            if (!_newConnect.ConnectIsOpen(begin))
                                _mainViewModel.ErrorMessageListBoxText += $"{DateTime.Now} Ошибка подключения COM-порта\n";
                        }
                        catch { }
                    }
                    else if (_mainViewModel.StateConnectionButton == "Отключить")
                    {
                        timerUpdateDate.Stop();
                        Thread.Sleep(100);
                        _mainViewModel.StateConnectionButton = "";
                        _newConnect.ConnectClose();
                        _mainViewModel.StateConnectionButton = "Подключить";
                        _mainViewModel.PortsComboBoxEnabled = true;
                        _mainViewModel.SpeedComboBoxEnabled = true;
                        _mainViewModel.AddressDeviceNumericUpDownEnabled = true;
                    }
                    else
                    {
                        _stopRequested = true;
                    }
                }
                else
                {
                    if (_mainViewModel.StateConnectionButton == "Подключить")
                    {
                        try
                        {
                            _newConnect = new CreateNewConnect(_mainViewModel.PortsComboBoxText, Convert.ToInt32(_mainViewModel.SpeedComboBoxPropertyText));

                            if (_newConnect == null) { return; }
                            requestMastType = _newConnect.RequestToExchangeData(Convert.ToByte(_mainViewModel.AddressDeviceNumericUpDownValue.ToString()), begin);

                            int num = _mainViewModel.AddressDeviceNumericUpDownValue;

                            if (requestMastType.Count > 0)
                            {
                                _currentNamePort = _mainViewModel.PortsComboBoxText;
                                _mainViewModel.StateConnectionButton = "Отключить";
                                _mainViewModel.PortsComboBoxEnabled = false;
                                _mainViewModel.SpeedComboBoxEnabled = false;
                                _mainViewModel.AddressDeviceNumericUpDownEnabled = false;
                                timerUpdateDate.Start();
                                _mainViewModel.ErrorMessageListBoxText += $"{DateTime.Now} Подключение к {_mainViewModel.PortsComboBoxText} со скоростью {_mainViewModel.SpeedComboBoxPropertyText} и устройству с ID = {_mainViewModel.AddressDeviceNumericUpDownValue}\n";

                                begin = 0;
                                DeviceIdentification(begin);

                                Thread.Sleep(100);
                                begin = 4;
                                byte addr = Convert.ToByte(_mainViewModel.AddressDeviceNumericUpDownValue);
                                DeterminationOfMastType(addr, begin);
                                return;
                            }
                            else
                            {
                                _newConnect.ConnectClose();
                                _mainViewModel.ErrorMessageListBoxText += $"{DateTime.Now} Ошибка подключения COM-порта\n";
                            }
                        }
                        catch { }
                    }
                    else if (_mainViewModel.StateConnectionButton == "Отключить")
                    {
                        timerUpdateDate.Stop();
                        Thread.Sleep(100);
                        _mainViewModel.StateConnectionButton = "";
                        _newConnect.ConnectClose();
                        _mainViewModel.StateConnectionButton = "Подключить";
                        _mainViewModel.PortsComboBoxEnabled = true;
                        _mainViewModel.SpeedComboBoxEnabled = true;
                        _mainViewModel.AddressDeviceNumericUpDownEnabled = true;
                    }
                }
            }
        }
        /// <summary>
        /// Load Second Window With Parameters
        /// </summary>
        public void LoadParametersWindow()
        {
            if (parametersWindow == null)
            {
                parametersWindow = new ParametersWindow();
                parametersWindow.DataContext = this;
                parametersWindow.Show();

            }
        }

        /// <summary>
        /// Get Information(ID Device, Firmware version, Firmware date) 
        /// </summary>
        /// <param name="begin"></param>
        public void DeviceIdentification(byte begin)
        {
            List<int> requestValidationResult = new List<int>();

            requestValidationResult = _newConnect.GetMassData(begin);

            if (requestValidationResult.Count > 0)
            {
                string identificationNumber = requestValidationResult[0].ToString("X");

                string date = requestValidationResult[1].ToString(); // прием входной строки дня и месяца
                string day = " ";
                string month = " ";
                if (date.Count() == 4)
                {
                    day = date.Substring(0, 1); // извлекаем первые цифры как день
                    month = date.Substring(2, 2); // извлекаем последние цифры как месяц
                }
                else if (date.Count() == 5)
                {
                    day = date.Substring(0, 2); // извлекаем первые цифры как день
                    month = date.Substring(3, 2); // извлекаем последние цифры как месяц
                }

                string versionAndYear = requestValidationResult[2].ToString();
                string programVersion = versionAndYear.Substring(0, 1);
                string programSubversion = versionAndYear.Substring(1, 2);
                string year = versionAndYear.Substring(3, 2);

                string resultDateAndMonth = $"{day}.{month}.{year}";

                _deviceInformation = $"Идентификационный номер устройства - 0x{identificationNumber}. Версия прошивки - {programVersion}.{programSubversion}. Дата прошивки: {resultDateAndMonth}";
            }
        }
        /// <summary>
        /// Get status information
        /// </summary>
        public void Diagnostics()
        {
            if (_newConnect != null)
            {
                timerUpdateDate.Stop();
                Thread.Sleep(100);

                byte beginNum = 0X0012;
                int numChng = 1234;
                _newConnect.PutData(beginNum, numChng);

                byte rowEncoder = 0X0019;

                for (int i = 1; i < 4; i++)
                {
                    beginNum = 0X0017;
                    numChng = i;
                    _newConnect.PutData(beginNum, numChng);

                    byte addr = (byte)_mainViewModel.AddressDeviceNumericUpDownValue;
                    byte begin = 0X0018;
                    List<int> resultDiagnost = _newConnect.RequestToExchangeData(addr, begin);

                    _mainViewModel.ErrorMessageListBoxText += $"Статусный регистр энкодера {i}:";
                    ErrorsAndDiagnostics.DiagnostEncoder(resultDiagnost, _mainViewModel);


                    resultDiagnost = _newConnect.RequestToExchangeData(addr, rowEncoder);
                    if (resultDiagnost.Count > 0)
                        _mainViewModel.ErrorMessageListBoxText += $"Абсолютное угловое положение {i} энкодера = {resultDiagnost[0]}";
                    rowEncoder++;
                }

                beginNum = 0X0012;

                _newConnect.PutData(beginNum, 0);
                timerUpdateDate.Start();
            }
        }
        public void UpdateFirmware()
        {
            if (_mainViewModel.StateConnectionButton == "Отключить")
            {
                timerUpdateDate.Stop();

                OpenFileDialog openFileDialog = new OpenFileDialog();

                if (openFileDialog.ShowDialog() == true)
                {
                    _newConnect.PutData(71, 0xFF80);
                    _newConnect.ConnectClose();

                    string fileName = Path.GetFileName(openFileDialog.FileName);

                    ProcessStartInfo startInfo = new ProcessStartInfo();
                    startInfo.FileName = "cmd.exe";
                    startInfo.Arguments = $"/K UpdateFirmware.exe {_currentNamePort} {fileName}";
                    Process process = Process.Start(startInfo);
                    process.WaitForExit();

                    byte begin = 0;
                    DeviceIdentification(begin);
                }
                if (!_newConnect.ConnectIsOpen())
                    _newConnect = new CreateNewConnect(_currentNamePort, Convert.ToInt32(_mainViewModel.SpeedComboBoxPropertyText), (byte)_mainViewModel.AddressDeviceNumericUpDownValue);

                timerUpdateDate.Start();
            }
        }
        public void DeterminationOfMastType(byte addr, byte begin)
        {
            List<int> requestValidationResult = new List<int>();

            requestValidationResult = _newConnect.RequestToExchangeData(addr, begin);

            ErrorsAndDiagnostics.DeterminationOfMastTypeAnswer(requestValidationResult, _mainViewModel);
        }
        /// <summary>
        /// Set zero position
        /// </summary>
        public void ChangeZeroPositionValue()
        {
            if (_newConnect != null && _mainViewModel.StateConnectionButton == "Отключить")
            {
                List<int> requestValidationResult = new List<int>();
                byte begin = 8;
                int zeroPositionCalibration = 1;
                byte addr = _newConnect.Addr;

                _newConnect.PutData(begin, zeroPositionCalibration);

                Thread.Sleep(100);

                requestValidationResult = _newConnect.RequestToExchangeData(addr, begin);

                _mainViewModel.ErrorMessageListBoxText += ErrorsAndDiagnostics.ChangeZeroPositionValueAnswer(requestValidationResult);

                RecordingDeviceReadings();
            }
        }
        List<int> RecordingResultOfChanges(byte begin, int speed, byte addr)
        {
            _newConnect.ConnectClose();

            _newConnect = new CreateNewConnect(_currentNamePort, speed);

            _mainViewModel.PortsComboBoxText = _currentNamePort;

            if (_newConnect == null) { return new List<int>(); }

            Thread.Sleep(100);

            return _newConnect.RequestToExchangeData(addr, begin);
        }

        List<int> requestMassDataMast;
        bool startMethodMastPosition;
        private object mastPositionLock = new object();
        void MassDataMast()
        {
            lock (mastPositionLock)
            {
                startMethodMastPosition = true;

                requestMassDataMast = new List<int>();

                byte begin = 0x0003;

                requestMassDataMast = _newConnect.GetMassData(begin);

                startMethodMastPosition = false;
            }
        }
        void RecordingDeviceReadings()
        {
            if (requestMassDataMast[4] != 0)
            {
                //Записываем положение мачты
                _mainViewModel.MastPositionTextBoxPropertyText = requestMassDataMast[4].ToString();

                //Обновление ProgressBar
                int minProgressBar = Convert.ToInt32(_mainViewModel.MastPositionProgressBarMinimum);
                int maxProgressBar = 8010;
                if (requestMassDataMast[4] >= minProgressBar && requestMassDataMast[4] <= maxProgressBar)
                {
                    _mainViewModel.MastPositionProgressBarValue = requestMassDataMast[4].ToString();
                    float mastPositionPersent = (requestMassDataMast[4] - minProgressBar) * 100 / (maxProgressBar - minProgressBar);
                    _mainViewModel.MastPositionLabelContent = mastPositionPersent.ToString() + " % Положения мачты";
                }

                //Записываем в новое окно параметры
                //if (parametrsForm != null)
                // parametrsForm.WriteToTableForm();
            }
        }
        public void MystTypeChange()
        {
            if (_newConnect != null && _mainViewModel.StateConnectionButton == "Отключить" && _mainViewModel.MystTypeComboBoxText != "")
            {
                byte begin = 4;
                string mastType = _mainViewModel.MystTypeComboBoxText;
                byte addr = _newConnect.Addr;
                int speed = _newConnect.BaudRate;
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

                _newConnect.PutData(begin, mastTypeConvert);

                DeterminationOfMastType(addr, begin);
            }
        }
        public void ChangeSpeed()
        {
            if (_newConnect != null && _mainViewModel.StateConnectionButton == "Отключить" && _mainViewModel.ExchngSpdInWorkComboBoxText != "")
            {
                List<int> requestValidationResult = new List<int>();
                byte begin = 10;
                byte addr = _newConnect.Addr;
                string newSpeed = _mainViewModel.ExchngSpdInWorkComboBoxText;

                _newConnect.PutData(begin, Convert.ToInt32(newSpeed.Substring(0, newSpeed.Length - 2)));

                requestValidationResult = RecordingResultOfChanges(begin, Convert.ToInt32(newSpeed), addr);

                _mainViewModel.ErrorMessageListBoxText += ErrorsAndDiagnostics.ChangeSpeedAnswer(requestValidationResult, _mainViewModel, newSpeed);
            }
        }
        public void ChangeAddress()
        {
            if (_newConnect != null && _mainViewModel.StateConnectionButton == "Отключить")
            {
                string newAddrText = _mainViewModel.AddrssDvsInWorkTextBoxValue.ToString();
                if (string.IsNullOrEmpty(newAddrText)) { return; }

                if (int.TryParse(newAddrText, out int value) && value >= 1 && value <= 247)
                {
                    List<int> requestValidationResult = new List<int>();
                    byte begin = 12;
                    byte newAddr = Convert.ToByte(newAddrText);
                    int speed = _newConnect.BaudRate;

                    _newConnect.PutData(begin, newAddr);

                    requestValidationResult = RecordingResultOfChanges(begin, speed, newAddr);

                    _mainViewModel.ErrorMessageListBoxText += ErrorsAndDiagnostics.ChangeAddressAnswer(requestValidationResult, _mainViewModel, newAddr);
                }
                else
                {
                    MessageBox.Show("Значение должно быть в диапазоне от 1 до 247.");
                }
            }
        }


    }
}
