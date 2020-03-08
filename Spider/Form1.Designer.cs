namespace Spider
{
    partial class form1
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.TAB1 = new System.Windows.Forms.TabPage();
            this.label3 = new System.Windows.Forms.Label();
            this.labelInstallLocation = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.labelUnstallString2 = new System.Windows.Forms.Label();
            this.labelPathInRegistry2 = new System.Windows.Forms.Label();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.AppName1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.appVersion1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.appCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.appUninstallString1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.appInstallLocation1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.appPublisher1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.appPathInRegistry1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.labelNamePathinRegistry = new System.Windows.Forms.Label();
            this.labelNameUninsatallStirng = new System.Windows.Forms.Label();
            this.labelUninstallStirng = new System.Windows.Forms.Label();
            this.labelPathInRegistry = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.AppName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AppVersion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AppPublisher = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.InstallLocation = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UninstallString = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PathInRegistry = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ListPC = new System.Windows.Forms.ListBox();
            this.GetPCNames = new System.Windows.Forms.Button();
            this.listShablons = new System.Windows.Forms.ComboBox();
            this.GetListShablons = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.tabControl1.SuspendLayout();
            this.TAB1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Alignment = System.Windows.Forms.TabAlignment.Left;
            this.tabControl1.Controls.Add(this.TAB1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Left;
            this.tabControl1.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
            this.tabControl1.ItemSize = new System.Drawing.Size(60, 200);
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Multiline = true;
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1291, 848);
            this.tabControl1.TabIndex = 0;
            this.tabControl1.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.tabControl1_DrawItem);
            // 
            // TAB1
            // 
            this.TAB1.Controls.Add(this.label3);
            this.TAB1.Controls.Add(this.labelInstallLocation);
            this.TAB1.Controls.Add(this.label1);
            this.TAB1.Controls.Add(this.label2);
            this.TAB1.Controls.Add(this.labelUnstallString2);
            this.TAB1.Controls.Add(this.labelPathInRegistry2);
            this.TAB1.Controls.Add(this.dataGridView2);
            this.TAB1.Controls.Add(this.labelNamePathinRegistry);
            this.TAB1.Controls.Add(this.labelNameUninsatallStirng);
            this.TAB1.Controls.Add(this.labelUninstallStirng);
            this.TAB1.Controls.Add(this.labelPathInRegistry);
            this.TAB1.Controls.Add(this.dataGridView1);
            this.TAB1.Controls.Add(this.ListPC);
            this.TAB1.Controls.Add(this.GetPCNames);
            this.TAB1.Controls.Add(this.listShablons);
            this.TAB1.Controls.Add(this.GetListShablons);
            this.TAB1.Location = new System.Drawing.Point(204, 4);
            this.TAB1.Name = "TAB1";
            this.TAB1.Padding = new System.Windows.Forms.Padding(3);
            this.TAB1.Size = new System.Drawing.Size(1083, 840);
            this.TAB1.TabIndex = 0;
            this.TAB1.Text = "Search Apps";
            this.TAB1.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(265, 768);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(74, 13);
            this.label3.TabIndex = 16;
            this.label3.Text = "Install location";
            // 
            // labelInstallLocation
            // 
            this.labelInstallLocation.AutoSize = true;
            this.labelInstallLocation.Location = new System.Drawing.Point(353, 768);
            this.labelInstallLocation.Name = "labelInstallLocation";
            this.labelInstallLocation.Size = new System.Drawing.Size(96, 13);
            this.labelInstallLocation.TabIndex = 15;
            this.labelInstallLocation.Text = "TextInstallLocation";
            this.labelInstallLocation.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(265, 737);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 13);
            this.label1.TabIndex = 14;
            this.label1.Text = "Path in registry:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(265, 711);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 13);
            this.label2.TabIndex = 13;
            this.label2.Text = "Unistall string:";
            // 
            // labelUnstallString2
            // 
            this.labelUnstallString2.AutoSize = true;
            this.labelUnstallString2.Location = new System.Drawing.Point(353, 711);
            this.labelUnstallString2.Name = "labelUnstallString2";
            this.labelUnstallString2.Size = new System.Drawing.Size(96, 13);
            this.labelUnstallString2.TabIndex = 12;
            this.labelUnstallString2.Text = "labelUninstallString";
            this.labelUnstallString2.Visible = false;
            // 
            // labelPathInRegistry2
            // 
            this.labelPathInRegistry2.AutoSize = true;
            this.labelPathInRegistry2.Location = new System.Drawing.Point(353, 737);
            this.labelPathInRegistry2.Name = "labelPathInRegistry2";
            this.labelPathInRegistry2.Size = new System.Drawing.Size(103, 13);
            this.labelPathInRegistry2.TabIndex = 11;
            this.labelPathInRegistry2.Text = "Text_PathInRegsitry";
            this.labelPathInRegistry2.Visible = false;
            // 
            // dataGridView2
            // 
            this.dataGridView2.AllowUserToAddRows = false;
            this.dataGridView2.AllowUserToDeleteRows = false;
            this.dataGridView2.AllowUserToOrderColumns = true;
            this.dataGridView2.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.AppName1,
            this.appVersion1,
            this.appCount,
            this.appUninstallString1,
            this.appInstallLocation1,
            this.appPublisher1,
            this.appPathInRegistry1});
            this.dataGridView2.Location = new System.Drawing.Point(268, 413);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.ReadOnly = true;
            this.dataGridView2.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView2.Size = new System.Drawing.Size(717, 287);
            this.dataGridView2.TabIndex = 10;
            this.dataGridView2.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dataGridView2_CellFormatting);
            this.dataGridView2.SelectionChanged += new System.EventHandler(this.dataGridView2_SelectionChanged);
            // 
            // AppName1
            // 
            this.AppName1.DataPropertyName = "DisplayName";
            this.AppName1.HeaderText = "Name";
            this.AppName1.Name = "AppName1";
            this.AppName1.ReadOnly = true;
            // 
            // appVersion1
            // 
            this.appVersion1.DataPropertyName = "DisplayVersion";
            this.appVersion1.HeaderText = "Version";
            this.appVersion1.Name = "appVersion1";
            this.appVersion1.ReadOnly = true;
            // 
            // appCount
            // 
            this.appCount.DataPropertyName = "Count";
            this.appCount.HeaderText = "Count";
            this.appCount.Name = "appCount";
            this.appCount.ReadOnly = true;
            // 
            // appUninstallString1
            // 
            this.appUninstallString1.DataPropertyName = "UninstallString";
            this.appUninstallString1.HeaderText = "\"Uninstall String\"";
            this.appUninstallString1.Name = "appUninstallString1";
            this.appUninstallString1.ReadOnly = true;
            this.appUninstallString1.Visible = false;
            // 
            // appInstallLocation1
            // 
            this.appInstallLocation1.DataPropertyName = "InstallLocation";
            this.appInstallLocation1.HeaderText = "Install Location";
            this.appInstallLocation1.Name = "appInstallLocation1";
            this.appInstallLocation1.ReadOnly = true;
            this.appInstallLocation1.Visible = false;
            // 
            // appPublisher1
            // 
            this.appPublisher1.DataPropertyName = "Publisher";
            this.appPublisher1.HeaderText = "Publisher";
            this.appPublisher1.Name = "appPublisher1";
            this.appPublisher1.ReadOnly = true;
            // 
            // appPathInRegistry1
            // 
            this.appPathInRegistry1.DataPropertyName = "PathInRegistry";
            this.appPathInRegistry1.HeaderText = "Path In Registry";
            this.appPathInRegistry1.Name = "appPathInRegistry1";
            this.appPathInRegistry1.ReadOnly = true;
            this.appPathInRegistry1.Visible = false;
            // 
            // labelNamePathinRegistry
            // 
            this.labelNamePathinRegistry.AutoSize = true;
            this.labelNamePathinRegistry.Location = new System.Drawing.Point(268, 353);
            this.labelNamePathinRegistry.Name = "labelNamePathinRegistry";
            this.labelNamePathinRegistry.Size = new System.Drawing.Size(79, 13);
            this.labelNamePathinRegistry.TabIndex = 9;
            this.labelNamePathinRegistry.Text = "Path in registry:";
            // 
            // labelNameUninsatallStirng
            // 
            this.labelNameUninsatallStirng.AutoSize = true;
            this.labelNameUninsatallStirng.Location = new System.Drawing.Point(265, 316);
            this.labelNameUninsatallStirng.Name = "labelNameUninsatallStirng";
            this.labelNameUninsatallStirng.Size = new System.Drawing.Size(72, 13);
            this.labelNameUninsatallStirng.TabIndex = 8;
            this.labelNameUninsatallStirng.Text = "Unistall string:";
            // 
            // labelUninstallStirng
            // 
            this.labelUninstallStirng.AutoSize = true;
            this.labelUninstallStirng.Location = new System.Drawing.Point(265, 329);
            this.labelUninstallStirng.Name = "labelUninstallStirng";
            this.labelUninstallStirng.Size = new System.Drawing.Size(96, 13);
            this.labelUninstallStirng.TabIndex = 7;
            this.labelUninstallStirng.Text = "labelUninstallString";
            this.labelUninstallStirng.Visible = false;
            // 
            // labelPathInRegistry
            // 
            this.labelPathInRegistry.AutoSize = true;
            this.labelPathInRegistry.Location = new System.Drawing.Point(265, 366);
            this.labelPathInRegistry.Name = "labelPathInRegistry";
            this.labelPathInRegistry.Size = new System.Drawing.Size(103, 13);
            this.labelPathInRegistry.TabIndex = 6;
            this.labelPathInRegistry.Text = "Text_PathInRegsitry";
            this.labelPathInRegistry.Visible = false;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.AppName,
            this.AppVersion,
            this.AppPublisher,
            this.InstallLocation,
            this.UninstallString,
            this.PathInRegistry});
            this.dataGridView1.Location = new System.Drawing.Point(268, 33);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(720, 267);
            this.dataGridView1.TabIndex = 5;
            this.dataGridView1.SelectionChanged += new System.EventHandler(this.dataGridView1_SelectionChanged);
            // 
            // AppName
            // 
            this.AppName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.AppName.DataPropertyName = "DisplayName";
            this.AppName.FillWeight = 49.80103F;
            this.AppName.HeaderText = "Name";
            this.AppName.Name = "AppName";
            this.AppName.ReadOnly = true;
            this.AppName.Width = 60;
            // 
            // AppVersion
            // 
            this.AppVersion.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.AppVersion.DataPropertyName = "DisplayVersion";
            this.AppVersion.FillWeight = 48.80502F;
            this.AppVersion.HeaderText = "Version ";
            this.AppVersion.Name = "AppVersion";
            this.AppVersion.ReadOnly = true;
            this.AppVersion.Width = 70;
            // 
            // AppPublisher
            // 
            this.AppPublisher.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.AppPublisher.DataPropertyName = "Publisher";
            this.AppPublisher.FillWeight = 48.80502F;
            this.AppPublisher.HeaderText = "Publisher";
            this.AppPublisher.Name = "AppPublisher";
            this.AppPublisher.ReadOnly = true;
            this.AppPublisher.Width = 75;
            // 
            // InstallLocation
            // 
            this.InstallLocation.DataPropertyName = "InstallLocation";
            this.InstallLocation.HeaderText = "InstallLocation";
            this.InstallLocation.Name = "InstallLocation";
            this.InstallLocation.ReadOnly = true;
            this.InstallLocation.Visible = false;
            // 
            // UninstallString
            // 
            this.UninstallString.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.UninstallString.DataPropertyName = "UninstallString";
            this.UninstallString.FillWeight = 48.80502F;
            this.UninstallString.HeaderText = "Uninstall String";
            this.UninstallString.Name = "UninstallString";
            this.UninstallString.ReadOnly = true;
            this.UninstallString.Visible = false;
            // 
            // PathInRegistry
            // 
            this.PathInRegistry.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.PathInRegistry.DataPropertyName = "PathInRegistry";
            this.PathInRegistry.HeaderText = "PathInRegistry";
            this.PathInRegistry.Name = "PathInRegistry";
            this.PathInRegistry.ReadOnly = true;
            this.PathInRegistry.Visible = false;
            // 
            // ListPC
            // 
            this.ListPC.FormattingEnabled = true;
            this.ListPC.Location = new System.Drawing.Point(50, 237);
            this.ListPC.Name = "ListPC";
            this.ListPC.Size = new System.Drawing.Size(196, 511);
            this.ListPC.TabIndex = 3;
            this.ListPC.SelectedIndexChanged += new System.EventHandler(this.ListPC_SelectedIndexChanged);
            // 
            // GetPCNames
            // 
            this.GetPCNames.Enabled = false;
            this.GetPCNames.Location = new System.Drawing.Point(50, 158);
            this.GetPCNames.Name = "GetPCNames";
            this.GetPCNames.Size = new System.Drawing.Size(196, 40);
            this.GetPCNames.TabIndex = 2;
            this.GetPCNames.Text = "Get PC";
            this.GetPCNames.UseVisualStyleBackColor = true;
            this.GetPCNames.Click += new System.EventHandler(this.GetPC_Click);
            // 
            // listShablons
            // 
            this.listShablons.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.listShablons.Enabled = false;
            this.listShablons.FormattingEnabled = true;
            this.listShablons.Location = new System.Drawing.Point(50, 105);
            this.listShablons.Name = "listShablons";
            this.listShablons.Size = new System.Drawing.Size(196, 21);
            this.listShablons.TabIndex = 1;
            this.listShablons.SelectedIndexChanged += new System.EventHandler(this.ListApp_SelectedIndexChanged);
            // 
            // GetListShablons
            // 
            this.GetListShablons.Location = new System.Drawing.Point(50, 33);
            this.GetListShablons.Name = "GetListShablons";
            this.GetListShablons.Size = new System.Drawing.Size(106, 44);
            this.GetListShablons.TabIndex = 0;
            this.GetListShablons.Text = "Ge tList Shablons";
            this.GetListShablons.UseVisualStyleBackColor = true;
            this.GetListShablons.Click += new System.EventHandler(this.Update_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(204, 4);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1163, 840);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            this.tabPage3.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage3.Location = new System.Drawing.Point(204, 4);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(1163, 840);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "tabPage3";
            // 
            // form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1296, 848);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "form1";
            this.Text = "Form1";
            this.tabControl1.ResumeLayout(false);
            this.TAB1.ResumeLayout(false);
            this.TAB1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage TAB1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Button GetListShablons;
        private System.Windows.Forms.ComboBox listShablons;
        private System.Windows.Forms.ListBox ListPC;
        private System.Windows.Forms.Button GetPCNames;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label labelPathInRegistry;
        private System.Windows.Forms.Label labelUninstallStirng;
        private System.Windows.Forms.Label labelNamePathinRegistry;
        private System.Windows.Forms.Label labelNameUninsatallStirng;
        private System.Windows.Forms.DataGridViewTextBoxColumn AppName;
        private System.Windows.Forms.DataGridViewTextBoxColumn AppVersion;
        private System.Windows.Forms.DataGridViewTextBoxColumn AppPublisher;
        private System.Windows.Forms.DataGridViewTextBoxColumn InstallLocation;
        private System.Windows.Forms.DataGridViewTextBoxColumn UninstallString;
        private System.Windows.Forms.DataGridViewTextBoxColumn PathInRegistry;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label labelUnstallString2;
        private System.Windows.Forms.Label labelPathInRegistry2;
        private System.Windows.Forms.DataGridViewTextBoxColumn AppName1;
        private System.Windows.Forms.DataGridViewTextBoxColumn appVersion1;
        private System.Windows.Forms.DataGridViewTextBoxColumn appCount;
        private System.Windows.Forms.DataGridViewTextBoxColumn appUninstallString1;
        private System.Windows.Forms.DataGridViewTextBoxColumn appInstallLocation1;
        private System.Windows.Forms.DataGridViewTextBoxColumn appPublisher1;
        private System.Windows.Forms.DataGridViewTextBoxColumn appPathInRegistry1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label labelInstallLocation;
    }
}

