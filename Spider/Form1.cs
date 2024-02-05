using OfficeOpenXml;
using OfficeOpenXml.FormulaParsing.LexicalAnalysis;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using System.Xml.Linq;

namespace Spider
{
    public partial class form1 : Form
    {

        ShowResultSearch showRes = new ShowResultSearch();
        List<App> apps = new List<App>();
        BindingList<Appv2> unicapps = new BindingList<Appv2>();
        private ListView tmplistView;
        private Label tmplabelAllPC;
        private Label tmplabelOnlinePC;
        private Label tmplabelOfflinePC;

        Dictionary<string, PCstatus> dicpcstatustb1 = new Dictionary<string, PCstatus>();
        Dictionary<string, PCstatus> dicpcstatustb2 = new Dictionary<string, PCstatus>();
        Dictionary<string, PCstatus> dicpcstatustb3 = new Dictionary<string, PCstatus>();
        Dictionary<string, PCstatus> tmpdicpcstatus = new Dictionary<string, PCstatus>();
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
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            if (this.tabControl1.SelectedIndex != -1) {
                if (this.tabControl1.SelectedIndex == 5) {
                     AddControltoSplit();                    
                }
                else
                {                    
                    AddControltoTab();                
                    tabControl2.Focus();
                }
            }
        }

        private void AddControltoTab() {
            this.groupSTorage.Location = new System.Drawing.Point(651, 521);
            this.groupRAM.Location = new System.Drawing.Point(651, 361);
            this.groupBox4.Location = new System.Drawing.Point(651, 315);
            this.groupBox3.Location = new System.Drawing.Point(651, 93);
            this.groupNamePC.Location = new System.Drawing.Point(651, 15);
            this.groupMainBoard.Location = new System.Drawing.Point(651, 214);
            this.groupSMonitor.Location  = new System.Drawing.Point(651, 685);
            TabPage control = this.tabControl2.SelectedTab;
            control.Controls.Add(this.groupSTorage);
            control.Controls.Add(this.groupRAM);
            control.Controls.Add(this.groupBox4);
            control.Controls.Add(this.groupBox3);
            control.Controls.Add(this.groupNamePC);
            control.Controls.Add(this.groupMainBoard);
            control.Controls.Add(this.groupSMonitor);
        }
        private void AddControltoSplit()
        {
            this.groupSTorage.Location = new System.Drawing.Point(256, 521);            
            this.groupRAM.Location = new System.Drawing.Point(256, 361);
            this.groupBox4.Location = new System.Drawing.Point(256, 315);
            this.groupBox3.Location = new System.Drawing.Point(256, 93);
            this.groupNamePC.Location = new System.Drawing.Point(256, 15);
            this.groupMainBoard.Location = new System.Drawing.Point(256, 214);
            this.groupSMonitor.Location = new System.Drawing.Point(256, 685);
            this.splitContainer2.Panel2.Controls.Add(this.groupSTorage);
            this.splitContainer2.Panel2.Controls.Add(this.groupRAM);
            this.splitContainer2.Panel2.Controls.Add(this.groupBox4);
            this.splitContainer2.Panel2.Controls.Add(this.groupBox3);
            this.splitContainer2.Panel2.Controls.Add(this.groupNamePC);
            this.splitContainer2.Panel2.Controls.Add(this.groupMainBoard);
            this.splitContainer2.Panel2.Controls.Add(this.groupSMonitor);
            
        }


        private void buttonTestOnlinePC_Click(object sender, EventArgs e)
        {
            //string pcname = this.tmplistBox.GetItemText(this.tmplistBox.SelectedItem);
            if(this.tmplistView.SelectedItems.Count == 0)
                return;
            string pcname = this.tmplistView.SelectedItems[0].Text;
            //pcname = pcname.ToLower() + ".omsu.vmr";
            //IPAddress[] address = null;
            //string ipaddress = String.Empty;
            bool onlinepc = PingAndWrite(this.tmplistView.SelectedItems[0]);

            //try
            //{
            //    address = Dns.GetHostAddresses(pcname);
            //}
            //catch { }

            //ipaddress = address != null ? address[0].ToString() : String.Empty;
            //onlinepc = ipaddress == String.Empty ? false : PingHost(ipaddress);

            
            this.labelDNSname.Text = pcname;
            this.labelIP.Text = this.tmpdicpcstatus[pcname].IP == String.Empty ? "Not defined" : this.tmpdicpcstatus[pcname].IP;
            //this.labelOnline.Text = Isactive ? "YES" : "NO";
            this.labelDNSname.Visible = true;
            this.labelIP.Visible = true;
            this.tmplistView.SelectedItems[0].ForeColor = onlinepc? Color.Green: Color.Red;
            //this.labelOnline.Visible = true;
            if (onlinepc)
            {
                EnableRMS();
            }
            else {
                DisableRMS(this.tmpdicpcstatus[pcname].OnlineStatus);
            }            

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
            //string pcname = this.tmplistBox.GetItemText(this.tmplistBox.SelectedItem);
            string pcname = this.tmplistView.SelectedItems[0].Text;
            string ip = this.tmpdicpcstatus[pcname].IP;
            pcname = "@" + pcname.ToUpper();
            StartRMS(pcname, ip);
        }


        void StartRMS( string namepc, string ip) {
        string args = $"/create /name:{namepc} /host:{ip} /{this.typeRMSConnect.ToString()}";


            Process.Start(new ProcessStartInfo
            {
                FileName = @"C:\Program Files (x86)\Remote Manipulator System - Viewer\rutview.exe",
                Arguments = args

            });
        }

        private void tabPage4_Enter(object sender, EventArgs e)
        {
            
            
            this.tmplistView = this.listViewSortedPC1;
            this.tmpdicpcstatus = this.dicpcstatustb2;
            this.tmplabelAllPC = this.labelTextAllPC2;
            this.tmplabelOnlinePC = this.labelTextOnlinePC2;
            this.tmplabelOfflinePC = this.labelTextOfflinePC2;
            DisableRMS(OnlineState.Undefined);
            this.groupBoxPingRMS.Location = new Point(458, 405);
            this.tabPage4.Controls.Add(this.groupBoxPingRMS);
        }
        private void tabPage6_Enter(object sender, EventArgs e) {
            this.tmplistView = this.listViewPCSameMB;
           
        }
        private void tabPage1_Enter(object sender, EventArgs e)
        {
            
            this.tmplistView = listViewPC1;
            this.tmpdicpcstatus = this.dicpcstatustb1;
            this.tmplabelAllPC = this.labelTextAllPC;
            this.tmplabelOnlinePC = this.labelTextOnlinePC;
            this.tmplabelOfflinePC = this.labelTextOfflinePC;
            
            DisableRMS(OnlineState.Undefined);
            this.groupBoxPingRMS.Location = new Point(17, 364);
            this.groupBoxViewCountPC.Location = new Point(439, 369);
            this.tabPage1.Controls.Add(this.groupBoxPingRMS);
            this.tabPage1.Controls.Add(this.groupBoxViewCountPC);

        }

        private void tabPage5_Enter(object sender, EventArgs e)
        {           
            this.tmplistView = this.listViewSortedPC2;
            this.tmpdicpcstatus = this.dicpcstatustb3;
            this.tmplabelAllPC = this.labelTextAllPC3;
            this.tmplabelOnlinePC = this.labelTextOnlinePC3;
            this.tmplabelOfflinePC = this.labelTextOfflinePC3;

            
            DisableRMS(OnlineState.Undefined);
            this.groupBoxPingRMS.Location = new Point(14, 359);
            this.splitContainer2.Panel2.Controls.Add(this.groupBoxPingRMS);

            AddControltoSplit();            
        }

        private void DisableRMS(OnlineState _onlinestate)
        {
            this.buttonRMS.Enabled = false;
            this.buttonRMS.Text = "RMS OFF";
            this.radioButtonRMSFileTransfer.Enabled = false;
            this.radioButtonRMSInventory.Enabled = false;
            this.radioButtonRMSRemoteControle.Enabled = false;
            this.radioButtonRMSRemoteTerminal.Enabled = false;
            this.radioButtonRMSRemoteRegistry.Enabled = false;
            this.radioButtonRMSRemoteControle.Checked = true;

            if (_onlinestate == OnlineState.Undefined)
            {
                this.labelIP.Text = String.Empty;
                this.labelDNSname.Text = String.Empty;
            }

        }
        private void EnableRMS()
        {
            this.buttonRMS.Enabled = true;
            this.buttonRMS.Text = "RMS ON";
            this.radioButtonRMSFileTransfer.Enabled = true;
            this.radioButtonRMSInventory.Enabled = true;
            this.radioButtonRMSRemoteControle.Enabled = true;
            this.radioButtonRMSRemoteTerminal.Enabled = true;
            this.radioButtonRMSRemoteRegistry.Enabled = true;
            this.radioButtonRMSRemoteControle.Checked = true;                        
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

        private void buttonTestListPCOnlone_Click(object sender, EventArgs e)
        {
            this.buttonTestListPCOnlone.Text = $"Ping in process...";
            Ping_and_Color();           
            
        }        

        private async void Ping_and_Color()
        {
            int count = this.tmpdicpcstatus.Count;            
            ServiceThread servthrd;
            StatusThread[] statusThreads = new StatusThread[count];
            for (int i = 0; i < count; ++i)
                statusThreads[i] = new StatusThread();
            int trenum = 0;

            foreach (ListViewItem  lstview in this.tmplistView.Items)            
            {
                servthrd = new ServiceThread(lstview, statusThreads[trenum]);               
                new Thread( new ParameterizedThreadStart( (obj) =>
                {
                    ServiceThread servthrd2 = obj as ServiceThread;
                    bool onlinepc;

                    if (servthrd2 != null)
                    {
                        onlinepc = PingAndWrite(servthrd2.Lstvwitem);
                        servthrd2.SetColor(onlinepc);
                        servthrd2.StThread.IsComplit = true;
                    }
                    
                })).Start(servthrd);
                ++trenum;
            }

            await Task.Run(() =>
            {
                while (statusThreads.Where(x => x.IsComplit == false).Count() > 0)
                {
                    Thread.Sleep(500);
                }
            });

            this.tmplabelOnlinePC.Text = this.tmpdicpcstatus.Where(x => x.Value.OnlineStatus == OnlineState.Online).Count().ToString();
            this.tmplabelOfflinePC.Text = this.tmpdicpcstatus.Where(x => x.Value.OnlineStatus == OnlineState.Offline).Count().ToString();
            this.tmplabelOnlinePC.Visible = true;
            this.tmplabelOfflinePC.Visible = true;
            this.buttonViewOnline.Enabled = true;
            this.buttonViewOffline.Enabled = true;
            this.buttonViewOFFandON.Enabled = true;

            //MessageBox.Show($"Pingin successfull for {count} pc.", "Pingin confirm", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
            MessageBox.Show($"Pingin successfull for {count} pc.", "Pingin confirm", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            this.buttonTestListPCOnlone.Text = "test ping the computer list";
        }
        private bool PingAndWrite(ListViewItem lstvitem)
        {
            IPAddress[] ipadresses = null;
            string ipadress = string.Empty;
            string pcname = string.Empty;
            bool online = false;
            pcname = lstvitem.Text.ToLower() + ".omsu.vmr";

            try
            {
                ipadresses = Dns.GetHostAddresses(pcname);
            }
            catch { }

            ipadress = ipadresses != null ? ipadresses[0].ToString() : String.Empty;
            online = ipadress == String.Empty ? false : PingHost(ipadress);

            this.tmpdicpcstatus[lstvitem.Text].OnlineStatus = online ? OnlineState.Online : OnlineState.Offline;
            this.tmpdicpcstatus[lstvitem.Text].DNSName = pcname;
            this.tmpdicpcstatus[lstvitem.Text].IP = ipadress;

            return online;
        }

        private void buttonViewOnline_Click(object sender, EventArgs e)
        {            
            showPCandStatus(tmpdicpcstatus.Where(x => x.Value.OnlineStatus == OnlineState.Online).ToDictionary(x=>x.Key,x=>x.Value));
        }
        private void buttonViewOffline_Click(object sender, EventArgs e)
        {
            showPCandStatus(tmpdicpcstatus.Where(x => x.Value.OnlineStatus == OnlineState.Offline).ToDictionary(x => x.Key, x => x.Value));
        }

        private void buttonViewOFFandON_Click(object sender, EventArgs e)
        {
            showPCandStatus(tmpdicpcstatus);
        }

        private void showPCandStatus(Dictionary<string, PCstatus> _dicpcstatus) { 
            this.tmplistView.Items.Clear();
            ListViewItem tmplistviewitem;
            foreach (KeyValuePair<string, PCstatus> itempcstatus in _dicpcstatus) {
                tmplistviewitem = this.tmplistView.Items.Add(itempcstatus.Key);                
                tmplistviewitem.ForeColor = itempcstatus.Value.OnlineStatus == OnlineState.Online ? Color.Green : Color.Red;
            }            
        }

        private void button_DelAllOldPC_Click(object sender, EventArgs e)
        {
           //MessageBox.Show($"PC with same Mainboard MAC {string.Join(Environment.NewLine, pCNameStatuses.Where(x=>x.State == NameState.Previous).Select(x=>x.Name).ToList())}");
            string fullpathfile;
            if (pCNameStatuses.Count > 0) {
                foreach (PCNameStatus item in pCNameStatuses.Where(x => x.State == NameState.Previous)) { 
                    fullpathfile = Path.Combine(showPC.GetPathToDirARM(), $"{ item.Name}.xml");
                    if (File.Exists(fullpathfile)) {
                        try
                        {
                            File.Delete(fullpathfile);
                            //MessageBox.Show($"Full path to file: {fullpathfile}");
                            dicPC.Remove(item.Name);                            

                        }
                        catch (Exception exp)
                        {
                            MessageBox.Show(exp.Message);
                        }
                    }
                }
                this.buttonTab4GetMainBoardWithSameSN.PerformClick();
            }
        }

        private void button_DeletePreviosARM_Click(object sender, EventArgs e)
        {
            if (this.tmplistView.SelectedItems.Count > 0) {
                string pcName = this.tmplistView.SelectedItems[0].Text;
                if (!string.IsNullOrEmpty(pcName)) {
                    string fullpathPCName = Path.Combine(showPC.GetPathToDirARM(), $"{pcName}.xml");
                    if (File.Exists(fullpathPCName))
                    {
                        try
                        {
                            File.Delete(fullpathPCName);
                            //MessageBox.Show($"Full path to file: {fullpathPCName}");
                            dicPC.Remove(pcName);
                            this.buttonTab4GetMainBoardWithSameSN.PerformClick();
                            
                        }
                        catch (Exception exp)
                        {
                            MessageBox.Show(exp.Message);
                        }
                    }

                    
                }

            }
        }

        private void button_LoadToXLSX_Click(object sender, EventArgs e)
        {

            List<string> lstApp = new List<string>();
            Regex namergx;
            Dictionary<string, string> PtrnNames = new Dictionary<string, string>() {
                { "Rutoken Drivers","(Драйверы Рутокен)|(Rutoken Drivers)" },
                {"TrueConf","trueconf"},
                {"Континент АП","континент ап"}
            };
        
            String PathToReport = Path.Combine(Environment.CurrentDirectory,"Report");
            if(!Directory.Exists(PathToReport))
                Directory.CreateDirectory(PathToReport);
            string NameApp = this.listShablons.GetItemText(this.listShablons.SelectedItem);
            string Date = DateTime.Now.ToString("_yyyy_mm_dd(HH_mm)");
            string FullPath = Path.Combine(PathToReport,NameApp+Date+".xlsx");
            if (File.Exists(FullPath))
                File.Delete(FullPath);

            using (var package = new ExcelPackage(new FileInfo(FullPath)))
            {                
                var ws = package.Workbook.Worksheets.Add("ARMs");
                ws.Cells["A1"].Value = "Имя АРМ";
                ws.Column(1).Width = 24;


                int n = 1; // столбец
                int m = 2; // строка

                foreach (KeyValuePair<string, List<App>> item in showRes.DicPCApps)
                {
                    n = 1;
                    ws.Cells[m, n].Value = item.Key;
                    foreach (App itemapp in item.Value) {
                        foreach (KeyValuePair<string, string> itemname in PtrnNames) {
                            namergx = new Regex(itemname.Value,RegexOptions.IgnoreCase);
                            if (namergx.IsMatch(itemapp.DisplayName))
                                itemapp.DisplayName = itemname.Key;
                        }
                        if (lstApp.Contains(itemapp.DisplayName)) {

                            ws.Cells[m, lstApp.IndexOf(itemapp.DisplayName)+2].Value = itemapp.DisplayVersion;
                        }
                        else {
                            lstApp.Add(itemapp.DisplayName);
                            ws.Cells[1, lstApp.Count + 1].Value = itemapp.DisplayName;
                            ws.Cells[m, lstApp.Count + 1].Value = itemapp.DisplayVersion;
                        }                        
                    }

                    m += 1;
                }

                ws.Cells[1, 1, 1, lstApp.Count+1].AutoFilter = true;
                ws.Cells[1, 2,1, lstApp.Count + 1].AutoFitColumns();                
                package.Save();
            }
            if (File.Exists(FullPath))
                OpenExplorer(FullPath);
        }

        private void OpenExplorer(string line) {
            Process.Start(new ProcessStartInfo { FileName = "explorer", Arguments = $"/n, /select, \"{line}\"" });
        }

        private void button_DelOutdateData_Click(object sender, EventArgs e)
        {
            string pathA = @"\\fileserv.omsu.vmr\inventory$\ARM";
            string pathB = @"\\fileserv.omsu.vmr\inventory$\SearchApps";

            System.IO.DirectoryInfo dir1 = new System.IO.DirectoryInfo(pathA);
            System.IO.DirectoryInfo dir2 = new System.IO.DirectoryInfo(pathB);

            // Take a snapshot of the file system.  
            IEnumerable<System.IO.FileInfo> list1 = dir1.GetFiles("*.*", System.IO.SearchOption.AllDirectories);
            IEnumerable<System.IO.FileInfo> list2 = dir2.GetFiles("*.*", System.IO.SearchOption.AllDirectories);

            //A custom file comparer defined below  
            CompareFileByNme myFileCompare = new CompareFileByNme();

            // This query determines whether the two folders contain  
            // identical file lists, based on the custom file comparer  
            // that is defined in the FileCompare class.  
            // The query executes immediately because it returns a bool. 
            

            foreach (var item in list2)
            {
                if (!list1.Contains(item, myFileCompare))
                {
                    Console.WriteLine(item.FullName);
                    File.Delete($@"{item.FullName}");
                }
            }
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
