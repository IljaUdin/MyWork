using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace ReaderPorts
{
    public partial class MainForm : Form
    {
        MyTabControl myTabControl;
        CreateNewConnect newConnect;
        StreamWriter streamReadLog;

        Dictionary<Series, string> chartPortDictionary = new Dictionary<Series, string>();
        List<CreateNewConnect> portsMass = new List<CreateNewConnect>();

        int maxPointsDiagram = 100;
        public MainForm()
        {
            InitializeComponent();
        }

        //Обновляем список Com-портов
        private void bnUpdate_Click(object sender, EventArgs e)
        {
            string[] portsNames = SerialPort.GetPortNames();

            cmbBxPrts.Text = "";
            cmbBxPrts.Items.Clear();
            if (portsNames.Length != 0)
            {
                cmbBxPrts.Items.AddRange(portsNames);
                cmbBxPrts.SelectedIndex = 0;
            }
        }

        //Подключение к Com-порту
        private void bnConnection_Click(object sender, EventArgs e)
        {
            if (cmbBxPrts.Text != "")
            {
                if (bnConnection.Text == "Connect")
                {
                    try
                    {
                        newConnect = new CreateNewConnect(cmbBxPrts.Text, int.Parse(cmbBxExchngSpd.Text), cmbBxTypePrtcls.Text, byte.Parse(nmrcAddrssDvs.Text),
                                                          Convert.ToByte(nmrcBegin.Value), Convert.ToByte(nmrcNmbrСlls.Value));
                        if (newConnect == null) { return; }
                        portsMass.Add(newConnect);
                        bnConnection.Text = "Disconnect";
                        cmbBxTypePrtcls.Enabled = false;
                        cmbBxExchngSpd.Enabled = false;
                        nmrcAddrssDvs.Enabled = false;

                        //Создание экземпляров для вкладок TabPage
                        myTabControl.CreateNewTabPage(newConnect);
                    }
                    catch
                    {
                        listBoxErrorMessage.Items.Add(DateTime.Now + " " + "Ошибка подключения COM-порта");
                    }

                    if (newConnect.TypeProtocol == "SLIP")
                    try
                    {
                        myTabControl.WriteToTableDescriptor();
                    }
                    catch { }

                }
                else if (bnConnection.Text == "Disconnect")
                {
                    for (int i = 0; i < portsMass.Count; i++)
                    {
                        if (portsMass[i].PortName == cmbBxPrts.SelectedItem.ToString())
                        {
                            do
                            {
                                //Задержка необходима для успешного завершения всех запущенных процессов
                                Thread.Sleep(100);

                                // Удаление полей в MainTable
                                for (int j = MainTable.Rows.Count - 1; j >= 0; j--)
                                {
                                    if (portsMass[i].PortName == MainTable.Rows[j].Cells["Port"].Value.ToString())
                                    {
                                        // Удаление переменных в графике
                                        Series seriesToRemove = chartPort.Series.FindByName(MainTable.Rows[j].Cells["Port"].Value.ToString() + " " + MainTable.Rows[j].Cells["Position"].Value.ToString());

                                        if (chartPortDictionary.Count > 0 && seriesToRemove != null)
                                            chartPortDictionary.Remove(seriesToRemove);

                                        if (seriesToRemove != null)
                                            chartPort.Series.Remove(seriesToRemove);

                                        MainTable.Rows.Remove(MainTable.Rows[j]);
                                    }
                                }
                                // Удаление вкладки TabPage
                                for (int j = 0; j < myTabControl.TabPagesList.Count; j++)
                                {
                                    if (portsMass[i].PortName == myTabControl.TabPagesList[j].PortName)
                                    {
                                        myTabControl.RemoveTabPage(portsMass[i].PortName);
                                    }
                                }

                                portsMass[i].ConnectClose();
                                portsMass.RemoveRange(i, 1);
                                bnConnection.Text = "Connect";
                                cmbBxTypePrtcls.Enabled = true;
                                cmbBxExchngSpd.Enabled = true;
                                nmrcAddrssDvs.Enabled = true;

                                return;
                            }
                            while (tickLaunchMethod != 0);
                        }
                    }
                }
            }
        }

        int tickTimer = 0;
        int tickLaunchMethod = 0;
        async void MethodLaunchTask()
        {
            tickLaunchMethod++;
            if (tickTimer == tickLaunchMethod)
            {
                List<Task> tasks = new List<Task>();

                foreach (var myPage in myTabControl.TabPagesList)
                {
                    tasks.Add(myPage.ReadData());
                }
                await Task.WhenAll(tasks);

                WriteToDiagram();
                WriteToLogData();

                if (listBoxErrorMessage.Items.Count < 200)
                    ErrorWriteToListBox();
            }
            if (tickLaunchMethod > 2)
                tickLaunchMethod = 1;
            tickLaunchMethod--;
        }

        void ErrorWriteToListBox()
        {
            for (int i = 0; i < myTabControl.TabPagesList.Count; i++)
            {
                if (myTabControl.TabPagesList[i].ErrorWriteToTable != null)
                {
                    listBoxErrorMessage.Items.Add(DateTime.Now + " " + myTabControl.TabPagesList[i].ErrorWriteToTable);
                    myTabControl.TabPagesList[i].ErrorWriteToTable = null;
                    listBoxErrorMessage.Items.Add("-----------------");
                    listBoxErrorMessage.TopIndex = listBoxErrorMessage.Items.Count - 1;
                }
                if (myTabControl.TabPagesList[i].ErrorGetMassData != null)
                {
                    listBoxErrorMessage.Items.Add(DateTime.Now + " " + myTabControl.TabPagesList[i].ErrorGetMassData);
                    myTabControl.TabPagesList[i].ErrorGetMassData = null;
                    listBoxErrorMessage.Items.Add("-----------------");
                    listBoxErrorMessage.TopIndex = listBoxErrorMessage.Items.Count - 1;
                }
            }
        }

        List<int> massToLogData = new List<int>();
        void WriteToLogData()
        {
            for (int i = 0; i < MainTable.Rows.Count; i++)
                massToLogData.Add(Convert.ToInt32(MainTable.Rows[i].Cells["Dec"].Value));

            WriteToCSVFile(massToLogData);
            massToLogData.RemoveRange(0, massToLogData.Count);
        }
        void WriteToCSVFile(List<int> massData)
        {
            //Запись в файл для вывода в формат CSV
            if (streamReadLog != null)
                if (massData != null && massData.Count > 0)
                {
                    streamReadLog?.Close();
                    //Запись в файл для вывода в формат CSV
                    streamReadLog = new StreamWriter("ReadPorts.txt", true);
                    streamReadLog?.Write($"{DateTime.Now},");
                    for (int i = 0; i < MainTable.Rows.Count; i++)
                    {
                        if (i >= massData.Count)
                            break;

                        streamReadLog?.Write($" {massData[i]}");
                    }
                    streamReadLog?.WriteLine();
                    streamReadLog?.Close();
                }
        }

        //Обработка события записи в устройство
        private void MainTable_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 6)
                if (MainTable.CurrentCell.Value != null)
                {
                    ushort numChng = 0;

                    string temporaireNum = MainTable.CurrentCell.Value.ToString();

                    if (short.TryParse(temporaireNum, out short decimalValue))
                    {
                        if (decimalValue >= short.MinValue && decimalValue <= short.MaxValue)
                        {
                            // число в допустимом диапазоне, добавляем его в таблицу
                            numChng = (ushort)decimalValue;
                        }
                    }
                    else if (temporaireNum.StartsWith("0x") && ushort.TryParse(temporaireNum.Substring(2), NumberStyles.HexNumber, null, out ushort hexValue))
                    {
                        if (hexValue >= ushort.MinValue && hexValue <= ushort.MaxValue)
                        {
                            // число в допустимом диапазоне, добавляем его в таблицу
                            numChng = ushort.Parse(hexValue.ToString());
                        }
                    }
                    else
                    {
                        // невозможно преобразовать ввод в целое число, выводим сообщение об ошибке
                        listBoxErrorMessage.Items.Add(DateTime.Now + " " + "Неверный формат или число вне допустимого диапазона.");
                        return;
                    }

                    byte BeginPosition = Convert.ToByte(MainTable.Rows[e.RowIndex].Cells["Position"].Value.ToString());

                    for (int i = 0; i < portsMass.Count; i++)
                        if (portsMass[i].PortName == MainTable.Rows[e.RowIndex].Cells["Port"].Value.ToString())
                        {
                            timerUpdateDate.Enabled = false;
                            Thread.Sleep(100);
                            portsMass[i].PutData(BeginPosition, numChng, portsMass[i].TypeProtocol);
                            timerUpdateDate.Enabled = true;
                            break;
                        }
                }
        }
        private void nmrcNmbrСlls_ValueChanged(object sender, EventArgs e)
        {
            //Для таблицы NewConnect
            /*if (nmrcNmbrСlls.Value > dataGridView3.Rows.Count)
            {
                for (int j = dataGridView3.Rows.Count; j < nmrcNmbrСlls.Value; j++)
                {
                    dataGridView3.Rows.Add();
                    //Принудительно присваиваем значение, иначе не будет работать CheckBox
                    dataGridView3.Rows[j].Cells[2].Value = false;
                    dataGridView3.Rows[j].HeaderCell.Value = string.Format((j).ToString(), "0");
                }
            }
            else if (nmrcNmbrСlls.Value < dataGridView3.Rows.Count)
            {
                for (int j = dataGridView3.Rows.Count; j > Convert.ToInt32(nmrcNmbrСlls.Value); j--)
                    dataGridView3.Rows.Remove(dataGridView3.Rows[j - 1]);
            }*/
        }
        private void bnLogger_Click(object sender, EventArgs e)
        {
            {
                if (bnLogger.Text == "Log On")
                {
                    bnLogger.Text = "Log Off";

                    //Запись в файл для вывода в формат CSV
                    streamReadLog = new StreamWriter("ReadPorts.txt", true);
                }
                else if (bnLogger.Text == "Log Off")
                {
                    {
                        bnLogger.Text = "Log On";

                        streamReadLog.Close();
                        streamReadLog = null;
                    }
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            bnUpdate_Click(sender, e);
            nmrcNmbrСlls_ValueChanged(sender, e);
            myTabControl = new MyTabControl(tabControl1, timerUpdateDate, MainTable, chartPort, chartPortDictionary);
            timerUpdateDate.Enabled = true;
        }

        private void timerUpdateDate_Tick(object sender, EventArgs e)
        {
            tickTimer++;
            if (tickTimer < 2)
                MethodLaunchTask();
            tickTimer--;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (streamReadLog != null)
                streamReadLog.Close();
        }
        private void qToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }
        //Обработка события выбора Com-порта
        private void cmbBxPrts_SelectionChangeCommitted(object sender, EventArgs e)
        {
            for (int i = 0; i < portsMass.Count; i++)
                //Если имя порта добавлено в массив
                if (portsMass[i].PortName == cmbBxPrts.SelectedItem.ToString())
                {
                    if (portsMass[i].ConnectIsOpen())
                    {
                        bnConnection.Text = "Disconnect";
                        cmbBxTypePrtcls.Text = portsMass[i].TypeProtocol;
                        cmbBxExchngSpd.Text = portsMass[i].BaudRate.ToString();
                        nmrcNmbrСlls.Text = portsMass[i].QtyRows.ToString();
                        cmbBxTypePrtcls.Enabled = false;
                        cmbBxExchngSpd.Enabled = false;
                        nmrcAddrssDvs.Enabled = false;
                        break;
                    }
                }
                //если имя порта нет в списке
                else
                {
                    bnConnection.Text = "Connect";
                    cmbBxTypePrtcls.Enabled = true;
                    cmbBxExchngSpd.Enabled = true;
                    nmrcAddrssDvs.Enabled = true;
                }
        }
        private void MainTable_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 2 && e.RowIndex >= 0)
                if (e.RowIndex >= 0 && e.RowIndex < MainTable.Rows.Count)
                {
                    //Условие чтобы CheckBox в таблице отрабатывал мгновенно
                    if ((bool)MainTable.Rows[e.RowIndex].Cells["Range"].Value == false)
                    {
                        MainTable.Rows[e.RowIndex].Cells["Range"].Value = true;
                    }
                    else
                    {
                        MainTable.Rows[e.RowIndex].Cells["Range"].Value = false;
                    }
                }

            if (e.ColumnIndex == 3 && e.RowIndex >= 0)
            {
                string portNameMainTable = MainTable.Rows[e.RowIndex].Cells["Port"].Value.ToString();
                string positionNameMainTable = MainTable.Rows[e.RowIndex].Cells["Position"].Value.ToString();
                int rowIndex = MainTable.Rows[e.RowIndex].Index;

                if ((bool)(MainTable.Rows[e.RowIndex].Cells["Graf"].Value) == false)
                {
                    MainTable.Rows[e.RowIndex].Cells["Graf"].Value = true;
                    AddToChart(portNameMainTable, positionNameMainTable, rowIndex);
                }
                else
                {
                    MainTable.Rows[e.RowIndex].Cells["Graf"].Value = false;
                    DeleteFromChart(portNameMainTable, positionNameMainTable, rowIndex);
                }
            }

        }
        private void MainTable_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 2 && e.RowIndex >= 0)
                if (e.RowIndex >= 0 && e.RowIndex < MainTable.Rows.Count)
                {
                    //Условие чтобы CheckBox в таблице отрабатывал мгновенно
                    if ((bool)MainTable.Rows[e.RowIndex].Cells["Range"].Value == false)
                    {
                        MainTable.Rows[e.RowIndex].Cells["Range"].Value = true;
                    }
                    else
                    {
                        MainTable.Rows[e.RowIndex].Cells["Range"].Value = false;
                    }
                }
            if (e.ColumnIndex == 3 && e.RowIndex >= 0)
            {
                string portNameMainTable = MainTable.Rows[e.RowIndex].Cells["Port"].Value.ToString();
                string positionNameMainTable = MainTable.Rows[e.RowIndex].Cells["Position"].Value.ToString();
                int rowIndex = MainTable.Rows[e.RowIndex].Index;

                if ((bool)(MainTable.Rows[e.RowIndex].Cells["Graf"].Value) == false)
                {
                    MainTable.Rows[e.RowIndex].Cells["Graf"].Value = true;
                    AddToChart(portNameMainTable, positionNameMainTable, rowIndex);
                }
                else
                {
                    MainTable.Rows[e.RowIndex].Cells["Graf"].Value = false;
                    DeleteFromChart(portNameMainTable, positionNameMainTable, rowIndex);
                }
            }
        }
        void WriteToDiagram()
        {
            for (int i = 0; i < MainTable.Rows.Count; i++)
                if ((bool)MainTable.Rows[i].Cells["Graf"].Value == true)
                    for (int j = 0; j < chartPort.Series.Count; j++)
                        for (int k = 0; k < chartPortDictionary.Count; k++)
                            if (chartPort.Series[j].Name == chartPortDictionary.Keys.ElementAt(k).Name)
                                if (MainTable.Rows[i].Index.ToString() == chartPortDictionary.Values.ElementAt(k).ToString())
                                {
                                    if (chartPort.Series[chartPort.Series[j].Name].Points.Count > maxPointsDiagram)
                                        chartPort.Series[chartPort.Series[j].Name].Points.RemoveAt(0);

                                    // Добавляем последующие точки
                                    chartPort.Series[chartPort.Series[j].Name].Points.AddXY(DateTime.Now.ToString("HH:mm:ss"), Convert.ToDouble(MainTable.Rows[i].Cells["Dec"].Value));
                                }
        }
        void AddToChart(string portNameAddToMainTable, string positionNameAddToMainTable, int rowIndex)
        {
            var series = new Series(portNameAddToMainTable + " " + positionNameAddToMainTable);
            series.ChartType = SeriesChartType.Spline;
            chartPort.Series.Add(series);
            chartPortDictionary.Add(series, rowIndex.ToString());
        }
        void DeleteFromChart(string portNameDeleteFromMainTable, string positionNameDeleteFromMainTable, int rowIndex)
        {
            if (chartPort.Series.IsUniqueName(portNameDeleteFromMainTable + " " + positionNameDeleteFromMainTable)) { return; }
            // Ищем серию с указанным именем в Chart и удаляем ее
            Series seriesToRemove = chartPort.Series.FindByName(portNameDeleteFromMainTable + " " + positionNameDeleteFromMainTable);
            if (seriesToRemove != null)
            {
                chartPort.Series.Remove(seriesToRemove);
                chartPortDictionary.Remove(seriesToRemove);
            }
        }
        private void clearErrorListBoxToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            listBoxErrorMessage.Items.Clear();
        }
    }
}





