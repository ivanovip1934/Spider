using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

            var var1 = from item in dicPC
                       group item by item.Value.MainBoard.SerialNumber
                       into ws
                       where ws.Count() > 1
                       select ws;

            foreach (IGrouping<string, KeyValuePair<string, ComputerInfo>> item1 in var1)
            {
                this.listBoxTab5ListSerail.Items.AddRange(item1.Select(x => x.Value.Name).ToArray());

            }

                //    //dicPC.GroupBy(x=>x.Value.MainBoard.SerialNumber, (PC, var1)=> new { pc.Name,var1.Count})

            }
    }

}
