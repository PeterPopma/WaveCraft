using WaveCraft.Synth;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using System.Windows.Forms;

namespace WaveCraft
{
    public partial class FormCustomShape : Form
    {
        private FormMain myParent = null;
        private int[] WaveData = new int[SynthGenerator.SHAPE_NUMPOINTS];
        bool isMouseButtonDown = false;
        Point previousPoint;
        Timer aTimer = new Timer();
        int AdjustDataWidth = 0;


        public FormCustomShape()
        {
            InitializeComponent();
            aTimer.Interval = 100;
            aTimer.Tick += new EventHandler(TimerEventProcessor);
        }

        public FormMain MyParent { get => myParent; set => myParent = value; }

        private void PictureBoxPaint(object sender, PaintEventArgs e)
        {
            Control control = (Control)sender;

            using (LinearGradientBrush brush = new LinearGradientBrush(control.ClientRectangle,
                                                                       Color.FromArgb(195, 70, 70),
                                                                       Color.FromArgb(15, 0, 0),
                                                                       90F))
            {
                e.Graphics.FillRectangle(brush, control.ClientRectangle);
                ControlPaint.DrawBorder(e.Graphics, control.ClientRectangle, Color.Gray, ButtonBorderStyle.Solid);
                DrawGraph(e);
            }
        }

        private void DrawGraph(PaintEventArgs e)
        {
            Pen pen = new Pen(Color.White);
            for (int i = 0; i < WaveData.Length - 1; i++)
            {
                e.Graphics.DrawLine(pen, new Point(i, WaveData[i]), new Point(i + 1, WaveData[i + 1]));
            }
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            if (this.ClientRectangle.Width == 0 || this.ClientRectangle.Height == 0)
            {
                return;
            }
            using (LinearGradientBrush brush = new LinearGradientBrush(this.ClientRectangle,
                                                                       Color.Black,
                                                                       Color.FromArgb(70, 77, 95),
                                                                       90F))
            {
                e.Graphics.FillRectangle(brush, this.ClientRectangle);
            }
        }

        private void TimerEventProcessor(Object myObject,
                           EventArgs myEventArgs)
        {
            AdjustDataWidth++;

            int begin_x = previousPoint.X - AdjustDataWidth;
            if (begin_x < 0)
            {
                begin_x = 0;
            }
            int x_position = begin_x + 1;
            int end_x = previousPoint.X;
            int begin_y = WaveData[begin_x];
            int end_y = previousPoint.Y;

            // adjust all points left of mouse pointer
            while (x_position < end_x)
            {
                int interpolated_value = (((x_position - begin_x) * end_y) + ((end_x - x_position) * begin_y)) / (end_x - begin_x);
                WaveData[x_position] = (WaveData[x_position] + interpolated_value) / 2;
                x_position++;
            }

            begin_x = previousPoint.X;
            x_position = begin_x + 1;
            end_x = begin_x + AdjustDataWidth;
            if (end_x > SynthGenerator.SHAPE_NUMPOINTS - 1)
            {
                end_x = SynthGenerator.SHAPE_NUMPOINTS - 1;
            }
            begin_y = previousPoint.Y;
            end_y = WaveData[end_x];

            // adjust all points right of mouse pointer
            while (x_position < end_x)
            {
                int interpolated_value = (((x_position - begin_x) * end_y) + ((end_x - x_position) * begin_y)) / (end_x - begin_x);
                WaveData[x_position] = (WaveData[x_position] + interpolated_value) / 2;
                x_position++;
            }

            Refresh();
        }

        private void FormCustomWave_Load(object sender, EventArgs e)
        {
            int[] shape = myParent.SynthGenerator.CurrentWave.ShapeWave;
            if (Text.Equals("Wave Shape End"))
            {
                shape = myParent.SynthGenerator.CurrentWave.ShapeWaveEnd;
                pictureBoxCopyFromBegin.Visible = true;
            }
            else
            {
                pictureBoxCopyFromBegin.Visible = false;
            }

            if (shape.Length < SynthGenerator.SHAPE_NUMPOINTS)
            {
                for (int i = 0; i < WaveData.Length; i++)
                {
                    WaveData[i] = SynthGenerator.SHAPE_MAX_VALUE / 2;
                }
            }
            else
            {
                for (int i = 0; i < WaveData.Length; i++)
                {
                    WaveData[i] = SynthGenerator.SHAPE_MAX_VALUE - shape[i];
                }
            }

            pictureBoxCustomWave.Paint += new PaintEventHandler(PictureBoxPaint);
            pictureBoxCustomWave.Refresh();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void buttonApply_Click(object sender, EventArgs e)
        {
            int[] shape = new int[SynthGenerator.SHAPE_NUMPOINTS];

            for (int i = 0; i < WaveData.Length; i++)
            {
                // Note that graph is upside-down
                shape[i] = SynthGenerator.SHAPE_MAX_VALUE - WaveData[i];
            }
            if (Text.Equals("Wave Shape End"))
            {
                myParent.SynthGenerator.CurrentWave.ShapeWaveEnd = shape;
                myParent.pictureBoxCustomWaveEnd.Refresh();
            }
            else
            {
                myParent.SynthGenerator.CurrentWave.ShapeWave = shape;
                myParent.pictureBoxCustomWave.Refresh();
            }

            myParent.SynthGenerator.UpdateCurrentWaveData();

            Close();
        }

        private void pictureBoxCustomWave_MouseDown(object sender, MouseEventArgs e)
        {
            if (!isMouseButtonDown && e.X >= 0 && e.X < SynthGenerator.SHAPE_NUMPOINTS)
            {
                int mouseY = e.Y;
                if (mouseY > SynthGenerator.SHAPE_MAX_VALUE)
                {
                    mouseY = SynthGenerator.SHAPE_MAX_VALUE;
                }
                if (mouseY < 0)
                {
                    mouseY = 0;
                }
                WaveData[e.X] = mouseY;
                Refresh();
                previousPoint.X = e.X;
                previousPoint.Y = mouseY;
                isMouseButtonDown = true;
                AdjustDataWidth = 1;
                aTimer.Enabled = true;
            }
        }

        private void pictureBoxCustomWave_MouseMove(object sender, MouseEventArgs e)
        {
            if (isMouseButtonDown && e.X != previousPoint.X && e.X >= 0 && e.X < SynthGenerator.SHAPE_NUMPOINTS)
            {
                int mouseY = e.Y;
                if (mouseY > SynthGenerator.SHAPE_MAX_VALUE)
                {
                    mouseY = SynthGenerator.SHAPE_MAX_VALUE;
                }
                if (mouseY < 0)
                {
                    mouseY = 0;
                }
                EditData(e.X, mouseY);
                previousPoint.X = e.X;
                previousPoint.Y = mouseY;
                Refresh();
            }
        }

        private void pictureBoxCustomWave_MouseUp(object sender, MouseEventArgs e)
        {
            isMouseButtonDown = false;
            aTimer.Enabled = false;
        }

        // Fill all data from previous point to current point with interpolated values
        private void EditData(int X, int Y)
        {
            double increment = (previousPoint.Y - Y) / Math.Abs(X - previousPoint.X);
            int position = X;
            double value = Y;
            while (position != previousPoint.X)
            {
                WaveData[position] = (int)value;
                value += increment;
                if (X > previousPoint.X)
                {
                    position--;
                }
                else
                {
                    position++;
                }
            }
        }

        private void pictureBoxSine_Click(object sender, EventArgs e)
        {
            Shapes.Sines(WaveData);
            Refresh();
        }

        private void pictureBoxTriangle_Click(object sender, EventArgs e)
        {
            Shapes.IncDec(WaveData);
            Refresh();
        }

        private void pictureBoxSquare_Click(object sender, EventArgs e)
        {
            Shapes.Square(WaveData);
            Refresh();
        }

        private void pictureBoxSawtooth_Click(object sender, EventArgs e)
        {
            Shapes.IncreasingLineair(WaveData);
            Refresh();
        }

        private void pictureBoxFlat_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < WaveData.Length; i++)
            {
                WaveData[i] = SynthGenerator.SHAPE_MAX_VALUE / 2;
            }
            Refresh();
        }

        private void pictureBoxCopyFromBegin_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < myParent.SynthGenerator.CurrentWave.ShapeWave.Length; i++)
            {
                WaveData[i] = SynthGenerator.SHAPE_MAX_VALUE - myParent.SynthGenerator.CurrentWave.ShapeWave[i];
            }
            Refresh();
        }
    }
}
