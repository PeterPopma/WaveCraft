
namespace WaveCraft
{
    partial class FormFFTChart
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.chartFFT = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.labelFFTPeriod = new System.Windows.Forms.Label();
            this.comboBoxFFTWindow = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.labelScale = new System.Windows.Forms.Label();
            this.labelFrequencyRange = new System.Windows.Forms.Label();
            this.gradientButton8 = new WaveCraft.CustomControls.GradientButton();
            this.gradientButton7 = new WaveCraft.CustomControls.GradientButton();
            this.gradientButton6 = new WaveCraft.CustomControls.GradientButton();
            this.gradientButton5 = new WaveCraft.CustomControls.GradientButton();
            this.gradientButton4 = new WaveCraft.CustomControls.GradientButton();
            this.gradientButton3 = new WaveCraft.CustomControls.GradientButton();
            this.gradientButton2 = new WaveCraft.CustomControls.GradientButton();
            this.gradientButton1 = new WaveCraft.CustomControls.GradientButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radioButtonChannelBoth = new System.Windows.Forms.RadioButton();
            this.radioButtonChannelRight = new System.Windows.Forms.RadioButton();
            this.radioButtonChannelLeft = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.chartFFT)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // chartFFT
            // 
            this.chartFFT.BackColor = System.Drawing.Color.Transparent;
            this.chartFFT.BackSecondaryColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            chartArea1.AxisX.LabelStyle.ForeColor = System.Drawing.Color.White;
            chartArea1.AxisX.LineColor = System.Drawing.Color.DarkGray;
            chartArea1.AxisX.MajorGrid.LineColor = System.Drawing.Color.DarkGray;
            chartArea1.AxisX.MajorGrid.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dash;
            chartArea1.AxisX.ScaleBreakStyle.LineColor = System.Drawing.Color.White;
            chartArea1.AxisX.TitleForeColor = System.Drawing.Color.Maroon;
            chartArea1.AxisY.LabelStyle.ForeColor = System.Drawing.Color.White;
            chartArea1.AxisY.MajorGrid.LineColor = System.Drawing.Color.Gray;
            chartArea1.AxisY.MajorGrid.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dash;
            chartArea1.AxisY.MajorTickMark.LineColor = System.Drawing.Color.DimGray;
            chartArea1.AxisY.Maximum = 100D;
            chartArea1.AxisY.Minimum = 0D;
            chartArea1.AxisY.MinorGrid.LineColor = System.Drawing.Color.DarkGray;
            chartArea1.AxisY.MinorTickMark.LineColor = System.Drawing.Color.DarkGray;
            chartArea1.BackColor = System.Drawing.Color.Silver;
            chartArea1.BackGradientStyle = System.Windows.Forms.DataVisualization.Charting.GradientStyle.LeftRight;
            chartArea1.BackSecondaryColor = System.Drawing.Color.White;
            chartArea1.Name = "ChartArea1";
            this.chartFFT.ChartAreas.Add(chartArea1);
            this.chartFFT.Cursor = System.Windows.Forms.Cursors.Default;
            this.chartFFT.Location = new System.Drawing.Point(3, 55);
            this.chartFFT.Name = "chartFFT";
            series1.BackSecondaryColor = System.Drawing.Color.Black;
            series1.ChartArea = "ChartArea1";
            series1.Color = System.Drawing.Color.Blue;
            series1.Name = "Series1";
            series2.ChartArea = "ChartArea1";
            series2.Color = System.Drawing.Color.Red;
            series2.Name = "Series2";
            this.chartFFT.Series.Add(series1);
            this.chartFFT.Series.Add(series2);
            this.chartFFT.Size = new System.Drawing.Size(1272, 748);
            this.chartFFT.TabIndex = 0;
            this.chartFFT.Text = "chartFFT";
            this.chartFFT.Click += new System.EventHandler(this.chartFFT_Click);
            // 
            // labelFFTPeriod
            // 
            this.labelFFTPeriod.AutoSize = true;
            this.labelFFTPeriod.BackColor = System.Drawing.Color.Transparent;
            this.labelFFTPeriod.Cursor = System.Windows.Forms.Cursors.Hand;
            this.labelFFTPeriod.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelFFTPeriod.ForeColor = System.Drawing.Color.White;
            this.labelFFTPeriod.Location = new System.Drawing.Point(544, 32);
            this.labelFFTPeriod.Name = "labelFFTPeriod";
            this.labelFFTPeriod.Size = new System.Drawing.Size(51, 20);
            this.labelFFTPeriod.TabIndex = 3;
            this.labelFFTPeriod.Text = "label1";
            // 
            // comboBoxFFTWindow
            // 
            this.comboBoxFFTWindow.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxFFTWindow.FormattingEnabled = true;
            this.comboBoxFFTWindow.Items.AddRange(new object[] {
            "128",
            "256",
            "512",
            "1024",
            "2048",
            "4096",
            "8192",
            "16384",
            "32768",
            "65536",
            "131072"});
            this.comboBoxFFTWindow.Location = new System.Drawing.Point(979, 35);
            this.comboBoxFFTWindow.Name = "comboBoxFFTWindow";
            this.comboBoxFFTWindow.Size = new System.Drawing.Size(135, 21);
            this.comboBoxFFTWindow.TabIndex = 4;
            this.comboBoxFFTWindow.SelectedIndexChanged += new System.EventHandler(this.comboBoxFFTWindow_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(907, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "# Samples";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(237, 779);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 20);
            this.label2.TabIndex = 6;
            this.label2.Text = "Scale: 1:";
            // 
            // labelScale
            // 
            this.labelScale.AutoSize = true;
            this.labelScale.BackColor = System.Drawing.Color.Transparent;
            this.labelScale.Cursor = System.Windows.Forms.Cursors.Hand;
            this.labelScale.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelScale.ForeColor = System.Drawing.Color.White;
            this.labelScale.Location = new System.Drawing.Point(301, 779);
            this.labelScale.Name = "labelScale";
            this.labelScale.Size = new System.Drawing.Size(18, 20);
            this.labelScale.TabIndex = 7;
            this.labelScale.Text = "1";
            // 
            // labelFrequencyRange
            // 
            this.labelFrequencyRange.AutoSize = true;
            this.labelFrequencyRange.BackColor = System.Drawing.Color.Transparent;
            this.labelFrequencyRange.Cursor = System.Windows.Forms.Cursors.Hand;
            this.labelFrequencyRange.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelFrequencyRange.ForeColor = System.Drawing.Color.White;
            this.labelFrequencyRange.Location = new System.Drawing.Point(592, 780);
            this.labelFrequencyRange.Name = "labelFrequencyRange";
            this.labelFrequencyRange.Size = new System.Drawing.Size(156, 20);
            this.labelFrequencyRange.TabIndex = 10;
            this.labelFrequencyRange.Text = "xxxxxxxxxxxxxxxxxxxxx";
            this.labelFrequencyRange.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // gradientButton8
            // 
            this.gradientButton8.Active = false;
            this.gradientButton8.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.gradientButton8.FlatAppearance.BorderSize = 2;
            this.gradientButton8.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.gradientButton8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gradientButton8.ForeColor = System.Drawing.Color.Black;
            this.gradientButton8.HorizontalGradient = false;
            this.gradientButton8.Location = new System.Drawing.Point(905, 779);
            this.gradientButton8.Name = "gradientButton8";
            this.gradientButton8.Size = new System.Drawing.Size(64, 24);
            this.gradientButton8.TabIndex = 130;
            this.gradientButton8.Text = ">>>";
            this.gradientButton8.UseVisualStyleBackColor = true;
            this.gradientButton8.Click += new System.EventHandler(this.gradientButton8_Click);
            // 
            // gradientButton7
            // 
            this.gradientButton7.Active = false;
            this.gradientButton7.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.gradientButton7.FlatAppearance.BorderSize = 2;
            this.gradientButton7.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.gradientButton7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gradientButton7.ForeColor = System.Drawing.Color.Black;
            this.gradientButton7.HorizontalGradient = false;
            this.gradientButton7.Location = new System.Drawing.Point(833, 779);
            this.gradientButton7.Name = "gradientButton7";
            this.gradientButton7.Size = new System.Drawing.Size(64, 24);
            this.gradientButton7.TabIndex = 129;
            this.gradientButton7.Text = ">>";
            this.gradientButton7.UseVisualStyleBackColor = true;
            this.gradientButton7.Click += new System.EventHandler(this.gradientButton7_Click);
            // 
            // gradientButton6
            // 
            this.gradientButton6.Active = false;
            this.gradientButton6.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.gradientButton6.FlatAppearance.BorderSize = 2;
            this.gradientButton6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.gradientButton6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gradientButton6.ForeColor = System.Drawing.Color.Black;
            this.gradientButton6.HorizontalGradient = false;
            this.gradientButton6.Location = new System.Drawing.Point(761, 779);
            this.gradientButton6.Name = "gradientButton6";
            this.gradientButton6.Size = new System.Drawing.Size(64, 24);
            this.gradientButton6.TabIndex = 128;
            this.gradientButton6.Text = ">";
            this.gradientButton6.UseVisualStyleBackColor = true;
            this.gradientButton6.Click += new System.EventHandler(this.gradientButton6_Click);
            // 
            // gradientButton5
            // 
            this.gradientButton5.Active = false;
            this.gradientButton5.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.gradientButton5.FlatAppearance.BorderSize = 2;
            this.gradientButton5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.gradientButton5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gradientButton5.ForeColor = System.Drawing.Color.Black;
            this.gradientButton5.HorizontalGradient = false;
            this.gradientButton5.Location = new System.Drawing.Point(512, 779);
            this.gradientButton5.Name = "gradientButton5";
            this.gradientButton5.Size = new System.Drawing.Size(64, 24);
            this.gradientButton5.TabIndex = 127;
            this.gradientButton5.Text = "<";
            this.gradientButton5.UseVisualStyleBackColor = true;
            this.gradientButton5.Click += new System.EventHandler(this.gradientButton5_Click);
            // 
            // gradientButton4
            // 
            this.gradientButton4.Active = false;
            this.gradientButton4.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.gradientButton4.FlatAppearance.BorderSize = 2;
            this.gradientButton4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.gradientButton4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gradientButton4.ForeColor = System.Drawing.Color.Black;
            this.gradientButton4.HorizontalGradient = false;
            this.gradientButton4.Location = new System.Drawing.Point(440, 779);
            this.gradientButton4.Name = "gradientButton4";
            this.gradientButton4.Size = new System.Drawing.Size(64, 24);
            this.gradientButton4.TabIndex = 126;
            this.gradientButton4.Text = "<<";
            this.gradientButton4.UseVisualStyleBackColor = true;
            this.gradientButton4.Click += new System.EventHandler(this.gradientButton4_Click);
            // 
            // gradientButton3
            // 
            this.gradientButton3.Active = false;
            this.gradientButton3.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.gradientButton3.FlatAppearance.BorderSize = 2;
            this.gradientButton3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.gradientButton3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gradientButton3.ForeColor = System.Drawing.Color.Black;
            this.gradientButton3.HorizontalGradient = false;
            this.gradientButton3.Location = new System.Drawing.Point(368, 779);
            this.gradientButton3.Name = "gradientButton3";
            this.gradientButton3.Size = new System.Drawing.Size(64, 24);
            this.gradientButton3.TabIndex = 125;
            this.gradientButton3.Text = "<<<";
            this.gradientButton3.UseVisualStyleBackColor = true;
            this.gradientButton3.Click += new System.EventHandler(this.gradientButton3_Click);
            // 
            // gradientButton2
            // 
            this.gradientButton2.Active = false;
            this.gradientButton2.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.gradientButton2.FlatAppearance.BorderSize = 2;
            this.gradientButton2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.gradientButton2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gradientButton2.ForeColor = System.Drawing.Color.Black;
            this.gradientButton2.HorizontalGradient = false;
            this.gradientButton2.Location = new System.Drawing.Point(398, 33);
            this.gradientButton2.Name = "gradientButton2";
            this.gradientButton2.Size = new System.Drawing.Size(98, 24);
            this.gradientButton2.TabIndex = 124;
            this.gradientButton2.Text = "<<";
            this.gradientButton2.UseVisualStyleBackColor = true;
            this.gradientButton2.Click += new System.EventHandler(this.gradientButton2_Click);
            // 
            // gradientButton1
            // 
            this.gradientButton1.Active = false;
            this.gradientButton1.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.gradientButton1.FlatAppearance.BorderSize = 2;
            this.gradientButton1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.gradientButton1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gradientButton1.ForeColor = System.Drawing.Color.Black;
            this.gradientButton1.HorizontalGradient = false;
            this.gradientButton1.Location = new System.Drawing.Point(698, 33);
            this.gradientButton1.Name = "gradientButton1";
            this.gradientButton1.Size = new System.Drawing.Size(98, 24);
            this.gradientButton1.TabIndex = 123;
            this.gradientButton1.Text = ">>";
            this.gradientButton1.UseVisualStyleBackColor = true;
            this.gradientButton1.Click += new System.EventHandler(this.gradientButton1_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.Controls.Add(this.radioButtonChannelBoth);
            this.groupBox1.Controls.Add(this.radioButtonChannelRight);
            this.groupBox1.Controls.Add(this.radioButtonChannelLeft);
            this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.groupBox1.ForeColor = System.Drawing.Color.White;
            this.groupBox1.Location = new System.Drawing.Point(95, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(203, 51);
            this.groupBox1.TabIndex = 131;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "channel";
            // 
            // radioButtonChannelBoth
            // 
            this.radioButtonChannelBoth.AutoSize = true;
            this.radioButtonChannelBoth.Checked = true;
            this.radioButtonChannelBoth.Location = new System.Drawing.Point(29, 20);
            this.radioButtonChannelBoth.Name = "radioButtonChannelBoth";
            this.radioButtonChannelBoth.Size = new System.Drawing.Size(46, 17);
            this.radioButtonChannelBoth.TabIndex = 22;
            this.radioButtonChannelBoth.TabStop = true;
            this.radioButtonChannelBoth.Text = "both";
            this.radioButtonChannelBoth.UseVisualStyleBackColor = true;
            this.radioButtonChannelBoth.CheckedChanged += new System.EventHandler(this.radioButtonChannelBoth_CheckedChanged);
            // 
            // radioButtonChannelRight
            // 
            this.radioButtonChannelRight.AutoSize = true;
            this.radioButtonChannelRight.Location = new System.Drawing.Point(131, 20);
            this.radioButtonChannelRight.Name = "radioButtonChannelRight";
            this.radioButtonChannelRight.Size = new System.Drawing.Size(45, 17);
            this.radioButtonChannelRight.TabIndex = 21;
            this.radioButtonChannelRight.Text = "right";
            this.radioButtonChannelRight.UseVisualStyleBackColor = true;
            this.radioButtonChannelRight.CheckedChanged += new System.EventHandler(this.radioButtonChannelRight_CheckedChanged);
            // 
            // radioButtonChannelLeft
            // 
            this.radioButtonChannelLeft.AutoSize = true;
            this.radioButtonChannelLeft.Location = new System.Drawing.Point(81, 20);
            this.radioButtonChannelLeft.Name = "radioButtonChannelLeft";
            this.radioButtonChannelLeft.Size = new System.Drawing.Size(39, 17);
            this.radioButtonChannelLeft.TabIndex = 20;
            this.radioButtonChannelLeft.Text = "left";
            this.radioButtonChannelLeft.UseVisualStyleBackColor = true;
            this.radioButtonChannelLeft.CheckedChanged += new System.EventHandler(this.radioButtonChannelLeft_CheckedChanged);
            // 
            // FormFFTChart
            // 
            this.ClientSize = new System.Drawing.Size(1289, 822);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.gradientButton8);
            this.Controls.Add(this.gradientButton7);
            this.Controls.Add(this.gradientButton6);
            this.Controls.Add(this.gradientButton5);
            this.Controls.Add(this.gradientButton4);
            this.Controls.Add(this.gradientButton3);
            this.Controls.Add(this.gradientButton2);
            this.Controls.Add(this.gradientButton1);
            this.Controls.Add(this.labelFrequencyRange);
            this.Controls.Add(this.labelScale);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBoxFFTWindow);
            this.Controls.Add(this.labelFFTPeriod);
            this.Controls.Add(this.chartFFT);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormFFTChart";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Frequency Analysis";
            this.Load += new System.EventHandler(this.FormFFT_Load);
            ((System.ComponentModel.ISupportInitialize)(this.chartFFT)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart chartFFT;
        private System.Windows.Forms.Label labelFFTPeriod;
        private System.Windows.Forms.ComboBox comboBoxFFTWindow;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label labelScale;
        private System.Windows.Forms.Label labelFrequencyRange;
        private CustomControls.GradientButton gradientButton1;
        private CustomControls.GradientButton gradientButton2;
        private CustomControls.GradientButton gradientButton3;
        private CustomControls.GradientButton gradientButton4;
        private CustomControls.GradientButton gradientButton5;
        private CustomControls.GradientButton gradientButton6;
        private CustomControls.GradientButton gradientButton7;
        private CustomControls.GradientButton gradientButton8;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radioButtonChannelBoth;
        private System.Windows.Forms.RadioButton radioButtonChannelRight;
        private System.Windows.Forms.RadioButton radioButtonChannelLeft;
    }
}
