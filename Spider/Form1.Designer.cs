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
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.AppName1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.appVersion1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.appPublisher1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.appUninstallString1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.appInstallLocation1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.appPathInRegistry1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.appCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AppItem = new System.Windows.Forms.DataGridViewTextBoxColumn();
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
            this.GetPC = new System.Windows.Forms.Button();
            this.ListApp = new System.Windows.Forms.ComboBox();
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
            this.tabControl1.Size = new System.Drawing.Size(1829, 848);
            this.tabControl1.TabIndex = 0;
            this.tabControl1.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.tabControl1_DrawItem);
            // 
            // TAB1
            // 
            this.TAB1.Controls.Add(this.dataGridView2);
            this.TAB1.Controls.Add(this.labelNamePathinRegistry);
            this.TAB1.Controls.Add(this.labelNameUninsatallStirng);
            this.TAB1.Controls.Add(this.labelUninstallStirng);
            this.TAB1.Controls.Add(this.labelPathInRegistry);
            this.TAB1.Controls.Add(this.dataGridView1);
            this.TAB1.Controls.Add(this.ListPC);
            this.TAB1.Controls.Add(this.GetPC);
            this.TAB1.Controls.Add(this.ListApp);
            this.TAB1.Controls.Add(this.GetListShablons);
            this.TAB1.Location = new System.Drawing.Point(204, 4);
            this.TAB1.Name = "TAB1";
            this.TAB1.Padding = new System.Windows.Forms.Padding(3);
            this.TAB1.Size = new System.Drawing.Size(1621, 840);
            this.TAB1.TabIndex = 0;
            this.TAB1.Text = "Name1";
            this.TAB1.UseVisualStyleBackColor = true;
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
            this.appPublisher1,
            this.appUninstallString1,
            this.appInstallLocation1,
            this.appPathInRegistry1,
            this.appCount,
            this.AppItem});
            this.dataGridView2.Location = new System.Drawing.Point(310, 492);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.ReadOnly = true;
            this.dataGridView2.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView2.Size = new System.Drawing.Size(1156, 334);
            this.dataGridView2.TabIndex = 10;
            this.dataGridView2.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dataGridView2_CellFormatting);
            this.dataGridView2.SelectionChanged += new System.EventHandler(this.dataGridView2_SelectionChanged);
            // 
            // AppName1
            // 
            this.AppName1.DataPropertyName = "AppItem.DisplayName";
            this.AppName1.HeaderText = "Name";
            this.AppName1.Name = "AppName1";
            this.AppName1.ReadOnly = true;
            this.AppName1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // appVersion1
            // 
            this.appVersion1.DataPropertyName = "AppItem.DisplayVersion";
            this.appVersion1.HeaderText = "Version";
            this.appVersion1.Name = "appVersion1";
            this.appVersion1.ReadOnly = true;
            this.appVersion1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // appPublisher1
            // 
            this.appPublisher1.DataPropertyName = "AppItem.Publisher";
            this.appPublisher1.HeaderText = "Publisher";
            this.appPublisher1.Name = "appPublisher1";
            this.appPublisher1.ReadOnly = true;
            this.appPublisher1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // appUninstallString1
            // 
            this.appUninstallString1.DataPropertyName = "AppItem.UninstallString";
            this.appUninstallString1.HeaderText = "\"Uninstall String\"";
            this.appUninstallString1.Name = "appUninstallString1";
            this.appUninstallString1.ReadOnly = true;
            this.appUninstallString1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // appInstallLocation1
            // 
            this.appInstallLocation1.DataPropertyName = "AppItem.InstallLocation";
            this.appInstallLocation1.HeaderText = "Install Location";
            this.appInstallLocation1.Name = "appInstallLocation1";
            this.appInstallLocation1.ReadOnly = true;
            this.appInstallLocation1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // appPathInRegistry1
            // 
            this.appPathInRegistry1.DataPropertyName = "AppItem.PathInRegistry";
            this.appPathInRegistry1.HeaderText = "Path In Registry";
            this.appPathInRegistry1.Name = "appPathInRegistry1";
            this.appPathInRegistry1.ReadOnly = true;
            this.appPathInRegistry1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // appCount
            // 
            this.appCount.DataPropertyName = "Count";
            this.appCount.HeaderText = "Count";
            this.appCount.Name = "appCount";
            this.appCount.ReadOnly = true;
            this.appCount.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // AppItem
            // 
            this.AppItem.DataPropertyName = "AppItem";
            this.AppItem.HeaderText = "AppItem1";
            this.AppItem.Name = "AppItem";
            this.AppItem.ReadOnly = true;
            this.AppItem.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.AppItem.Visible = false;
            // 
            // labelNamePathinRegistry
            // 
            this.labelNamePathinRegistry.AutoSize = true;
            this.labelNamePathinRegistry.Location = new System.Drawing.Point(307, 418);
            this.labelNamePathinRegistry.Name = "labelNamePathinRegistry";
            this.labelNamePathinRegistry.Size = new System.Drawing.Size(79, 13);
            this.labelNamePathinRegistry.TabIndex = 9;
            this.labelNamePathinRegistry.Text = "Path in registry:";
            // 
            // labelNameUninsatallStirng
            // 
            this.labelNameUninsatallStirng.AutoSize = true;
            this.labelNameUninsatallStirng.Location = new System.Drawing.Point(307, 392);
            this.labelNameUninsatallStirng.Name = "labelNameUninsatallStirng";
            this.labelNameUninsatallStirng.Size = new System.Drawing.Size(72, 13);
            this.labelNameUninsatallStirng.TabIndex = 8;
            this.labelNameUninsatallStirng.Text = "Unistall string:";
            // 
            // labelUninstallStirng
            // 
            this.labelUninstallStirng.AutoSize = true;
            this.labelUninstallStirng.Location = new System.Drawing.Point(395, 392);
            this.labelUninstallStirng.Name = "labelUninstallStirng";
            this.labelUninstallStirng.Size = new System.Drawing.Size(96, 13);
            this.labelUninstallStirng.TabIndex = 7;
            this.labelUninstallStirng.Text = "labelUninstallString";
            // 
            // labelPathInRegistry
            // 
            this.labelPathInRegistry.AutoSize = true;
            this.labelPathInRegistry.Location = new System.Drawing.Point(395, 418);
            this.labelPathInRegistry.Name = "labelPathInRegistry";
            this.labelPathInRegistry.Size = new System.Drawing.Size(103, 13);
            this.labelPathInRegistry.TabIndex = 6;
            this.labelPathInRegistry.Text = "Text_PathInRegsitry";
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
            this.dataGridView1.Location = new System.Drawing.Point(307, 33);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(1159, 352);
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
            this.ListPC.Location = new System.Drawing.Point(50, 328);
            this.ListPC.Name = "ListPC";
            this.ListPC.Size = new System.Drawing.Size(231, 498);
            this.ListPC.TabIndex = 3;
            this.ListPC.SelectedIndexChanged += new System.EventHandler(this.ListPC_SelectedIndexChanged);
            // 
            // GetPC
            // 
            this.GetPC.Enabled = false;
            this.GetPC.Location = new System.Drawing.Point(50, 218);
            this.GetPC.Name = "GetPC";
            this.GetPC.Size = new System.Drawing.Size(196, 40);
            this.GetPC.TabIndex = 2;
            this.GetPC.Text = "Get PC";
            this.GetPC.UseVisualStyleBackColor = true;
            this.GetPC.Click += new System.EventHandler(this.GetPC_Click);
            // 
            // ListApp
            // 
            this.ListApp.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ListApp.Enabled = false;
            this.ListApp.FormattingEnabled = true;
            this.ListApp.Location = new System.Drawing.Point(50, 153);
            this.ListApp.Name = "ListApp";
            this.ListApp.Size = new System.Drawing.Size(196, 21);
            this.ListApp.TabIndex = 1;
            this.ListApp.SelectedIndexChanged += new System.EventHandler(this.ListApp_SelectedIndexChanged);
            // 
            // GetListShablons
            // 
            this.GetListShablons.Location = new System.Drawing.Point(50, 47);
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
            this.tabPage2.Size = new System.Drawing.Size(1621, 840);
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
            this.tabPage3.Size = new System.Drawing.Size(1621, 840);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "tabPage3";
            // 
            // form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1678, 848);
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
        private System.Windows.Forms.ComboBox ListApp;
        private System.Windows.Forms.ListBox ListPC;
        private System.Windows.Forms.Button GetPC;
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
        private System.Windows.Forms.DataGridViewTextBoxColumn AppName1;
        private System.Windows.Forms.DataGridViewTextBoxColumn appVersion1;
        private System.Windows.Forms.DataGridViewTextBoxColumn appPublisher1;
        private System.Windows.Forms.DataGridViewTextBoxColumn appUninstallString1;
        private System.Windows.Forms.DataGridViewTextBoxColumn appInstallLocation1;
        private System.Windows.Forms.DataGridViewTextBoxColumn appPathInRegistry1;
        private System.Windows.Forms.DataGridViewTextBoxColumn appCount;
        private System.Windows.Forms.DataGridViewTextBoxColumn AppItem;
    }
}

