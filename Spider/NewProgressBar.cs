using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Spider
{
    public class ColorProgressBar : ProgressBar
    {
        public Brush ColorBar { get; set; }
        public ColorProgressBar()
        {
            this.SetStyle(ControlStyles.UserPaint, true);
            this.ColorBar = Brushes.Lime;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            
            Rectangle rec = e.ClipRectangle;

            rec.Width = (int)(rec.Width * ((double)Value / Maximum)) - 4;
            if (ProgressBarRenderer.IsSupported)
                ProgressBarRenderer.DrawHorizontalBar(e.Graphics, e.ClipRectangle);
            rec.Height = rec.Height - 4;
            e.Graphics.FillRectangle(this.ColorBar, 2, 2, rec.Width, rec.Height);
        }
    }
}
