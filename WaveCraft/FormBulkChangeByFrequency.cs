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
    public partial class FormBulkChangeByFrequency : Form
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
        double minFrequency = 10;
        double maxFrequency = 20000;

        public FormBulkChangeByFrequency()
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

        public double CalculateCurrentFactor(int position, double MinChange, double MaxChange)
        {
            // note that graph is upside down
            return (MaxChange * (SHAPE_MAX_VALUE-waveData[position]) + MinChange * waveData[position]) / SHAPE_MAX_VALUE;
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
            double waveFrequency = (wave.MinFrequency + wave.MaxFrequency) / 2;
            if(waveFrequency<minFrequency || waveFrequency>maxFrequency)
            {
                return;
            }

            double MinChange = Convert.ToDouble(labelChangeMin.Text);
            double MaxChange = Convert.ToDouble(labelChangeMax.Text);

            double factor = CalculateCurrentFactor(FrequencyToGraphPos(waveFrequency), MinChange, MaxChange );

            if (radioButtonChangeVolume.Checked)
            {
                AdjustWaveVolume(wave, factor);
            }
            else if (radioButtonChangeWeight.Checked)
            {
                AdjustWaveWeight(wave, factor);
            }
            else
            {
                AdjustWavePhase(wave, factor);
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

        private void AdjustWaveVolume(WaveInfo wave, double factor)
        {
            int new_min = (int)(wave.MinVolume * factor);
            if (new_min > SynthGenerator.MAX_VOLUME)
            {
                new_min = SynthGenerator.MAX_VOLUME;
            }
            int new_max = (int)(wave.MaxVolume * factor);
            if (new_max > SynthGenerator.MAX_VOLUME)
            {
                new_max = SynthGenerator.MAX_VOLUME;
            }
            // change the volume graph, so that it matches the desired pattern
            for (int position = 0; position < wave.ShapeVolume.Length; position++)
            {
                double volume = CalculateCurrentVolume(position, wave);
                volume *= factor;
                wave.ShapeVolume[position] = (int)(SynthGenerator.SHAPE_MAX_VALUE * ((volume - new_min) / (new_max - new_min)));
            }
            wave.MinVolume = new_min;
            wave.MaxVolume = new_max;
        }

        private void AdjustWaveWeight(WaveInfo wave, double factor)
        {
            int new_min = (int)(wave.MinWeight * factor);
            if (new_min > SynthGenerator.MAX_WEIGHT)
            {
                new_min = SynthGenerator.MAX_WEIGHT;
            }
            int new_max = (int)(wave.MaxWeight * factor);
            if (new_max > SynthGenerator.MAX_WEIGHT)
            {
                new_max = SynthGenerator.MAX_WEIGHT;
            }
            // change the weight graph, so that it matches the desired pattern
            for (int position = 0; position < wave.ShapeWeight.Length; position++)
            {
                double weight = CalculateCurrentWeight(position, wave);
                weight *= factor;
                wave.ShapeWeight[position] = (int)(SynthGenerator.SHAPE_MAX_VALUE * ((weight - new_min) / (new_max - new_min)));
            }
            wave.MinWeight = new_min;
            wave.MaxWeight = new_max;
        }

        private void AdjustWavePhase(WaveInfo wave, double factor)
        {
            // change the phase graph, so that it matches the desired pattern
            for (int position = 0; position < wave.ShapePhase.Length; position++)
            {
                int position_in_wavedata = (wave.NumSamples() * position / wave.ShapeWeight.Length);
                int desired_pattern_position = SHAPE_NUM_POINTS * (wave.StartPosition + position_in_wavedata) / myParent.SynthGenerator.NumSamples();
                int phase = wave.ShapePhase[position];
                phase = (int)((phase + (factor*SynthGenerator.SHAPE_MAX_VALUE)) % SynthGenerator.SHAPE_MAX_VALUE);
                wave.ShapePhase[position] = phase;
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

        private void FormBulkChangeByFrequency_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < waveData.Length; i++)
            {
                waveData[i] = 250;
            }
            minFrequency = myParent.SynthGenerator.FindMinFrequency();
            maxFrequency = myParent.SynthGenerator.FindMaxFrequency();
            textBoxFrequency1.Text = minFrequency.ToString();
            textBoxFrequency2.Text = maxFrequency.ToString();
            textBoxChange1.Text = labelChangeMin.Text = "1.000";
            textBoxChange2.Text = labelChangeMax.Text = "1.500";
            UpdateFrequencyLabels();

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

        private void textBoxFrequency1_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                double change = Convert.ToDouble(textBoxChange1.Text);
                if (change>0 && change<=1000)
                {
                    if (change<Convert.ToDouble(textBoxChange2.Text))
                    {
                        labelChangeMin.Text = change.ToString("0.000");
                    }
                    else
                    {
                        labelChangeMax.Text = change.ToString("0.000");
                    }
                }
            }
            catch (Exception)
            {
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

        private void textBoxChange2_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                double change = Convert.ToDouble(textBoxChange2.Text);
                if (change > 0 && change <= 1000)
                {
                    if (change < Convert.ToDouble(textBoxChange1.Text))
                    {
                        labelChangeMin.Text = change.ToString("0.000");
                    }
                    else
                    {
                        labelChangeMax.Text = change.ToString("0.000");
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        public double GraphToFrequency(int graphX)
        {
            double baseValue = Math.Pow(maxFrequency / minFrequency, 1 / (double)SynthGenerator.SHAPE_NUMPOINTS);

            return minFrequency * Math.Pow(baseValue, graphX);
        }

        private int FrequencyToGraphPos(double frequency)
        {
            int graphPos = 0;
            while(GraphToFrequency(graphPos)<frequency && graphPos<SHAPE_NUM_POINTS-1)
            {
                graphPos++;
            }

            return graphPos;
        }

        private void UpdateFrequencyLabels()
        {
            double freq1 = Convert.ToDouble(textBoxFrequency1.Text);
            double freq2 = Convert.ToDouble(textBoxFrequency2.Text);
            if (freq1 > freq2)
            {
                minFrequency = freq2;
                maxFrequency = freq1;
            }
            else
            {
                minFrequency = freq1;
                maxFrequency = freq2;
            }
            labelXAxis1.Text = minFrequency.ToString("0");
            labelXAxis2.Text = GraphToFrequency(100).ToString("0");
            labelXAxis3.Text = GraphToFrequency(200).ToString("0");
            labelXAxis4.Text = GraphToFrequency(300).ToString("0");
            labelXAxis5.Text = GraphToFrequency(400).ToString("0");
            labelXAxis6.Text = GraphToFrequency(500).ToString("0");
            labelXAxis7.Text = GraphToFrequency(600).ToString("0");
            labelXAxis8.Text = GraphToFrequency(700).ToString("0");
            labelXAxis9.Text = GraphToFrequency(800).ToString("0");
            labelXAxis10.Text = GraphToFrequency(900).ToString("0");
            labelXAxis11.Text = maxFrequency.ToString("0");
        }

        private void textBoxFrequency1_KeyUp_1(object sender, KeyEventArgs e)
        {
            try
            {
                UpdateFrequencyLabels();
            }
            catch (Exception)
            {
            }
        }

        private void textBoxFrequency2_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                UpdateFrequencyLabels();
            }
            catch (Exception)
            {
            }
        }
    }
}
