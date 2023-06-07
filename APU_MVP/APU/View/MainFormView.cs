using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Threading;

namespace APU
{
    public interface IMainFormView
    {
        void UpdatePortList(List<string> portsName);
        void SetConnectButtonText(bool isConnected);
        void UpdateDataOnForm(List<int> requestMassDataMast);
    }
    public partial class MainFormView : Form, IMainFormView
    {
        CreateNewElementOnForm newElementOnForm;
        DevicePresenter devicePresenter;
        
        public MainFormView()
        {
            InitializeComponent();

            devicePresenter = new DevicePresenter(this);
        }
        public void UpdatePortList(List<string> portsName)
        {
            cmbBxPrts.DataSource = portsName;
        }

        public void SetConnectButtonText(bool isConnected)
        {
            bnConnection.Text = isConnected ? "Отключить" : "Подключить";
        }
        public void UpdateDataOnForm(List<int> requestMassDataMast)
        {
            newElementOnForm.TextBoxAngeleActualRead.Invoke((MethodInvoker)(() => newElementOnForm.TextBoxAngeleActualRead.Text = requestMassDataMast[0].ToString()));
            newElementOnForm.TextBoxSpeedActualRead.Invoke((MethodInvoker)(() => newElementOnForm.TextBoxSpeedActualRead.Text = ((short)requestMassDataMast[2]).ToString()));
            newElementOnForm.TextBoxCurrentActualRead.Invoke((MethodInvoker)(() => newElementOnForm.TextBoxCurrentActualRead.Text = (Math.Abs((short)requestMassDataMast[3])).ToString()));
        }
        private void bnUpdate_Click(object sender, EventArgs e)
        {
            devicePresenter.RefreshPorts();
        }

        private void bnConnection_Click(object sender, EventArgs e)
        {
            try
            {
                devicePresenter.ToggleConnection(cmbBxPrts.Text, int.Parse(cmbBxExchngSpd.Text), byte.Parse(nmrcAddrssDvs.Text));

                if (bnConnection.Text == "Отключить")
                {
                    newElementOnForm = new CreateNewElementOnForm(this, devicePresenter);
                    newElementOnForm.NewElementAdd();
                    cmbBxPrts.Enabled = false;
                    cmbBxExchngSpd.Enabled = false;
                    nmrcAddrssDvs.Enabled = false;

                    listBoxErrorMessage.Items.Add($"{DateTime.Now} Подключение к {cmbBxPrts.Text} со скоростью {cmbBxExchngSpd.Text} и устройству с ID = {nmrcAddrssDvs.Text}");
                    listBoxErrorMessage.TopIndex = listBoxErrorMessage.Items.Count - 1;
                }
                else if (bnConnection.Text == "Подключить" && newElementOnForm != null)
                {
                    newElementOnForm.DeleteElement();
                    cmbBxPrts.Enabled = true;
                    cmbBxExchngSpd.Enabled = true;
                    nmrcAddrssDvs.Enabled = true;
                }
            }
            catch
            {
                listBoxErrorMessage.Items.Add(DateTime.Now + " " + "Ошибка подключения COM-порта");
                listBoxErrorMessage.TopIndex = listBoxErrorMessage.Items.Count - 1;
            }
        }
        private void MainForm_Load(object sender, EventArgs e)
        {
            bnUpdate_Click(sender, e);
        }

        private void qToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void clearErrorListBoxToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listBoxErrorMessage.Items.Clear();
        }
    }
}
