namespace APU
{
    partial class MainFormView
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.nmrcAddrssDvs = new System.Windows.Forms.NumericUpDown();
            this.cmbBxExchngSpd = new System.Windows.Forms.ComboBox();
            this.cmbBxPrts = new System.Windows.Forms.ComboBox();
            this.bnConnection = new System.Windows.Forms.Button();
            this.bnUpdate = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.qToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.clearErrorListBoxToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.listBoxErrorMessage = new System.Windows.Forms.ListBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmrcAddrssDvs)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.nmrcAddrssDvs);
            this.groupBox1.Controls.Add(this.cmbBxExchngSpd);
            this.groupBox1.Controls.Add(this.cmbBxPrts);
            this.groupBox1.Controls.Add(this.bnConnection);
            this.groupBox1.Controls.Add(this.bnUpdate);
            this.groupBox1.Location = new System.Drawing.Point(12, 29);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox1.Size = new System.Drawing.Size(537, 58);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Подключение";
            // 
            // nmrcAddrssDvs
            // 
            this.nmrcAddrssDvs.Location = new System.Drawing.Point(447, 29);
            this.nmrcAddrssDvs.Margin = new System.Windows.Forms.Padding(2);
            this.nmrcAddrssDvs.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nmrcAddrssDvs.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nmrcAddrssDvs.Name = "nmrcAddrssDvs";
            this.nmrcAddrssDvs.Size = new System.Drawing.Size(82, 23);
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
            "9600",
            "14400",
            "19200",
            "38400",
            "56000",
            "57600",
            "115200"});
            this.cmbBxExchngSpd.Location = new System.Drawing.Point(327, 29);
            this.cmbBxExchngSpd.Margin = new System.Windows.Forms.Padding(2);
            this.cmbBxExchngSpd.Name = "cmbBxExchngSpd";
            this.cmbBxExchngSpd.Size = new System.Drawing.Size(107, 23);
            this.cmbBxExchngSpd.TabIndex = 3;
            // 
            // cmbBxPrts
            // 
            this.cmbBxPrts.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBxPrts.FormattingEnabled = true;
            this.cmbBxPrts.Location = new System.Drawing.Point(210, 29);
            this.cmbBxPrts.Margin = new System.Windows.Forms.Padding(2);
            this.cmbBxPrts.Name = "cmbBxPrts";
            this.cmbBxPrts.Size = new System.Drawing.Size(107, 23);
            this.cmbBxPrts.TabIndex = 2;
            this.cmbBxPrts.Tag = "";
            // 
            // bnConnection
            // 
            this.bnConnection.Location = new System.Drawing.Point(110, 29);
            this.bnConnection.Margin = new System.Windows.Forms.Padding(2);
            this.bnConnection.Name = "bnConnection";
            this.bnConnection.Size = new System.Drawing.Size(93, 25);
            this.bnConnection.TabIndex = 1;
            this.bnConnection.Text = "Подключить";
            this.bnConnection.UseVisualStyleBackColor = true;
            this.bnConnection.Click += new System.EventHandler(this.bnConnection_Click);
            // 
            // bnUpdate
            // 
            this.bnUpdate.Location = new System.Drawing.Point(12, 29);
            this.bnUpdate.Margin = new System.Windows.Forms.Padding(2);
            this.bnUpdate.Name = "bnUpdate";
            this.bnUpdate.Size = new System.Drawing.Size(88, 25);
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
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(4, 1, 0, 1);
            this.menuStrip1.Size = new System.Drawing.Size(559, 24);
            this.menuStrip1.TabIndex = 10;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.qToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(48, 22);
            this.fileToolStripMenuItem.Text = "Файл";
            // 
            // qToolStripMenuItem
            // 
            this.qToolStripMenuItem.Name = "qToolStripMenuItem";
            this.qToolStripMenuItem.Size = new System.Drawing.Size(120, 22);
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
            this.listBoxErrorMessage.ItemHeight = 15;
            this.listBoxErrorMessage.Location = new System.Drawing.Point(13, 550);
            this.listBoxErrorMessage.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.listBoxErrorMessage.Name = "listBoxErrorMessage";
            this.listBoxErrorMessage.Size = new System.Drawing.Size(536, 139);
            this.listBoxErrorMessage.TabIndex = 11;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(559, 701);
            this.Controls.Add(this.listBoxErrorMessage);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "MainForm";
            this.Text = "APU";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nmrcAddrssDvs)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
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
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem clearErrorListBoxToolStripMenuItem;
        private System.Windows.Forms.ListBox listBoxErrorMessage;
    }
}

