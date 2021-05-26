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
    public partial class FormPhase : Form
    {
        private FormMain myParent = null;
        private int[] WaveData = new int[SynthGenerator.SHAPE_NUMPOINTS];
        bool isMouseButtonDown = false;
        Point previousPoint;
        Timer aTimer = new Timer();
        int AdjustDataWidth = 0;
        Random random = new Random();

        public FormPhase()
        {
            InitializeComponent();
            aTimer.Interval = 50;
            aTimer.Tick += new EventHandler(TimerEventProcessor);
        }

        public FormMain MyParent { get => myParent; set => myParent = value; }

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

        private void buttonApply_Click(object sender, EventArgs e)
        {
            myParent.SynthGenerator.CurrentWave.ShapePhase = new int[SynthGenerator.SHAPE_NUMPOINTS];
            for (int i = 0; i < WaveData.Length; i++)
            {
                // Note that graph is upside-down
                myParent.SynthGenerator.CurrentWave.ShapePhase[i] = SynthGenerator.SHAPE_MAX_VALUE - WaveData[i];
            }
            myParent.pictureBoxPhaseShape.Refresh();
            myParent.UpdateWaveControls();
            myParent.SynthGenerator.UpdateCurrentWaveData();

            Close();
        }

        private void PictureBoxPaint(object sender, PaintEventArgs e)
        {
            Control control = (Control)sender;

            using (LinearGradientBrush brush = new LinearGradientBrush(control.ClientRectangle,
                                                                       Color.FromArgb(120, 87, 195),
                                                                       Color.FromArgb(0, 0, 15),
                                                                       90F))
            {
                e.Graphics.FillRectangle(brush, control.ClientRectangle);
                ControlPaint.DrawBorder(e.Graphics, control.ClientRectangle, Color.Gray, ButtonBorderStyle.Solid);
                DrawGraph(e);
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

        private void DrawGraph(PaintEventArgs e)
        {
            Pen pen = new Pen(Color.White);
            for (int i = 0; i < WaveData.Length - 1; i++)
            {
                int y1 = WaveData[i];
                int y2 = WaveData[i + 1];
                if (y1 > pictureBoxPhaseShape.Height - 2)
                {
                    y1 = pictureBoxPhaseShape.Height - 2;
                }
                if (y2 > pictureBoxPhaseShape.Height - 2)
                {
                    y2 = pictureBoxPhaseShape.Height - 2;
                }
                e.Graphics.DrawLine(pen, new Point(i, y1), new Point(i + 1, y2));
            }
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

        private void FormVolume_Load(object sender, EventArgs e)
        {
            if (myParent.SynthGenerator.CurrentWave.ShapePhase.Length < SynthGenerator.SHAPE_NUMPOINTS)     // No data yet
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
                    WaveData[i] = SynthGenerator.SHAPE_MAX_VALUE - myParent.SynthGenerator.CurrentWave.ShapePhase[i];
                }
            }

            pictureBoxPhaseShape.Paint += new PaintEventHandler(PictureBoxPaint);
            pictureBoxPhaseShape.Refresh();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void buttonSine_Click(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < WaveData.Length; i++)
                {
                    WaveData[i] = (int)(((int)(Math.Sin(i / (double)WaveData.Length * 2 * Convert.ToInt32(textBoxNumSines.Text) * Math.PI) * SynthGenerator.SHAPE_MAX_VALUE + SynthGenerator.SHAPE_MAX_VALUE)) / 2.0);
                }
            }
            catch (Exception)
            {
                // probably bad input from textbox; ignore
            }

            Refresh();
        }

        private void buttonFlat_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < WaveData.Length; i++)
            {
                WaveData[i] = (int)(SynthGenerator.SHAPE_MAX_VALUE / 2.0);
            }
            Refresh();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            try
            {
                int value = SynthGenerator.SHAPE_MAX_VALUE - (int)(SynthGenerator.SHAPE_MAX_VALUE * Convert.ToDouble(textBoxFlatValue.Text) / (2*Math.PI));
                Shapes.Flat(WaveData, value);
            }
            catch (Exception)
            {
                // probably bad input from textbox; ignore
            }
            Refresh();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Shapes.DecreasingLineair(WaveData);     // note that graph is upside-down
            Refresh();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Shapes.IncreasingLineair(WaveData);     // note that graph is upside-down
            Refresh();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Shapes.DecreasingLogarithmic(WaveData);     // note that graph is upside-down
            Refresh();
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            Shapes.IncreasingLogarithmic(WaveData);     // note that graph is upside-down
            Refresh();
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            Shapes.IncDec(WaveData);
            Refresh();
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            Shapes.DecInc(WaveData);
            Refresh();
        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            try
            {
                int numSines = Convert.ToInt32(textBoxNumIncSines.Text);
                if (numSines > 9)
                {
                    numSines = 9;
                }
                Shapes.IncSines(WaveData, numSines);
            }
            catch (Exception)
            {
                // probably bad input from textbox; ignore
            }
            Refresh();
        }

        private void pictureBox10_Click(object sender, EventArgs e)
        {
            try
            {
                int numSines = Convert.ToInt32(textBoxNumDecSines.Text);
                if (numSines > 9)
                {
                    numSines = 9;
                }
                Shapes.DecSines(WaveData, numSines);
            }
            catch (Exception)
            {
                // probably bad input from textbox; ignore
            }
            Refresh();
        }

        private void pictureBox11_Click(object sender, EventArgs e)
        {
            Shapes.RandomWaves(WaveData);
            Refresh();
        }

        private void pictureBox12_Click(object sender, EventArgs e)
        {
            try
            {
                Shapes.Sines(WaveData, Convert.ToInt32(textBoxNumSines.Text));
            }
            catch (Exception)
            {
                // probably bad input from textbox; ignore
            }

            Refresh();
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            Shapes.Spikes(WaveData);
            Refresh();
        }

        private void pictureBoxFadeInSines_Click(object sender, EventArgs e)
        {
            try
            {
                Shapes.Sines(WaveData, Convert.ToInt32(textBoxFadeInSines.Text), true);
            }
            catch (Exception)
            {
                // probably bad input from textbox; ignore
            }

            Refresh();
        }

        private void pictureBoxFadeOutSines_Click(object sender, EventArgs e)
        {
            try
            {
                Shapes.Sines(WaveData, Convert.ToInt32(textBoxFadeOutSines.Text), false, true);
            }
            catch (Exception)
            {
                // probably bad input from textbox; ignore
            }

            Refresh();
        }
    }
}
