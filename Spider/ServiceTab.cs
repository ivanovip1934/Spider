using System;
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
        Dictionary<string, ComputerInfo> PCWithSameMainBoardSN;
        List<PCNameStatus> pCNameStatuses = new List<PCNameStatus>();
        

        private void buttonTab4GetPC_Click(object sender, EventArgs e)
        {
            showPC = new HWInfoGeneral();
            dicPC = showPC.GetDicPC();
            MessageBox.Show(dicPC.Count.ToString());







        }

        private void buttonTab4GetMainBoardWithSameSN_Click(object sender, EventArgs e)
        {
            
            ComputerInfo current;
            ListViewItem tmplistviewitem;
            this.tmplistView.Items.Clear();
            pCNameStatuses.Clear();
            this.button_DeletePreviosARM.Visible = false;
            List<string> arrNamePC = new List<string>();
            ComputerInfo infoPC;
            List<ComputerInfo> lsinfoPC = new List<ComputerInfo>();
            MainBoardInfo mb = new MainBoardInfo();
            List<ComputerInfo> dicpc = dicPC.Where(x => x.Value.MainBoard.MAC != null).Select(x => x.Value).ToList();

            foreach ( ComputerInfo item in dicpc.ToArray()) {
                infoPC = item;
                mb = infoPC.MainBoard;

                dicpc.Remove(item);
                lsinfoPC = dicpc.Where(x => x.MainBoard.IsSameMAC(mb)).ToList();
                if (lsinfoPC.Count > 0) {                    
                    //this.listViewPCSameMB.Items.Add(infoPC.Name);
                    //current = new PCNameStatus(item.Name, NameState.Current);
                    current = item;
                    foreach (ComputerInfo infopc in lsinfoPC) {
                        if (current.DateCollectedInfo.CompareTo(infopc.DateCollectedInfo) > 0)
                        {
                            pCNameStatuses.Add(new PCNameStatus(infopc.Name, NameState.Previous));
                            tmplistviewitem = this.listViewPCSameMB.Items.Add(infopc.Name);
                            tmplistviewitem.ForeColor =  Color.Red;
                        }
                        else
                        {
                            pCNameStatuses.Add(new PCNameStatus(current.Name, NameState.Previous));
                            tmplistviewitem = this.listViewPCSameMB.Items.Add(current.Name);
                            tmplistviewitem.ForeColor = Color.Red;
                            current = infopc;
                        }
                        //this.listViewPCSameMB.Items.Add(infopc.Name);
                        dicpc.Remove(infopc);
                    }
                    pCNameStatuses.Add(new PCNameStatus(current.Name, NameState.Current));
                    tmplistviewitem = this.listViewPCSameMB.Items.Add(current.Name);
                    tmplistviewitem.ForeColor = Color.Green;
                }            
                
            }
            
        }


        private void buttonTab4GetMainBoardMAC_Click(object sender, EventArgs e)
        {
            this.listBoxTab5ListManufacturer.Items.Clear();

            this.listBoxTab5ListManufacturer.Items.AddRange(this.dicPC.Where(x=> x.Value.MainBoard.MAC != null).Select(x => x.Value.MainBoard.Manufacturer).Distinct().ToArray());
        }

        private void listBoxTab5ListManufacturer_SelectedIndexChanged(object sender, EventArgs e)
        {
            string pattern = @":\[(.*)\]";
            string str1 = String.Empty;       
            string str123 = String.Empty;
            if (this.listBoxTab5ListManufacturer.SelectedIndex != -1) {
                var var1 = dicPC.Where(x => x.Value.MainBoard.Manufacturer == this.listBoxTab5ListManufacturer.SelectedItem.ToString() && x.Value.MainBoard.MAC != null).SelectMany(x => x.Value.MainBoard.MAC);
                string [] arrstr = var1.Where(x=> (Regex.Match(x, pattern, RegexOptions.IgnoreCase)).Success).Select(x => (Regex.Match(x, pattern, RegexOptions.IgnoreCase).Groups[1].Value).Substring(0,6)).Distinct().ToArray();
                foreach (var item in arrstr) {
                    str1 += item + "\n";                
                }
            }
            MessageBox.Show(str1);   
        }

        private void listViewPCSameMB_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowComputer2(this.tmplistView);

            if (this.tmplistView.SelectedItems.Count > 0 && pCNameStatuses.Count >0)
            {
                // check arm name is previos
                string PCName = this.tmplistView.SelectedItems[0].Text;
                if (pCNameStatuses.First(x => x.Name.Equals(PCName)).State == NameState.Previous) { 
                    this.button_DeletePreviosARM.Text = $"DELETE {PCName}.xml file";
                    this.button_DeletePreviosARM.Visible = true;
                }else
                    this.button_DeletePreviosARM.Visible = false;
            }

        }


    }
}

