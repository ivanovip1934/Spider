using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Spider
{
    public partial class form1
    {
        private void butGetPC2_Click(object sender, EventArgs e)
        {
            showPC = new HWInfoGeneral();
            dicPC = showPC.GetDicPC();
            this.dgShowCPU.Rows.Clear();
            this.dgShowMainBoard.Rows.Clear();
            this.dgShowStorages.Rows.Clear();
            this.dgShowOSs.Rows.Clear();
            this.dgShowMonitors.Rows.Clear();

            var query = dicPC.OrderBy(x => x.Value.CPU.Name).
                GroupBy(x => x.Value.CPU.Name,
                    (modelCPU, cpus) => new { ModelCPU = modelCPU, Count = cpus.Count() });
            foreach (var item in query)
            {
                this.dgShowCPU.Rows.Add(item.ModelCPU, item.Count);
            }
            this.dgShowCPU.ClearSelection();

            var query1 = dicPC.OrderBy(x => x.Value.MainBoard.Product)
                .GroupBy(x => new { x.Value.MainBoard.Product, x.Value.MainBoard.Manufacturer },
                        (MainBoard, cpus) => new { ModelMainBoard = MainBoard.Product,
                            MainBoard.Manufacturer, Count = cpus.Count() });
            foreach (var item in query1)
            {
                this.dgShowMainBoard.Rows.Add(item.ModelMainBoard, item.Manufacturer, item.Count);
            }
            this.dgShowMainBoard.ClearSelection();

            var query2 = dicPC.Select(x => x.Value).
                SelectMany(x => x.Storage).
                GroupBy(x => new { x.Model, x.Size, x.IsSSD },
                (Disc, discs) => new { Disc.Model, SizeDisc = Disc.Size, Disc.IsSSD, Count = discs.Count() })
                .ToList().
                OrderBy(x => x.Model);
            foreach (var item in query2)
            {
                this.dgShowStorages.Rows.Add(item.Model, item.SizeDisc, item.IsSSD ? "YES" : "NO", item.Count);
            }
            this.dgShowStorages.ClearSelection();

            var query3 = dicPC.Select(x => x.Value.OS).GroupBy(x => new { x.ProductName, x.Version, x.Build, x.IsX64 },
                            (Os, systems) => new { OSName = Os.ProductName, Os.Version, Os.Build, Os.IsX64, Count = systems.Count() }).
                            OrderBy(x => x.OSName).ThenBy(x => x.Build);

            foreach (var item in query3)
            {
                this.dgShowOSs.Rows.Add(item.OSName, item.Build, item.Version, item.IsX64 ? "YES" : "NO", item.Count);
            }
            this.dgShowOSs.ClearSelection();

            var query4 = dicPC.Select(x => x.Value).SelectMany(x => x.Monitors).GroupBy(x => new { x.Manufacturer, x.Model, x.PanelSize, x.TrueResolution },
                            (Monitor, Monitors) => new { Monitor.Manufacturer, Monitor.Model, Size = Monitor.PanelSize, Resolution = Monitor.TrueResolution, Count = Monitors.Count() }).
                            OrderBy(x => x.Manufacturer).ThenBy(x => x.Model);

            foreach (var item in query4)
            {
                this.dgShowMonitors.Rows.Add(item.Manufacturer, item.Model, item.Size, item.Resolution, item.Count);
            }
            this.dgShowMonitors.ClearSelection();
            this.tmplistView.Items.Clear();



            //this.listModelCPU.Items.AddRange(query.Select(x => $"{x.ModelCPU} \t:{x.Count}").ToArray());

        }


        private void dgShowOSs_SelectionChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dgShowOSs.SelectedRows)
            {
                OperationSystem os = new OperationSystem()
                {
                    ProductName = row.Cells["OSName"].Value?.ToString(),
                    Build = row.Cells["Build"].Value?.ToString(),
                    Version = row.Cells["Version"].Value?.ToString(),
                    IsX64 = row.Cells["isX64"].Value?.ToString() == "YES" ? true : false
                };
                ShowPCtoList(dicPC, os);
            }
        }

        private void dgShowMonitors_SelectionChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dgShowMonitors.SelectedRows)
            {
                if (Enum.TryParse(row.Cells["Manufacturer1"].Value?.ToString(), out Manufacturers manufacturer)) {
                MonitorInfo monitor = new MonitorInfo()
                {
                    ID = String.Empty,
                    Manufacturer = manufacturer,
                    Model = row.Cells["Model1"].Value?.ToString(),
                    PanelSize = String.Empty,
                    TrueResolution = String.Empty,
                    SerailNumber = String.Empty
                };
                ShowPCtoList(dicPC, monitor);
                }
            }
        }

        private void dgShowMainBoard_SelectionChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dgShowMainBoard.SelectedRows)
            {
                MainBoardInfo mb = new MainBoardInfo()
                {
                    Product = row.Cells["ModelMainBoard"].Value?.ToString(),
                    Manufacturer = row.Cells["Manufacturer"].Value?.ToString(),
                    SerialNumber = "",
                    SMBIOSBIOSVersion = ""
                };
                ShowPCtoList(dicPC, mb);
            }
        }

        private void dgShowStorages_SelectionChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dgShowStorages.SelectedRows)
            {
                DiskInfo disc = new DiskInfo()
                {
                    Model = row.Cells["Model"].Value?.ToString(),
                    IsSSD = false,
                    Index = 0,
                    Size = 0
                };
                ShowPCtoList(dicPC, disc);
            }
        }

        private void dgShowCPU_SelectionChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dgShowCPU.SelectedRows)
            {
                CpuInfo cpu = new CpuInfo()
                {
                    Name = row.Cells["ModelCPU"].Value?.ToString(),
                    ProcessorId = ""
                };
                ShowPCtoList(dicPC, cpu);
            }

        }


        private void ShowPCtoList<T>(SortedDictionary<string, ComputerInfo> dicComputers, T pattern) where T : new()
        {
            
            this.tmplistView.Items.Clear();
            this.tmpdicpcstatus.Clear();
            Type t = dicComputers.First().Value.GetType();
            PropertyInfo[] members = t.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo memberInfo in members)
            {
                if (memberInfo.PropertyType == typeof(List<T>)) {                    
                    foreach (KeyValuePair<string, ComputerInfo> item in dicComputers)
                    {
                        List<T> obj = (List<T>)memberInfo.GetValue(item.Value);
                        foreach (T ositem in obj) {
                            if (ositem is IIsSame) {
                                if ((ositem as IIsSame).IsSame(pattern))
                                {
                                    
                                    this.tmplistView.Items.Add(item.Key);
                                    this.tmpdicpcstatus.Add(item.Key, new PCstatus(string.Empty, string.Empty, OnlineState.Undefined));                                   

                                }
                            }                        
                        }                       
                    }
                }
                else if (memberInfo.PropertyType == typeof(T))
                {
                    foreach (KeyValuePair<string, ComputerInfo> item in dicComputers)
                    {
                        T obj = (T)memberInfo.GetValue(item.Value);
                        if (obj is IIsSame)
                        {
                            if ((obj as IIsSame).IsSame(pattern))
                            {
                                
                                this.tmplistView.Items.Add(item.Key);
                                this.tmpdicpcstatus.Add(item.Key, new PCstatus(string.Empty, string.Empty, OnlineState.Undefined));
                            }
                        }
                    }
                }
            }

            this.tmplabelAllPC.Text = this.tmpdicpcstatus.Count().ToString();
            this.tmplabelAllPC.Visible = true;
            this.tmplabelOnlinePC.Visible = false;
            this.tmplabelOfflinePC.Visible = false;
        }

        



        private void ShowComputer2(ListView listView)
        {
            if (listView.SelectedItems.Count > 0)
            {
                this.pc = dicPC[listView.SelectedItems[0].Text]; // .GetItemText(this.listPC1.SelectedItem)];
                ////this.dataGridView1.DataSource = this.apps;
                this.labelTab2ShowNamePC.Text = pc.Name;
                this.labelTab2ShowDateCollectedInfo.Text = pc.DateCollectedInfo.ToString();
                this.labelTab2ShowOSVersion.Text = pc.OS.ProductName;
                this.labelTab2ShowBuildOS.Text = pc.OS.Build;
                this.labelTab2ShowOSBIT.Text = pc.OS.IsX64 ? "x64" : "x86";
                this.labelTab2ShowOSInstallDate.Text = $"{this.pc.OS.InstallDate.Year}" +
                                                       $"-{this.pc.OS.InstallDate.Month}" +
                                                       $"-{this.pc.OS.InstallDate.Day}";
                this.labelTab2ShowMainboardManufacturer.Text = pc.MainBoard.Manufacturer;
                this.labelTab2ShowMainBoardModel.Text = pc.MainBoard.Product;
                this.labelTab2ShowMainBoardVersionBIOS.Text = $"BIOS: {pc.MainBoard.SMBIOSBIOSVersion}";
                string nameCPU = pc.CPU.Name;
                if (nameCPU.Contains("Intel") || nameCPU.Contains("Pentium"))
                {
                    this.labelTab2ShowCPUName.ForeColor = System.Drawing.Color.Blue;
                }
                else if (nameCPU.Contains("AMD"))
                    this.labelTab2ShowCPUName.ForeColor = System.Drawing.Color.Red;
                this.labelTab2ShowCPUName.Text = nameCPU;
                this.labelTab2ShowRAMTotal.Text = $"Total: {pc.Memory.Sum(x => x.Capacity)} GB.";
                ViewRAM(pc.Memory, this.groupRAM, this.labelTab2ShowRAMtitle);
                ViewListDisk(pc.Storage, this.groupSTorage, this.labelTab2ShowStorageTitle);
                ViewMonitor(pc.Monitors, this.groupSMonitor, this.labelTab2ShowMonitorTitle);

                this.labelTab2ShowOSBIT.Visible = true;
                this.labelTab2ShowDateCollectedInfo.Visible = true;
                this.labelTab2ShowBuildOS.Visible = true;
                this.labelTab2ShowNamePC.Visible = true;
                this.labelTab2ShowOSVersion.Visible = true;
                this.labelTab2ShowMainboardManufacturer.Visible = true;
                this.labelTab2ShowMainBoardModel.Visible = true;
                this.labelTab2ShowMainBoardVersionBIOS.Visible = true;
                this.labelTab2ShowCPUName.Visible = true;
                this.labelTab2ShowRAMTotal.Visible = true;
                this.labelTab2ShowRAMtitle.Visible = true;
                this.labelTab2ShowStorageTitle.Visible = true;
                this.labelTab2ShowOSInstallDate.Visible = true;

            }
        }

    }    
}
