namespace APU
{
    partial class MainForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.nmrcAddrssDvs = new System.Windows.Forms.NumericUpDown();
            this.cmbBxExchngSpd = new System.Windows.Forms.ComboBox();
            this.cmbBxPrts = new System.Windows.Forms.ComboBox();
            this.bnConnection = new System.Windows.Forms.Button();
            this.bnUpdate = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.updateFirmwareToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.qToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.clearErrorListBoxToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.listBoxErrorMessage = new System.Windows.Forms.ListBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.bnChngSpeed = new System.Windows.Forms.Button();
            this.cmbBxExchngSpdInWork = new System.Windows.Forms.ComboBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.nmrcAddrssDvsInWork = new System.Windows.Forms.NumericUpDown();
            this.bnChngAddr = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.bnChngMystType = new System.Windows.Forms.Button();
            this.cmbBxMystType = new System.Windows.Forms.ComboBox();
            this.bnChngZeroPositionValue = new System.Windows.Forms.Button();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.txtBoxMastPosition = new System.Windows.Forms.TextBox();
            this.timerUpdateDate = new System.Windows.Forms.Timer(this.components);
            this.bnChngZeroPositionValueBreak = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmrcAddrssDvs)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmrcAddrssDvsInWork)).BeginInit();
            this.groupBox4.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.nmrcAddrssDvs);
            this.groupBox1.Controls.Add(this.cmbBxExchngSpd);
            this.groupBox1.Controls.Add(this.cmbBxPrts);
            this.groupBox1.Controls.Add(this.bnConnection);
            this.groupBox1.Controls.Add(this.bnUpdate);
            this.groupBox1.Location = new System.Drawing.Point(10, 25);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox1.Size = new System.Drawing.Size(460, 50);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Подключение";
            // 
            // nmrcAddrssDvs
            // 
            this.nmrcAddrssDvs.Location = new System.Drawing.Point(383, 25);
            this.nmrcAddrssDvs.Margin = new System.Windows.Forms.Padding(2);
            this.nmrcAddrssDvs.Maximum = new decimal(new int[] {
            247,
            0,
            0,
            0});
            this.nmrcAddrssDvs.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nmrcAddrssDvs.Name = "nmrcAddrssDvs";
            this.nmrcAddrssDvs.Size = new System.Drawing.Size(70, 20);
            this.nmrcAddrssDvs.TabIndex = 4;
            this.nmrcAddrssDvs.Tag = "";
            this.nmrcAddrssDvs.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // cmbBxExchngSpd
            // 
            this.cmbBxExchngSpd.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBxExchngSpd.FormattingEnabled = true;
            this.cmbBxExchngSpd.Items.AddRange(new object[] {
            "Автопоиск",
            "9600",
            "14400",
            "19200",
            "38400",
            "56000",
            "57600",
            "115200"});
            this.cmbBxExchngSpd.Location = new System.Drawing.Point(280, 25);
            this.cmbBxExchngSpd.Margin = new System.Windows.Forms.Padding(2);
            this.cmbBxExchngSpd.Name = "cmbBxExchngSpd";
            this.cmbBxExchngSpd.Size = new System.Drawing.Size(92, 21);
            this.cmbBxExchngSpd.TabIndex = 3;
            // 
            // cmbBxPrts
            // 
            this.cmbBxPrts.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBxPrts.FormattingEnabled = true;
            this.cmbBxPrts.Location = new System.Drawing.Point(180, 25);
            this.cmbBxPrts.Margin = new System.Windows.Forms.Padding(2);
            this.cmbBxPrts.Name = "cmbBxPrts";
            this.cmbBxPrts.Size = new System.Drawing.Size(92, 21);
            this.cmbBxPrts.TabIndex = 2;
            this.cmbBxPrts.Tag = "";
            // 
            // bnConnection
            // 
            this.bnConnection.Location = new System.Drawing.Point(94, 25);
            this.bnConnection.Margin = new System.Windows.Forms.Padding(2);
            this.bnConnection.Name = "bnConnection";
            this.bnConnection.Size = new System.Drawing.Size(77, 21);
            this.bnConnection.TabIndex = 1;
            this.bnConnection.Text = "Подключить";
            this.bnConnection.UseVisualStyleBackColor = true;
            this.bnConnection.Click += new System.EventHandler(this.bnConnection_Click);
            // 
            // bnUpdate
            // 
            this.bnUpdate.Location = new System.Drawing.Point(10, 25);
            this.bnUpdate.Margin = new System.Windows.Forms.Padding(2);
            this.bnUpdate.Name = "bnUpdate";
            this.bnUpdate.Size = new System.Drawing.Size(75, 21);
            this.bnUpdate.TabIndex = 0;
            this.bnUpdate.Text = "Обновить";
            this.bnUpdate.UseVisualStyleBackColor = true;
            this.bnUpdate.Click += new System.EventHandler(this.bnUpdate_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.toolStripMenuItem1,
            this.clearErrorListBoxToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(3, 1, 0, 1);
            this.menuStrip1.Size = new System.Drawing.Size(604, 24);
            this.menuStrip1.TabIndex = 9;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.updateFirmwareToolStripMenuItem,
            this.qToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(48, 22);
            this.fileToolStripMenuItem.Text = "Файл";
            // 
            // updateFirmwareToolStripMenuItem
            // 
            this.updateFirmwareToolStripMenuItem.Name = "updateFirmwareToolStripMenuItem";
            this.updateFirmwareToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
            this.updateFirmwareToolStripMenuItem.Text = "Обновить прошивку";
            this.updateFirmwareToolStripMenuItem.Click += new System.EventHandler(this.updateFirmwareToolStripMenuItem_Click);
            // 
            // qToolStripMenuItem
            // 
            this.qToolStripMenuItem.Name = "qToolStripMenuItem";
            this.qToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
            this.qToolStripMenuItem.Text = "Закрыть";
            this.qToolStripMenuItem.Click += new System.EventHandler(this.qToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(22, 22);
            this.toolStripMenuItem1.Text = "|";
            // 
            // clearErrorListBoxToolStripMenuItem
            // 
            this.clearErrorListBoxToolStripMenuItem.Name = "clearErrorListBoxToolStripMenuItem";
            this.clearErrorListBoxToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
            this.clearErrorListBoxToolStripMenuItem.Text = "Очистка окна сообщений";
            this.clearErrorListBoxToolStripMenuItem.Click += new System.EventHandler(this.clearErrorListBoxToolStripMenuItem_Click);
            // 
            // listBoxErrorMessage
            // 
            this.listBoxErrorMessage.FormattingEnabled = true;
            this.listBoxErrorMessage.Location = new System.Drawing.Point(10, 221);
            this.listBoxErrorMessage.Name = "listBoxErrorMessage";
            this.listBoxErrorMessage.Size = new System.Drawing.Size(586, 186);
            this.listBoxErrorMessage.TabIndex = 10;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.bnChngSpeed);
            this.groupBox2.Controls.Add(this.cmbBxExchngSpdInWork);
            this.groupBox2.Location = new System.Drawing.Point(150, 90);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(115, 125);
            this.groupBox2.TabIndex = 12;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Скорость обмена";
            // 
            // bnChngSpeed
            // 
            this.bnChngSpeed.Location = new System.Drawing.Point(6, 88);
            this.bnChngSpeed.Name = "bnChngSpeed";
            this.bnChngSpeed.Size = new System.Drawing.Size(100, 23);
            this.bnChngSpeed.TabIndex = 12;
            this.bnChngSpeed.Text = "Изменить";
            this.bnChngSpeed.UseVisualStyleBackColor = true;
            this.bnChngSpeed.Click += new System.EventHandler(this.bnChngSpeed_Click);
            // 
            // cmbBxExchngSpdInWork
            // 
            this.cmbBxExchngSpdInWork.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBxExchngSpdInWork.FormattingEnabled = true;
            this.cmbBxExchngSpdInWork.Items.AddRange(new object[] {
            "9600",
            "14400",
            "19200",
            "38400",
            "56000",
            "57600",
            "115200"});
            this.cmbBxExchngSpdInWork.Location = new System.Drawing.Point(6, 26);
            this.cmbBxExchngSpdInWork.Margin = new System.Windows.Forms.Padding(2);
            this.cmbBxExchngSpdInWork.Name = "cmbBxExchngSpdInWork";
            this.cmbBxExchngSpdInWork.Size = new System.Drawing.Size(100, 21);
            this.cmbBxExchngSpdInWork.TabIndex = 5;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.nmrcAddrssDvsInWork);
            this.groupBox3.Controls.Add(this.bnChngAddr);
            this.groupBox3.Location = new System.Drawing.Point(300, 90);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(115, 125);
            this.groupBox3.TabIndex = 13;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Адрес устройства";
            // 
            // nmrcAddrssDvsInWork
            // 
            this.nmrcAddrssDvsInWork.Location = new System.Drawing.Point(6, 27);
            this.nmrcAddrssDvsInWork.Margin = new System.Windows.Forms.Padding(2);
            this.nmrcAddrssDvsInWork.Maximum = new decimal(new int[] {
            247,
            0,
            0,
            0});
            this.nmrcAddrssDvsInWork.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nmrcAddrssDvsInWork.Name = "nmrcAddrssDvsInWork";
            this.nmrcAddrssDvsInWork.Size = new System.Drawing.Size(100, 20);
            this.nmrcAddrssDvsInWork.TabIndex = 15;
            this.nmrcAddrssDvsInWork.Tag = "";
            this.nmrcAddrssDvsInWork.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // bnChngAddr
            // 
            this.bnChngAddr.Location = new System.Drawing.Point(6, 88);
            this.bnChngAddr.Name = "bnChngAddr";
            this.bnChngAddr.Size = new System.Drawing.Size(100, 23);
            this.bnChngAddr.TabIndex = 14;
            this.bnChngAddr.Text = "Изменить";
            this.bnChngAddr.UseVisualStyleBackColor = true;
            this.bnChngAddr.Click += new System.EventHandler(this.bnChngAddr_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.bnChngMystType);
            this.groupBox4.Controls.Add(this.cmbBxMystType);
            this.groupBox4.Location = new System.Drawing.Point(10, 90);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(115, 125);
            this.groupBox4.TabIndex = 14;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Тип мачты";
            // 
            // bnChngMystType
            // 
            this.bnChngMystType.Location = new System.Drawing.Point(6, 88);
            this.bnChngMystType.Name = "bnChngMystType";
            this.bnChngMystType.Size = new System.Drawing.Size(100, 23);
            this.bnChngMystType.TabIndex = 13;
            this.bnChngMystType.Text = "Изменить";
            this.bnChngMystType.UseVisualStyleBackColor = true;
            this.bnChngMystType.Click += new System.EventHandler(this.bnChngMystType_Click);
            // 
            // cmbBxMystType
            // 
            this.cmbBxMystType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBxMystType.FormattingEnabled = true;
            this.cmbBxMystType.Items.AddRange(new object[] {
            "ПМ-1",
            "ПМ-2",
            "ПМ-3"});
            this.cmbBxMystType.Location = new System.Drawing.Point(6, 26);
            this.cmbBxMystType.Margin = new System.Windows.Forms.Padding(2);
            this.cmbBxMystType.Name = "cmbBxMystType";
            this.cmbBxMystType.Size = new System.Drawing.Size(100, 21);
            this.cmbBxMystType.TabIndex = 12;
            // 
            // bnChngZeroPositionValue
            // 
            this.bnChngZeroPositionValue.Location = new System.Drawing.Point(450, 90);
            this.bnChngZeroPositionValue.Name = "bnChngZeroPositionValue";
            this.bnChngZeroPositionValue.Size = new System.Drawing.Size(115, 50);
            this.bnChngZeroPositionValue.TabIndex = 14;
            this.bnChngZeroPositionValue.Text = "Задать нулевое положение";
            this.bnChngZeroPositionValue.UseVisualStyleBackColor = true;
            this.bnChngZeroPositionValue.Click += new System.EventHandler(this.bnChngZeroPositionValue_Click);
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.txtBoxMastPosition);
            this.groupBox6.Location = new System.Drawing.Point(480, 25);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(116, 50);
            this.groupBox6.TabIndex = 16;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Положение мачты";
            // 
            // txtBoxMastPosition
            // 
            this.txtBoxMastPosition.Location = new System.Drawing.Point(6, 25);
            this.txtBoxMastPosition.Name = "txtBoxMastPosition";
            this.txtBoxMastPosition.Size = new System.Drawing.Size(100, 20);
            this.txtBoxMastPosition.TabIndex = 11;
            // 
            // timerUpdateDate
            // 
            this.timerUpdateDate.Tick += new System.EventHandler(this.timerUpdateDate_Tick);
            // 
            // bnChngZeroPositionValueBreak
            // 
            this.bnChngZeroPositionValueBreak.Location = new System.Drawing.Point(450, 164);
            this.bnChngZeroPositionValueBreak.Name = "bnChngZeroPositionValueBreak";
            this.bnChngZeroPositionValueBreak.Size = new System.Drawing.Size(115, 50);
            this.bnChngZeroPositionValueBreak.TabIndex = 16;
            this.bnChngZeroPositionValueBreak.Text = "Сбросить нулевое положение";
            this.bnChngZeroPositionValueBreak.UseVisualStyleBackColor = true;
            this.bnChngZeroPositionValueBreak.Click += new System.EventHandler(this.bnChngZeroPositionValueBreak_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(604, 411);
            this.Controls.Add(this.bnChngZeroPositionValue);
            this.Controls.Add(this.bnChngZeroPositionValueBreak);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.listBoxErrorMessage);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "MainForm";
            this.Text = "APU V1.0";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nmrcAddrssDvs)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nmrcAddrssDvsInWork)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        internal System.Windows.Forms.NumericUpDown nmrcAddrssDvs;
        private System.Windows.Forms.ComboBox cmbBxExchngSpd;
        internal System.Windows.Forms.ComboBox cmbBxPrts;
        private System.Windows.Forms.Button bnConnection;
        private System.Windows.Forms.Button bnUpdate;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem qToolStripMenuItem;
        private System.Windows.Forms.ListBox listBoxErrorMessage;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ComboBox cmbBxExchngSpdInWork;
        private System.Windows.Forms.Button bnChngSpeed;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem clearErrorListBoxToolStripMenuItem;
        private System.Windows.Forms.Button bnChngAddr;
        internal System.Windows.Forms.NumericUpDown nmrcAddrssDvsInWork;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.ComboBox cmbBxMystType;
        private System.Windows.Forms.Button bnChngMystType;
        private System.Windows.Forms.Button bnChngZeroPositionValue;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.TextBox txtBoxMastPosition;
        private System.Windows.Forms.Timer timerUpdateDate;
        private System.Windows.Forms.Button bnChngZeroPositionValueBreak;
        private System.Windows.Forms.ToolStripMenuItem updateFirmwareToolStripMenuItem;
    }
}

