using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace Spider
{
    public partial class form1 : Form
    {

        ShowResultSearch showRes = new ShowResultSearch();
        List<App> apps = new List<App>();
        BindingList<Appv2> unicapps = new BindingList<Appv2>();        

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

            //showRes.GetPCApps(); 
            ClearDatafromControls();
            this.unicapps = new BindingList<Appv2>();
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
                this.labelUninstallStirng.Text = row.Cells[2].Value?.ToString();
                this.labelPathInRegistry.Text = row.Cells["PathInRegistry"].Value.ToString();
                this.labelUninstallStirng.Visible = true;
                this.labelPathInRegistry.Visible = true;
            }
        }

        private void dataGridView2_SelectionChanged(object sender, EventArgs e)
        {

            App testapp1 = new App();
            this.ListPC.Items.Clear();

            //if (this.firstStart)
            //    this.firstStart = false;
            //else
            //{
            foreach (DataGridViewRow row in dataGridView2.SelectedRows)
            {
                testapp1 = new App(row.Cells["AppName1"].Value?.ToString(),
                    row.Cells["appVersion1"].Value?.ToString(),
                    row.Cells["appUninstallString1"].Value?.ToString(),
                    row.Cells["appInstallLocation1"].Value?.ToString(),
                    row.Cells["appPublisher1"].Value?.ToString(),
                    row.Cells["appPathInRegistry1"].Value?.ToString()
                    );
            }

            this.labelUnstallString2.Text = testapp1.UninstallString;
            this.labelPathInRegistry2.Text = testapp1.PathInRegistry;
            this.labelInstallLocation.Text = testapp1.InstallLocation;
            this.labelPathInRegistry2.Visible = true;
            this.labelUnstallString2.Visible = true;
            this.labelInstallLocation.Visible = true;

            foreach (KeyValuePair<string, List<App>> item in showRes.DicPCApps)
            {

                if (item.Value.Any(x => x.IsSame(testapp1)))
                {
                    this.ListPC.Items.Add(item.Key);
                }

            }
            //}

            //    this.AppName1,
            //this.appVersion1,
            //this.appInstallLocation1,
            //this.appUninstallString1,
            //this.appPublisher1,
            //this.appPathInRegistry1,
            //this.appCount});


            //    foreach (KeyValuePair<string, List<App>> item in showRes.DicPCApps)
            //    {

            //        if (item.Value.Any(x=>x.IsSame(testapp1)))
            //        {                        
            //            this.ListPC.Items.Add(item.Key);
            //        }

            //    }
            //}
        }

        private void dataGridView2_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dataGridView2.Rows[e.RowIndex].DataBoundItem != null && dataGridView2.Columns[e.ColumnIndex].DataPropertyName.Contains(".")) {

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

        public void ClearDatafromControls() {
            this.ListPC.Items.Clear(); //3
            this.ListPC.Items.Clear(); //1
            this.dataGridView1.DataSource = new List<App>(); //2
            this.dataGridView2.Rows.Clear(); //4   
            this.labelPathInRegistry2.Visible = false; //5
            this.labelUnstallString2.Visible = false;//6
            this.labelUninstallStirng.Visible = false;
            this.labelPathInRegistry.Visible = false;
        }

        
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
