using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WaveCraft.Custom_Controls
{
    class GradientListBox : ListBox
    {
        public GradientListBox()
        {
            this.SetStyle(
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.ResizeRedraw |
                ControlStyles.UserPaint,
                true);
            this.DrawMode = DrawMode.OwnerDrawFixed;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Region iRegion = new Region(e.ClipRectangle);
            using (LinearGradientBrush brush = new LinearGradientBrush(this.ClientRectangle,
                                                                       Color.FromArgb(70, 77, 95),
                                                                       Color.Black,
                                                                       0F))
            {
                e.Graphics.FillRegion(brush, iRegion);
            }

            try
            {
                for (int i = 0; i < this.Items.Count; ++i)
                {
                    Rectangle irect = this.GetItemRectangle(i);
                    OnDrawItem(new DrawItemEventArgs(e.Graphics, this.Font, irect, i, DrawItemState.Default, this.ForeColor, this.BackColor));
                }
            }
            catch(ArgumentException)
            {
                // sometime the rectangle is not defined
            }

            base.OnPaint(e);
        }

        protected override void OnSelectedIndexChanged(EventArgs e)
        {
                    this.Invalidate();
            base.OnSelectedIndexChanged(e);
        }

        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            if ((this.SelectionMode == SelectionMode.One && this.SelectedIndex == e.Index)
                || (this.SelectionMode == SelectionMode.MultiSimple && this.SelectedIndices.Contains(e.Index))
                || (this.SelectionMode == SelectionMode.MultiExtended && this.SelectedIndices.Contains(e.Index)))
            {
                e.Graphics.FillRectangle(Brushes.DodgerBlue, e.Bounds);
                e.Graphics.DrawString(this.Items[e.Index].ToString(), e.Font, Brushes.White, e.Bounds, StringFormat.GenericDefault);
            }
            else
            {
                using (LinearGradientBrush brush = new LinearGradientBrush(e.Bounds,
                                                   Color.FromArgb(70, 77, 95),
                                                   Color.Black,
                                                   0F))
                {
                    e.Graphics.FillRectangle(brush, e.Bounds);
                    e.Graphics.DrawString(this.Items[e.Index].ToString(), e.Font, Brushes.White, e.Bounds, StringFormat.GenericDefault);
                }
            }

            base.OnDrawItem(e);
        }

    }
}
