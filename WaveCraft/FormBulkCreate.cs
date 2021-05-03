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
    public partial class FormBulkCreate : Form
    {
        private FormMain myParent = null;
        private int[] WaveData = new int[SynthGenerator.SHAPE_NUMPOINTS];
        bool isMouseButtonDown = false;
        Point previousPoint;
        Timer aTimer = new Timer();
        int AdjustDataWidth = 0;
        Random random = new Random();

        public FormBulkCreate()
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
                WaveData[e.X] = e.Y;
                Refresh();
                previousPoint.X = e.X;
                previousPoint.Y = e.Y;
                isMouseButtonDown = true;
                AdjustDataWidth = 1;
                aTimer.Enabled = true;
            }
        }

        private void pictureBoxCustomWave_MouseMove(object sender, MouseEventArgs e)
        {
            if (isMouseButtonDown && e.X != previousPoint.X && e.X >= 0 && e.X < SynthGenerator.SHAPE_NUMPOINTS)
            {
                EditData(e.X, e.Y);
                previousPoint.X = e.X;
                previousPoint.Y = e.Y;
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
            try
            {
                double value = Convert.ToDouble(textBoxFrequency1.Text);
                if (value >= 1 && value <= 22000)
                    myParent.SynthGenerator.MinFrequencyBulkCreate = value;
            }
            catch (Exception) { };
            try
            {
                double value = Convert.ToDouble(textBoxFrequency2.Text);
                if (value >= 1 && value <= 22000)
                    myParent.SynthGenerator.MaxFrequencyBulkCreate = value;
            }
            catch (Exception) { };
            try
            {
                int value = Convert.ToInt32(textBoxAmount.Text);
                if (value > 0 && value <= 1000)
                    myParent.SynthGenerator.AmountBulkCreate = value;
            }
            catch (Exception) { };

            CreateWaves();

            myParent.UpdateWaveControls();
            myParent.SynthGenerator.UpdateAllWaveData();

            Close();
        }

        private void CreateWaves()
        {
            List<int> lotteryPot = new List<int>();
            for( int k=0; k<WaveData.Length;  k++)
            {
                // Note that graph is upside-down
                int amount = SynthGenerator.SHAPE_MAX_VALUE - WaveData[k];
                for (int i = 0; i < amount; i++)
                {
                    lotteryPot.Add(k);
                }
            }
            for (int k = 0; k < myParent.SynthGenerator.AmountBulkCreate; k++)
            {
                int value = lotteryPot[random.Next(lotteryPot.Count)];
                double frequency = (value * myParent.SynthGenerator.MaxFrequencyBulkCreate) + ((SynthGenerator.SHAPE_NUMPOINTS - value) * myParent.SynthGenerator.MinFrequencyBulkCreate);
                frequency /= (double)SynthGenerator.SHAPE_NUMPOINTS;
                WaveInfo wave = myParent.SynthGenerator.CloneWave();
                wave.MinFrequency = wave.MaxFrequency = frequency;
                Shapes.Flat(wave.ShapeFrequency);
            }
        }

        private void PictureBoxPaint(object sender, PaintEventArgs e)
        {
            Control control = (Control)sender;

            using (LinearGradientBrush brush = new LinearGradientBrush(control.ClientRectangle,
                                                                       Color.FromArgb(70, 87, 195),
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

        private void FormBulkCreate_Load(object sender, EventArgs e)
        {
            WaveData = myParent.SynthGenerator.ShapeBulkCreate;

            textBoxFrequency1.Text = myParent.SynthGenerator.MinFrequencyBulkCreate.ToString();
            textBoxFrequency2.Text = myParent.SynthGenerator.MaxFrequencyBulkCreate.ToString();
            textBoxAmount.Text = myParent.SynthGenerator.AmountBulkCreate.ToString();

            pictureBoxCustomWave.Paint += new PaintEventHandler(PictureBoxPaint);
            pictureBoxCustomWave.Refresh();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

    }
}
