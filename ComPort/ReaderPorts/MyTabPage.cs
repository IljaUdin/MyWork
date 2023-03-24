using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace ReaderPorts
{
    internal class MyTabPage
    {
        TabControl tabControl;
        public TabPage newTab;
        System.Windows.Forms.Timer timerUpdateDate;
        public CreateNewConnect newConnect;
        DataGridView MainTable;
        Chart chartPort;

        Dictionary<Series, string> chartPortDictionary;
        List<int> massDataFromPort;
        public List<int> nmbCurrentRowIndex = new List<int>();
        public List<int> nmbCurrentRowIndexWithOutBegin = new List<int>();
        string portName;
        DataGridView newDataGridView;
        string errorWriteToTable;

        public string ErrorWriteToTable
        {
            get { return errorWriteToTable; }
            set { errorWriteToTable = value; }
        }
        public string ErrorGetMassData
        {
            get { return newConnect.ErrorGetMassData; }
            set { newConnect.ErrorGetMassData = value; }
        }
        public DataGridView NewDataGridViewName
        {
            get { return newDataGridView; }
        }
        public string PortName
        {
            get { return portName; }
        }
        public List<int> MassDataFromPort
        {
            get { return massDataFromPort; }
        }
        public MyTabPage(TabControl tabControl, System.Windows.Forms.Timer timer, DataGridView MainTable, Chart chartPort, Dictionary<Series, string> chartPortDictionary)
        {
            this.tabControl = tabControl;
            timerUpdateDate = timer;
            this.MainTable = MainTable;
            this.chartPort = chartPort;
            this.chartPortDictionary = chartPortDictionary;
        }
        public void CreateNewTabPage(CreateNewConnect newConnect)
        {
            this.newConnect = newConnect;
            newTab = new TabPage(newConnect.PortName);
            portName = newConnect.PortName;
            tabControl.TabPages.Insert(tabControl.TabPages.Count, newTab);
            tabControl.SelectedTab = newTab;
            CreateDataGridView(newConnect.PortName);

        }

        private void CreateDataGridView(string name)
        {
            newDataGridView = new DataGridView();
            newDataGridView.Name = $"{name}";
            newDataGridView.Location = new Point(10, 5);
            newDataGridView.Size = new Size(755, 600);
            newDataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            newDataGridView.AllowUserToAddRows = false;
            newDataGridView.AllowUserToDeleteRows = false;
            newDataGridView.AllowUserToResizeColumns = false;
            newDataGridView.AllowUserToResizeRows = false;
            newDataGridView.AlternatingRowsDefaultCellStyle = new DataGridViewCellStyle() { BackColor = Color.LightGray };
            newDataGridView.CellBorderStyle = DataGridViewCellBorderStyle.Sunken;
            newDataGridView.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            newDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            newDataGridView.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            newDataGridView.RowTemplate.Height = 20;
            newDataGridView.RowHeadersWidth = 55;
            newDataGridView.RowHeadersVisible = true;
            newDataGridView.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            newDataGridView.RowHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            newDataGridView.Columns.Clear();
            {
                newDataGridView.Columns.Add("Hex_new", "Hex");
                newDataGridView.Columns["Hex_new"].Width = 200;
                newDataGridView.Columns["Hex_new"].ReadOnly = true;
                newDataGridView.Columns["Hex_new"].SortMode = DataGridViewColumnSortMode.NotSortable;

                newDataGridView.Columns.Add("Dec_new", "Dec");
                newDataGridView.Columns["Dec_new"].Width = 300;
                newDataGridView.Columns["Dec_new"].ReadOnly = true;
                newDataGridView.Columns["Dec_new"].SortMode = DataGridViewColumnSortMode.NotSortable;

                DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn();
                checkBoxColumn.Name = "Add_new";
                checkBoxColumn.HeaderText = "Add";
                checkBoxColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
                checkBoxColumn.Width = 100;
                newDataGridView.Columns.Add(checkBoxColumn);

                newDataGridView.Columns.Add("Record_new", "Record");
                newDataGridView.Columns["Record_new"].Width = 200;
                newDataGridView.Columns["Record_new"].SortMode = DataGridViewColumnSortMode.NotSortable;

                newDataGridView.Columns.Add("Descriptor_new", "Descriptor");
                newDataGridView.Columns["Descriptor_new"].Width = 200;
                newDataGridView.Columns["Descriptor_new"].SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            newDataGridView.Rows.Clear();
            for (int i = 0; i < newConnect.QtyRows; i++)
            {
                newDataGridView.Rows.Add();
                newDataGridView.Rows[i].Cells["Add_new"].Value = false;
                newDataGridView.Rows[i].HeaderCell.Value = $"{i}";
            }

            newTab.Controls.Add(newDataGridView);
        }

        public void WriteToTableSellStr()
        {
            if (newConnect.TypeProtocol == "SLIP")
            {
                newConnect.GetMassSellStr();
                for (int i = 0; i < newDataGridView.Rows.Count; i++)
                    newDataGridView.Rows[i].Cells["Descriptor_new"].Value = newConnect.massSellStr[i];
            }
        }
        public async Task ReadData()
        {
            massDataFromPort = new List<int>();
            await Task.Run(() =>
            {
                massDataFromPort = newConnect.GetMassData();
                WriteToTable();
            }).ConfigureAwait(false);
        }
        public void WriteToTable()
        {
            if (massDataFromPort != null && massDataFromPort.Count > 0)
                if (massDataFromPort.Count == newDataGridView.Rows.Count)
                {
                    for (int i = 0; i < newDataGridView.Rows.Count; i++)
                    {
                        newDataGridView.Rows[i].Cells["Hex_new"].Value = string.Format("0x{0:X4}", massDataFromPort[i]);
                        newDataGridView.Rows[i].Cells["Dec_new"].Value = Convert.ToString((short)massDataFromPort[i]);
                    }

                    if (MainTable.Rows.Count > 0)
                        for (int i = 0; i < MainTable.Rows.Count; i++)
                            if (MainTable.Rows[i].Cells["Port"].Value.ToString() == newConnect.PortName)
                                for (int j = 0; j < newDataGridView.Rows.Count; j++)
                                    if (MainTable.Rows[i].Cells["Position"].Value.ToString() == (newDataGridView.Rows[j].Index + newConnect.Begin).ToString())
                                    {
                                        MainTable.Rows[i].Cells["Hex"].Value = string.Format("0x{0:X4}", massDataFromPort[j]);
                                        if (Convert.ToBoolean(MainTable.Rows[i].Cells["Range"].Value))
                                            MainTable.Rows[i].Cells["Dec"].Value = Convert.ToString(massDataFromPort[j]);
                                        else
                                            MainTable.Rows[i].Cells["Dec"].Value = Convert.ToString((short)massDataFromPort[j]);
                                    }
                }
        }
        public void newDataGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 3)
                if (newDataGridView.CurrentCell.Value != null)
                {
                    ushort numChng = 0;

                    string temporaireNum = newDataGridView.CurrentCell.Value.ToString();

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
                        errorWriteToTable = "Неверный формат или число вне допустимого диапазона.";
                        return;
                    }

                    byte BeginPut = Convert.ToByte(newDataGridView.CurrentCell.RowIndex.ToString());

                    if (newConnect.ConnectIsOpen())
                    {
                        timerUpdateDate.Enabled = false;
                        Thread.Sleep(100);
                        newConnect.PutData(BeginPut, numChng, newConnect.TypeProtocol);
                        timerUpdateDate.Enabled = true;
                    }
                }
        }
        int selectedRowIndex;
        public void newDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 2 && e.RowIndex >= 0)
                if (newDataGridView.Rows[e.RowIndex].Cells[2].Value != null)
                    //Условие чтобы CheckBox в таблице отрабатывал мгновенно
                    if ((bool)newDataGridView.Rows[e.RowIndex].Cells[2].Value == false)
                    {
                        newDataGridView.Rows[e.RowIndex].Cells[2].Value = true;

                        MainTable.Rows.Add();
                        for (int g = MainTable.Rows.Count - 1; g < MainTable.Rows.Count; g++)
                        {
                            MainTable.Rows[g].Cells["Range"].Value = false;
                            MainTable.Rows[g].Cells["Graf"].Value = false;
                            MainTable.Rows[g].Cells["Port"].Value = PortName;
                            MainTable.Rows[g].Cells["Position"].Value = e.RowIndex + newConnect.Begin;
                            MainTable.Rows[g].Cells["Descriptor"].Value = newDataGridView.Rows[e.RowIndex].Cells[4].Value;
                        }
                    }
                    else
                    {
                        newDataGridView.Rows[e.RowIndex].Cells[2].Value = false;
                        selectedRowIndex = e.RowIndex + newConnect.Begin;

                        //Удаление выбранной строки из графика
                        for (int i = 0; i < MainTable.Rows.Count; i++)
                            if (MainTable.Rows[i].Cells["Port"].Value.ToString() == newConnect.PortName)
                                if (MainTable.Rows[i].Cells["Position"].Value.ToString() == selectedRowIndex.ToString())
                                {
                                    string portNameDeleteFromMainTable = MainTable.Rows[i].Cells["Port"].Value.ToString();
                                    string positionNameDeleteFromMainTable = MainTable.Rows[i].Cells["Position"].Value.ToString();
                                    MainTable.Rows[i].Cells["Graf"].Value = false;

                                    if (chartPort.Series.Count > 0)
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

                                    //Удаление выбранной строки из MainTable
                                    MainTable.Rows.RemoveAt(i);
                                    break;
                                }
                    }
        }
        public void newDataGridView_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 2 && e.RowIndex >= 0)
                if (newDataGridView.Rows[e.RowIndex].Cells[2].Value != null)
                    //Условие чтобы CheckBox в таблице отрабатывал мгновенно
                    if ((bool)newDataGridView.Rows[e.RowIndex].Cells[2].Value == false)
                    {
                        newDataGridView.Rows[e.RowIndex].Cells[2].Value = true;

                        MainTable.Rows.Add();
                        for (int g = MainTable.Rows.Count - 1; g < MainTable.Rows.Count; g++)
                        {
                            MainTable.Rows[g].Cells["Range"].Value = false;
                            MainTable.Rows[g].Cells["Graf"].Value = false;
                            MainTable.Rows[g].Cells["Port"].Value = PortName;
                            MainTable.Rows[g].Cells["Position"].Value = e.RowIndex + newConnect.Begin;
                        }
                    }
                    else
                    {
                        newDataGridView.Rows[e.RowIndex].Cells[2].Value = false;
                        selectedRowIndex = e.RowIndex + newConnect.Begin;

                        //Удаление выбранной строки из графика
                        for (int i = 0; i < MainTable.Rows.Count; i++)
                            if (MainTable.Rows[i].Cells["Port"].Value.ToString() == newConnect.PortName)
                                if (MainTable.Rows[i].Cells["Position"].Value.ToString() == selectedRowIndex.ToString())
                                {
                                    string portNameDeleteFromMainTable = MainTable.Rows[i].Cells["Port"].Value.ToString();
                                    string positionNameDeleteFromMainTable = MainTable.Rows[i].Cells["Position"].Value.ToString();
                                    MainTable.Rows[i].Cells["Graf"].Value = false;

                                    if (chartPort.Series.Count > 0)
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

                                    //Удаление выбранной строки из MainTable
                                    MainTable.Rows.RemoveAt(i);
                                    break;
                                }
                    }
        }
    }
}
