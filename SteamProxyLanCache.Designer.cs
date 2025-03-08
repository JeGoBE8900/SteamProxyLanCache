namespace SteamProxyLanCache
{
    partial class SteamProxyLanCache
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SteamProxyLanCache));
            btnRun = new Button();
            label1 = new Label();
            label2 = new Label();
            txtLocalCache = new TextBox();
            lblStatus = new Label();
            folderBrowserDialog1 = new FolderBrowserDialog();
            btnCacheFolderBrowser = new Button();
            cboSteamContent = new ComboBox();
            chbLocalCacheUse = new CheckBox();
            label5 = new Label();
            label6 = new Label();
            txtDepotFilter = new TextBox();
            nfTray = new NotifyIcon(components);
            tabControl1 = new TabControl();
            tabPage1 = new TabPage();
            groupBox2 = new GroupBox();
            lvDiskSpace = new ListView();
            columnHeader4 = new ColumnHeader();
            columnHeader9 = new ColumnHeader();
            columnHeader6 = new ColumnHeader();
            columnHeader5 = new ColumnHeader();
            columnHeader7 = new ColumnHeader();
            columnHeader8 = new ColumnHeader();
            groupBox1 = new GroupBox();
            lblDnsStatus = new Label();
            tabPage2 = new TabPage();
            btnSave = new Button();
            label4 = new Label();
            nudKeepUnusedDays = new NumericUpDown();
            btnSaveSettings = new Button();
            label3 = new Label();
            chbAutoStart = new CheckBox();
            tabPage3 = new TabPage();
            lvLog = new ListView();
            columnHeader1 = new ColumnHeader();
            columnHeader2 = new ColumnHeader();
            columnHeader3 = new ColumnHeader();
            tabPage4 = new TabPage();
            txtInfo = new TextBox();
            tabControl1.SuspendLayout();
            tabPage1.SuspendLayout();
            groupBox2.SuspendLayout();
            groupBox1.SuspendLayout();
            tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudKeepUnusedDays).BeginInit();
            tabPage3.SuspendLayout();
            tabPage4.SuspendLayout();
            SuspendLayout();
            // 
            // btnRun
            // 
            btnRun.Location = new Point(19, 31);
            btnRun.Name = "btnRun";
            btnRun.Size = new Size(276, 23);
            btnRun.TabIndex = 0;
            btnRun.Text = "Start";
            btnRun.UseVisualStyleBackColor = true;
            btnRun.Click += btnRun_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(6, 16);
            label1.Name = "label1";
            label1.Size = new Size(77, 15);
            label1.TabIndex = 3;
            label1.Text = "Steam cache:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(6, 45);
            label2.Name = "label2";
            label2.Size = new Size(72, 15);
            label2.TabIndex = 5;
            label2.Text = "Local cache:";
            // 
            // txtLocalCache
            // 
            txtLocalCache.Enabled = false;
            txtLocalCache.Location = new Point(137, 42);
            txtLocalCache.Name = "txtLocalCache";
            txtLocalCache.ReadOnly = true;
            txtLocalCache.Size = new Size(517, 23);
            txtLocalCache.TabIndex = 4;
            // 
            // lblStatus
            // 
            lblStatus.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lblStatus.AutoSize = true;
            lblStatus.Location = new Point(138, 57);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(38, 15);
            lblStatus.TabIndex = 7;
            lblStatus.Text = "status";
            // 
            // btnCacheFolderBrowser
            // 
            btnCacheFolderBrowser.Location = new Point(660, 45);
            btnCacheFolderBrowser.Name = "btnCacheFolderBrowser";
            btnCacheFolderBrowser.Size = new Size(39, 23);
            btnCacheFolderBrowser.TabIndex = 10;
            btnCacheFolderBrowser.Text = "...";
            btnCacheFolderBrowser.UseVisualStyleBackColor = true;
            btnCacheFolderBrowser.Click += button1_Click;
            // 
            // cboSteamContent
            // 
            cboSteamContent.DropDownStyle = ComboBoxStyle.DropDownList;
            cboSteamContent.FormattingEnabled = true;
            cboSteamContent.Location = new Point(137, 13);
            cboSteamContent.Name = "cboSteamContent";
            cboSteamContent.Size = new Size(562, 23);
            cboSteamContent.TabIndex = 11;
            // 
            // chbLocalCacheUse
            // 
            chbLocalCacheUse.AutoSize = true;
            chbLocalCacheUse.Checked = true;
            chbLocalCacheUse.CheckState = CheckState.Checked;
            chbLocalCacheUse.Location = new Point(137, 80);
            chbLocalCacheUse.Name = "chbLocalCacheUse";
            chbLocalCacheUse.Size = new Size(15, 14);
            chbLocalCacheUse.TabIndex = 12;
            chbLocalCacheUse.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(7, 79);
            label5.Name = "label5";
            label5.Size = new Size(63, 15);
            label5.TabIndex = 13;
            label5.Text = "Use cache:";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(8, 109);
            label6.Name = "label6";
            label6.Size = new Size(47, 15);
            label6.TabIndex = 14;
            label6.Text = "Depots:";
            // 
            // txtDepotFilter
            // 
            txtDepotFilter.Location = new Point(137, 106);
            txtDepotFilter.MaxLength = 999;
            txtDepotFilter.Name = "txtDepotFilter";
            txtDepotFilter.Size = new Size(562, 23);
            txtDepotFilter.TabIndex = 15;
            // 
            // nfTray
            // 
            nfTray.BalloonTipText = "Steam Proxy LAN Cache";
            nfTray.BalloonTipTitle = "Steam Proxy LAN Cache";
            nfTray.Icon = (Icon)resources.GetObject("nfTray.Icon");
            nfTray.Text = "Steam Proxy LAN Cache";
            nfTray.MouseClick += nfTray_MouseClick;
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Controls.Add(tabPage2);
            tabControl1.Controls.Add(tabPage3);
            tabControl1.Controls.Add(tabPage4);
            tabControl1.Dock = DockStyle.Fill;
            tabControl1.Location = new Point(0, 0);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(1102, 600);
            tabControl1.TabIndex = 16;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(groupBox2);
            tabPage1.Controls.Add(groupBox1);
            tabPage1.Location = new Point(4, 24);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(1094, 572);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "     Monitor     ";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(lvDiskSpace);
            groupBox2.Location = new Point(6, 98);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(1088, 168);
            groupBox2.TabIndex = 9;
            groupBox2.TabStop = false;
            groupBox2.Text = "   Disk space   ";
            // 
            // lvDiskSpace
            // 
            lvDiskSpace.Columns.AddRange(new ColumnHeader[] { columnHeader4, columnHeader9, columnHeader6, columnHeader5, columnHeader7, columnHeader8 });
            lvDiskSpace.Dock = DockStyle.Fill;
            lvDiskSpace.FullRowSelect = true;
            lvDiskSpace.GridLines = true;
            lvDiskSpace.Location = new Point(3, 19);
            lvDiskSpace.Name = "lvDiskSpace";
            lvDiskSpace.Size = new Size(1082, 146);
            lvDiskSpace.TabIndex = 0;
            lvDiskSpace.UseCompatibleStateImageBehavior = false;
            lvDiskSpace.View = View.Details;
            // 
            // columnHeader4
            // 
            columnHeader4.Text = "Disk";
            // 
            // columnHeader9
            // 
            columnHeader9.Text = "Name";
            columnHeader9.Width = 150;
            // 
            // columnHeader6
            // 
            columnHeader6.Text = "Max space";
            columnHeader6.Width = 100;
            // 
            // columnHeader5
            // 
            columnHeader5.Text = "Current used";
            columnHeader5.Width = 100;
            // 
            // columnHeader7
            // 
            columnHeader7.Text = "Percentage";
            columnHeader7.Width = 100;
            // 
            // columnHeader8
            // 
            columnHeader8.Text = "Cache folder";
            columnHeader8.Width = 450;
            // 
            // groupBox1
            // 
            groupBox1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            groupBox1.Controls.Add(lblDnsStatus);
            groupBox1.Controls.Add(btnRun);
            groupBox1.Controls.Add(lblStatus);
            groupBox1.Location = new Point(6, 6);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(1085, 86);
            groupBox1.TabIndex = 8;
            groupBox1.TabStop = false;
            groupBox1.Text = "   httpListener : 80   ";
            // 
            // lblDnsStatus
            // 
            lblDnsStatus.AutoSize = true;
            lblDnsStatus.Location = new Point(587, 31);
            lblDnsStatus.Name = "lblDnsStatus";
            lblDnsStatus.Size = new Size(58, 15);
            lblDnsStatus.TabIndex = 8;
            lblDnsStatus.Text = "dnsStatus";
            // 
            // tabPage2
            // 
            tabPage2.Controls.Add(btnSave);
            tabPage2.Controls.Add(label4);
            tabPage2.Controls.Add(nudKeepUnusedDays);
            tabPage2.Controls.Add(btnSaveSettings);
            tabPage2.Controls.Add(label3);
            tabPage2.Controls.Add(chbAutoStart);
            tabPage2.Controls.Add(label1);
            tabPage2.Controls.Add(txtDepotFilter);
            tabPage2.Controls.Add(txtLocalCache);
            tabPage2.Controls.Add(label6);
            tabPage2.Controls.Add(label2);
            tabPage2.Controls.Add(label5);
            tabPage2.Controls.Add(btnCacheFolderBrowser);
            tabPage2.Controls.Add(chbLocalCacheUse);
            tabPage2.Controls.Add(cboSteamContent);
            tabPage2.Location = new Point(4, 24);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(1094, 572);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "     Settings     ";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // btnSave
            // 
            btnSave.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnSave.Location = new Point(904, 541);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(182, 23);
            btnSave.TabIndex = 21;
            btnSave.Text = "Save settings";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += button1_Click_1;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(8, 146);
            label4.Name = "label4";
            label4.Size = new Size(123, 15);
            label4.TabIndex = 20;
            label4.Text = "Unused days removal:";
            // 
            // nudKeepUnusedDays
            // 
            nudKeepUnusedDays.Location = new Point(137, 144);
            nudKeepUnusedDays.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            nudKeepUnusedDays.Name = "nudKeepUnusedDays";
            nudKeepUnusedDays.Size = new Size(120, 23);
            nudKeepUnusedDays.TabIndex = 19;
            nudKeepUnusedDays.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // btnSaveSettings
            // 
            btnSaveSettings.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnSaveSettings.Location = new Point(1506, 657);
            btnSaveSettings.Name = "btnSaveSettings";
            btnSaveSettings.Size = new Size(138, 23);
            btnSaveSettings.TabIndex = 18;
            btnSaveSettings.Text = "Save Settings";
            btnSaveSettings.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(8, 193);
            label3.Name = "label3";
            label3.Size = new Size(62, 15);
            label3.TabIndex = 17;
            label3.Text = "Auto start:";
            // 
            // chbAutoStart
            // 
            chbAutoStart.AutoSize = true;
            chbAutoStart.Checked = true;
            chbAutoStart.CheckState = CheckState.Checked;
            chbAutoStart.Location = new Point(137, 193);
            chbAutoStart.Name = "chbAutoStart";
            chbAutoStart.Size = new Size(15, 14);
            chbAutoStart.TabIndex = 16;
            chbAutoStart.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            tabPage3.Controls.Add(lvLog);
            tabPage3.Location = new Point(4, 24);
            tabPage3.Name = "tabPage3";
            tabPage3.Size = new Size(1094, 572);
            tabPage3.TabIndex = 2;
            tabPage3.Text = "          Log          ";
            tabPage3.UseVisualStyleBackColor = true;
            // 
            // lvLog
            // 
            lvLog.Columns.AddRange(new ColumnHeader[] { columnHeader1, columnHeader2, columnHeader3 });
            lvLog.Dock = DockStyle.Fill;
            lvLog.FullRowSelect = true;
            lvLog.GridLines = true;
            lvLog.Location = new Point(0, 0);
            lvLog.Name = "lvLog";
            lvLog.ShowGroups = false;
            lvLog.Size = new Size(1094, 572);
            lvLog.TabIndex = 10;
            lvLog.UseCompatibleStateImageBehavior = false;
            lvLog.View = View.Details;
            // 
            // columnHeader1
            // 
            columnHeader1.Text = "Time";
            columnHeader1.Width = 150;
            // 
            // columnHeader2
            // 
            columnHeader2.Text = "Log";
            columnHeader2.Width = 500;
            // 
            // columnHeader3
            // 
            columnHeader3.Text = "";
            columnHeader3.Width = 500;
            // 
            // tabPage4
            // 
            tabPage4.Controls.Add(txtInfo);
            tabPage4.Location = new Point(4, 24);
            tabPage4.Name = "tabPage4";
            tabPage4.Size = new Size(1094, 572);
            tabPage4.TabIndex = 3;
            tabPage4.Text = "          Info          ";
            tabPage4.UseVisualStyleBackColor = true;
            // 
            // txtInfo
            // 
            txtInfo.Dock = DockStyle.Fill;
            txtInfo.Location = new Point(0, 0);
            txtInfo.Multiline = true;
            txtInfo.Name = "txtInfo";
            txtInfo.Size = new Size(1094, 572);
            txtInfo.TabIndex = 0;
            // 
            // SteamProxyLanCache
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1102, 600);
            Controls.Add(tabControl1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "SteamProxyLanCache";
            Text = "Steam Proxy Lan Cache";
            FormClosing += SteamProxyLanCache_FormClosing;
            Load += SteamProxyLanCache_Load;
            Resize += SteamProxyLanCache_Resize;
            tabControl1.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            groupBox2.ResumeLayout(false);
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            tabPage2.ResumeLayout(false);
            tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudKeepUnusedDays).EndInit();
            tabPage3.ResumeLayout(false);
            tabPage4.ResumeLayout(false);
            tabPage4.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Button btnRun;
        private Label label1;
        private Label label2;
        private TextBox txtLocalCache;
        private Label lblStatus;
        private FolderBrowserDialog folderBrowserDialog1;
        private Button btnCacheFolderBrowser;
        private ComboBox cboSteamContent;
        private CheckBox chbLocalCacheUse;
        private Label label5;
        private Label label6;
        private TextBox txtDepotFilter;
        private NotifyIcon nfTray;
        private TabControl tabControl1;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private TabPage tabPage3;
        private ListView lvLog;
        private ColumnHeader columnHeader1;
        private ColumnHeader columnHeader2;
        private ColumnHeader columnHeader3;
        private Button btnSaveSettings;
        private Label label3;
        private CheckBox chbAutoStart;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private ListView lvDiskSpace;
        private ColumnHeader columnHeader4;
        private ColumnHeader columnHeader5;
        private ColumnHeader columnHeader6;
        private ColumnHeader columnHeader7;
        private ColumnHeader columnHeader8;
        private ColumnHeader columnHeader9;
        private Label label4;
        private NumericUpDown nudKeepUnusedDays;
        private TabPage tabPage4;
        private TextBox txtInfo;
        private Label lblDnsStatus;
        private Button btnSave;
    }
}
