using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace APU
{
    internal class CreateNewElementOnForm
    {
        Form form;
        DevicePresenter devicePresenter;
        GroupBox groupBoxControl, groupBoxParameters, groupBoxParametrsActualRead;
        Button buttonSTOP, buttonLEFT, buttonRIGHT, buttonZERO;
        Label labelAngele, labelAngeleUnit, labelSpeed, labelSpeedUnit, labelCurrent, labelCurrentUnit, labelAngeleActualRead, labelAngeleActualReadUnit, 
              labelSpeedActualRead, labelSpeedActualReadUnit, labelCurrentActualRead, labelCurrentActualReadUnit;
        TrackBar trackBarAngele, trackBarSpeed, trackBarCurrent;
        NumericUpDown numericUpDownAngele, numericUpDownSpeed, numericUpDownCurrent;
        TextBox textBoxAngeleActualRead, textBoxSpeedActualRead, textBoxCurrentActualRead;

        public TextBox TextBoxAngeleActualRead
        {
            get { return textBoxAngeleActualRead; }
        }
        public TextBox TextBoxSpeedActualRead
        {
            get { return textBoxSpeedActualRead; }
        }
        public TextBox TextBoxCurrentActualRead
        {
            get { return textBoxCurrentActualRead; }
        }
        public Button ButtonSTOP
        {
            get { return buttonSTOP; }
        }
        public Button ButtonLEFT
        {
            get { return buttonSTOP; }
        }
        public Button ButtonRIGHT
        {
            get { return buttonSTOP; }
        }
        public Button ButtonZERO
        {
            get { return buttonSTOP; }
        }

        public CreateNewElementOnForm(Form form, DevicePresenter devicePresenter)
        {
            this.form = form;
            this.devicePresenter = devicePresenter;
        }
        public void NewElementAdd()
        {
            // создаем groupBox
            groupBoxControl = new GroupBox();
            groupBoxControl.Text = "Управление";
            groupBoxControl.Location = new Point(12, 92);
            groupBoxControl.Size = new Size(537, 116);
            form.Controls.Add(groupBoxControl);

            // создаем кнопку внутри groupBox
            buttonSTOP = new Button();
            buttonSTOP.Text = "СТОП";
            buttonSTOP.Size = new Size(120, 70);
            buttonSTOP.Location = new Point(10, 30);
            buttonSTOP.BackColor = Color.Red;
            buttonSTOP.Click += new EventHandler(buttonSTOP_Click);
            groupBoxControl.Controls.Add(buttonSTOP);

            buttonLEFT = new Button();
            buttonLEFT.Text = "Пуск влево";
            buttonLEFT.Size = new Size(120, 70);
            buttonLEFT.Location = new Point(140, 30);
            buttonLEFT.Click += new EventHandler(buttonLEFT_Click);
            groupBoxControl.Controls.Add(buttonLEFT);

            buttonRIGHT = new Button();
            buttonRIGHT.Text = "Пуск вправо";
            buttonRIGHT.Size = new Size(120, 70);
            buttonRIGHT.Location = new Point(270, 30);
            buttonRIGHT.Click += new EventHandler(buttonRIGHT_Click);
            groupBoxControl.Controls.Add(buttonRIGHT);

            buttonZERO = new Button();
            buttonZERO.Text = "Задать ноль";
            buttonZERO.Size = new Size(120, 70);
            buttonZERO.Location = new Point(400, 30);
            buttonZERO.Click += new EventHandler(buttonZERO_Click);
            groupBoxControl.Controls.Add(buttonZERO);

            groupBoxParameters = new GroupBox();
            groupBoxParameters.Text = "Заданные параметры";
            groupBoxParameters.Location = new Point(12, 210);
            groupBoxParameters.Size = new Size(537, 170);
            form.Controls.Add(groupBoxParameters);

            labelAngele = new Label();
            labelAngele.Text = "Угол";
            labelAngele.Location = new Point(10, 40);
            groupBoxParameters.Controls.Add(labelAngele);

            labelAngeleUnit = new Label();
            labelAngeleUnit.Text = "град";
            labelAngeleUnit.Location = new Point(470, 40);
            labelAngeleUnit.Size = new Size(50, 15);
            groupBoxParameters.Controls.Add(labelAngeleUnit);

            trackBarAngele = new TrackBar();
            trackBarAngele.Minimum = -185;
            trackBarAngele.Maximum = 185;
            trackBarAngele.TickFrequency = 1;
            trackBarAngele.LargeChange = 500;
            trackBarAngele.Width = 200;
            trackBarAngele.Value = 0;
            trackBarAngele.Location = new Point(150, 40);
            trackBarAngele.Scroll += TrackBarAngele_Scroll;
            groupBoxParameters.Controls.Add(trackBarAngele);

            numericUpDownAngele = new NumericUpDown();
            numericUpDownAngele.Minimum = -185;
            numericUpDownAngele.Maximum = 185;
            numericUpDownAngele.Width = 80;
            numericUpDownAngele.Value = 0;
            numericUpDownAngele.Location = new Point(370, 40);
            numericUpDownAngele.DecimalPlaces = 1;
            numericUpDownAngele.ValueChanged += NumericUpDownAngele_ValueChanged;
            groupBoxParameters.Controls.Add(numericUpDownAngele);

            labelSpeed = new Label();
            labelSpeed.Text = "Скорость";
            labelSpeed.Location = new Point(10, 80);
            groupBoxParameters.Controls.Add(labelSpeed);

            labelSpeedUnit = new Label();
            labelSpeedUnit.Text = "об/мин";
            labelSpeedUnit.Location = new Point(470, 80);
            labelSpeedUnit.Size = new Size(50, 15);
            groupBoxParameters.Controls.Add(labelSpeedUnit);

            trackBarSpeed = new TrackBar();
            trackBarSpeed.Minimum = 0;
            trackBarSpeed.Maximum = 3000;
            trackBarSpeed.TickFrequency = 100;
            trackBarSpeed.LargeChange = 500;
            trackBarSpeed.Width = 200;
            trackBarSpeed.Value = 500;
            trackBarSpeed.Location = new Point(150, 80);
            trackBarSpeed.Scroll += TrackBarSpeed_Scroll;
            groupBoxParameters.Controls.Add(trackBarSpeed);

            numericUpDownSpeed = new NumericUpDown();
            numericUpDownSpeed.Minimum = 0;
            numericUpDownSpeed.Maximum = 3000;
            numericUpDownSpeed.Width = 80;
            numericUpDownSpeed.Value = 500;
            numericUpDownSpeed.Location = new Point(370, 80);
            numericUpDownSpeed.ValueChanged += NumericUpDownSpeed_ValueChanged;
            groupBoxParameters.Controls.Add(numericUpDownSpeed);

            labelCurrent = new Label();
            labelCurrent.Text = "Ток";
            labelCurrent.Location = new Point(10, 120);
            groupBoxParameters.Controls.Add(labelCurrent);

            labelCurrentUnit = new Label();
            labelCurrentUnit.Text = "мА";
            labelCurrentUnit.Location = new Point(470, 120);
            labelCurrentUnit.Size = new Size(50, 15);
            groupBoxParameters.Controls.Add(labelCurrentUnit);

            trackBarCurrent = new TrackBar();
            trackBarCurrent.Minimum = 0;
            trackBarCurrent.Maximum = 6000;
            trackBarCurrent.TickFrequency = 100;
            trackBarCurrent.LargeChange = 500;
            trackBarCurrent.Width = 200;
            trackBarCurrent.Value = 6000;
            trackBarCurrent.Location = new Point(150, 120);
            trackBarCurrent.Scroll += TrackBarCurrent_Scroll;
            groupBoxParameters.Controls.Add(trackBarCurrent);

            numericUpDownCurrent = new NumericUpDown();
            numericUpDownCurrent.Minimum = 0;
            numericUpDownCurrent.Maximum = 6000;
            numericUpDownCurrent.Width = 80;
            numericUpDownCurrent.Value = 6000;
            numericUpDownCurrent.Location = new Point(370, 120);
            numericUpDownCurrent.ValueChanged += NumericUpDownCurrent_ValueChanged;
            groupBoxParameters.Controls.Add(numericUpDownCurrent);

            // создаем groupBox
            groupBoxParametrsActualRead = new GroupBox();
            groupBoxParametrsActualRead.Text = "Текущие параметры";
            groupBoxParametrsActualRead.Location = new Point(12, 385);
            groupBoxParametrsActualRead.Size = new Size(537, 160);
            form.Controls.Add(groupBoxParametrsActualRead);

            //Элементы для вывода текущего значения угла
            labelAngeleActualRead = new Label();
            labelAngeleActualRead.Text = "Угол";
            labelAngeleActualRead.Location = new Point(10, 40);
            labelAngeleActualRead.Size = new Size(50, 15);
            groupBoxParametrsActualRead.Controls.Add(labelAngeleActualRead);

            textBoxAngeleActualRead = new TextBox();
            textBoxAngeleActualRead.Location = new Point(80, 40);
            textBoxAngeleActualRead.ReadOnly = true;
            groupBoxParametrsActualRead.Controls.Add(textBoxAngeleActualRead);

            labelAngeleActualReadUnit = new Label();
            labelAngeleActualReadUnit.Text = "град";
            labelAngeleActualReadUnit.Location = new Point(200, 40);
            labelAngeleActualReadUnit.Size = new Size(50, 15);
            groupBoxParametrsActualRead.Controls.Add(labelAngeleActualReadUnit);

            //Элементы для вывода текущего значения скорости
            labelSpeedActualRead = new Label();
            labelSpeedActualRead.Text = "Скорость";
            labelSpeedActualRead.Location = new Point(10, 80);
            labelSpeedActualRead.Size = new Size(50, 15);
            groupBoxParametrsActualRead.Controls.Add(labelSpeedActualRead);

            textBoxSpeedActualRead = new TextBox();
            textBoxSpeedActualRead.Location = new Point(80, 80);
            textBoxSpeedActualRead.ReadOnly = true;
            groupBoxParametrsActualRead.Controls.Add(textBoxSpeedActualRead);

            labelSpeedActualReadUnit = new Label();
            labelSpeedActualReadUnit.Text = "об/мин";
            labelSpeedActualReadUnit.Location = new Point(200, 80);
            labelSpeedActualReadUnit.Size = new Size(50, 15);
            groupBoxParametrsActualRead.Controls.Add(labelSpeedActualReadUnit);

            //Элементы для вывода текущего значения тока
            labelCurrentActualRead = new Label();
            labelCurrentActualRead.Text = "Ток";
            labelCurrentActualRead.Location = new Point(10, 120);
            labelCurrentActualRead.Size = new Size(50, 15);
            groupBoxParametrsActualRead.Controls.Add(labelCurrentActualRead);

            textBoxCurrentActualRead = new TextBox();
            textBoxCurrentActualRead.Location = new Point(80, 120);
            textBoxCurrentActualRead.ReadOnly = true;
            groupBoxParametrsActualRead.Controls.Add(textBoxCurrentActualRead);

            labelCurrentActualReadUnit = new Label();
            labelCurrentActualReadUnit.Text = "mA";
            labelCurrentActualReadUnit.Location = new Point(200, 120);
            labelCurrentActualReadUnit.Size = new Size(50, 15);
            groupBoxParametrsActualRead.Controls.Add(labelCurrentActualReadUnit);
        }
        public void DeleteElement()
        {
            ButtonSTOP.Click -= new EventHandler(buttonSTOP_Click);
            ButtonLEFT.Click -= new EventHandler(buttonLEFT_Click);
            ButtonRIGHT.Click -= new EventHandler(buttonRIGHT_Click);
            ButtonZERO.Click -= new EventHandler(buttonZERO_Click);
            form.Controls.Remove(groupBoxControl);
            form.Controls.Remove(groupBoxParameters);
            form.Controls.Remove(groupBoxParametrsActualRead);
        }

        public void buttonSTOP_Click(object sender, EventArgs e)
        {
            devicePresenter.PutData(8, 0x0);
        }
        public void buttonLEFT_Click(object sender, EventArgs e)
        {
            //_newConnect.PutData(14, -1);
            // _newConnect.PutData(8, 0xF1);
            devicePresenter.PutData(8, 0x111);

        }
        public void buttonRIGHT_Click(object sender, EventArgs e)
        {
            //_newConnect.PutData(14, 1);
            devicePresenter.PutData(8, 0x101);
        }
        public void buttonZERO_Click(object sender, EventArgs e)
        {
            devicePresenter.PutData(8, 0x121);
        }
        private void TrackBarAngele_Scroll(object sender, EventArgs e)
        {
            numericUpDownAngele.Value = trackBarAngele.Value;
            devicePresenter.PutData(9, trackBarAngele.Value * 10);
        }
        private void NumericUpDownAngele_ValueChanged(object sender, EventArgs e)
        {
            trackBarAngele.Value = (int)numericUpDownAngele.Value;
            devicePresenter.PutData(9, (int)numericUpDownAngele.Value * 10);
        }
        private void TrackBarSpeed_Scroll(object sender, EventArgs e)
        {
            numericUpDownSpeed.Value = trackBarSpeed.Value;
            devicePresenter.PutData(11, trackBarSpeed.Value);
        }
        private void NumericUpDownSpeed_ValueChanged(object sender, EventArgs e)
        {
            trackBarSpeed.Value = (int)numericUpDownSpeed.Value;
            devicePresenter.PutData(11, (int)numericUpDownSpeed.Value);
        }
        private void TrackBarCurrent_Scroll(object sender, EventArgs e)
        {
            numericUpDownCurrent.Value = trackBarCurrent.Value;
            devicePresenter.PutData(13, trackBarCurrent.Value);
        }
        private void NumericUpDownCurrent_ValueChanged(object sender, EventArgs e)
        {
            trackBarCurrent.Value = (int)numericUpDownCurrent.Value;
            devicePresenter.PutData(13, (int)numericUpDownCurrent.Value);
        }
    }
}
