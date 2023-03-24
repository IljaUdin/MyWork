namespace ReaderPorts
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.qToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.clearErrorListBoxToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MainTable = new System.Windows.Forms.DataGridView();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.chartPort = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.timerUpdateDate = new System.Windows.Forms.Timer(this.components);
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.cmbBxTypePrtcls = new System.Windows.Forms.ComboBox();
            this.nmrcBegin = new System.Windows.Forms.NumericUpDown();
            this.nmrcNmbrСlls = new System.Windows.Forms.NumericUpDown();
            this.nmrcAddrssDvs = new System.Windows.Forms.NumericUpDown();
            this.cmbBxExchngSpd = new System.Windows.Forms.ComboBox();
            this.cmbBxPrts = new System.Windows.Forms.ComboBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.bnLogger = new System.Windows.Forms.Button();
            this.bnConnection = new System.Windows.Forms.Button();
            this.bnUpdate = new System.Windows.Forms.Button();
            this.listBoxErrorMessage = new System.Windows.Forms.ListBox();
            this.Hex = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Dec = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Range = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Graf = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Port = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Position = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Record = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Descriptor = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MainTable)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartPort)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmrcBegin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmrcNmbrСlls)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmrcAddrssDvs)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
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
            this.menuStrip1.Size = new System.Drawing.Size(1234, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.qToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 22);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // qToolStripMenuItem
            // 
            this.qToolStripMenuItem.Name = "qToolStripMenuItem";
            this.qToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.qToolStripMenuItem.Text = "Close";
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
            this.clearErrorListBoxToolStripMenuItem.Size = new System.Drawing.Size(109, 22);
            this.clearErrorListBoxToolStripMenuItem.Text = "ClearErrorListBox";
            this.clearErrorListBoxToolStripMenuItem.Click += new System.EventHandler(this.clearErrorListBoxToolStripMenuItem_Click_1);
            // 
            // MainTable
            // 
            this.MainTable.AllowUserToAddRows = false;
            this.MainTable.AllowUserToDeleteRows = false;
            this.MainTable.AllowUserToResizeColumns = false;
            this.MainTable.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.MainTable.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.MainTable.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MainTable.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.MainTable.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Sunken;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.ActiveCaption;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.MainTable.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.MainTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.MainTable.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Hex,
            this.Dec,
            this.Range,
            this.Graf,
            this.Port,
            this.Position,
            this.Record,
            this.Descriptor});
            this.MainTable.Cursor = System.Windows.Forms.Cursors.Default;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.MainTable.DefaultCellStyle = dataGridViewCellStyle3;
            this.MainTable.GridColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.MainTable.Location = new System.Drawing.Point(8, 16);
            this.MainTable.Margin = new System.Windows.Forms.Padding(2);
            this.MainTable.Name = "MainTable";
            this.MainTable.RowHeadersWidth = 50;
            this.MainTable.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.White;
            this.MainTable.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.MainTable.RowTemplate.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.MainTable.RowTemplate.Height = 20;
            this.MainTable.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.MainTable.ShowCellToolTips = false;
            this.MainTable.Size = new System.Drawing.Size(680, 600);
            this.MainTable.TabIndex = 3;
            this.MainTable.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.MainTable_CellContentClick);
            this.MainTable.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.MainTable_CellContentDoubleClick);
            this.MainTable.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.MainTable_CellEndEdit);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.chartPort);
            this.groupBox2.Controls.Add(this.MainTable);
            this.groupBox2.Location = new System.Drawing.Point(7, 15);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox2.Size = new System.Drawing.Size(1208, 619);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Data memory";
            // 
            // chartPort
            // 
            chartArea1.Name = "ChartArea1";
            this.chartPort.ChartAreas.Add(chartArea1);
            legend1.Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Bottom;
            legend1.Name = "Legend1";
            this.chartPort.Legends.Add(legend1);
            this.chartPort.Location = new System.Drawing.Point(693, 14);
            this.chartPort.Name = "chartPort";
            this.chartPort.Size = new System.Drawing.Size(510, 600);
            this.chartPort.TabIndex = 4;
            // 
            // timerUpdateDate
            // 
            this.timerUpdateDate.Tick += new System.EventHandler(this.timerUpdateDate_Tick);
            // 
            // cmbBxTypePrtcls
            // 
            this.cmbBxTypePrtcls.FormattingEnabled = true;
            this.cmbBxTypePrtcls.Items.AddRange(new object[] {
            "ModBus",
            "SLIP"});
            this.cmbBxTypePrtcls.Location = new System.Drawing.Point(280, 25);
            this.cmbBxTypePrtcls.Margin = new System.Windows.Forms.Padding(2);
            this.cmbBxTypePrtcls.Name = "cmbBxTypePrtcls";
            this.cmbBxTypePrtcls.Size = new System.Drawing.Size(119, 21);
            this.cmbBxTypePrtcls.TabIndex = 6;
            this.cmbBxTypePrtcls.Tag = "";
            this.cmbBxTypePrtcls.Text = "ModBus";
            this.toolTip1.SetToolTip(this.cmbBxTypePrtcls, "Ports");
            // 
            // nmrcBegin
            // 
            this.nmrcBegin.Location = new System.Drawing.Point(597, 25);
            this.nmrcBegin.Margin = new System.Windows.Forms.Padding(2);
            this.nmrcBegin.Name = "nmrcBegin";
            this.nmrcBegin.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.nmrcBegin.Size = new System.Drawing.Size(70, 20);
            this.nmrcBegin.TabIndex = 6;
            this.toolTip1.SetToolTip(this.nmrcBegin, "Begin");
            // 
            // nmrcNmbrСlls
            // 
            this.nmrcNmbrСlls.Location = new System.Drawing.Point(677, 25);
            this.nmrcNmbrСlls.Margin = new System.Windows.Forms.Padding(2);
            this.nmrcNmbrСlls.Maximum = new decimal(new int[] {
            120,
            0,
            0,
            0});
            this.nmrcNmbrСlls.Name = "nmrcNmbrСlls";
            this.nmrcNmbrСlls.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.nmrcNmbrСlls.Size = new System.Drawing.Size(70, 20);
            this.nmrcNmbrСlls.TabIndex = 5;
            this.toolTip1.SetToolTip(this.nmrcNmbrСlls, "Number of Columns");
            this.nmrcNmbrСlls.Value = new decimal(new int[] {
            32,
            0,
            0,
            0});
            this.nmrcNmbrСlls.ValueChanged += new System.EventHandler(this.nmrcNmbrСlls_ValueChanged);
            // 
            // nmrcAddrssDvs
            // 
            this.nmrcAddrssDvs.Location = new System.Drawing.Point(514, 25);
            this.nmrcAddrssDvs.Margin = new System.Windows.Forms.Padding(2);
            this.nmrcAddrssDvs.Name = "nmrcAddrssDvs";
            this.nmrcAddrssDvs.Size = new System.Drawing.Size(70, 20);
            this.nmrcAddrssDvs.TabIndex = 4;
            this.nmrcAddrssDvs.Tag = "";
            this.toolTip1.SetToolTip(this.nmrcAddrssDvs, "Device Address");
            this.nmrcAddrssDvs.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // cmbBxExchngSpd
            // 
            this.cmbBxExchngSpd.FormattingEnabled = true;
            this.cmbBxExchngSpd.Items.AddRange(new object[] {
            "1200",
            "2400",
            "4800",
            "9600",
            "14400",
            "19200",
            "38400",
            "57600",
            "115200"});
            this.cmbBxExchngSpd.Location = new System.Drawing.Point(410, 25);
            this.cmbBxExchngSpd.Margin = new System.Windows.Forms.Padding(2);
            this.cmbBxExchngSpd.Name = "cmbBxExchngSpd";
            this.cmbBxExchngSpd.Size = new System.Drawing.Size(92, 21);
            this.cmbBxExchngSpd.TabIndex = 3;
            this.cmbBxExchngSpd.Text = "115200";
            this.toolTip1.SetToolTip(this.cmbBxExchngSpd, "Exchange speed");
            // 
            // cmbBxPrts
            // 
            this.cmbBxPrts.FormattingEnabled = true;
            this.cmbBxPrts.Location = new System.Drawing.Point(180, 25);
            this.cmbBxPrts.Margin = new System.Windows.Forms.Padding(2);
            this.cmbBxPrts.Name = "cmbBxPrts";
            this.cmbBxPrts.Size = new System.Drawing.Size(92, 21);
            this.cmbBxPrts.TabIndex = 2;
            this.cmbBxPrts.Tag = "";
            this.toolTip1.SetToolTip(this.cmbBxPrts, "Ports");
            this.cmbBxPrts.SelectionChangeCommitted += new System.EventHandler(this.cmbBxPrts_SelectionChangeCommitted);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Location = new System.Drawing.Point(0, 91);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1230, 665);
            this.tabControl1.TabIndex = 7;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tabPage1.Controls.Add(this.groupBox2);
            this.tabPage1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1222, 639);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "MainPage";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cmbBxTypePrtcls);
            this.groupBox1.Controls.Add(this.nmrcBegin);
            this.groupBox1.Controls.Add(this.bnLogger);
            this.groupBox1.Controls.Add(this.nmrcNmbrСlls);
            this.groupBox1.Controls.Add(this.nmrcAddrssDvs);
            this.groupBox1.Controls.Add(this.cmbBxExchngSpd);
            this.groupBox1.Controls.Add(this.cmbBxPrts);
            this.groupBox1.Controls.Add(this.bnConnection);
            this.groupBox1.Controls.Add(this.bnUpdate);
            this.groupBox1.Location = new System.Drawing.Point(11, 26);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox1.Size = new System.Drawing.Size(840, 50);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Connection";
            // 
            // bnLogger
            // 
            this.bnLogger.Location = new System.Drawing.Point(757, 25);
            this.bnLogger.Margin = new System.Windows.Forms.Padding(2);
            this.bnLogger.Name = "bnLogger";
            this.bnLogger.Size = new System.Drawing.Size(75, 21);
            this.bnLogger.TabIndex = 2;
            this.bnLogger.Text = "Log On";
            this.bnLogger.UseVisualStyleBackColor = true;
            this.bnLogger.Click += new System.EventHandler(this.bnLogger_Click);
            // 
            // bnConnection
            // 
            this.bnConnection.Location = new System.Drawing.Point(96, 25);
            this.bnConnection.Margin = new System.Windows.Forms.Padding(2);
            this.bnConnection.Name = "bnConnection";
            this.bnConnection.Size = new System.Drawing.Size(75, 21);
            this.bnConnection.TabIndex = 1;
            this.bnConnection.Text = "Connect";
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
            this.bnUpdate.Text = "Update";
            this.bnUpdate.UseVisualStyleBackColor = true;
            this.bnUpdate.Click += new System.EventHandler(this.bnUpdate_Click);
            // 
            // listBoxErrorMessage
            // 
            this.listBoxErrorMessage.FormattingEnabled = true;
            this.listBoxErrorMessage.Location = new System.Drawing.Point(856, 29);
            this.listBoxErrorMessage.Name = "listBoxErrorMessage";
            this.listBoxErrorMessage.Size = new System.Drawing.Size(370, 69);
            this.listBoxErrorMessage.TabIndex = 8;
            // 
            // Hex
            // 
            this.Hex.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Hex.HeaderText = "Hex";
            this.Hex.MinimumWidth = 10;
            this.Hex.Name = "Hex";
            this.Hex.ReadOnly = true;
            this.Hex.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Hex.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Hex.Width = 60;
            // 
            // Dec
            // 
            this.Dec.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Dec.HeaderText = "Dec";
            this.Dec.MinimumWidth = 10;
            this.Dec.Name = "Dec";
            this.Dec.ReadOnly = true;
            this.Dec.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Dec.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Dec.Width = 60;
            // 
            // Range
            // 
            this.Range.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.Range.HeaderText = "Range";
            this.Range.MinimumWidth = 10;
            this.Range.Name = "Range";
            this.Range.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // Graf
            // 
            this.Graf.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.Graf.HeaderText = "Graf";
            this.Graf.MinimumWidth = 10;
            this.Graf.Name = "Graf";
            this.Graf.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // Port
            // 
            this.Port.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Port.HeaderText = "Port";
            this.Port.MinimumWidth = 10;
            this.Port.Name = "Port";
            this.Port.ReadOnly = true;
            this.Port.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Port.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Port.Width = 60;
            // 
            // Position
            // 
            this.Position.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Position.HeaderText = "Position";
            this.Position.MinimumWidth = 10;
            this.Position.Name = "Position";
            this.Position.ReadOnly = true;
            this.Position.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Position.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Position.Width = 60;
            // 
            // Record
            // 
            this.Record.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Record.HeaderText = "Record";
            this.Record.MinimumWidth = 10;
            this.Record.Name = "Record";
            this.Record.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Record.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Record.Width = 75;
            // 
            // Descriptor
            // 
            this.Descriptor.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Descriptor.HeaderText = "Descriptor";
            this.Descriptor.MinimumWidth = 10;
            this.Descriptor.Name = "Descriptor";
            this.Descriptor.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Descriptor.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Descriptor.Width = 200;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1234, 768);
            this.Controls.Add(this.listBoxErrorMessage);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Ports V1.0";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MainTable)).EndInit();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chartPort)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmrcBegin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmrcNmbrСlls)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmrcAddrssDvs)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem qToolStripMenuItem;
        private System.Windows.Forms.DataGridView MainTable;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Timer timerUpdateDate;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.GroupBox groupBox1;
        internal System.Windows.Forms.ComboBox cmbBxTypePrtcls;
        internal System.Windows.Forms.NumericUpDown nmrcBegin;
        private System.Windows.Forms.Button bnLogger;
        internal System.Windows.Forms.NumericUpDown nmrcNmbrСlls;
        internal System.Windows.Forms.NumericUpDown nmrcAddrssDvs;
        private System.Windows.Forms.ComboBox cmbBxExchngSpd;
        internal System.Windows.Forms.ComboBox cmbBxPrts;
        private System.Windows.Forms.Button bnConnection;
        private System.Windows.Forms.Button bnUpdate;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartPort;
        private System.Windows.Forms.ListBox listBoxErrorMessage;
        private System.Windows.Forms.ToolStripMenuItem clearErrorListBoxToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Hex;
        private System.Windows.Forms.DataGridViewTextBoxColumn Dec;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Range;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Graf;
        private System.Windows.Forms.DataGridViewTextBoxColumn Port;
        private System.Windows.Forms.DataGridViewTextBoxColumn Position;
        private System.Windows.Forms.DataGridViewTextBoxColumn Record;
        private System.Windows.Forms.DataGridViewTextBoxColumn Descriptor;
    }
}

