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
    public partial class FormBulkChangeOverTime : Form
    {
        private const int SHAPE_NUM_POINTS = 1000;
        private const int SHAPE_MAX_VALUE = 500;

        private FormMain myParent = null;
        private int[] waveData = new int[SHAPE_NUM_POINTS];
        bool isMouseButtonDown = false;
        Point previousPoint;
        Timer aTimer = new Timer();
        int AdjustDataWidth = 0;
        Random random = new Random();
        double value1, value2;

        public FormBulkChangeOverTime()
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

        private void GroupBoxPaint(object sender, PaintEventArgs e)
        {
            Control control = (Control)sender;

            using (LinearGradientBrush brush = new LinearGradientBrush(control.ClientRectangle,
                                                                       Color.Black,
                                                                       Color.FromArgb(70, 77, 95),
                                                                       90F))
            {
                e.Graphics.FillRectangle(brush, control.ClientRectangle);
                ControlPaint.DrawBorder(e.Graphics, control.ClientRectangle, Color.Gray, ButtonBorderStyle.Solid);
            }
        }

        public double CalculateCurrentFactor(int position, double MinChange, double MaxChange)
        {
            // note that graph is upside down
            return (MaxChange * (SHAPE_MAX_VALUE-waveData[position]) + MinChange * waveData[position]) / SHAPE_MAX_VALUE;
        }

        private double CalculateCurrentFrequency(int position, WaveInfo waveInfo)
        {
            return (waveInfo.MaxFrequency * waveInfo.ShapeFrequency[position] + (waveInfo.MinFrequency * (SynthGenerator.SHAPE_MAX_VALUE - waveInfo.ShapeFrequency[position]))) / (double)SynthGenerator.SHAPE_MAX_VALUE;
        }

        private double CalculateCurrentVolume(int position, WaveInfo waveInfo)
        {
            return (waveInfo.MaxVolume * waveInfo.ShapeVolume[position] + (waveInfo.MinVolume * (SynthGenerator.SHAPE_MAX_VALUE - waveInfo.ShapeVolume[position]))) / (double)SynthGenerator.SHAPE_MAX_VALUE;
        }

        private double CalculateCurrentWeight(int position, WaveInfo waveInfo)
        {
            return (waveInfo.MaxWeight * waveInfo.ShapeWeight[position] + (waveInfo.MinWeight * (SynthGenerator.SHAPE_MAX_VALUE - waveInfo.ShapeWeight[position]))) / (double)SynthGenerator.SHAPE_MAX_VALUE;
        }

        private void AdjustValues(WaveInfo wave)
        {
            double MinChange = Convert.ToDouble(labelChangeMin.Text);
            double MaxChange = Convert.ToDouble(labelChangeMax.Text);

            if (radioButtonChangeFrequency.Checked)
            {
                AdjustWaveFrequency(wave, MinChange, MaxChange);
            }
            else if (radioButtonChangeVolume.Checked)
            {
                AdjustWaveVolume(wave, MinChange, MaxChange);
            }
            else if (radioButtonChangeWeight.Checked)
            {
                AdjustWaveWeight(wave, MinChange, MaxChange);
            }
            else
            {
                AdjustWavePhase(wave, MinChange, MaxChange);
            }
        }

        private void buttonApply_Click(object sender, EventArgs e)
        {

            if (myParent.listBoxWaves.SelectedItems.Count > 1)
            {
                foreach (string item in myParent.listBoxWaves.SelectedItems)
                {
                    WaveInfo wave = myParent.SynthGenerator.GetCurrentWaveByDisplayName(item);
                    AdjustValues(wave);
                }
            }
            else
            {
                foreach (WaveInfo wave in myParent.SynthGenerator.Waves)
                {
                    AdjustValues(wave);
                }
            }

            myParent.pictureBoxFrequencyShape.Refresh();
            myParent.UpdateWaveControls();
            myParent.SynthGenerator.UpdateAllWaveData();

            Close();
        }

        private void AdjustWaveFrequency(WaveInfo wave, double MinChange, double MaxChange)
        {
            double new_min = wave.MinFrequency * MinChange;
            double new_max = wave.MaxFrequency * MaxChange;
            if (new_min < SynthGenerator.MIN_FREQUENCY)
            {
                new_min = SynthGenerator.MIN_FREQUENCY;
            }
            if (new_max < SynthGenerator.MIN_FREQUENCY)
            {
                new_max = SynthGenerator.MIN_FREQUENCY;
            }
            if (new_min > SynthGenerator.MAX_FREQUENCY)
            {
                new_min = SynthGenerator.MAX_FREQUENCY;
            }
            if (new_max > SynthGenerator.MAX_FREQUENCY)
            {
                new_max = SynthGenerator.MAX_FREQUENCY;
            }
            // change the freq. graph, so that it matches the desired pattern
            for (int position = 0; position < wave.ShapeFrequency.Length; position++)
            {
                int position_in_wavedata = (wave.NumSamples() * position / wave.ShapeFrequency.Length);
                int desired_pattern_position = SHAPE_NUM_POINTS * (wave.StartPosition + position_in_wavedata) / myParent.SynthGenerator.NumSamples();
                double factor = CalculateCurrentFactor(desired_pattern_position, MinChange, MaxChange);
                double freq = CalculateCurrentFrequency(position, wave);
                freq *= factor;
                wave.ShapeFrequency[position] = (int)(SynthGenerator.SHAPE_MAX_VALUE * ((freq - new_min) / (new_max - new_min)));
            }
            wave.MinFrequency = new_min;
            wave.MaxFrequency = new_max;
        }

        private void SetWaveFrequency(WaveInfo wave, double MinValue, double MaxValue)
        {
            wave.MinFrequency = MinValue;
            wave.MaxFrequency = MaxValue;
            if (wave.MinFrequency < SynthGenerator.MIN_FREQUENCY)
            {
                wave.MinFrequency = SynthGenerator.MIN_FREQUENCY;
            }
            if (wave.MaxFrequency < SynthGenerator.MIN_FREQUENCY)
            {
                wave.MaxFrequency = SynthGenerator.MIN_FREQUENCY;
            }
            if (wave.MinFrequency > SynthGenerator.MAX_FREQUENCY)
            {
                wave.MinFrequency = SynthGenerator.MAX_FREQUENCY;
            }
            if (wave.MaxFrequency > SynthGenerator.MAX_FREQUENCY)
            {
                wave.MaxFrequency = SynthGenerator.MAX_FREQUENCY;
            }

            // copy the shape
            for (int position = 0; position < wave.ShapeFrequency.Length; position++)
            {
                wave.ShapeFrequency[position] = SynthGenerator.SHAPE_MAX_VALUE - waveData[position];
            }
        }

        private void AdjustWaveVolume(WaveInfo wave, double MinChange, double MaxChange)
        {
            int new_min = (int)(wave.MinVolume * MinChange);
            int new_max = (int)(wave.MaxVolume * MaxChange);
            if (new_min < 0)
            {
                new_min = 0;
            }
            if (new_max < 0)
            {
                new_max = 0;
            }
            if (new_min > SynthGenerator.MAX_VOLUME)
            {
                new_min = SynthGenerator.MAX_VOLUME;
            }
            if (new_max > SynthGenerator.MAX_VOLUME)
            {
                new_max = SynthGenerator.MAX_VOLUME;
            }
            // change the volume graph, so that it matches the desired pattern
            for (int position = 0; position < wave.ShapeVolume.Length; position++)
            {
                int position_in_wavedata = (wave.NumSamples() * position / wave.ShapeVolume.Length);
                int desired_pattern_position = SHAPE_NUM_POINTS * (wave.StartPosition + position_in_wavedata) / myParent.SynthGenerator.NumSamples();
                double factor = CalculateCurrentFactor(desired_pattern_position, MinChange, MaxChange);
                double volume = CalculateCurrentVolume(position, wave);
                volume *= factor;
                wave.ShapeVolume[position] = (int)(SynthGenerator.SHAPE_MAX_VALUE * ((volume - new_min) / (new_max - new_min)));
            }
            wave.MinVolume = new_min;
            wave.MaxVolume = new_max;
        }

        private void SetWaveVolume(WaveInfo wave, double MinValue, double MaxValue)
        {
            wave.MinVolume = (int)MinValue;
            wave.MaxVolume = (int)MaxValue;
            if (wave.MinVolume < 0)
            {
                wave.MinVolume = 0;
            }
            if (wave.MaxVolume < 0)
            {
                wave.MaxVolume = 0;
            }
            if (wave.MinVolume > SynthGenerator.MAX_VOLUME)
            {
                wave.MinVolume = SynthGenerator.MAX_VOLUME;
            }
            if (wave.MaxVolume > SynthGenerator.MAX_VOLUME)
            {
                wave.MaxVolume = SynthGenerator.MAX_VOLUME;
            }

            // copy the shape
            for (int position = 0; position < wave.ShapeFrequency.Length; position++)
            {
                wave.ShapeVolume[position] = SynthGenerator.SHAPE_MAX_VALUE - waveData[position];
            }
        }

        private void AdjustWaveWeight(WaveInfo wave, double MinChange, double MaxChange)
        {
            int new_min = (int)(wave.MinWeight * MinChange);
            int new_max = (int)(wave.MaxWeight * MaxChange);
            if (new_min < 0)
            {
                new_min = 0;
            }
            if (new_max < 0)
            {
                new_max = 0;
            }
            if (new_min > SynthGenerator.MAX_WEIGHT)
            {
                new_min = SynthGenerator.MAX_WEIGHT;
            }
            if (new_max > SynthGenerator.MAX_WEIGHT)
            {
                new_max = SynthGenerator.MAX_WEIGHT;
            }
            // change the weight graph, so that it matches the desired pattern
            for (int position = 0; position < wave.ShapeWeight.Length; position++)
            {
                int position_in_wavedata = (wave.NumSamples() * position / wave.ShapeWeight.Length);
                int desired_pattern_position = SHAPE_NUM_POINTS * (wave.StartPosition + position_in_wavedata) / myParent.SynthGenerator.NumSamples();
                double factor = CalculateCurrentFactor(desired_pattern_position, MinChange, MaxChange);
                double weight = CalculateCurrentWeight(position, wave);
                weight *= factor;
                wave.ShapeWeight[position] = (int)(SynthGenerator.SHAPE_MAX_VALUE * ((weight - new_min) / (new_max - new_min)));
            }
            wave.MinWeight = new_min;
            wave.MaxWeight = new_max;
        }

        private void SetWaveWeight(WaveInfo wave, double MinValue, double MaxValue)
        {
            wave.MinWeight = (int)MinValue;
            wave.MaxWeight = (int)MaxValue;
            if (wave.MinWeight < 0)
            {
                wave.MinWeight = 0;
            }
            if (wave.MaxWeight < 0)
            {
                wave.MaxWeight = 0;
            }
            if (wave.MinWeight > SynthGenerator.MAX_WEIGHT)
            {
                wave.MinWeight = SynthGenerator.MAX_WEIGHT;
            }
            if (wave.MaxWeight > SynthGenerator.MAX_WEIGHT)
            {
                wave.MaxWeight = SynthGenerator.MAX_WEIGHT;
            }

            // copy the shape
            for (int position = 0; position < wave.ShapeFrequency.Length; position++)
            {
                wave.ShapeWeight[position] = SynthGenerator.SHAPE_MAX_VALUE - waveData[position];
            }
        }

        private void AdjustWavePhase(WaveInfo wave, double MinChange, double MaxChange)
        {
            // change the phase graph, so that it matches the desired pattern
            for (int position = 0; position < wave.ShapePhase.Length; position++)
            {
                int position_in_wavedata = (wave.NumSamples() * position / wave.ShapeWeight.Length);
                int desired_pattern_position = SHAPE_NUM_POINTS * (wave.StartPosition + position_in_wavedata) / myParent.SynthGenerator.NumSamples();
                double factor = CalculateCurrentFactor(desired_pattern_position, MinChange, MaxChange);
                int phase = wave.ShapePhase[position];
                phase = (int)((phase + (factor*SynthGenerator.SHAPE_MAX_VALUE)) % SynthGenerator.SHAPE_MAX_VALUE);
                wave.ShapePhase[position] = phase;
            }
        }

        private void SetWavePhase(WaveInfo wave, double MinValue, double MaxValue)
        {
            for (int position = 0; position < wave.ShapeFrequency.Length; position++)
            {
                double current_value = (waveData[position] * MinValue + (SynthGenerator.SHAPE_MAX_VALUE - waveData[position]) * MaxValue) / SynthGenerator.SHAPE_MAX_VALUE;
                wave.ShapePhase[position] = (int)(current_value%(2*Math.PI) * SynthGenerator.SHAPE_MAX_VALUE / (2 * Math.PI));
            }
        }

        private void PictureBoxPaint(object sender, PaintEventArgs e)
        {
            Control control = (Control)sender;

            using (LinearGradientBrush brush = new LinearGradientBrush(control.ClientRectangle,
                                                                       Color.FromArgb(70, 195, 87),
                                                                       Color.FromArgb(0, 15, 0),
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
            for (int i = 0; i < waveData.Length - 1; i++)
            {
                e.Graphics.DrawLine(pen, new Point(i, waveData[i]), new Point(i + 1, waveData[i + 1]));
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
                waveData[position] = (int)value;
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

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
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
            int begin_y = waveData[begin_x];
            int end_y = previousPoint.Y;

            // adjust all points left of mouse pointer
            while (x_position < end_x)
            {
                int interpolated_value = (((x_position - begin_x) * end_y) + ((end_x - x_position) * begin_y)) / (end_x - begin_x);
                waveData[x_position] = (waveData[x_position] + interpolated_value) / 2;
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
            end_y = waveData[end_x];

            // adjust all points right of mouse pointer
            while (x_position < end_x)
            {
                int interpolated_value = (((x_position - begin_x) * end_y) + ((end_x - x_position) * begin_y)) / (end_x - begin_x);
                waveData[x_position] = (waveData[x_position] + interpolated_value) / 2;
                x_position++;
            }

            Refresh();
        }

        private void FormFrequency_Load(object sender, EventArgs e)
        {
            groupBox2.Paint += new PaintEventHandler(GroupBoxPaint);
            groupBox2.Refresh();

            groupBox3.Paint += new PaintEventHandler(GroupBoxPaint);
            groupBox3.Refresh();

            for (int i = 0; i < waveData.Length; i++)
            {
                waveData[i] = 250;
            }

            value1 = 1;
            value2 = 1.5;
            textBoxValue1.Text = labelChangeMin.Text = value1.ToString("0.000");
            textBoxValue2.Text = labelChangeMax.Text = value2.ToString("0.000");

            pictureBoxFrequencyShape.Paint += new PaintEventHandler(PictureBoxPaint);
            pictureBoxFrequencyShape.Refresh();
        }

        private void pictureBoxFrequencyShape_MouseDown(object sender, MouseEventArgs e)
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
                waveData[e.X] = mouseY;
                Refresh();
                previousPoint.X = e.X;
                previousPoint.Y = mouseY;
                isMouseButtonDown = true;
                AdjustDataWidth = 1;
                aTimer.Enabled = true;
            }
        }

        private void pictureBoxFrequencyShape_MouseMove(object sender, MouseEventArgs e)
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

        private void pictureBoxFrequencyShape_MouseUp(object sender, MouseEventArgs e)
        {
            {
                isMouseButtonDown = false;
                aTimer.Enabled = false;
            }
        }

        private void textBoxValue1_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                double change = Convert.ToDouble(textBoxValue1.Text);
                if (change>=0 && change<=SynthGenerator.MAX_FREQUENCY)
                {
                    value1 = change;
                    UpdateMinMaxLabels();
                }
            }
            catch (Exception)
            {
            }
        }

        private void UpdateMinMaxLabels()
        {
            if (value1 > value2)
            {
                labelChangeMin.Text = value2.ToString("0.000");
                labelChangeMax.Text = value1.ToString("0.000");
            }
            else
            {
                labelChangeMin.Text = value1.ToString("0.000");
                labelChangeMax.Text = value2.ToString("0.000");
            }
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < waveData.Length; i++)
            {
                waveData[i] = (int)(SynthGenerator.SHAPE_MAX_VALUE / 2.0);
            }
            Refresh();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < waveData.Length; i++)
            {
                waveData[i] = (int)((SynthGenerator.SHAPE_NUMPOINTS - i) / (double)SynthGenerator.SHAPE_NUMPOINTS * SynthGenerator.SHAPE_MAX_VALUE);
            }
            Refresh();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < waveData.Length; i++)
            {
                waveData[i] = (int)(i / (double)SynthGenerator.SHAPE_NUMPOINTS * SynthGenerator.SHAPE_MAX_VALUE);
            }
            Refresh();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            double factor = Math.Pow(SynthGenerator.SHAPE_MAX_VALUE, 1.0 / SynthGenerator.SHAPE_NUMPOINTS);
            for (int i = 0; i < waveData.Length; i++)
            {
                waveData[i] = SynthGenerator.SHAPE_MAX_VALUE - (int)Math.Pow(factor, i);
            }
            Refresh();
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            double factor = Math.Pow(SynthGenerator.SHAPE_MAX_VALUE, 1.0 / SynthGenerator.SHAPE_NUMPOINTS);
            for (int i = 0; i < waveData.Length; i++)
            {
                waveData[i] = SynthGenerator.SHAPE_MAX_VALUE - (int)Math.Pow(factor, SynthGenerator.SHAPE_NUMPOINTS - i);
            }
            Refresh();
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < waveData.Length / 2; i++)
            {
                waveData[i] = (int)(((SynthGenerator.SHAPE_NUMPOINTS - i * 2) / (double)SynthGenerator.SHAPE_NUMPOINTS) * SynthGenerator.SHAPE_MAX_VALUE);
            }
            for (int i = waveData.Length / 2; i < waveData.Length; i++)
            {
                waveData[i] = (int)(((i - waveData.Length / 2) * SynthGenerator.SHAPE_MAX_VALUE) / (waveData.Length / 2));
            }
            Refresh();
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < waveData.Length / 2; i++)
            {
                waveData[i] = (int)(((i) * SynthGenerator.SHAPE_MAX_VALUE) / (waveData.Length / 2));
            }
            for (int i = waveData.Length / 2; i < waveData.Length; i++)
            {
                waveData[i] = (int)(((SynthGenerator.SHAPE_NUMPOINTS - (i - waveData.Length / 2) * 2) / (double)SynthGenerator.SHAPE_NUMPOINTS) * SynthGenerator.SHAPE_MAX_VALUE);
            }
            Refresh();
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            int x_position = 0;
            while (x_position < waveData.Length)
            {
                if (x_position < SynthGenerator.SHAPE_NUMPOINTS - 5 && random.Next(15) == 11)
                {
                    int amplitude = random.Next(SynthGenerator.SHAPE_MAX_VALUE / 2);
                    int up_down_spike = 1;
                    if (random.Next(100) < 50)
                    {
                        // up spike
                        up_down_spike = -1;
                    }
                    for (int j = 0; j < 5; j++)
                    {
                        waveData[x_position] = SynthGenerator.SHAPE_MAX_VALUE / 2 - ((int)(amplitude * Math.Sin(((j + 1) / 6.0) * Math.PI)) * up_down_spike);
                        x_position++;
                    }
                }
                else
                {
                    waveData[x_position] = SynthGenerator.SHAPE_MAX_VALUE / 2;
                    x_position++;
                }
            }
            Refresh();
        }

        private void pictureBox12_Click(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < waveData.Length; i++)
                {
                    waveData[i] = (int)(((int)(Math.Sin(i / (double)waveData.Length * 2 * Convert.ToInt32(textBoxNumSines.Text) * Math.PI) * SynthGenerator.SHAPE_MAX_VALUE + SynthGenerator.SHAPE_MAX_VALUE)) / 2.0);
                }
            }
            catch (Exception)
            {
                // probably bad input from textbox; ignore
            }
            Refresh();
        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < waveData.Length; i++)
            {
                double factor = Math.Pow(1.003, i);   // between 1 and 20
                waveData[i] = (int)(((int)(Math.Sin(i / (double)waveData.Length * factor * Math.PI) * SynthGenerator.SHAPE_MAX_VALUE + SynthGenerator.SHAPE_MAX_VALUE)) / 2.0);
            }
            Refresh();
        }

        private void pictureBox10_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < waveData.Length; i++)
            {
                double factor = Math.Pow(1.003, i);   // between 1 and 20
                waveData[waveData.Length - 1 - i] = (int)(((int)(Math.Sin(i / (double)waveData.Length * factor * Math.PI) * SynthGenerator.SHAPE_MAX_VALUE + SynthGenerator.SHAPE_MAX_VALUE)) / 2.0);
            }
            Refresh();
        }

        private void pictureBox11_Click(object sender, EventArgs e)
        {
            int amplitude = random.Next(SynthGenerator.SHAPE_MAX_VALUE / 2);
            int period = random.Next(50) + 5;
            bool amplitude_increasing = false;
            for (int i = 0; i < waveData.Length; i++)
            {
                if (random.Next(20) == 3)
                {
                    amplitude_increasing = !amplitude_increasing;
                }
                if (amplitude_increasing)
                {
                    if (amplitude < SynthGenerator.SHAPE_MAX_VALUE / 2)
                    {
                        amplitude++;
                    }
                    else
                    {
                        amplitude_increasing = false;
                    }
                }
                else
                {
                    if (amplitude > 0)
                    {
                        amplitude--;
                    }
                    else
                    {
                        amplitude_increasing = true;
                    }
                }
                waveData[i] = (int)((Math.Sin(i / (double)waveData.Length * period * Math.PI) * amplitude) + (SynthGenerator.SHAPE_MAX_VALUE / 2.0));
            }
            Refresh();
        }

        private void textBoxValue2_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                double change = Convert.ToDouble(textBoxValue2.Text);
                if (change >= 0 && change <= SynthGenerator.MAX_FREQUENCY)
                {
                    value2 = change;
                    UpdateMinMaxLabels();
                }
            }
            catch (Exception)
            {
            }
        }

        private void radioButtonChangeValue_CheckedChanged(object sender, EventArgs e)
        {
            textBoxValue1.Text = labelChangeMin.Text = "1.000";
            textBoxValue2.Text = labelChangeMax.Text = "1.500";
        }

        private void pictureBoxFadeInSines_Click(object sender, EventArgs e)
        {
            try
            {
                Shapes.Sines(waveData, Convert.ToInt32(textBoxFadeInSines.Text), true);
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
                Shapes.Sines(waveData, Convert.ToInt32(textBoxFadeOutSines.Text), false, true);
            }
            catch (Exception)
            {
                // probably bad input from textbox; ignore
            }

            Refresh();
        }

        private void radioButtonSetValue_CheckedChanged(object sender, EventArgs e)
        {
            if(radioButtonChangeFrequency.Checked)
            {
                textBoxValue1.Text = labelChangeMin.Text = "20";
                textBoxValue2.Text = labelChangeMax.Text = "20000";
            }
            else if(radioButtonChangePhase.Checked)
            {
                textBoxValue1.Text = labelChangeMin.Text = "0";
                textBoxValue2.Text = labelChangeMax.Text = (2 * Math.PI).ToString("##.###");
            }
            else
            {
                textBoxValue1.Text = labelChangeMin.Text = "0";
                textBoxValue2.Text = labelChangeMax.Text = "1000";
            }
        }
    }
}
