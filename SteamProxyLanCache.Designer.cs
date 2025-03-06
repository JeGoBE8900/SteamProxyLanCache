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
            btnRun = new Button();
            label1 = new Label();
            label2 = new Label();
            txtLocalCache = new TextBox();
            lblStatus = new Label();
            label4 = new Label();
            lvLog = new ListView();
            columnHeader1 = new ColumnHeader();
            columnHeader2 = new ColumnHeader();
            columnHeader3 = new ColumnHeader();
            folderBrowserDialog1 = new FolderBrowserDialog();
            btnCacheFolderBrowser = new Button();
            cboSteamContent = new ComboBox();
            chbLocalCacheUse = new CheckBox();
            label5 = new Label();
            label6 = new Label();
            txtDepotFilter = new TextBox();
            SuspendLayout();
            // 
            // btnRun
            // 
            btnRun.Location = new Point(821, 12);
            btnRun.Name = "btnRun";
            btnRun.Size = new Size(157, 23);
            btnRun.TabIndex = 0;
            btnRun.Text = "Start";
            btnRun.UseVisualStyleBackColor = true;
            btnRun.Click += btnRun_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(77, 15);
            label1.TabIndex = 3;
            label1.Text = "Steam cache:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 38);
            label2.Name = "label2";
            label2.Size = new Size(72, 15);
            label2.TabIndex = 5;
            label2.Text = "Local cache:";
            // 
            // txtLocalCache
            // 
            txtLocalCache.Enabled = false;
            txtLocalCache.Location = new Point(106, 35);
            txtLocalCache.Name = "txtLocalCache";
            txtLocalCache.ReadOnly = true;
            txtLocalCache.Size = new Size(517, 23);
            txtLocalCache.TabIndex = 4;
            // 
            // lblStatus
            // 
            lblStatus.AutoSize = true;
            lblStatus.Location = new Point(877, 43);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(38, 15);
            lblStatus.TabIndex = 7;
            lblStatus.Text = "status";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(12, 135);
            label4.Name = "label4";
            label4.Size = new Size(30, 15);
            label4.TabIndex = 8;
            label4.Text = "Log;";
            // 
            // lvLog
            // 
            lvLog.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            lvLog.Columns.AddRange(new ColumnHeader[] { columnHeader1, columnHeader2, columnHeader3 });
            lvLog.FullRowSelect = true;
            lvLog.GridLines = true;
            lvLog.Location = new Point(106, 135);
            lvLog.Name = "lvLog";
            lvLog.ShowGroups = false;
            lvLog.Size = new Size(1032, 456);
            lvLog.TabIndex = 9;
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
            columnHeader2.Width = 400;
            // 
            // columnHeader3
            // 
            columnHeader3.Text = "";
            columnHeader3.Width = 300;
            // 
            // btnCacheFolderBrowser
            // 
            btnCacheFolderBrowser.Location = new Point(629, 35);
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
            cboSteamContent.Location = new Point(106, 6);
            cboSteamContent.Name = "cboSteamContent";
            cboSteamContent.Size = new Size(562, 23);
            cboSteamContent.TabIndex = 11;
            // 
            // chbLocalCacheUse
            // 
            chbLocalCacheUse.AutoSize = true;
            chbLocalCacheUse.Checked = true;
            chbLocalCacheUse.CheckState = CheckState.Checked;
            chbLocalCacheUse.Location = new Point(106, 64);
            chbLocalCacheUse.Name = "chbLocalCacheUse";
            chbLocalCacheUse.Size = new Size(15, 14);
            chbLocalCacheUse.TabIndex = 12;
            chbLocalCacheUse.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(12, 64);
            label5.Name = "label5";
            label5.Size = new Size(63, 15);
            label5.TabIndex = 13;
            label5.Text = "Use cache:";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(12, 96);
            label6.Name = "label6";
            label6.Size = new Size(75, 15);
            label6.TabIndex = 14;
            label6.Text = "Filter depots:";
            // 
            // txtDepotFilter
            // 
            txtDepotFilter.Location = new Point(106, 93);
            txtDepotFilter.MaxLength = 999;
            txtDepotFilter.Name = "txtDepotFilter";
            txtDepotFilter.Size = new Size(562, 23);
            txtDepotFilter.TabIndex = 15;
            // 
            // SteamProxyLanCache
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1150, 603);
            Controls.Add(txtDepotFilter);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(chbLocalCacheUse);
            Controls.Add(cboSteamContent);
            Controls.Add(btnCacheFolderBrowser);
            Controls.Add(lvLog);
            Controls.Add(label4);
            Controls.Add(lblStatus);
            Controls.Add(label2);
            Controls.Add(txtLocalCache);
            Controls.Add(label1);
            Controls.Add(btnRun);
            Name = "SteamProxyLanCache";
            Text = "SteamProxyLanCache";
            Load += SteamProxyLanCache_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnRun;
        private Label label1;
        private Label label2;
        private TextBox txtLocalCache;
        private Label lblStatus;
        private Label label4;
        private ListView lvLog;
        private ColumnHeader columnHeader1;
        private ColumnHeader columnHeader2;
        private FolderBrowserDialog folderBrowserDialog1;
        private Button btnCacheFolderBrowser;
        private ComboBox cboSteamContent;
        private ColumnHeader columnHeader3;
        private CheckBox chbLocalCacheUse;
        private Label label5;
        private Label label6;
        private TextBox txtDepotFilter;
    }
}
