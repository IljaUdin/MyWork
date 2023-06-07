using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Threading;

namespace APU
{
    public class DevicePresenter
    {
        IMainFormView mainFormView;
        CreateNewConnectModel model;
        DispatcherTimer timerUpdateDate;

        List<int> requestMassDataMast;
        bool _isConnected;
        bool _startMethodUpdateData;
        int _timerUpdateMilliseconds = 100;
        public DevicePresenter(IMainFormView mainFormView)
        {
            this.mainFormView = mainFormView;
            model = new CreateNewConnectModel();
            _isConnected = false;

            timerUpdateDate = new DispatcherTimer();
            timerUpdateDate.Interval = TimeSpan.FromMilliseconds(_timerUpdateMilliseconds);
            timerUpdateDate.Tick += Timer_Tick;
        }
        /// <summary>
        /// Update list of available COM-ports
        /// </summary>
        public void RefreshPorts()
        {
            List<string> ports = model.GetAvailablePorts();
            mainFormView.UpdatePortList(ports);
        }
        /// <summary>
        /// Try Connect To Device
        /// </summary>
        /// <param name="portName"></param>
        /// <param name="baudRate"></param>
        /// <param name="addr"></param>
        public void ToggleConnection(string portName, int baudRate, byte addr)
        {
            if (_isConnected)
            {
                timerUpdateDate.Stop();
                model.ConnectClose();
                _isConnected = false;
            }
            else
            {
                _isConnected = model.Connect(portName, baudRate, addr);

                if (_isConnected) { timerUpdateDate.Start(); }
            }

            mainFormView.SetConnectButtonText(_isConnected);
        }
        /// <summary>
        /// Write data to device
        /// </summary>
        /// <param name="BeginPut"></param>
        /// <param name="numChng"></param>
        public void PutData(byte BeginPut, int numChng)
        {
            model.PutData(BeginPut, numChng);
        }
        void UpdateData()
        {
            _startMethodUpdateData = true;

            requestMassDataMast = new List<int>();

            byte begin = 20;
            byte Qty = 4;

            requestMassDataMast = model.GetMassData(begin, Qty);

            if (requestMassDataMast.Count < Qty)
                return;

            mainFormView.UpdateDataOnForm(requestMassDataMast);

            _startMethodUpdateData = false;
        }
        private async void Timer_Tick(object sender, EventArgs e)
        {
            await Task.Run(() =>
            {
                if (!_startMethodUpdateData)
                    UpdateData();
            });
        }
    }
}
