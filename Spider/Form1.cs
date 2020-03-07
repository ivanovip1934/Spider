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
        bool firstStart = true;

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
            this.firstStart = true;
            showRes = new ShowResultSearch();
            this.apps = new List<App>();
            this.dataGridView1.DataSource = this.apps;
            this.unicapps = new BindingList<Appv2>();
            this.dataGridView2.DataSource = this.unicapps;


            this.ListApp.Items.Clear();
            this.ListPC.Items.Clear();
            string[] nameDirs = showRes.GetNameDirectories();
            this.ListApp.Items.AddRange(nameDirs);
            this.GetPC.Enabled = false;


            this.ListApp.Enabled = true;
        }

        private void GetPC_Click(object sender, EventArgs e)
        {

            
            this.ListPC.Items.Clear();
            showRes.GetPCApps();
            this.dataGridView1.DataSource = new List<App>();
            this.dataGridView2.DataSource = new BindingList<Appv2>();
            this.firstStart = true;

            this.unicapps = new BindingList<Appv2>(showRes.GetUnicVersion());
            this.dataGridView2.DataSource = this.unicapps;            
            foreach (KeyValuePair<string, List<App>> item in showRes.DicPCApps)
            {
                this.ListPC.Items.Add(item.Key);
            }
        }

        private void ListApp_SelectedIndexChanged(object sender, EventArgs e)
        {
            showRes.SetNameCurDir(this.ListApp.GetItemText(this.ListApp.SelectedItem));
            this.GetPC.Enabled = true;
        }

        private void ListPC_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.apps = showRes.DicPCApps[this.ListPC.GetItemText(this.ListPC.SelectedItem)];
            this.dataGridView1.DataSource = this.apps;           
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            //MessageBox.Show("Privet");
            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                this.labelUninstallStirng.Text = row.Cells[2].Value?.ToString();
                this.labelPathInRegistry.Text = row.Cells["PathInRegistry"].Value.ToString();                 
            }
        }

        private void dataGridView2_SelectionChanged(object sender, EventArgs e)
        {
            App testapp1 = new App();
            this.ListPC.Items.Clear();

            if (this.firstStart)
                this.firstStart = false;
            else
            {
                testapp1 = ((Appv2)dataGridView2.CurrentRow.DataBoundItem).AppItem;

                foreach (KeyValuePair<string, List<App>> item in showRes.DicPCApps)
                {

                    if (item.Value.Any(x=>x.IsSame(testapp1)))
                    {                        
                        this.ListPC.Items.Add(item.Key);
                    }

                }
            }
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
