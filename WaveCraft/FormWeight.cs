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
    public partial class FormWeight : Form
    {
        private FormMain myParent = null;
        private int[] WaveData = new int[SynthGenerator.SHAPE_NUMPOINTS];
        bool isMouseButtonDown = false;
        Point previousPoint;
        Timer aTimer = new Timer();
        int AdjustDataWidth = 0;
        Random random = new Random();

        public FormWeight()
        {
            InitializeComponent();
            aTimer.Interval = 100;
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
            myParent.SynthGenerator.CurrentWave.ShapeWeight = new int[SynthGenerator.SHAPE_NUMPOINTS];
            for (int i = 0; i < WaveData.Length; i++)
            {
                // Note that graph is upside-down
                myParent.SynthGenerator.CurrentWave.ShapeWeight[i] = SynthGenerator.SHAPE_MAX_VALUE - WaveData[i];
            }
            myParent.pictureBoxWeightShape.Refresh();
            myParent.SynthGenerator.CurrentWave.MinWeight = Convert.ToInt32(labelWeightMin.Text);
            myParent.SynthGenerator.CurrentWave.MaxWeight = Convert.ToInt32(labelWeightMax.Text);
            myParent.UpdateWaveControls();
            myParent.SynthGenerator.UpdateCurrentWaveData();

            Close();
        }

        private void PictureBoxPaint(object sender, PaintEventArgs e)
        {
            Control control = (Control)sender;

            using (LinearGradientBrush brush = new LinearGradientBrush(control.ClientRectangle,
                                                                       Color.FromArgb(195, 195, 70),
                                                                       Color.FromArgb(15, 0, 0),
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
                e.Graphics.DrawLine(pen, new Point(i, WaveData[i]), new Point(i + 1, WaveData[i + 1]));
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

        private void FormWeight_Load(object sender, EventArgs e)
        {
            if (myParent.SynthGenerator.CurrentWave.ShapeWeight.Length < SynthGenerator.SHAPE_NUMPOINTS)     // No data yet
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
                    WaveData[i] = SynthGenerator.SHAPE_MAX_VALUE - myParent.SynthGenerator.CurrentWave.ShapeWeight[i];
                }
            }

            colorSliderWeight1.Value = myParent.SynthGenerator.CurrentWave.MinWeight;
            colorSliderWeight2.Value = myParent.SynthGenerator.CurrentWave.MaxWeight;
            textBoxVolume1.Text = colorSliderWeight1.Value.ToString();
            textBoxVolume2.Text = colorSliderWeight2.Value.ToString();

            pictureBoxCustomWave.Paint += new PaintEventHandler(PictureBoxPaint);
            pictureBoxCustomWave.Refresh();
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

        private void gradientButton1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < WaveData.Length; i++)
            {
                WaveData[i] = (int)(((int)(Math.Sin(i / (double)WaveData.Length * 4 * Math.PI) * SynthGenerator.SHAPE_MAX_VALUE + SynthGenerator.SHAPE_MAX_VALUE)) / 2.0);
            }
            Refresh();
        }

        private void gradientButton2_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < WaveData.Length; i++)
            {
                WaveData[i] = (int)(((int)(Math.Sin(i / (double)WaveData.Length * 6 * Math.PI) * SynthGenerator.SHAPE_MAX_VALUE + SynthGenerator.SHAPE_MAX_VALUE)) / 2.0);
            }
            Refresh();
        }

        private void buttonIncreasing_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < WaveData.Length; i++)
            {
                WaveData[i] = (int)((SynthGenerator.SHAPE_NUMPOINTS - i) / (double)SynthGenerator.SHAPE_NUMPOINTS * SynthGenerator.SHAPE_MAX_VALUE);
            }
            Refresh();
        }

        private void buttonDecreasing_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < WaveData.Length; i++)
            {
                WaveData[i] = (int)(i / (double)SynthGenerator.SHAPE_NUMPOINTS * SynthGenerator.SHAPE_MAX_VALUE);
            }
            Refresh();
        }

        private void buttonVolumeMinus1_Click(object sender, EventArgs e)
        {
            if (colorSliderWeight1.Value > 0)
            {
                colorSliderWeight1.Value -= 1;
            }
            textBoxVolume1.Text = colorSliderWeight1.Value.ToString();
        }

        private void buttonVolumeMinus10_Click(object sender, EventArgs e)
        {
            if (colorSliderWeight1.Value > 9)
            {
                colorSliderWeight1.Value -= 10;
            }
            textBoxVolume1.Text = colorSliderWeight1.Value.ToString();
        }

        private void buttonEndVolMinus1_Click(object sender, EventArgs e)
        {
            if (colorSliderWeight2.Value > 0)
            {
                colorSliderWeight2.Value -= 1;
            }
            textBoxVolume2.Text = colorSliderWeight2.Value.ToString();
        }

        private void buttonEndVolMinus10_Click(object sender, EventArgs e)
        {
            if (colorSliderWeight2.Value > 9)
            {
                colorSliderWeight2.Value -= 10;
            }
            textBoxVolume2.Text = colorSliderWeight2.Value.ToString();
        }

        private void buttonEndVolPlus1_Click(object sender, EventArgs e)
        {
            if (colorSliderWeight2.Value < colorSliderWeight2.Maximum)
            {
                colorSliderWeight2.Value += 1;
            }
            textBoxVolume2.Text = colorSliderWeight2.Value.ToString();
        }

        private void buttonEndVolPlus10_Click(object sender, EventArgs e)
        {
            if (colorSliderWeight2.Value < colorSliderWeight2.Maximum - 9)
            {
                colorSliderWeight2.Value += 10;
            }
            textBoxVolume2.Text = colorSliderWeight2.Value.ToString();
        }

        private void buttonVolumePlus1_Click(object sender, EventArgs e)
        {
            if (colorSliderWeight1.Value < colorSliderWeight1.Maximum)
            {
                colorSliderWeight1.Value += 1;
            }
            textBoxVolume1.Text = colorSliderWeight1.Value.ToString();
        }

        private void buttonVolumePlus10_Click(object sender, EventArgs e)
        {
            if (colorSliderWeight1.Value < colorSliderWeight1.Maximum - 9)
            {
                colorSliderWeight1.Value += 10;
            }
            textBoxVolume1.Text = colorSliderWeight1.Value.ToString();
        }

        private void UpdateWeightLabels()
        {
            if (colorSliderWeight1.Value>colorSliderWeight2.Value)
            {
                labelWeightMin.Text = colorSliderWeight2.Value.ToString();
                labelWeightMax.Text = colorSliderWeight1.Value.ToString();
            }
            else
            {
                labelWeightMin.Text = colorSliderWeight1.Value.ToString();
                labelWeightMax.Text = colorSliderWeight2.Value.ToString();
            }
        }

        private void colorSliderBeginVolume_ValueChanged(object sender, EventArgs e)
        {
            UpdateWeightLabels();
        }

        private void colorSliderEndVolume_ValueChanged(object sender, EventArgs e)
        {
            UpdateWeightLabels();
        }

        private void textBoxVolume1_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                colorSliderWeight1.Value = colorSliderWeight2.Value = Convert.ToDecimal(textBoxVolume1.Text);
                textBoxVolume2.Text = textBoxVolume1.Text;
            }
            catch (Exception)
            {
            }
        }

        private void textBoxVolume2_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                colorSliderWeight2.Value = Convert.ToDecimal(textBoxVolume2.Text);
            }
            catch (Exception)
            {
            }
        }

        private void colorSliderVolume1_MouseUp(object sender, MouseEventArgs e)
        {
            textBoxVolume1.Text = colorSliderWeight1.Value.ToString();
        }

        private void colorSliderVolume2_MouseUp(object sender, MouseEventArgs e)
        {
            textBoxVolume2.Text = colorSliderWeight2.Value.ToString();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            Shapes.Flat(WaveData);
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
