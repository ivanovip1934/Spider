using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Spider
{
    public partial class form1 : Form
    {

        ShowResultSearch showRes = new ShowResultSearch();
        List<App> apps = new List<App>();
        BindingList<Appv2> unicapps = new BindingList<Appv2>();
        private ListBox tmplistBox;
        private enum TypeRMSConnect { 
        fullcontrol,
        ftp,
        telnet,
        registry,
        devicemanager
        }
        TypeRMSConnect typeRMSConnect;


        public form1()
        {
            InitializeComponent();
        }

        private void tabControl1_DrawItem(object sender, DrawItemEventArgs e)
        {
            Graphics g = e.Graphics;
            Brush _textBrush;

            // Get the item from the collection.
            TabPage _tabPage = tabControl1.TabPages[e.Index];

            // Get the real bounds for the tab rectangle.
            Rectangle _tabBounds = tabControl1.GetTabRect(e.Index);

            if (e.State == DrawItemState.Selected)
            {

                // Draw a different background color, and don't paint a focus rectangle.
                _textBrush = new SolidBrush(Color.Green);
                g.FillRectangle(Brushes.DeepSkyBlue, e.Bounds);
            }
            else
            {
                _textBrush = new System.Drawing.SolidBrush(e.ForeColor);
                e.DrawBackground();
            }

            // Use our own font.
            Font _tabFont = new Font("Arial", (float)16.0, FontStyle.Bold, GraphicsUnit.Pixel);

            // Draw string. Center the text.
            StringFormat _stringFlags = new StringFormat();
            _stringFlags.Alignment = StringAlignment.Center;
            _stringFlags.LineAlignment = StringAlignment.Center;
            g.DrawString(_tabPage.Text, _tabFont, _textBrush, _tabBounds, new StringFormat(_stringFlags));
            
        }

        private void Update_Click(object sender, EventArgs e)
        {
            ClearDatafromControls();
            showRes = new ShowResultSearch();
            this.listShablons.Items.Clear();
            string[] nameDirs = showRes.GetNameDirectories();
            this.listShablons.Items.AddRange(nameDirs);
            this.listShablons.Enabled = true;
            this.GetPCNames.Enabled = false;
        }

        private void GetPC_Click(object sender, EventArgs e)
        {

            ClearDatafromControls();
            this.unicapps = new BindingList<Appv2>(showRes.GetUnicVersion());
            foreach (Appv2 appv2 in unicapps)
            {
                this.dataGridView2.Rows.Add(appv2.AppItem.DisplayName,
                                            appv2.AppItem.DisplayVersion,
                                            appv2.Count,
                                            appv2.AppItem.UninstallString,
                                            appv2.AppItem.InstallLocation,
                                            appv2.AppItem.Publisher,
                                            appv2.AppItem.PathInRegistry
                                            );
            }
            dataGridView2.ClearSelection();

            foreach (KeyValuePair<string, List<App>> item in showRes.DicPCApps)
            {
                this.ListPC.Items.Add(item.Key);
            }
        }

        private void ListApp_SelectedIndexChanged(object sender, EventArgs e)
        {
            showRes.SetNameCurDir(this.listShablons.GetItemText(this.listShablons.SelectedItem));
            this.GetPCNames.Enabled = true;
        }

        private void ListPC_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.ListPC.SelectedIndex != -1)
            {
                this.apps = showRes.DicPCApps[this.ListPC.GetItemText(this.ListPC.SelectedItem)];
                this.dataGridView1.DataSource = this.apps;
            }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                this.labelUninstallStirng.Text = row.Cells["UninstallString"].Value?.ToString();
                this.labelPathInRegistry.Text = row.Cells["PathInRegistry"].Value?.ToString();
                this.labelUninstallStirng.Visible = true;
                this.labelPathInRegistry.Visible = true;
            }
        }
        private void dataGridView2_SelectionChanged(object sender, EventArgs e)
        {

            App selectedapp = new App();
            this.ListPC.Items.Clear();

            foreach (DataGridViewRow row in dataGridView2.SelectedRows)
            {
                selectedapp = new App(row.Cells["AppName1"].Value?.ToString(),
                    row.Cells["appVersion1"].Value?.ToString(),
                    row.Cells["appUninstallString1"].Value?.ToString(),
                    row.Cells["appInstallLocation1"].Value?.ToString(),
                    row.Cells["appPublisher1"].Value?.ToString(),
                    row.Cells["appPathInRegistry1"].Value?.ToString()
                    );
            }

            this.labelUnstallString2.Text = selectedapp.UninstallString;
            this.labelPathInRegistry2.Text = selectedapp.PathInRegistry;
            this.labelInstallLocation.Text = selectedapp.InstallLocation;
            this.labelPathInRegistry2.Visible = true;
            this.labelUnstallString2.Visible = true;
            this.labelInstallLocation.Visible = true;

            foreach (KeyValuePair<string, List<App>> item in showRes.DicPCApps)
            {

                if (item.Value.Any(x => x.IsSame(selectedapp)))
                {
                    this.ListPC.Items.Add(item.Key);
                }
            }
        }

        private void dataGridView2_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dataGridView2.Rows[e.RowIndex].DataBoundItem != null && dataGridView2.Columns[e.ColumnIndex].DataPropertyName.Contains("."))
            {

                e.Value = BindProperty(dataGridView2.Rows[e.RowIndex].DataBoundItem, dataGridView2.Columns[e.ColumnIndex].DataPropertyName);
            }
        }

        private string BindProperty(object property, string propertyName)
        {
            string retValue = "";

            if (propertyName.Contains("."))
            {
                PropertyInfo[] arrayProperties;
                string leftPropertyName;

                leftPropertyName = propertyName.Substring(0, propertyName.IndexOf("."));
                arrayProperties = property.GetType().GetProperties();

                foreach (PropertyInfo propertyInfo in arrayProperties)
                {
                    if (propertyInfo.Name == leftPropertyName)
                    {
                        retValue = BindProperty(
                          propertyInfo.GetValue(property, null),
                          propertyName.Substring(propertyName.IndexOf(".") + 1));
                        break;
                    }
                }
            }
            else
            {
                Type propertyType;
                PropertyInfo propertyInfo;

                propertyType = property.GetType();
                propertyInfo = propertyType.GetProperty(propertyName);
                retValue = propertyInfo.GetValue(property, null)?.ToString();
            }

            return retValue;
        }

        public void ClearDatafromControls()
        {           
            this.ListPC.Items.Clear(); 
            this.ListPC.Items.Clear(); 
            this.dataGridView1.DataSource = new List<App>(); 
            this.dataGridView2.Rows.Clear(); 
            this.labelPathInRegistry2.Visible = false; 
            this.labelUnstallString2.Visible = false;
            this.labelUninstallStirng.Visible = false;
            this.labelPathInRegistry.Visible = false;
        }

        private void tabControl2_Selected(object sender, TabControlEventArgs e)
        {
            AddControltoTab();
            
            //MessageBox.Show("tabControl2_Selected " + this.tabControl2.SelectedTab.Name + " " + this.tabControl1.SelectedIndex.ToString());

            //AddGroupPingRMS();
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.tabControl1.SelectedIndex == 1) {
                AddControltoTab();
                //MessageBox.Show("tabControl1_SelectedIndexChanged" + this.tabControl2.SelectedTab.Name + " " + this.tabControl1.SelectedIndex.ToString());
                //this.tabControl2.SelectedTab = tabPage1;
                //this.tabPage1_Enter(sender, e);
                tabControl2.Focus();
                //tabControl2.SelectedTab = this.tabPage6;
                //tabControl2.SelectedTab = this.tabPage1;
                //AddGroupPingRMS();
            }
        }

        private void AddControltoTab() {                        
            TabPage control = this.tabControl2.SelectedTab;
            control.Controls.Add(this.groupSTorage);
            control.Controls.Add(this.groupRAM);
            control.Controls.Add(this.groupBox4);
            control.Controls.Add(this.groupBox3);
            control.Controls.Add(this.groupNamePC);
            control.Controls.Add(this.groupMainBoard);
        }
        

        private void buttonTestOnlinePC_Click(object sender, EventArgs e)
        {
            string pcname = this.tmplistBox.GetItemText(this.tmplistBox.SelectedItem);
            pcname = pcname.ToLower() + ".omsu.vmr";
            IPAddress[] address = null;
            string ipaddress = String.Empty;
            bool Isactive;


            try
            {

                address = Dns.GetHostAddresses(pcname);

            }
            catch { }
            ipaddress = address != null ? address[0].ToString() : String.Empty;
            Isactive = ipaddress == String.Empty ? false : PingHost(ipaddress);

            
            this.labelDNSname.Text = pcname;
            this.labelIP.Text = ipaddress == String.Empty ? "Not defined" : ipaddress;
            this.labelOnline.Text = Isactive ? "YES" : "NO";
            this.labelDNSname.Visible = true;
            this.labelIP.Visible = true;
            this.labelOnline.Visible = true;
            this.buttonRMS.Enabled = Isactive ? true : false;
            this.radioButtonRMSRemoteRegistry.Enabled = Isactive ? true : false;
            this.radioButtonRMSInventory.Enabled = Isactive ? true : false;
            this.radioButtonRMSFileTransfer.Enabled = Isactive ? true : false;
            this.radioButtonRMSRemoteControle.Enabled = Isactive ? true : false;            
            this.radioButtonRMSRemoteTerminal.Enabled = Isactive ? true : false;
            this.buttonRMS.Text = Isactive ? "RMS ON" : "RMS OFF";


        }
        public static bool PingHost(string nameOrAddress)
        {
            bool pingable = false;
            Ping pinger = null;

            try
            {
                pinger = new Ping();
                PingReply reply = pinger.Send(nameOrAddress);
                pingable = reply.Status == IPStatus.Success;
            }
            catch (PingException)
            {
                // Discard PingExceptions and return false;
            }
            finally
            {
                if (pinger != null)
                {
                    pinger.Dispose();
                }
            }

            return pingable;
        }

        private void button25_Click(object sender, EventArgs e)
        {
            string pcname = this.tmplistBox.GetItemText(this.tmplistBox.SelectedItem);
            pcname = pcname.ToLower() + ".omsu.vmr";
            StartRMS(pcname);
        }


        void StartRMS( string namepc) {
            string args = $"/create /host:{namepc} /{this.typeRMSConnect.ToString()}";


            Process.Start(new ProcessStartInfo
            {
                FileName = @"C:\Program Files (x86)\Remote Manipulator System - Viewer\rutview.exe",
                Arguments = args

            });
        }

        private void tabPage4_Enter(object sender, EventArgs e)
        {
            this.tmplistBox = listFilteredPC;
            //MessageBox.Show(tmplistBox.Name);
            DisableRMS();
            this.groupBoxPingRMS.Location = new Point(458, 405);
            this.tabPage4.Controls.Add(this.groupBoxPingRMS);
        }
        private void tabPage1_Enter(object sender, EventArgs e)
        {
            this.tmplistBox = listPC1;
            //MessageBox.Show(tmplistBox.Name);
            DisableRMS();
            this.groupBoxPingRMS.Location = new Point(20, 529);
            this.tabPage1.Controls.Add(this.groupBoxPingRMS);

        }

        private void tabPage5_Enter(object sender, EventArgs e)
        {
            this.tmplistBox = listBoxTab3FilteredPCNames;
            //MessageBox.Show(tmplistBox.Name);
            DisableRMS();
            this.groupBoxPingRMS.Location = new Point(14, 359);
            this.splitContainer2.Panel2.Controls.Add(this.groupBoxPingRMS);

        }

        private void DisableRMS() {

            this.buttonRMS.Enabled = false;
            this.buttonRMS.Text = "RMS OFF";
            this.radioButtonRMSFileTransfer.Enabled = false;
            this.radioButtonRMSInventory.Enabled = false;
            this.radioButtonRMSRemoteControle.Enabled = false;
            this.radioButtonRMSRemoteTerminal.Enabled = false;
            this.radioButtonRMSRemoteRegistry.Enabled = false;

            this.radioButtonRMSRemoteControle.Checked = true;

            this.labelOnline.Text = String.Empty;
            this.labelIP.Text = String.Empty;
            this.labelDNSname.Text = String.Empty;

        }

        


        private void radioButtonRMSRemoteControle_CheckedChanged(object sender, EventArgs e)
        {
            typeRMSConnect = TypeRMSConnect.fullcontrol;
        }

        private void radioButtonRMSFileTransfer_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonRMSFileTransfer.Checked)
                typeRMSConnect = TypeRMSConnect.ftp;
        }

        private void radioButtonRMSRemoteTerminal_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonRMSRemoteTerminal.Checked)
                typeRMSConnect = TypeRMSConnect.telnet;
        }

        private void radioButtonRMSRemoteRegistry_CheckedChanged(object sender, EventArgs e)
        {
            if(radioButtonRMSRemoteRegistry.Checked)
                typeRMSConnect = TypeRMSConnect.registry;
        }

        private void radioButtonRMSInventory_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonRMSInventory.Checked)
                typeRMSConnect = TypeRMSConnect.devicemanager;
        }

        //private void AddGroupPingRMS()
        //{


        //    if (control != null)
        //    {

        //        switch (control.Name)
        //        {
        //            case "tabPage1":
        //                MessageBox.Show("tab1 name = " + control.Name);
        //                break;
        //            case "tabPage4":
        //                MessageBox.Show("tab2 name = " + control.Name);
        //                break;
        //            default:
        //                break;

        //        }

        //    }

        //}







































































































































        //private void dataGridView2_ColumnHeaderMouseClick( object sender, DataGridViewCellMouseEventArgs e)
        //{
        //    DataGridViewColumn newColumn = dataGridView2.Columns[e.ColumnIndex];
        //    DataGridViewColumn oldColumn = dataGridView2.SortedColumn;
        //    ListSortDirection direction;

        //    // If oldColumn is null, then the DataGridView is not sorted.
        //    if (oldColumn != null)
        //    {
        //        // Sort the same column again, reversing the SortOrder.
        //        if (oldColumn == newColumn &&
        //            dataGridView2.SortOrder == SortOrder.Ascending)
        //        {
        //            direction = ListSortDirection.Descending;
        //        }
        //        else
        //        {
        //            // Sort a new column and remove the old SortGlyph.
        //            direction = ListSortDirection.Ascending;
        //            oldColumn.HeaderCell.SortGlyphDirection = SortOrder.None;
        //        }
        //    }
        //    else
        //    {
        //        direction = ListSortDirection.Ascending;
        //    }

        //    // Sort the selected column.
        //    dataGridView2.Sort(newColumn, direction);
        //    newColumn.HeaderCell.SortGlyphDirection =
        //        direction == ListSortDirection.Ascending ?
        //        SortOrder.Ascending : SortOrder.Descending;
        //}

        //private void dataGridView2_DataBindingComplete(object sender,
        //    DataGridViewBindingCompleteEventArgs e)
        //{
        //    // Put each of the columns into programmatic sort mode.
        //    foreach (DataGridViewColumn column in dataGridView2.Columns)
        //    {
        //        column.SortMode = DataGridViewColumnSortMode.Programmatic;
        //    }
        //}


    }
}
