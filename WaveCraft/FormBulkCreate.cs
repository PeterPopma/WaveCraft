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
                int mouseY = e.Y;
                if(mouseY>SynthGenerator.SHAPE_MAX_VALUE)
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
            Cursor = Cursors.WaitCursor;

            try
            {
                double otherFrequency = myParent.SynthGenerator.BulkOtherFrequency = Convert.ToDouble(textBoxTargetFrequency.Text);
                if(otherFrequency<10 || otherFrequency>22000)
                {
                    myParent.SynthGenerator.BulkOtherFrequency = otherFrequency;
                }
            }
            catch (Exception)
            {
            }
            myParent.SynthGenerator.CreateBulkWaves(checkBoxStartFrequency.Checked, checkBoxEndFrequency.Checked);

            myParent.UpdateWaveControls();
            myParent.SynthGenerator.UpdateAllWaveData();

            Cursor = Cursors.Default;

            Close();
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
            textBoxTargetFrequency.Text = myParent.SynthGenerator.BulkOtherFrequency.ToString();
            UpdateFrequencyLabels();

            pictureBoxCustomWave.Paint += new PaintEventHandler(PictureBoxPaint);
            pictureBoxCustomWave.Refresh();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
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

        private void textBoxFrequency1_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                UpdateFrequencyLabels();
            }
            catch (Exception)
            {
            }
        }

        private void UpdateFrequencyLabels()
        {
            double freq1 = Convert.ToDouble(textBoxFrequency1.Text);
            double freq2 = Convert.ToDouble(textBoxFrequency2.Text);
            if (freq1 > freq2)
            {
                myParent.SynthGenerator.MinFrequencyBulkCreate = freq2;
                myParent.SynthGenerator.MaxFrequencyBulkCreate = freq1;
            }
            else
            {
                myParent.SynthGenerator.MinFrequencyBulkCreate = freq1;
                myParent.SynthGenerator.MaxFrequencyBulkCreate = freq2;
            }
            labelXAxis1.Text = myParent.SynthGenerator.MinFrequencyBulkCreate.ToString("0");
            labelXAxis2.Text = myParent.SynthGenerator.BulkGraphToFrequency(100).ToString("0");
            labelXAxis3.Text = myParent.SynthGenerator.BulkGraphToFrequency(200).ToString("0");
            labelXAxis4.Text = myParent.SynthGenerator.BulkGraphToFrequency(300).ToString("0");
            labelXAxis5.Text = myParent.SynthGenerator.BulkGraphToFrequency(400).ToString("0");
            labelXAxis6.Text = myParent.SynthGenerator.BulkGraphToFrequency(500).ToString("0");
            labelXAxis7.Text = myParent.SynthGenerator.BulkGraphToFrequency(600).ToString("0");
            labelXAxis8.Text = myParent.SynthGenerator.BulkGraphToFrequency(700).ToString("0");
            labelXAxis9.Text = myParent.SynthGenerator.BulkGraphToFrequency(800).ToString("0");
            labelXAxis10.Text = myParent.SynthGenerator.BulkGraphToFrequency(900).ToString("0");
            labelXAxis11.Text = myParent.SynthGenerator.MaxFrequencyBulkCreate.ToString("0");
        }

        private void textBoxAmount_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                double amount = Convert.ToInt32(textBoxAmount.Text);
                if(amount>=0 && amount<=1000)
                {
                    myParent.SynthGenerator.AmountBulkCreate = Convert.ToInt32(textBoxAmount.Text);
                }
            }
            catch (Exception)
            {
            }
        }

        private void checkBoxStartFrequency_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBoxStartFrequency.Checked)
            {
                textBoxTargetFrequency.Visible = true;
                checkBoxEndFrequency.Checked = false;
            }
            if (!checkBoxStartFrequency.Checked && !checkBoxEndFrequency.Checked)
            {
                textBoxTargetFrequency.Visible = false;
            }
        }

        private void checkBoxEndFrequency_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxEndFrequency.Checked)
            {
                textBoxTargetFrequency.Visible = true;
                checkBoxStartFrequency.Checked = false;
            }
            if (!checkBoxStartFrequency.Checked && !checkBoxEndFrequency.Checked)
            {
                textBoxTargetFrequency.Visible = false;
            }
        }
    }
}
