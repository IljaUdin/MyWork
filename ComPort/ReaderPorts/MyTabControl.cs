using System.Collections.Generic;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace ReaderPorts
{
    internal class MyTabControl
    {
        TabControl tabControl;
        Timer timerUpdateDate;
        MyTabPage myTabPage;
        DataGridView MainTable;
        Chart chartPort;

        Dictionary<Series, string> chartPortDictionary;
        List<MyTabPage> tabPagesList = new List<MyTabPage>();

        public List<MyTabPage> TabPagesList
        {
            get { return tabPagesList; }
        }
        public MyTabControl(TabControl tabControl, Timer timer, DataGridView MainTable, Chart chartPort, Dictionary<Series, string> chartPortDictionary)
        {
            this.tabControl = tabControl;
            timerUpdateDate = timer;
            this.MainTable = MainTable;
            this.chartPort = chartPort;
            this.chartPortDictionary = chartPortDictionary;
        }
        public void CreateNewTabPage(CreateNewConnect newConnect)
        {
            myTabPage = new MyTabPage(tabControl, timerUpdateDate, MainTable, chartPort, chartPortDictionary);
            tabPagesList.Add(myTabPage);
            myTabPage.CreateNewTabPage(newConnect);
            myTabPage.NewDataGridViewName.CellEndEdit += new DataGridViewCellEventHandler(myTabPage.newDataGridView_CellEndEdit);
            myTabPage.NewDataGridViewName.CellContentClick += new DataGridViewCellEventHandler(myTabPage.newDataGridView_CellContentClick);
            myTabPage.NewDataGridViewName.CellContentDoubleClick += new DataGridViewCellEventHandler(myTabPage.newDataGridView_CellContentDoubleClick);
        }
        public void RemoveTabPage(string name)
        {
            for (int i = 0; i < tabPagesList.Count; i++)
                if (tabPagesList[i].PortName == name)
                {
                    tabPagesList[i].NewDataGridViewName.CellEndEdit -= new DataGridViewCellEventHandler(tabPagesList[i].newDataGridView_CellEndEdit);
                    tabPagesList[i].NewDataGridViewName.CellContentClick -= new DataGridViewCellEventHandler(tabPagesList[i].newDataGridView_CellContentClick);
                    tabPagesList[i].NewDataGridViewName.CellContentDoubleClick -= new DataGridViewCellEventHandler(tabPagesList[i].newDataGridView_CellContentDoubleClick);
                    tabControl.TabPages.Remove(tabPagesList[i].newTab);
                    tabPagesList.RemoveRange(i, 1);
                    break;
                }
        }

        public void WriteToTableDescriptor()
        {
            myTabPage.WriteToTableSellStr();
        }
    }
}
