using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace EncoderWPF
{
    internal class ParametersViewModel : INotifyPropertyChanged
    {
        int _voltage3VTextBoxText;
        int _voltage5VTextBoxText;
        int _voltage27VTextBoxText;
        int _temperaturePlateTextBoxText;
        public int Voltage3VTextBoxText
        {
            get { return _voltage3VTextBoxText; }
            set
            {
                if (_voltage3VTextBoxText != value)
                {
                    _voltage3VTextBoxText = value;
                    OnPropertyChanged(nameof(Voltage3VTextBoxText));
                }
            }
        }
        public int Voltage5VTextBoxText
        {
            get { return _voltage5VTextBoxText; }
            set
            {
                if (_voltage5VTextBoxText != value)
                {
                    _voltage5VTextBoxText = value;
                    OnPropertyChanged(nameof(Voltage5VTextBoxText));
                }
            }
        }
        public int Voltage27VTextBoxText
        {
            get { return _voltage27VTextBoxText; }
            set
            {
                if (_voltage27VTextBoxText != value)
                {
                    _voltage27VTextBoxText = value;
                    OnPropertyChanged(nameof(Voltage27VTextBoxText));
                }
            }
        }
        public int TemperaturePlateTextBoxText
        {
            get { return _temperaturePlateTextBoxText; }
            set
            {
                if (_temperaturePlateTextBoxText != value)
                {
                    _temperaturePlateTextBoxText = value;
                    OnPropertyChanged(nameof(TemperaturePlateTextBoxText));
                }
            }
        }
        public ParametersViewModel()
        {

        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
