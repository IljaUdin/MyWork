using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace EncoderWPF
{
    internal class MainViewModel : ViewModelBase, INotifyPropertyChanged, IDataErrorInfo
    {
        ResultsProcessing _resultsProcessing;
        ObservableCollection<string> _portsNames;
        string _stateConnectionButton = "Подключить";
        string _portsComboBoxText;
        bool _portsComboBoxEnabled = true;
        string _speedComboBoxPropertyText;
        bool _speedComboBoxEnabled = true;
        int _addressDeviceNumericUpDownValue;
        bool _addressDeviceNumericUpDownEnabled = true;
        string _mystTypeComboBoxText;
        string _exchngSpdInWorkComboBoxText;
        int _addrssDvsInWorkTextBoxValue;
        string _mastPositionTextBoxPropertyText;
        string _mastPositionProgressBarValue, _mastPositionProgressBarMinimum;
        string _mastPositionLabelContent;
        string _errorMessageListBoxText;
        public ObservableCollection<string> PortsNames
        {
            get { return _portsNames; }
            set { Set(ref _portsNames, value); }
        }
        public string StateConnectionButton
        {
            get { return _stateConnectionButton; }
            set
            {
                if (_stateConnectionButton != value)
                {
                    _stateConnectionButton = value;
                    OnPropertyChanged(nameof(StateConnectionButton));
                }
            }
        }
        public bool PortsComboBoxEnabled
        {
            get { return _portsComboBoxEnabled; }
            set
            {
                if (_portsComboBoxEnabled != value)
                {
                    _portsComboBoxEnabled = value;
                    OnPropertyChanged(nameof(PortsComboBoxEnabled));
                }
            }
        }
        public string PortsComboBoxText
        {
            get { return _portsComboBoxText; }
            set
            {
                if (_portsComboBoxText != value)
                {
                    _portsComboBoxText = value;
                    OnPropertyChanged(nameof(PortsComboBoxText));
                }
            }
        }
        public string SpeedComboBoxPropertyText
        {
            get { return _speedComboBoxPropertyText; }
            set
            {
                if (_speedComboBoxPropertyText != value)
                {
                    _speedComboBoxPropertyText = value;
                    OnPropertyChanged(nameof(SpeedComboBoxPropertyText));
                }
            }
        }
        public bool SpeedComboBoxEnabled
        {
            get { return _speedComboBoxEnabled; }
            set
            {
                if (_speedComboBoxEnabled != value)
                {
                    _speedComboBoxEnabled = value;
                    OnPropertyChanged(nameof(SpeedComboBoxEnabled));
                }
            }
        }
        public int AddressDeviceNumericUpDownValue
        {
            get { return _addressDeviceNumericUpDownValue; }
            set
            {
                if (_addressDeviceNumericUpDownValue != value)
                {
                    _addressDeviceNumericUpDownValue = value;
                    OnPropertyChanged(nameof(AddressDeviceNumericUpDownValue));
                }
            }
        }
        // Условие для работы валидации. Так же требует реализации IDataErrorInfo и string Error
        public string this[string columnName]
        {
            get
            {
                if (columnName == nameof(AddressDeviceNumericUpDownValue))
                {
                    // Проверка ввода на цифры
                    int result;
                    if (!int.TryParse(AddressDeviceNumericUpDownValue.ToString(), out result) || AddressDeviceNumericUpDownValue < 1 || AddressDeviceNumericUpDownValue > 247)
                    {
                        return "Введите только цифры.";
                    }
                }
                return null;
            }
        }
        public string Error { get { return null; } }

        public bool AddressDeviceNumericUpDownEnabled
        {
            get { return _addressDeviceNumericUpDownEnabled; }
            set
            {
                if (_addressDeviceNumericUpDownEnabled != value)
                {
                    _addressDeviceNumericUpDownEnabled = value;
                    OnPropertyChanged(nameof(AddressDeviceNumericUpDownEnabled));
                }
            }
        }
        public string MystTypeComboBoxText
        {
            get { return _mystTypeComboBoxText; }
            set
            {
                if (_mystTypeComboBoxText != value)
                {
                    _mystTypeComboBoxText = value;
                    OnPropertyChanged(nameof(MystTypeComboBoxText));
                }
            }
        }
        public string ExchngSpdInWorkComboBoxText
        {
            get { return _exchngSpdInWorkComboBoxText; }
            set
            {
                if (_exchngSpdInWorkComboBoxText != value)
                {
                    _exchngSpdInWorkComboBoxText = value;
                    OnPropertyChanged(nameof(ExchngSpdInWorkComboBoxText));
                }
            }
        }
        
            public int AddrssDvsInWorkTextBoxValue
        {
            get { return _addrssDvsInWorkTextBoxValue; }
            set
            {
                if (_addrssDvsInWorkTextBoxValue != value)
                {
                    _addrssDvsInWorkTextBoxValue = value;
                    OnPropertyChanged(nameof(AddrssDvsInWorkTextBoxValue));
                }
            }
        }
        public string MastPositionTextBoxPropertyText
        {
            get { return _mastPositionTextBoxPropertyText; }
            set
            {
                if (_mastPositionTextBoxPropertyText != value)
                {
                    _mastPositionTextBoxPropertyText = value;
                    OnPropertyChanged(nameof(MastPositionTextBoxPropertyText));
                }
            }
        }
        public string MastPositionProgressBarValue
        {
            get { return _mastPositionProgressBarValue; }
            set
            {
                if (_mastPositionProgressBarValue != value)
                {
                    _mastPositionProgressBarValue = value;
                    OnPropertyChanged(nameof(MastPositionProgressBarValue));
                }
            }
        }
        public string MastPositionProgressBarMinimum
        {
            get { return _mastPositionProgressBarMinimum; }
            set
            {
                if (_mastPositionProgressBarMinimum != value)
                {
                    _mastPositionProgressBarMinimum = value;
                    OnPropertyChanged(nameof(MastPositionProgressBarMinimum));
                }
            }
        }
        public string MastPositionLabelContent
        {
            get { return _mastPositionLabelContent; }
            set
            {
                if (_mastPositionLabelContent != value)
                {
                    _mastPositionLabelContent = value;
                    OnPropertyChanged(nameof(MastPositionLabelContent));
                }
            }
        }
        public string ErrorMessageListBoxText
        {
            get { return _errorMessageListBoxText; }
            set
            {
                if (_errorMessageListBoxText != value)
                {
                    _errorMessageListBoxText = value;
                    OnPropertyChanged(nameof(ErrorMessageListBoxText));
                }
            }
        }
        public ICommand LoadParametersWindowCommand { get; private set; }
        public ICommand DiagnosticsCommand { get; private set; }
        public ICommand UpdateFirmwareCommand { get; }
        public ICommand CloseCommand { get; }
        public ICommand DeviceIdentificationCommand { get; }
        public ICommand ErrorMessageListBoxClearButton { get; }
        public ICommand RefreshPortsCommand { get; }
        public ICommand ConnectionCommand { get; }
        public ICommand ChangeZeroPositionValueCommand { get; }
        public ICommand MystTypeChangeCommand { get; }
        public ICommand ChangeSpeedCommand { get; }
        public ICommand ChangeAddressCommand { get; }
        

        public new event PropertyChangedEventHandler PropertyChanged;
        public MainViewModel()
        {
            _resultsProcessing = new ResultsProcessing(this);

            PortsNames = new ObservableCollection<string>();

            LoadParametersWindowCommand = new RelayCommand(LoadParametersWindow);
            DiagnosticsCommand = new RelayCommand(Diagnostics);
            UpdateFirmwareCommand = new RelayCommand(UpdateFirmware);
            CloseCommand = new RelayCommand(Close);
            DeviceIdentificationCommand = new RelayCommand(DeviceIdentification);
            ErrorMessageListBoxClearButton = new RelayCommand(ErrorMessageListBoxClear);
            RefreshPortsCommand = new RelayCommand(RefreshPorts);
            ConnectionCommand = new RelayCommand(Connection);
            ChangeZeroPositionValueCommand = new RelayCommand(ChangeZeroPositionValue);
            MystTypeChangeCommand = new RelayCommand(MystTypeChange);
            ChangeSpeedCommand = new RelayCommand(ChangeSpeed);
            ChangeAddressCommand = new RelayCommand(ChangeAddress);
        }
        void LoadParametersWindow()
        {
            _resultsProcessing.LoadParametersWindow();
        }
        void Diagnostics()
        {
            _resultsProcessing.Diagnostics();
        }
        void UpdateFirmware()
        {
            _resultsProcessing.UpdateFirmware();
        }
        void DeviceIdentification()
        {
            MessageBox.Show($"{_resultsProcessing.DeviceInformation}");
        }
        void Close()
        {
            Application.Current.Shutdown();
        }
        void ErrorMessageListBoxClear()
        {
            ErrorMessageListBoxText = string.Empty;
        }
        void RefreshPorts()
        {
            PortsNames.Clear();
            foreach (var portName in SerialPort.GetPortNames())
            {
                PortsNames.Add(portName);
            }
        }
        void Connection()
        {
            _resultsProcessing.Connection();
        }
        void ChangeZeroPositionValue()
        {
            _resultsProcessing.ChangeZeroPositionValue();
        }
        void MystTypeChange()
        {
            _resultsProcessing.MystTypeChange();
        }
        void ChangeSpeed()
        {
            _resultsProcessing.ChangeSpeed();
        }
        void ChangeAddress()
        {
            _resultsProcessing.ChangeAddress();
        }
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
