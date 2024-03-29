﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Spider
{
    public partial class form1
    {
        HWInfoGeneral showPC = new HWInfoGeneral();
        SortedDictionary<string,ComputerInfo> dicPC = new SortedDictionary<string,ComputerInfo>();
        ComputerInfo pc = new ComputerInfo();
        

        /*
         this.progressBar1 = new System.Windows.Forms.ProgressBar();
         this.tabPage1.Controls.Add(this.progressBar1);
         this.progressBar1.ForeColor = System.Drawing.Color.Lime;
            this.progressBar1.Location = new System.Drawing.Point(33, 502);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(153, 23);
            this.progressBar1.TabIndex = 22;
             
             */
        private void butGetPC_Click(object sender, EventArgs e)
        {
            showPC = new HWInfoGeneral();
            dicPC = showPC.GetDicPC();

            string[] query = dicPC.Select(x => x.Key).ToArray();
            ShowPC(query);


            //this.listPC1.Items.Clear();
            //this.listPC1.Items.AddRange(dicPC.Select(x => x.Key).ToArray());
            //this.labelTextAllPC.Text = listPC1.Items.Count.ToString();
            //this.labelTextAllPC.Visible = true;

            //this.listViewPC1.Items.Clear();
            //foreach (KeyValuePair<string, ComputerInfo> kvp in dicPC) { 
            
            ////this.listViewPC1.Items.Add(kvp.Key);
            //}
        }
        private void listPC1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //ShowComputer(this.listPC1);
            //if (this.listPC1.SelectedIndex != -1)
            //{
            //    this.pc = dicPC[this.listPC1.SelectedItem.ToString()]; // .GetItemText(this.listPC1.SelectedItem)];                
            //    PartitionInfo systempart = pc.Partitions.FirstOrDefault(x => x.IsSystem == true);
            //    int MaxValue = systempart.FullSize;
            //    int Value = systempart.AvailableFreeSpace;                
            //    this.progressBar1.Maximum = MaxValue;
            //    this.progressBar1.Value = MaxValue - Value;
            //    this.progressBar1.Step = 0;
            //    if (Value < 10) {                    
            //        this.progressBar1.ColorBar = Brushes.Red;                    
            //    }else if (Value >= 10)
            //    {
            //       this.progressBar1.ColorBar = Brushes.Lime;
            //    }
            //    this.progressBar1.PerformStep();
            //    this.labelTextTotalSize.Text = $"{MaxValue} Gb.";       
            //    this.labelTextFreeSpace.Text = $"{Value} Gb.";
            //    this.labelViewModelStorage.Text = $"{this.pc.Storage.FirstOrDefault(x=>x.Index == systempart.IndexDisc)?.Model}";
            //    this.labelTextTotalSize.Visible = true;
            //    this.labelTextFreeSpace.Visible = true;                
            //    this.labelTab2ShowNamePC.Visible = true;                
            //    this.labelViewModelStorage.Visible = true;
            //    DisableRMS();
            //}

        }

        private void listViewPC1_SelectedIndexChanged(object sender, EventArgs e) {

            ShowComputer2(this.tmplistView);
            if (this.tmplistView.SelectedItems.Count> 0)
            {
            //    var2 = lst.Items.IndexOf(lst.SelectedItems[0]);
            //}
            //if (this.listViewPC1.Se != -1)
            //{
                //this.pc = dicPC[this.listPC1.SelectedItem.ToString()];
                this.pc = dicPC[this.tmplistView.SelectedItems[0].Text];// .GetItemText(this.listPC1.SelectedItem)];                
                PartitionInfo systempart = pc.Partitions.FirstOrDefault(x => x.IsSystem == true);
                int MaxValue = systempart.FullSize;
                int Value = systempart.AvailableFreeSpace;
                this.progressBar1.Maximum = MaxValue;
                this.progressBar1.Value = MaxValue - Value;
                this.progressBar1.Step = 0;
                if (Value < 10)
                {
                    this.progressBar1.ColorBar = Brushes.Red;
                }
                else if (Value >= 10)
                {
                    this.progressBar1.ColorBar = Brushes.Lime;
                }
                this.progressBar1.PerformStep();
                this.labelTextTotalSize.Text = $"{MaxValue} Gb.";
                this.labelTextFreeSpace.Text = $"{Value} Gb.";
                this.labelViewModelStorage.Text = $"{this.pc.Storage.FirstOrDefault(x => x.Index == systempart.IndexDisc)?.Model}";
                this.labelTextTotalSize.Visible = true;
                this.labelTextFreeSpace.Visible = true;
                this.labelTab2ShowNamePC.Visible = true;
                this.labelViewModelStorage.Visible = true;

                if ((this.tmpdicpcstatus[this.tmplistView.SelectedItems[0].Text].OnlineStatus == OnlineState.Online) | (this.tmpdicpcstatus[this.tmplistView.SelectedItems[0].Text].OnlineStatus == OnlineState.Offline))
                {
                    this.labelDNSname.Text = this.tmpdicpcstatus[this.tmplistView.SelectedItems[0].Text].DNSName;
                    this.labelIP.Text = this.tmpdicpcstatus[this.tmplistView.SelectedItems[0].Text].IP;
                    this.labelDNSname.Visible = true;
                    this.labelIP.Visible = true;
                }                
                if (this.tmpdicpcstatus[this.tmplistView.SelectedItems[0].Text].OnlineStatus == OnlineState.Online)
                {

                    EnableRMS();
                }
                else {

                    DisableRMS(this.tmpdicpcstatus[this.tmplistView.SelectedItems[0].Text].OnlineStatus);
                }                
            }

        }

        private void butShowOldCPU_Click(object sender, EventArgs e)
        {
            bool flag = this.checkSSDExists.Checked;
            string pattern = this.textPatternProcessor.Text; //"CPU G1|CPU G2|i3-2|i3-3";
            Regex rgx = new Regex(pattern, RegexOptions.IgnoreCase);

            string[] query = dicPC.Where(x => rgx.IsMatch(x.Value.CPU.Name)).Where(x=>(x.Value.Storage.Any(y=>y.IsSSD == true)) == flag).Select(x => x.Key).ToArray();
            ShowPC(query);

            //this.listPC1.Items.Clear();
            //this.listPC1.Items.AddRange(query);
            //this.listViewPC1.Items.Clear();
            //foreach  (string _pcname in query)
            //{
            //    this.listViewPC1.Items.Add(_pcname);
            //}
            //this.labelTextAllPC.Text = query.Length.ToString();
            //this.labelTextAllPC.Visible = true;

        }

        private void buttonSizeDiscSless10G_Click(object sender, EventArgs e)
        {
            string[] query = dicPC.Where(x => x.Value.Partitions.FirstOrDefault(y=>y.IsSystem == true ).AvailableFreeSpace < 10 ).Select(x => x.Key).ToArray();
            ShowPC(query);
        }

        private void ShowPC( string[] arrpcname ) {

            
            this.listViewPC1.Items.Clear();
            foreach (string _pcname in arrpcname)
            {
                this.listViewPC1.Items.Add(_pcname);
            }
            this.tmplabelAllPC.Text = arrpcname.Length.ToString();
            this.tmplabelAllPC.Visible = true;
            this.tmplabelOnlinePC.Visible = false;
            this.tmplabelOfflinePC.Visible = false;
            this.buttonViewOnline.Enabled = false;
            this.buttonViewOffline.Enabled = false;
            this.tmpdicpcstatus.Clear();   

            foreach (string pcname in arrpcname)
            {
                this.tmpdicpcstatus.Add(pcname, new PCstatus(string.Empty, string.Empty, OnlineState.Undefined));
            }

        }

        private void ViewRAM(List<RAMInfo> memoryinfos, Control tabPage, Label label) {
            if (this.TSTlabel != null)
            {
                for (int k = 0; k < TSTlabel.Length; k++) {
                    TSTlabel[k].Dispose();                   
                }
            }          

            this.TSTlabel = new Label[memoryinfos.Count];
            int Y = label.Location.Y;
            int i = 0;
            int b = 20;            
            foreach (RAMInfo raminfo in memoryinfos) {
                TSTlabel[i] = (
                    new Label
                    {
                        AutoSize = true,
                        Font = label.Font,
                        ForeColor = label.ForeColor,
                        Location = new System.Drawing.Point(label.Location.X, Y + 2 + b),
                        Name = $"LabelMemorybank{i + 1}",
                        Size = new System.Drawing.Size(59, 20),
                        TabIndex = 30,
                        Text = $"  {i + 1}           {raminfo.Capacity}             {raminfo.PartNumber}",
                        Visible = true
                    }
                    ); ;
                tabPage.Controls.Add(TSTlabel[i]);               
                i++;
                b += 20;
            }
        }

        private void ViewListDisk(List<DiskInfo> discinfos, Control tabPage, Label label) {
            if (ArrlabelDiscView != null) {
                for (int index_1 = 0; index_1 < ArrlabelDiscView.Length; index_1++) {
                    ArrlabelDiscView[index_1].Dispose();
                    ArrlabelDiscSizeView[index_1].Dispose();
                }
            }

            this.ArrlabelDiscView = new Label[discinfos.Count];
            this.ArrlabelDiscSizeView = new Label[discinfos.Count];
            int X = label.Location.X;
            int Y = label.Location.Y + 5;
            int X_size = label.Font.Bold ? X + 55 : X;           
            int index_2 = 0;
            int shift = 20;

           
            // string typeStorage = String.Empty; // SSD or HDD
            discinfos.Sort();
            foreach (DiskInfo discinfo in discinfos)
            {
                ArrlabelDiscView[index_2] = (
                    new Label
                    {
                        AutoSize = true,                        
                        Font = label.Font,
                        ForeColor = label.ForeColor,
                        Location = new System.Drawing.Point(X, Y + shift),
                        Name = $"LabelDiscView{index_2 + 1}",
                        Size = new System.Drawing.Size(59, 20),
                        TabIndex = 30,
                        Text = $@" {discinfo.Index}       {(discinfo.IsSSD? "SSD":"HDD")}     {discinfo.Model}"
                    }
                    );
               
                ArrlabelDiscSizeView[index_2] = (
                    new Label
                    {
                        AutoSize = true,
                        Font = label.Font,
                        ForeColor = label.ForeColor,
                        Location = new System.Drawing.Point(X_size + 300, Y + shift),
                        Name = $"LabelDiscSizeView{index_2 + 1}",
                        Size = new System.Drawing.Size(59, 20),
                        TabIndex = 30,
                        Text = $"{discinfo.Size}"
                    }
                    );
                tabPage.Controls.Add(ArrlabelDiscView[index_2]);
                tabPage.Controls.Add(ArrlabelDiscSizeView[index_2]);
                index_2++;
                shift+= 20;
            }
        }

        private void ViewMonitor(List<MonitorInfo> monitorinfos, Control tabPage, Label label)
        {
            string manufacturer = String.Empty;
            string model = String.Empty;
            string trueresolution = String.Empty;
            string panelsize = String.Empty;
            string tmpstr = String.Empty;
            int sizemanufacturer = 0;
            int sizemodel = 0;
            
            if (this.ArrlabelMonitorView != null)
            {
                for (int k = 0; k < this.ArrlabelMonitorView.Length; k++)
                {
                    this.ArrlabelMonitorView[k].Dispose();
                }
            }

            this.ArrlabelMonitorView = new Label[monitorinfos.Count];
            int Y = label.Location.Y;
            int i = 0;
            int b = 20;
            foreach (MonitorInfo monitorinfo in monitorinfos)
            {
                
                manufacturer = string.Concat(monitorinfo.Manufacturer, new string(' ', 20 - monitorinfo.Manufacturer.ToString().Length) );
                sizemanufacturer = manufacturer.Length;                
                model = monitorinfo.Model is null? "--": monitorinfo.Model;
                model =  model.Length >15?  model :  string.Concat(model, new string(' ', 15 - model.Length));
                sizemodel = model.Length;
                trueresolution = monitorinfo.TrueResolution is null ? "--" : monitorinfo.TrueResolution;
                trueresolution = string.Concat(trueresolution , new string(' ', 20 - trueresolution.Length));
                panelsize = ((monitorinfo.PanelSize is null) || (monitorinfo.PanelSize == "Unknown")) ? "--": monitorinfo.PanelSize;                
                this.ArrlabelMonitorView[i] = (
                    new Label
                    {
                        AutoSize = true,
                        Font = label.Font,
                        ForeColor = label.ForeColor,
                        Location = new System.Drawing.Point(label.Location.X, Y + 2 + b),
                        Name = $"LabelMemorybank{i + 1}",
                        Size = new System.Drawing.Size(59, 20),
                        TabIndex = 30,
                        Text = $"  {i + 1}      {manufacturer}{model}{trueresolution}{panelsize}" 
                    }
                    );
                tabPage.Controls.Add(this.ArrlabelMonitorView[i]);
                i++;
                b += 20;
            }
        }

        private void textPatternProcessor_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
                this.butShowOldCPU_Click(sender, e);
        }

        private void textPatternProcessor_TextChanged(object sender, EventArgs e)
        {
            this.butShowFilteredPC.Text = $"Показать ПК подпавшие под фильтр:  {this.textPatternProcessor.Text}";
        }

        private void Char_Click(object sender, EventArgs e)
        {
            if (sender is Button)
            {
                Button button = sender as Button;
                string pattern = $"^{button.Text.Trim()}";
                Regex rgx = new Regex(pattern, RegexOptions.IgnoreCase);
                string[] query = dicPC.Where(x => rgx.IsMatch(x.Value.Name)).Select(x => x.Key).ToArray();
                ShowPC(query);
                //this.listPC1.Items.Clear();
                //this.listPC1.Items.AddRange(query);
                //this.labelTextAllPC.Text = query.Length.ToString();
                //this.labelTextAllPC.Visible = true;
            }
        }


    }
}
