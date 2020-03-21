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
        private delegate bool FilterComputers(ComputerInfo compinfo, ref bool flag);
        FilterComputers filtercomputer;
        FilteredMethods filtermethods = new FilteredMethods();
        private bool CheckSSD = true;

        private void buttonTab3GetPC_Click(object sender, EventArgs e)
        {
            showPC = new HWInfoGeneral();
            dicPC = showPC.GetDicPC();

            #region Add data to comboBoxTab3ListCPU
            var query = dicPC.OrderBy(x => x.Value.CPU.Name).
                GroupBy(x => x.Value.CPU.Name,
                    (modelCPU, cpus) => new { ModelCPU = modelCPU, Count = cpus.Count() });
            foreach (var item in query)
            {
                this.comboBoxTab3ListCPU.Items.Add(item.ModelCPU);
            }
            this.comboBoxTab3ListCPU.SelectedIndex = -1;

            this.checkBoxTab3SearchByCPU.Enabled = true;
            this.checkBoxTab3SearchByMainBoard.Enabled = true;
            #endregion

            #region Add data to comboBoxTab3ListMainBoard
            var queryMainBoard = dicPC.OrderBy(x => x.Value.MainBoard.Product).
                GroupBy(x => x.Value.MainBoard.Product,
                    (modelMainBoard, cpus) => new { ModelMainBoard = modelMainBoard, Count = cpus.Count() });
            foreach (var item in queryMainBoard)
            {
                this.comboBoxTab3ListMainBoard.Items.Add(item.ModelMainBoard);
            }
            this.comboBoxTab3ListMainBoard.SelectedIndex = -1;
            #endregion

            this.checkBoxTab3SearchByCPU.Enabled = true;
            this.checkBoxTab3SearchByMainBoard.Enabled = true;
            this.checkBoxTab3SearchByRAM.Enabled = true;


        }

        private void checkBoxTab3SearchByCPU_CheckedChanged(object sender, EventArgs e)
        {
            List<Control> controls = new List<Control>() {
            this.radioButtonSelectCPUFromList,
            this.radioButtonUseCPUPattern,            
            };


            if (this.checkBoxTab3SearchByCPU.Checked == true)
            {
                ChangeControlEnable(controls, true);
                radioButtonSelectCPUFromList_CheckedChanged(sender, e);
                radioButtonUseCPUPattern_CheckedChanged(sender, e);
            }
            else
                ChangeControlEnable(controls, false);

        }

        private static void ChangeControlEnable(List<Control> controls, bool flag)
        {

            if (flag)
                controls.ForEach(x => x.Enabled = true);
            else
                controls.ForEach(x => x.Enabled = false);

        }

        private void radioButtonSelectCPUFromList_CheckedChanged(object sender, EventArgs e)
        {
            if (this.radioButtonSelectCPUFromList.Checked == true)
            {
                this.comboBoxTab3ListCPU.Enabled = true;
                this.filtercomputer += filtermethods.FilterByNameCPU;
               
            }
            else
            {
                this.comboBoxTab3ListCPU.Enabled = false;
                this.filtercomputer -= filtermethods.FilterByNameCPU;
            }
        }

        private void radioButtonUseCPUPattern_CheckedChanged(object sender, EventArgs e)
        {
            if (this.radioButtonUseCPUPattern.Checked == true)
            {
                this.textBoxTab3PatternCPU.Enabled = true;
                this.filtercomputer += filtermethods.FilterByPatternCPU;
            }
            else
            {
                this.textBoxTab3PatternCPU.Enabled = false;
                this.filtercomputer -= filtermethods.FilterByPatternCPU;
            }
        }

        private void comboBoxTab3ListCPU_EnabledChanged(object sender, EventArgs e)
        {
            if (this.comboBoxTab3ListCPU.Enabled == false)
                this.comboBoxTab3ListCPU.SelectedIndex = -1;
        }

        private void radioButtonSelectCPUFromList_EnabledChanged(object sender, EventArgs e)
        {
            if (this.radioButtonSelectCPUFromList.Enabled == false)
                this.comboBoxTab3ListCPU.Enabled = false;            
        }

        private void radioButtonUseCPUPattern_EnabledChanged(object sender, EventArgs e)
        {
            if (this.radioButtonUseCPUPattern.Enabled == false)
                this.textBoxTab3PatternCPU.Enabled = false;
        }

        private void textBoxTab3PatternCPU_EnabledChanged(object sender, EventArgs e)
        {
            if (this.textBoxTab3PatternCPU.Enabled == false)
                this.textBoxTab3PatternCPU.Text = String.Empty;
        }

        private void buttonAdvancedSearch_Click(object sender, EventArgs e)
        {
            Dictionary<string, ComputerInfo> filteredComputers;
            if (filtercomputer != null)
            {
                bool flag = true;
                filteredComputers = dicPC.Where(x =>  (flag = true) == true && filtercomputer(x.Value, ref flag)).ToDictionary(x => x.Key, x => x.Value);
                string name_cpu_name = String.Empty;
                foreach (KeyValuePair<string, ComputerInfo> item in filteredComputers)
                {
                    name_cpu_name += $"{item.Key} - {item.Value.CPU.Name} - {item.Value.MainBoard.Product} - {item.Value.Memory.Sum(x=>x.Capacity)}Gb.\n";
                }
                MessageBox.Show(name_cpu_name);
            }
            else
                MessageBox.Show("Not filter SET");
        }

        private void comboBoxTab3ListCPU_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.comboBoxTab3ListCPU.SelectedIndex != -1)
            {
                filtermethods.ProcessorName = this.comboBoxTab3ListCPU.SelectedItem.ToString();
            }
            else
                filtermethods.ProcessorName = String.Empty;
        }

        private void textBoxTab3PatternCPU_TextChanged(object sender, EventArgs e)
        {
            if (this.textBoxTab3PatternCPU.Text.Trim() != String.Empty)
                filtermethods.PatternProcessorModel = this.textBoxTab3PatternCPU.Text;
            else
                filtermethods.PatternProcessorModel = String.Empty;
        }

        #region Search by MainBoard
        // Search by MainBoard
        private void checkBoxTab3SearchByMainBoard_CheckedChanged(object sender, EventArgs e)
        {
            if (this.checkBoxTab3SearchByMainBoard.Checked == true)
            {
                this.radioButtonSelectMainBoardFromList.Enabled = true;
                this.radioButtonUseMainBoardPattern.Enabled = true;               
            }
            else
            {
                this.radioButtonSelectMainBoardFromList.Enabled = false;
                this.radioButtonUseMainBoardPattern.Enabled = false;
            }
        }

        private void radioButtonSelectMainBoardFromList_CheckedChanged(object sender, EventArgs e)
        {
            if (this.radioButtonSelectMainBoardFromList.Checked == true)
            {
                this.comboBoxTab3ListMainBoard.Enabled = true;
                this.filtercomputer += filtermethods.FilterByModelMainBoard;

            }
            else
            { 
                this.comboBoxTab3ListMainBoard.Enabled = false;
                this.filtercomputer -= filtermethods.FilterByModelMainBoard;
            }
        }

        private void radioButtonUseMainBoardPattern_CheckedChanged(object sender, EventArgs e)
        {
            if (this.radioButtonUseMainBoardPattern.Checked == true)
            {
                this.textBoxTab3PatternMainBoard.Enabled = true;
                this.filtercomputer += filtermethods.FilterByPatternMainBoard;

            }
            else
            {
                this.textBoxTab3PatternMainBoard.Enabled = false;
                this.filtercomputer -= filtermethods.FilterByPatternMainBoard;
            }
        }


        private void comboBoxTab3ListMainBoard_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.comboBoxTab3ListMainBoard.SelectedIndex != -1)
            {
                filtermethods.MainBoardModel = this.comboBoxTab3ListMainBoard.SelectedItem.ToString();
            }
            else
                filtermethods.MainBoardModel = String.Empty;
        }


        private void comboBoxTab3ListMainBoard_EnabledChanged(object sender, EventArgs e)
        {
            if (this.comboBoxTab3ListMainBoard.Enabled == false)
                this.comboBoxTab3ListMainBoard.SelectedIndex = -1;
        }

        private void radioButtonSelectMainBoardFromList_EnabledChanged(object sender, EventArgs e)
        {
            if (this.radioButtonSelectMainBoardFromList.Enabled != true)
                this.radioButtonSelectMainBoardFromList.Checked = false;
        }

        private void radioButtonUseMainBoardPattern_EnabledChanged(object sender, EventArgs e)
        {
            if (this.radioButtonUseMainBoardPattern.Enabled != true)
                this.radioButtonUseMainBoardPattern.Checked = false;
        }

        private void textBoxTab3PatternMainBoard_EnabledChanged(object sender, EventArgs e)
        {
            if (this.textBoxTab3PatternMainBoard.Enabled == false)
                this.textBoxTab3PatternMainBoard.Text = String.Empty;
        }

        private void textBoxTab3PatternMainBoard_TextChanged(object sender, EventArgs e)
        {
            if (this.textBoxTab3PatternMainBoard.Text.Trim() != String.Empty)
                filtermethods.PatternMainBoardModel = this.textBoxTab3PatternMainBoard.Text;
            else
                filtermethods.PatternMainBoardModel = String.Empty;
        }

        // End search by MainBoard
        #endregion

        #region Search by RAM
        private void checkBoxTab3SearchByRAM_CheckedChanged(object sender, EventArgs e)
        {
            if (this.checkBoxTab3SearchByRAM.Checked == true)
            {
                this.radioButtonSelecTotalSizeRAM.Enabled = true;
                this.radioButtonSetMemorySizeRange.Enabled = true;
                this.radioButtonSetSizeOneModul.Enabled = true;
            }
            else
            {
                this.radioButtonSelecTotalSizeRAM.Enabled = false;
                this.radioButtonSetMemorySizeRange.Enabled = false;
                this.radioButtonSetSizeOneModul.Enabled = false;
            }
        }

        private void radioButtonSelecTotalSizeRAM_EnabledChanged(object sender, EventArgs e)
        {
            if (this.radioButtonSelecTotalSizeRAM.Enabled == false)
                this.radioButtonSelecTotalSizeRAM.Checked = false;
        }

        private void radioButtonSetMemorySizeRange_EnabledChanged(object sender, EventArgs e)
        {
            if (this.radioButtonSetMemorySizeRange.Enabled == false)
                this.radioButtonSetMemorySizeRange.Checked = false;
        }

        private void radioButtonSetSizeOneModul_EnabledChanged(object sender, EventArgs e)
        {
            if (this.radioButtonSetSizeOneModul.Enabled == false)
                this.radioButtonSetSizeOneModul.Checked = false;
        }

        private void radioButtonSelecTotalSizeRAM_CheckedChanged(object sender, EventArgs e)
        {
            if (this.radioButtonSelecTotalSizeRAM.Checked == true)
            {
                this.numericUpDownSelectTotalSizeRAM.Enabled = true;
                this.filtercomputer += filtermethods.FilterByTotalSizeRAM;
            }
            else
            {
                this.numericUpDownSelectTotalSizeRAM.Enabled = false;
                this.filtercomputer -= filtermethods.FilterByTotalSizeRAM;
            }

        }

        private void radioButtonSetMemorySizeRange_CheckedChanged(object sender, EventArgs e)
        {
            if (this.radioButtonSetMemorySizeRange.Checked == true)
            {
                this.numericUpDownSetMinSizeRAM.Enabled = true;
                this.numericUpDownSetMaxSizeRAM.Enabled = true;
                this.filtercomputer += filtermethods.FilterByRangeSizeRAM;
            }
            else
            {
                this.numericUpDownSetMinSizeRAM.Enabled = false;
                this.numericUpDownSetMaxSizeRAM.Enabled = false;
                this.filtercomputer -= filtermethods.FilterByRangeSizeRAM;
            }
        }

        private void radioButtonSetSizeOneModul_CheckedChanged(object sender, EventArgs e)
        {
            if (this.radioButtonSetSizeOneModul.Checked == true)
            {
                this.numericUpDownSetSizeOneModuleRAM.Enabled = true;
                this.filtercomputer += filtermethods.FilterBySizeOneModuleRAM;
            }
            else
            {
                this.numericUpDownSetSizeOneModuleRAM.Enabled = false;
                this.filtercomputer -= filtermethods.FilterBySizeOneModuleRAM;
            }
        }
        private void numericUpDownSelectTotalSizeRAM_ValueChanged(object sender, EventArgs e)
        {
            filtermethods.TotalSizeRAM = (int)this.numericUpDownSelectTotalSizeRAM.Value;
        }

        private void numericUpDownSetMinSizeRAM_ValueChanged(object sender, EventArgs e)
        {
            filtermethods.RangeSizeRAM["MinSizeRAM"] = (int)this.numericUpDownSetMinSizeRAM.Value;
        }

        private void numericUpDownSetMaxSizeRAM_ValueChanged(object sender, EventArgs e)
        {
            filtermethods.RangeSizeRAM["MaxSizeRAM"] = (int)this.numericUpDownSetMaxSizeRAM.Value;
        }

        private void numericUpDownSetSizeOneModuleRAM_ValueChanged(object sender, EventArgs e)
        {
            filtermethods.SizeOneModuleRAM = (int)this.numericUpDownSetSizeOneModuleRAM.Value;
        }

        private void numericUpDownSelectTotalSizeRAM_EnabledChanged(object sender, EventArgs e)
        {
            if (this.numericUpDownSelectTotalSizeRAM.Enabled == false)
                this.numericUpDownSelectTotalSizeRAM.Value = 0;
        }

        private void numericUpDownSetMinSizeRAM_EnabledChanged(object sender, EventArgs e)
        {
            if (this.numericUpDownSetMinSizeRAM.Enabled == false)
                this.numericUpDownSetMinSizeRAM.Value = 0;
        }

        private void numericUpDownSetMaxSizeRAM_EnabledChanged(object sender, EventArgs e)
        {
            if (this.numericUpDownSetMaxSizeRAM.Enabled == false)
                this.numericUpDownSetMaxSizeRAM.Value = 0;
        }

        private void numericUpDownSetSizeOneModuleRAM_EnabledChanged(object sender, EventArgs e)
        {
            if (this.numericUpDownSetSizeOneModuleRAM.Enabled == false)
                this.numericUpDownSetSizeOneModuleRAM.Value = 0;
        }

        #endregion


        private void buttonOnOFFCheckSSD_Click(object sender, EventArgs e)
        {
            if (CheckSSD) {
                this.CheckSSD = false;
                this.buttonOnOFFCheckSSD.Text = "OFF";
            }
            else
            {
                this.CheckSSD = true;
                this.buttonOnOFFCheckSSD.Text = "ON";
            }   
        }
    }
}
