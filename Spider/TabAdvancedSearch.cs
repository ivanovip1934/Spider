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
        Dictionary<string, ComputerInfo> filteredComputers;
        ComputerInfo FilteredPC;
        List<RadioButton> controls = new List<RadioButton>();
        private bool CheckSSD = true;
        private bool IsX64OS = true;

        private void buttonTab3GetPC_Click(object sender, EventArgs e)
        {
            showPC = new HWInfoGeneral();
            dicPC = showPC.GetDicPC();

            #region Add data to comboBoxTab3ListCPU
            this.comboBoxTab3ListCPU.Items.Clear();
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
            this.comboBoxTab3ListMainBoard.Items.Clear();
            var queryMainBoard = dicPC.OrderBy(x => x.Value.MainBoard.Product).
                GroupBy(x => x.Value.MainBoard.Product,
                    (modelMainBoard, cpus) => new { ModelMainBoard = modelMainBoard, Count = cpus.Count() });
            foreach (var item in queryMainBoard)
            {
                this.comboBoxTab3ListMainBoard.Items.Add(item.ModelMainBoard);
            }
            this.comboBoxTab3ListMainBoard.SelectedIndex = -1;
            #endregion

            #region Add data to comboBoxTab3ListSizeSSD

            this.comboBoxTab3ListSizeSSD.Items.Clear();
            this.comboBoxTab3ListSizeSSD.Items.AddRange(filtermethods.DicSizeSSD.Keys.ToArray());

            #endregion

            #region Add date to comboBoxTab3ListVersionOS
            this.comboBoxTab3ListBuildOS.Items.Clear();
            this.comboBoxTab3ListBuildOS.Items.AddRange(dicPC.Where(x => x.Value.OS.Version == "10").Select(x => x.Value.OS.Build).Distinct().OrderBy(x => x).ToArray());

            #endregion

            #region Add date to comboBoxTab3ListSizeMonitor
            this.comboBoxTab3ListSizeMonitor.Items.Clear();
            //this.comboBoxTab3ListSizeMonitor.Items.AddRange(dicPC.SelectMany(x => x.Value.Monitors).Select(x => x.PanelSize).Distinct().OrderBy(x => x).ToArray());
            List<string> ArrSizeMonitors = new List<string>();
            foreach (MonitorInfo monitor in dicPC.SelectMany(x => x.Value.Monitors)) {
                if (monitor.PanelSize != null)
                    ArrSizeMonitors.Add(monitor.PanelSize.ToString());
            }

            ArrSizeMonitors = ArrSizeMonitors.Distinct().OrderBy(x => x).ToList();
            this.comboBoxTab3ListSizeMonitor.Items.AddRange(ArrSizeMonitors.Distinct().OrderBy(x => x).ToArray());

            #endregion

            this.checkBoxTab3SearchByCPU.Enabled = true;
            this.checkBoxTab3SearchByMainBoard.Enabled = true;
            this.checkBoxTab3SearchByRAM.Enabled = true;
            this.checkBoxTab3SearchBySSD.Enabled = true;
            this.checkBoxTab3SearchByOS.Enabled = true;
            this.checkBoxTab3SearchByMonitor.Enabled = true;


        }

        private void checkBoxTab3SearchByCPU_CheckedChanged(object sender, EventArgs e)
        {
            if (controls.Count == 0)
            {
                controls = new List<RadioButton>() {
            this.radioButtonSelectCPUFromList,
            this.radioButtonUseCPUPattern,
            };
            }


            if (this.checkBoxTab3SearchByCPU.Checked == true)
            {
                ChangeControlEnable(controls, true);
                radioButtonSelectCPUFromList_CheckedChanged(sender, e);
                radioButtonUseCPUPattern_CheckedChanged(sender, e);
            }
            else
                ChangeControlEnable(controls, false);

        }

        private static void ChangeControlEnable(List<RadioButton> controls, bool flag)
        {

            if (flag)
            {
                controls.ForEach(x => x.Enabled = true);                

            }
            else { 
                controls.ForEach(x => x.Enabled = false);
                controls.ForEach(x => x.Checked = false);
            }

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
            this.listBoxTab3FilteredPCNames.Items.Clear();

            if (filtercomputer != null)
            {
                bool flag = true;
                filteredComputers = dicPC.Where(x => (flag = true) == true && filtercomputer(x.Value, ref flag)).ToDictionary(x => x.Key, x => x.Value);
                this.listBoxTab3FilteredPCNames.Items.AddRange(filteredComputers.Keys.ToArray());
                this.labelTab3ViewListFilteredARM.Text = filteredComputers.Count().ToString();
            }
            else
            {
                this.labelTab3ViewListFilteredARM.Text = "";
                MessageBox.Show("Not filter SET");
            }
            this.labelTab3ViewListFilteredARM.Visible = true;
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

        #region Search by SSD

        private void checkBoxTab3SearchBySSD_CheckedChanged(object sender, EventArgs e)
        {
            if (this.checkBoxTab3SearchBySSD.Checked == true)
            {
                this.radioButtonExistsSSD.Enabled = true;
                this.radioButtonSizeSSD.Enabled = true;
            }
            else {
                this.radioButtonExistsSSD.Enabled = false;
                this.radioButtonSizeSSD.Enabled = false;
                this.radioButtonExistsSSD.Checked = false;
                this.radioButtonSizeSSD.Checked = false;
            }
        }

        private void radioButtonExistsSSD_CheckedChanged(object sender, EventArgs e)
        {
            if (this.radioButtonExistsSSD.Checked == true)
            {
                this.buttonOnOFFCheckSSD.Enabled = true;
                this.filtercomputer += filtermethods.FilterByExistsSSD;

            }
            else
            {
                this.buttonOnOFFCheckSSD.Enabled = false;
                this.filtercomputer -= filtermethods.FilterByExistsSSD;
            }
        }

        private void radioButtonSizeSSD_CheckedChanged(object sender, EventArgs e)
        {
            if (this.radioButtonSizeSSD.Checked == true)
            {
                this.comboBoxTab3ListSizeSSD.Enabled = true;
                this.filtercomputer += filtermethods.FilterBySizeSSD;
            }
            else {
                this.comboBoxTab3ListSizeSSD.Enabled = false;
                this.filtercomputer -= filtermethods.FilterBySizeSSD;
            }
        }

        private void radioButtonExistsSSD_EnabledChanged(object sender, EventArgs e)
        {
            if (this.radioButtonExistsSSD.Enabled == false)
                this.buttonOnOFFCheckSSD.Enabled = false;
        }

        private void radioButtonSizeSSD_EnabledChanged(object sender, EventArgs e)
        {
            if (this.radioButtonSizeSSD.Enabled == false)
                this.comboBoxTab3ListSizeSSD.Enabled = false;
        }

        private void buttonOnOFFCheckSSD_EnabledChanged(object sender, EventArgs e)
        {
            if (this.buttonOnOFFCheckSSD.Enabled == true) {
                this.CheckSSD = true;
                this.buttonOnOFFCheckSSD.Text = "YES";
                filtermethods.ExistsSSD = this.CheckSSD;
            }
        }

        private void comboBoxTab3ListSizeSSD_EnabledChanged(object sender, EventArgs e)
        {
            if (this.comboBoxTab3ListSizeSSD.Enabled == false)
                this.comboBoxTab3ListSizeSSD.SelectedIndex = -1;
        }

        private void comboBoxTab3ListSizeSSD_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.comboBoxTab3ListSizeSSD.SelectedIndex != -1)
                filtermethods.SizeSSD = this.comboBoxTab3ListSizeSSD.SelectedItem.ToString();
            else
                filtermethods.SizeSSD = String.Empty;
        }


        private void buttonOnOFFCheckSSD_Click(object sender, EventArgs e)
        {
            if (CheckSSD) {
                this.CheckSSD = false;
                this.buttonOnOFFCheckSSD.Text = "NO";
            }
            else
            {
                this.CheckSSD = true;
                this.buttonOnOFFCheckSSD.Text = "YES";

            }
            filtermethods.ExistsSSD = this.CheckSSD;
        }



        private void panelTab3ViewRAM_SizeChanged(object sender, EventArgs e)
        {
            RelocationPanel(this.panelTab3ViewRAM, this.panelTab3ViewStorage);
        }

        private void panelTab3ViewStorage_SizeChanged(object sender, EventArgs e)
        {
            RelocationPanel(this.panelTab3ViewStorage, this.panelTab3ViewMonitor);
        }

        private void panelTab3ViewStorage_LocationChanged(object sender, EventArgs e)
        {
            RelocationPanel(this.panelTab3ViewStorage, this.panelTab3ViewMonitor);
        }


        public void ResizePanel(Panel panel, int count) {

            int X = panel.Width;
            int Y = panel.Height;
            if (count == 1)
                panel.Height = Y + 5;
            else
                panel.Height = Y + count * 16;
        }

        public void RelocationPanel(Panel leadpanel, Panel slavepanel) {

            int X = leadpanel.Location.X;
            int Y = leadpanel.Location.Y + leadpanel.Height;

            slavepanel.Location = new System.Drawing.Point(X, Y);
        }


        #endregion


        #region Search by Operating System

        private void checkBoxTab3SearchByOS_CheckedChanged(object sender, EventArgs e)
        {
            if (this.checkBoxTab3SearchByOS.Checked == true)
            {
                this.checkBoxTab3SelectSearchByVersionOS.Enabled = true;
                //this.checkBoxTab3SelectSearchByBuildOS.Enabled = true;
                this.checkBoxTab3SelectSearchByBitOS.Enabled = true;
                this.checkBoxTab3SelectSearchByDateInstalledOS.Enabled = true;
            }
            else {
                this.checkBoxTab3SelectSearchByVersionOS.Enabled = false;
                //this.checkBoxTab3SelectSearchByBuildOS.Enabled = false;
                this.checkBoxTab3SelectSearchByBitOS.Enabled = false;
                this.checkBoxTab3SelectSearchByDateInstalledOS.Enabled = false;
            }

        }

        private void Control_General_EnabledChanged(object sender, EventArgs e)
        {
            if (sender is CheckBox)
                if (((CheckBox)sender).Enabled == false)
                    ((CheckBox)sender).Checked = false;
            if (sender is RadioButton)
                if (((RadioButton)sender).Enabled == false)
                    ((RadioButton)sender).Checked = false;
            if (sender is ComboBox) {
                if (((ComboBox)sender).Enabled == false)
                    ((ComboBox)sender).SelectedIndex = -1;
            }
        }

        private void checkBoxTab3SelectSearchByVersionOS_CheckedChanged(object sender, EventArgs e)
        {
            if (this.checkBoxTab3SelectSearchByVersionOS.Checked == true)
            {
                this.comboBoxTab3ListVersionOS.Enabled = true;
                this.filtercomputer += filtermethods.FilterByVersionOS;
            }
            else
            {
                this.comboBoxTab3ListVersionOS.Enabled = false;
                this.filtercomputer -= filtermethods.FilterByVersionOS;
            }
        }

        private void checkBoxTab3SelectSearchByBuildOS_CheckedChanged(object sender, EventArgs e)
        {
            if (this.checkBoxTab3SelectSearchByBuildOS.Checked == true)
            {
                this.comboBoxTab3ListBuildOS.Enabled = true;
                this.filtercomputer += filtermethods.FilterByBuildOS;
            }
            else
            {
                this.comboBoxTab3ListBuildOS.Enabled = false;
                this.filtercomputer -= filtermethods.FilterByBuildOS;
            }
        }

        private void checkBoxTab3SelectSearchByBitOS_CheckedChanged(object sender, EventArgs e)
        {
            if (this.checkBoxTab3SelectSearchByBitOS.Checked == true) {

                this.buttonSelectBitOS.Enabled = true;
                this.filtercomputer += filtermethods.FilterByBitOS;
            }

            else
            {
                this.buttonSelectBitOS.Enabled = false;
                this.filtercomputer -= filtermethods.FilterByBitOS;
            }
        }

        private void checkBoxTab3SelectSearchByDateInstalledOS_CheckedChanged(object sender, EventArgs e)
        {
            if (this.checkBoxTab3SelectSearchByDateInstalledOS.Checked == true)
            {
                this.dateTimePickerTab3SetStartInstalledOS.Enabled = true;
                this.dateTimePickerTab3SetEndInstalledOS.Enabled = true;
                this.filtercomputer += filtermethods.FilterByInstallDateOS;
            }
            else {
                this.dateTimePickerTab3SetStartInstalledOS.Enabled = false;
                this.dateTimePickerTab3SetEndInstalledOS.Enabled = false;
                this.filtercomputer -= filtermethods.FilterByInstallDateOS;
            }
        }


        private void comboBoxTab3ListVersionOS_EnabledChanged(object sender, EventArgs e)
        {
            if (this.comboBoxTab3ListVersionOS.Enabled == false)
            {
                this.comboBoxTab3ListVersionOS.SelectedIndex = -1;
                this.comboBoxTab3ListBuildOS.Enabled = false;
                this.checkBoxTab3SelectSearchByBuildOS.Enabled = false;
            }


        }

        private void comboBoxTab3ListBuildOS_EnabledChanged(object sender, EventArgs e)
        {
            if (this.comboBoxTab3ListBuildOS.Enabled == false)
                this.comboBoxTab3ListBuildOS.SelectedIndex = -1;
        }

        private void buttonSelectBitOS_EnabledChanged(object sender, EventArgs e)
        {
            if (this.buttonSelectBitOS.Enabled == true)
            {
                this.IsX64OS = true;
                this.buttonSelectBitOS.Text = "x64";
                this.filtermethods.IsX64OS = this.IsX64OS;
            }

        }
        private void buttonSelectBitOS_Click(object sender, EventArgs e)
        {
            if (this.IsX64OS)
            {
                this.IsX64OS = false;
                this.buttonSelectBitOS.Text = "x86";
            }
            else {
                this.IsX64OS = true;
                this.buttonSelectBitOS.Text = "x64";
            }
            this.filtermethods.IsX64OS = this.IsX64OS;
        }

        private void dateTimePickerTab3SetStartInstalledOS_EnabledChanged(object sender, EventArgs e)
        {
            if (this.dateTimePickerTab3SetStartInstalledOS.Enabled == false)
                this.dateTimePickerTab3SetStartInstalledOS.Value = DateTime.Today;
        }

        private void dateTimePickerTab3SetEndInstalledOS_EnabledChanged(object sender, EventArgs e)
        {
            if (this.dateTimePickerTab3SetEndInstalledOS.Enabled == false)
                this.dateTimePickerTab3SetEndInstalledOS.Value = DateTime.Today;
        }


        private void comboBoxTab3ListVersionOS_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (this.comboBoxTab3ListVersionOS.SelectedIndex != -1)
            {
                string versionOS = this.comboBoxTab3ListVersionOS.SelectedItem.ToString();
                this.filtermethods.VersionOS = versionOS;
                if (versionOS == "Windows 10")
                    this.checkBoxTab3SelectSearchByBuildOS.Enabled = true;
                else
                    this.checkBoxTab3SelectSearchByBuildOS.Enabled = false;

            }
            else
            {
                this.filtermethods.VersionOS = "";
                this.checkBoxTab3SelectSearchByBuildOS.Enabled = false;

            }


        }

        private void comboBoxTab3ListBuildOS_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.comboBoxTab3ListBuildOS.SelectedIndex != -1)
            {
                this.filtermethods.BuildOS = this.comboBoxTab3ListBuildOS.SelectedItem.ToString();
            }
            else
            {
                this.filtermethods.BuildOS = "";
            }
        }

        private void dateTimePickerTab3SetStartInstalledOS_ValueChanged(object sender, EventArgs e)
        {
            this.filtermethods.RangeInstallDateOS["StartInstallDateOS"] = this.dateTimePickerTab3SetStartInstalledOS.Value;
        }

        private void dateTimePickerTab3SetEndInstalledOS_ValueChanged(object sender, EventArgs e)
        {
            this.filtermethods.RangeInstallDateOS["EndInstallDateOS"] = this.dateTimePickerTab3SetEndInstalledOS.Value;
        }

        #endregion

        #region Filtered by Monitor


        private void checkBoxTab3SearchByMonitor_CheckedChanged(object sender, EventArgs e)
        {
            if (this.checkBoxTab3SearchByMonitor.Checked == true)
            {
                this.radioButtonRangeSizeMonitor.Enabled = true;
                this.radioButtonSizeMonitor.Enabled = true;
            }
            else {
                this.radioButtonRangeSizeMonitor.Enabled = false;
                this.radioButtonSizeMonitor.Enabled = false;

            }
        }

        private void radioButtonSizeMonitor_CheckedChanged(object sender, EventArgs e)
        {
            if (this.radioButtonSizeMonitor.Checked == true)
            {
                this.comboBoxTab3ListSizeMonitor.Enabled = true;
                this.filtercomputer += filtermethods.FilterBySizeMonitor;
            }
            else {
                this.comboBoxTab3ListSizeMonitor.Enabled = false;
                this.filtercomputer -= filtermethods.FilterBySizeMonitor;
            }
        }

        private void radioButtonRangeSizeMonitor_CheckedChanged(object sender, EventArgs e)
        {
            if (this.radioButtonRangeSizeMonitor.Checked == true)
            {
                this.textBoxTab3PatternSizeMonitor.Enabled = true;
                this.filtercomputer += filtermethods.FilterByRangeSizeMonitor;

            }
            else {
                this.textBoxTab3PatternSizeMonitor.Enabled = false;
                this.filtercomputer -= filtermethods.FilterByRangeSizeMonitor;
            }
        }

        private void radioButtonSizeMonitor_EnabledChanged(object sender, EventArgs e)
        {
            if (this.radioButtonSizeMonitor.Enabled == false)            
                this.radioButtonSizeMonitor.Checked = false;
        }

        private void radioButtonRangeSizeMonitor_EnabledChanged(object sender, EventArgs e)
        {
            if (this.radioButtonRangeSizeMonitor.Enabled == false)
            {
                this.radioButtonRangeSizeMonitor.Checked = false;
            }
        }

        private void comboBoxTab3ListSizeMonitor_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.comboBoxTab3ListSizeMonitor.SelectedIndex != -1)
            {
                this.filtermethods.PanelSize = this.comboBoxTab3ListSizeMonitor.SelectedItem.ToString();
            }
            else
            {
                this.filtermethods.PanelSize = String.Empty;
            }
        }

        private void comboBoxTab3ListSizeMonitor_EnabledChanged(object sender, EventArgs e)
        {
            if (this.comboBoxTab3ListSizeMonitor.Enabled == false)
                this.comboBoxTab3ListSizeMonitor.SelectedIndex = -1;
        }

        private void textBoxTab3PatternSizeMonitor_TextChanged(object sender, EventArgs e)
        {
            if (this.textBoxTab3PatternSizeMonitor.Text.Trim() != String.Empty)
                filtermethods.RangePanelSize = this.textBoxTab3PatternSizeMonitor.Text;
            else
                filtermethods.RangePanelSize = String.Empty;
        }
        private void textBoxTab3PatternSizeMonitor_EnabledChanged(object sender, EventArgs e)
        {
            if (this.textBoxTab3PatternSizeMonitor.Enabled == false)
                this.textBoxTab3PatternSizeMonitor.Text = String.Empty;
        }
        #endregion


        private void listBoxTab3FilteredPCNames_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.listBoxTab3FilteredPCNames.SelectedIndex != -1)
            {
                this.FilteredPC = this.filteredComputers?[this.listBoxTab3FilteredPCNames.SelectedItem.ToString()];
                this.labelTab3ViewPCName.Text = this.FilteredPC.Name;
                this.labelTab3ViewVersionOS.Text = this.FilteredPC.OS.ProductName;
                this.labelTab3ViewBuildOS.Text = this.FilteredPC.OS.Build;
                this.labelTab3ViewPlatformOS.Text = this.FilteredPC.OS.IsX64 ? "x64" : "x86";
                this.labelTab3ViewInstallDateOS.Text = $"{this.FilteredPC.OS.InstallDate.Year}" +
                                                       $"-{this.FilteredPC.OS.InstallDate.Month}" +
                                                       $"-{this.FilteredPC.OS.InstallDate.Day}";
                this.labelTab3ViewModelMainBoard.Text = this.FilteredPC.MainBoard.Product;
                this.labelTab3ViewSNMainboard.Text = this.FilteredPC.MainBoard.SerialNumber;
                this.labelTab3ViewManufacturerMainBoard.Text = this.FilteredPC.MainBoard.Manufacturer;
                this.labelTab3ViewVersionBioslMainBoard.Text = this.FilteredPC.MainBoard.SMBIOSBIOSVersion;
                if (this.FilteredPC.CPU.Name.Contains("Intel") || this.FilteredPC.CPU.Name.Contains("Pentium"))
                    this.labelTab3ViewModelCPU.ForeColor = System.Drawing.Color.Blue;
                else
                    this.labelTab3ViewModelCPU.ForeColor = System.Drawing.Color.Red;
                this.labelTab3ViewModelCPU.Text = this.FilteredPC.CPU.Name;
                this.labelTab3ViewTotalSizeRAM.Text = this.FilteredPC.Memory.Sum(x => x.Capacity).ToString();
                int count = this.FilteredPC.Memory.Count;
                this.panelTab3ViewRAM.Size = new System.Drawing.Size(395, 85);
                ResizePanel(this.panelTab3ViewRAM, count);

                ViewRAM(this.FilteredPC.Memory, this.panelTab3ViewRAM, this.labelTab3TitleViewRAM);
                ViewListDisk(this.FilteredPC.Storage, this.panelTab3ViewStorage, this.labelTab3TitleStorage);
                ViewMonitor(this.FilteredPC.Monitors, this.panelTab3ViewMonitor, this.labelTab3TitleMonitor);







                this.labelTab3ViewPCName.Visible = true;
                this.labelTab3ViewVersionOS.Visible = true;
                this.labelTab3ViewBuildOS.Visible = true;
                this.labelTab3ViewPlatformOS.Visible = true;
                this.labelTab3ViewModelMainBoard.Visible = true;
                this.labelTab3ViewManufacturerMainBoard.Visible = true;
                this.labelTab3ViewVersionBioslMainBoard.Visible = true;
                this.labelTab3ViewModelCPU.Visible = true;
                this.labelTab3ViewTotalSizeRAM.Visible = true;
                this.labelTab3ViewSNMainboard.Visible = true;
                this.labelTab3ViewInstallDateOS.Visible = true;

            }
        }
    }
}