
using WaveCraft.Properties;

namespace WaveCraft
{
    partial class FormBulkCreate
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
            this.pictureBoxCustomWave = new System.Windows.Forms.PictureBox();
            this.textBoxFrequency1 = new System.Windows.Forms.TextBox();
            this.textBoxFrequency2 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxAmount = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.labelXAxis4 = new System.Windows.Forms.Label();
            this.labelXAxis2 = new System.Windows.Forms.Label();
            this.labelXAxis6 = new System.Windows.Forms.Label();
            this.labelXAxis1 = new System.Windows.Forms.Label();
            this.labelXAxis3 = new System.Windows.Forms.Label();
            this.labelXAxis5 = new System.Windows.Forms.Label();
            this.labelXAxis7 = new System.Windows.Forms.Label();
            this.labelXAxis8 = new System.Windows.Forms.Label();
            this.labelXAxis9 = new System.Windows.Forms.Label();
            this.labelXAxis10 = new System.Windows.Forms.Label();
            this.labelXAxis11 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.textBoxTargetFrequency = new System.Windows.Forms.TextBox();
            this.checkBoxStartFrequency = new System.Windows.Forms.CheckBox();
            this.checkBoxEndFrequency = new System.Windows.Forms.CheckBox();
            this.labelFrequency = new System.Windows.Forms.Label();
            this.buttonCancel = new WaveCraft.CustomControls.GradientButton();
            this.buttonCreate = new WaveCraft.CustomControls.GradientButton();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.radioButtonSetWeight = new System.Windows.Forms.RadioButton();
            this.radioButtonSetWeightChange = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCustomWave)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBoxCustomWave
            // 
            this.pictureBoxCustomWave.Location = new System.Drawing.Point(69, 25);
            this.pictureBoxCustomWave.Name = "pictureBoxCustomWave";
            this.pictureBoxCustomWave.Size = new System.Drawing.Size(1000, 500);
            this.pictureBoxCustomWave.TabIndex = 154;
            this.pictureBoxCustomWave.TabStop = false;
            this.pictureBoxCustomWave.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBoxCustomWave_MouseDown);
            this.pictureBoxCustomWave.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBoxCustomWave_MouseMove);
            this.pictureBoxCustomWave.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBoxCustomWave_MouseUp);
            // 
            // textBoxFrequency1
            // 
            this.textBoxFrequency1.BackColor = System.Drawing.Color.DimGray;
            this.textBoxFrequency1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxFrequency1.ForeColor = System.Drawing.Color.White;
            this.textBoxFrequency1.Location = new System.Drawing.Point(376, 588);
            this.textBoxFrequency1.Name = "textBoxFrequency1";
            this.textBoxFrequency1.Size = new System.Drawing.Size(63, 23);
            this.textBoxFrequency1.TabIndex = 163;
            this.textBoxFrequency1.KeyUp += new System.Windows.Forms.KeyEventHandler(this.textBoxFrequency1_KeyUp);
            // 
            // textBoxFrequency2
            // 
            this.textBoxFrequency2.BackColor = System.Drawing.Color.DimGray;
            this.textBoxFrequency2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxFrequency2.ForeColor = System.Drawing.Color.White;
            this.textBoxFrequency2.Location = new System.Drawing.Point(376, 616);
            this.textBoxFrequency2.Name = "textBoxFrequency2";
            this.textBoxFrequency2.Size = new System.Drawing.Size(63, 23);
            this.textBoxFrequency2.TabIndex = 164;
            this.textBoxFrequency2.KeyUp += new System.Windows.Forms.KeyEventHandler(this.textBoxFrequency2_KeyUp);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Yellow;
            this.label2.Location = new System.Drawing.Point(12, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 17);
            this.label2.TabIndex = 165;
            this.label2.Text = "weight";
            // 
            // textBoxAmount
            // 
            this.textBoxAmount.BackColor = System.Drawing.Color.DimGray;
            this.textBoxAmount.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxAmount.ForeColor = System.Drawing.Color.White;
            this.textBoxAmount.Location = new System.Drawing.Point(606, 598);
            this.textBoxAmount.Name = "textBoxAmount";
            this.textBoxAmount.Size = new System.Drawing.Size(84, 32);
            this.textBoxAmount.TabIndex = 166;
            this.textBoxAmount.Text = "10";
            this.textBoxAmount.KeyUp += new System.Windows.Forms.KeyEventHandler(this.textBoxAmount_KeyUp);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(518, 598);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(85, 26);
            this.label3.TabIndex = 167;
            this.label3.Text = "amount";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(282, 620);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 17);
            this.label1.TabIndex = 168;
            this.label1.Text = "frequency 2";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(282, 591);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(83, 17);
            this.label4.TabIndex = 169;
            this.label4.Text = "frequency 1";
            // 
            // labelXAxis4
            // 
            this.labelXAxis4.AutoSize = true;
            this.labelXAxis4.BackColor = System.Drawing.Color.Transparent;
            this.labelXAxis4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelXAxis4.Location = new System.Drawing.Point(341, 528);
            this.labelXAxis4.Name = "labelXAxis4";
            this.labelXAxis4.Size = new System.Drawing.Size(60, 17);
            this.labelXAxis4.TabIndex = 170;
            this.labelXAxis4.Text = "1000.00";
            // 
            // labelXAxis2
            // 
            this.labelXAxis2.AutoSize = true;
            this.labelXAxis2.BackColor = System.Drawing.Color.Transparent;
            this.labelXAxis2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelXAxis2.Location = new System.Drawing.Point(141, 528);
            this.labelXAxis2.Name = "labelXAxis2";
            this.labelXAxis2.Size = new System.Drawing.Size(60, 17);
            this.labelXAxis2.TabIndex = 171;
            this.labelXAxis2.Text = "1000.00";
            // 
            // labelXAxis6
            // 
            this.labelXAxis6.AutoSize = true;
            this.labelXAxis6.BackColor = System.Drawing.Color.Transparent;
            this.labelXAxis6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelXAxis6.Location = new System.Drawing.Point(541, 528);
            this.labelXAxis6.Name = "labelXAxis6";
            this.labelXAxis6.Size = new System.Drawing.Size(60, 17);
            this.labelXAxis6.TabIndex = 172;
            this.labelXAxis6.Text = "1000.00";
            // 
            // labelXAxis1
            // 
            this.labelXAxis1.AutoSize = true;
            this.labelXAxis1.BackColor = System.Drawing.Color.Transparent;
            this.labelXAxis1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelXAxis1.Location = new System.Drawing.Point(41, 528);
            this.labelXAxis1.Name = "labelXAxis1";
            this.labelXAxis1.Size = new System.Drawing.Size(60, 17);
            this.labelXAxis1.TabIndex = 173;
            this.labelXAxis1.Text = "1000.00";
            // 
            // labelXAxis3
            // 
            this.labelXAxis3.AutoSize = true;
            this.labelXAxis3.BackColor = System.Drawing.Color.Transparent;
            this.labelXAxis3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelXAxis3.Location = new System.Drawing.Point(241, 528);
            this.labelXAxis3.Name = "labelXAxis3";
            this.labelXAxis3.Size = new System.Drawing.Size(60, 17);
            this.labelXAxis3.TabIndex = 174;
            this.labelXAxis3.Text = "1000.00";
            // 
            // labelXAxis5
            // 
            this.labelXAxis5.AutoSize = true;
            this.labelXAxis5.BackColor = System.Drawing.Color.Transparent;
            this.labelXAxis5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelXAxis5.Location = new System.Drawing.Point(441, 528);
            this.labelXAxis5.Name = "labelXAxis5";
            this.labelXAxis5.Size = new System.Drawing.Size(60, 17);
            this.labelXAxis5.TabIndex = 175;
            this.labelXAxis5.Text = "1000.00";
            // 
            // labelXAxis7
            // 
            this.labelXAxis7.AutoSize = true;
            this.labelXAxis7.BackColor = System.Drawing.Color.Transparent;
            this.labelXAxis7.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelXAxis7.Location = new System.Drawing.Point(641, 528);
            this.labelXAxis7.Name = "labelXAxis7";
            this.labelXAxis7.Size = new System.Drawing.Size(60, 17);
            this.labelXAxis7.TabIndex = 176;
            this.labelXAxis7.Text = "1000.00";
            // 
            // labelXAxis8
            // 
            this.labelXAxis8.AutoSize = true;
            this.labelXAxis8.BackColor = System.Drawing.Color.Transparent;
            this.labelXAxis8.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelXAxis8.Location = new System.Drawing.Point(741, 527);
            this.labelXAxis8.Name = "labelXAxis8";
            this.labelXAxis8.Size = new System.Drawing.Size(60, 17);
            this.labelXAxis8.TabIndex = 177;
            this.labelXAxis8.Text = "1000.00";
            // 
            // labelXAxis9
            // 
            this.labelXAxis9.AutoSize = true;
            this.labelXAxis9.BackColor = System.Drawing.Color.Transparent;
            this.labelXAxis9.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelXAxis9.Location = new System.Drawing.Point(841, 527);
            this.labelXAxis9.Name = "labelXAxis9";
            this.labelXAxis9.Size = new System.Drawing.Size(60, 17);
            this.labelXAxis9.TabIndex = 178;
            this.labelXAxis9.Text = "1000.00";
            // 
            // labelXAxis10
            // 
            this.labelXAxis10.AutoSize = true;
            this.labelXAxis10.BackColor = System.Drawing.Color.Transparent;
            this.labelXAxis10.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelXAxis10.Location = new System.Drawing.Point(941, 527);
            this.labelXAxis10.Name = "labelXAxis10";
            this.labelXAxis10.Size = new System.Drawing.Size(60, 17);
            this.labelXAxis10.TabIndex = 179;
            this.labelXAxis10.Text = "1000.00";
            // 
            // labelXAxis11
            // 
            this.labelXAxis11.AutoSize = true;
            this.labelXAxis11.BackColor = System.Drawing.Color.Transparent;
            this.labelXAxis11.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelXAxis11.Location = new System.Drawing.Point(1041, 527);
            this.labelXAxis11.Name = "labelXAxis11";
            this.labelXAxis11.Size = new System.Drawing.Size(60, 17);
            this.labelXAxis11.TabIndex = 180;
            this.labelXAxis11.Text = "1000.00";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(12, 545);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(101, 17);
            this.label5.TabIndex = 181;
            this.label5.Text = "min. frequency";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(1021, 545);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(104, 17);
            this.label6.TabIndex = 182;
            this.label6.Text = "max. frequency";
            // 
            // textBoxTargetFrequency
            // 
            this.textBoxTargetFrequency.BackColor = System.Drawing.Color.DimGray;
            this.textBoxTargetFrequency.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxTargetFrequency.ForeColor = System.Drawing.Color.White;
            this.textBoxTargetFrequency.Location = new System.Drawing.Point(910, 617);
            this.textBoxTargetFrequency.Name = "textBoxTargetFrequency";
            this.textBoxTargetFrequency.Size = new System.Drawing.Size(101, 23);
            this.textBoxTargetFrequency.TabIndex = 183;
            this.textBoxTargetFrequency.Text = "440";
            this.textBoxTargetFrequency.Visible = false;
            // 
            // checkBoxStartFrequency
            // 
            this.checkBoxStartFrequency.AutoSize = true;
            this.checkBoxStartFrequency.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.checkBoxStartFrequency.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxStartFrequency.Location = new System.Drawing.Point(831, 587);
            this.checkBoxStartFrequency.Name = "checkBoxStartFrequency";
            this.checkBoxStartFrequency.Size = new System.Drawing.Size(83, 21);
            this.checkBoxStartFrequency.TabIndex = 184;
            this.checkBoxStartFrequency.Text = "start with";
            this.checkBoxStartFrequency.UseVisualStyleBackColor = false;
            this.checkBoxStartFrequency.CheckedChanged += new System.EventHandler(this.checkBoxStartFrequency_CheckedChanged);
            // 
            // checkBoxEndFrequency
            // 
            this.checkBoxEndFrequency.AutoSize = true;
            this.checkBoxEndFrequency.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.checkBoxEndFrequency.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxEndFrequency.Location = new System.Drawing.Point(926, 588);
            this.checkBoxEndFrequency.Name = "checkBoxEndFrequency";
            this.checkBoxEndFrequency.Size = new System.Drawing.Size(79, 21);
            this.checkBoxEndFrequency.TabIndex = 185;
            this.checkBoxEndFrequency.Text = "end with";
            this.checkBoxEndFrequency.UseVisualStyleBackColor = false;
            this.checkBoxEndFrequency.CheckedChanged += new System.EventHandler(this.checkBoxEndFrequency_CheckedChanged);
            // 
            // labelFrequency
            // 
            this.labelFrequency.AutoSize = true;
            this.labelFrequency.BackColor = System.Drawing.Color.Transparent;
            this.labelFrequency.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelFrequency.Location = new System.Drawing.Point(837, 618);
            this.labelFrequency.Name = "labelFrequency";
            this.labelFrequency.Size = new System.Drawing.Size(71, 17);
            this.labelFrequency.TabIndex = 186;
            this.labelFrequency.Text = "frequency";
            this.labelFrequency.Visible = false;
            // 
            // buttonCancel
            // 
            this.buttonCancel.Active = false;
            this.buttonCancel.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.buttonCancel.FlatAppearance.BorderSize = 2;
            this.buttonCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonCancel.ForeColor = System.Drawing.Color.Black;
            this.buttonCancel.HorizontalGradient = false;
            this.buttonCancel.Location = new System.Drawing.Point(612, 671);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(87, 22);
            this.buttonCancel.TabIndex = 160;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonCreate
            // 
            this.buttonCreate.Active = false;
            this.buttonCreate.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.buttonCreate.FlatAppearance.BorderSize = 2;
            this.buttonCreate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonCreate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonCreate.ForeColor = System.Drawing.Color.Black;
            this.buttonCreate.HorizontalGradient = false;
            this.buttonCreate.Location = new System.Drawing.Point(390, 671);
            this.buttonCreate.Name = "buttonCreate";
            this.buttonCreate.Size = new System.Drawing.Size(87, 22);
            this.buttonCreate.TabIndex = 159;
            this.buttonCreate.Text = "Create";
            this.buttonCreate.UseVisualStyleBackColor = true;
            this.buttonCreate.Click += new System.EventHandler(this.buttonApply_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.Aqua;
            this.label8.Location = new System.Drawing.Point(1078, 9);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(99, 17);
            this.label8.TabIndex = 187;
            this.label8.Text = "weight change";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.Aqua;
            this.label9.Location = new System.Drawing.Point(1078, 34);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(56, 17);
            this.label9.TabIndex = 188;
            this.label9.Text = "+100 %";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.Color.Transparent;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.Aqua;
            this.label10.Location = new System.Drawing.Point(1078, 264);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(32, 17);
            this.label10.TabIndex = 189;
            this.label10.Text = "0 %";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.BackColor = System.Drawing.Color.Transparent;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.Color.Aqua;
            this.label11.Location = new System.Drawing.Point(1078, 508);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(53, 17);
            this.label11.TabIndex = 190;
            this.label11.Text = "-100 %";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.BackColor = System.Drawing.Color.Transparent;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.Color.Yellow;
            this.label12.Location = new System.Drawing.Point(25, 32);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(40, 17);
            this.label12.TabIndex = 191;
            this.label12.Text = "1000";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.BackColor = System.Drawing.Color.Transparent;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.ForeColor = System.Drawing.Color.Yellow;
            this.label13.Location = new System.Drawing.Point(48, 508);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(16, 17);
            this.label13.TabIndex = 192;
            this.label13.Text = "0";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.BackColor = System.Drawing.Color.Transparent;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.ForeColor = System.Drawing.Color.Yellow;
            this.label14.Location = new System.Drawing.Point(33, 245);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(32, 17);
            this.label14.TabIndex = 193;
            this.label14.Text = "500";
            // 
            // radioButtonSetWeight
            // 
            this.radioButtonSetWeight.AutoSize = true;
            this.radioButtonSetWeight.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.radioButtonSetWeight.Checked = true;
            this.radioButtonSetWeight.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButtonSetWeight.ForeColor = System.Drawing.Color.Yellow;
            this.radioButtonSetWeight.Location = new System.Drawing.Point(106, 592);
            this.radioButtonSetWeight.Name = "radioButtonSetWeight";
            this.radioButtonSetWeight.Size = new System.Drawing.Size(89, 21);
            this.radioButtonSetWeight.TabIndex = 194;
            this.radioButtonSetWeight.TabStop = true;
            this.radioButtonSetWeight.Text = "set weight";
            this.radioButtonSetWeight.UseVisualStyleBackColor = false;
            // 
            // radioButtonSetWeightChange
            // 
            this.radioButtonSetWeightChange.AutoSize = true;
            this.radioButtonSetWeightChange.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.radioButtonSetWeightChange.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButtonSetWeightChange.ForeColor = System.Drawing.Color.Aqua;
            this.radioButtonSetWeightChange.Location = new System.Drawing.Point(106, 619);
            this.radioButtonSetWeightChange.Name = "radioButtonSetWeightChange";
            this.radioButtonSetWeightChange.Size = new System.Drawing.Size(140, 21);
            this.radioButtonSetWeightChange.TabIndex = 196;
            this.radioButtonSetWeightChange.Text = "set weight change";
            this.radioButtonSetWeightChange.UseVisualStyleBackColor = false;
            // 
            // FormBulkCreate
            // 
            this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.ClientSize = new System.Drawing.Size(1184, 712);
            this.Controls.Add(this.radioButtonSetWeightChange);
            this.Controls.Add(this.radioButtonSetWeight);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.labelFrequency);
            this.Controls.Add(this.checkBoxEndFrequency);
            this.Controls.Add(this.checkBoxStartFrequency);
            this.Controls.Add(this.textBoxTargetFrequency);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.labelXAxis11);
            this.Controls.Add(this.labelXAxis10);
            this.Controls.Add(this.labelXAxis9);
            this.Controls.Add(this.labelXAxis8);
            this.Controls.Add(this.labelXAxis7);
            this.Controls.Add(this.labelXAxis5);
            this.Controls.Add(this.labelXAxis3);
            this.Controls.Add(this.labelXAxis1);
            this.Controls.Add(this.labelXAxis6);
            this.Controls.Add(this.labelXAxis2);
            this.Controls.Add(this.labelXAxis4);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBoxAmount);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBoxFrequency2);
            this.Controls.Add(this.textBoxFrequency1);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonCreate);
            this.Controls.Add(this.pictureBoxCustomWave);
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormBulkCreate";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.FormBulkCreate_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCustomWave)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.PictureBox pictureBoxCustomWave;
        private CustomControls.GradientButton buttonCancel;
        private CustomControls.GradientButton buttonCreate;
        private System.Windows.Forms.TextBox textBoxFrequency1;
        private System.Windows.Forms.TextBox textBoxFrequency2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxAmount;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label labelXAxis4;
        private System.Windows.Forms.Label labelXAxis2;
        private System.Windows.Forms.Label labelXAxis6;
        private System.Windows.Forms.Label labelXAxis1;
        private System.Windows.Forms.Label labelXAxis3;
        private System.Windows.Forms.Label labelXAxis5;
        private System.Windows.Forms.Label labelXAxis7;
        private System.Windows.Forms.Label labelXAxis8;
        private System.Windows.Forms.Label labelXAxis9;
        private System.Windows.Forms.Label labelXAxis10;
        private System.Windows.Forms.Label labelXAxis11;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBoxTargetFrequency;
        private System.Windows.Forms.CheckBox checkBoxStartFrequency;
        private System.Windows.Forms.CheckBox checkBoxEndFrequency;
        private System.Windows.Forms.Label labelFrequency;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.RadioButton radioButtonSetWeight;
        private System.Windows.Forms.RadioButton radioButtonSetWeightChange;
    }
}
