using System;
using System.Collections.Generic;
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

        private void buttonTab4GetPC_Click(object sender, EventArgs e)
        {
            showPC = new HWInfoGeneral();
            dicPC = showPC.GetDicPC();
            MessageBox.Show(dicPC.Count.ToString());






        }

        private void buttonTab4GetMainBoardWithSameSN_Click(object sender, EventArgs e)
        {
            this.listBoxTab5ListSerail.Items.Clear();
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
                    this.listBoxTab5ListSerail.Items.Add(infoPC.Name);
                    foreach (ComputerInfo infopc in lsinfoPC) {
                        this.listBoxTab5ListSerail.Items.Add(infopc.Name);
                        dicpc.Remove(infopc);
                    }
                }               
                
            }




            //string SN = String.Empty;
            //int i

            //foreach (KeyValuePair<string, ComputerInfo> item in dicPC) {
            //    i = 0;
            //    SN = item.Value.MainBoard.SerialNumber;
            //    foreach (KeyValuePair<string, ComputerInfo> item1 in dicPC) {
            //        if (SN == item1.Value.MainBoard.SerialNumber)
            //            i++;
            //    }
            //    if (i >1)

            //}

            //var var1 = from item in dicPC
            //           group item by item.Value.MainBoard.SerialNumber
            //           into ws
            //           where ws.Count() > 1
            //           select ws;

            //foreach (IGrouping<string, KeyValuePair<string, ComputerInfo>> item1 in var1)
            //{
            //    this.listBoxTab5ListSerail.Items.AddRange(item1.Select(x => x.Value.Name).ToArray());

            //}

            //    //dicPC.GroupBy(x=>x.Value.MainBoard.SerialNumber, (PC, var1)=> new { pc.Name,var1.Count})

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

        private void listBoxTab5ListSerail_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.listBoxTab5ListSerail.SelectedIndex != -1)
            {
                ShowComputer(this.listBoxTab5ListSerail);
            }

        }

    }
}

