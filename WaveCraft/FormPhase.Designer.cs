
using WaveCraft.Properties;

namespace WaveCraft
{
    partial class FormPhase
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
            this.labelPhaseMin = new System.Windows.Forms.Label();
            this.labelPhaseMax = new System.Windows.Forms.Label();
            this.pictureBoxPhaseShape = new System.Windows.Forms.PictureBox();
            this.pictureBox11 = new System.Windows.Forms.PictureBox();
            this.pictureBox10 = new System.Windows.Forms.PictureBox();
            this.pictureBox9 = new System.Windows.Forms.PictureBox();
            this.pictureBox7 = new System.Windows.Forms.PictureBox();
            this.pictureBox6 = new System.Windows.Forms.PictureBox();
            this.pictureBox5 = new System.Windows.Forms.PictureBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.textBoxNumSines = new System.Windows.Forms.TextBox();
            this.pictureBox12 = new System.Windows.Forms.PictureBox();
            this.textBoxNumIncSines = new System.Windows.Forms.TextBox();
            this.textBoxNumDecSines = new System.Windows.Forms.TextBox();
            this.buttonCancel = new WaveCraft.CustomControls.GradientButton();
            this.buttonApply = new WaveCraft.CustomControls.GradientButton();
            this.pictureBox8 = new System.Windows.Forms.PictureBox();
            this.textBoxFlatValue = new System.Windows.Forms.TextBox();
            this.pictureBoxFadeOutSines = new System.Windows.Forms.PictureBox();
            this.pictureBoxFadeInSines = new System.Windows.Forms.PictureBox();
            this.textBoxFadeInSines = new System.Windows.Forms.TextBox();
            this.textBoxFadeOutSines = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPhaseShape)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox11)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox10)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox12)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxFadeOutSines)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxFadeInSines)).BeginInit();
            this.SuspendLayout();
            // 
            // labelPhaseMin
            // 
            this.labelPhaseMin.AutoSize = true;
            this.labelPhaseMin.BackColor = System.Drawing.Color.Transparent;
            this.labelPhaseMin.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelPhaseMin.Location = new System.Drawing.Point(1180, 490);
            this.labelPhaseMin.Name = "labelPhaseMin";
            this.labelPhaseMin.Size = new System.Drawing.Size(26, 29);
            this.labelPhaseMin.TabIndex = 140;
            this.labelPhaseMin.Text = "0";
            // 
            // labelPhaseMax
            // 
            this.labelPhaseMax.AutoSize = true;
            this.labelPhaseMax.BackColor = System.Drawing.Color.Transparent;
            this.labelPhaseMax.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelPhaseMax.Location = new System.Drawing.Point(1180, 5);
            this.labelPhaseMax.Name = "labelPhaseMax";
            this.labelPhaseMax.Size = new System.Drawing.Size(45, 29);
            this.labelPhaseMax.TabIndex = 139;
            this.labelPhaseMax.Text = "2 π";
            // 
            // pictureBoxPhaseShape
            // 
            this.pictureBoxPhaseShape.Location = new System.Drawing.Point(173, 12);
            this.pictureBoxPhaseShape.Name = "pictureBoxPhaseShape";
            this.pictureBoxPhaseShape.Size = new System.Drawing.Size(1000, 500);
            this.pictureBoxPhaseShape.TabIndex = 154;
            this.pictureBoxPhaseShape.TabStop = false;
            this.pictureBoxPhaseShape.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBoxCustomWave_MouseDown);
            this.pictureBoxPhaseShape.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBoxCustomWave_MouseMove);
            this.pictureBoxPhaseShape.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBoxCustomWave_MouseUp);
            // 
            // pictureBox11
            // 
            this.pictureBox11.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox11.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox11.Image = global::WaveCraft.Properties.Resources.waves;
            this.pictureBox11.InitialImage = null;
            this.pictureBox11.Location = new System.Drawing.Point(88, 8);
            this.pictureBox11.Name = "pictureBox11";
            this.pictureBox11.Size = new System.Drawing.Size(70, 70);
            this.pictureBox11.TabIndex = 229;
            this.pictureBox11.TabStop = false;
            this.pictureBox11.Click += new System.EventHandler(this.pictureBox11_Click);
            // 
            // pictureBox10
            // 
            this.pictureBox10.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox10.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox10.Image = global::WaveCraft.Properties.Resources.decsine;
            this.pictureBox10.InitialImage = null;
            this.pictureBox10.Location = new System.Drawing.Point(88, 312);
            this.pictureBox10.Name = "pictureBox10";
            this.pictureBox10.Size = new System.Drawing.Size(70, 70);
            this.pictureBox10.TabIndex = 228;
            this.pictureBox10.TabStop = false;
            this.pictureBox10.Click += new System.EventHandler(this.pictureBox10_Click);
            // 
            // pictureBox9
            // 
            this.pictureBox9.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox9.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox9.Image = global::WaveCraft.Properties.Resources.incsine;
            this.pictureBox9.InitialImage = null;
            this.pictureBox9.Location = new System.Drawing.Point(88, 236);
            this.pictureBox9.Name = "pictureBox9";
            this.pictureBox9.Size = new System.Drawing.Size(70, 70);
            this.pictureBox9.TabIndex = 227;
            this.pictureBox9.TabStop = false;
            this.pictureBox9.Click += new System.EventHandler(this.pictureBox9_Click);
            // 
            // pictureBox7
            // 
            this.pictureBox7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox7.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox7.Image = global::WaveCraft.Properties.Resources.decinc;
            this.pictureBox7.InitialImage = null;
            this.pictureBox7.Location = new System.Drawing.Point(88, 158);
            this.pictureBox7.Name = "pictureBox7";
            this.pictureBox7.Size = new System.Drawing.Size(70, 70);
            this.pictureBox7.TabIndex = 225;
            this.pictureBox7.TabStop = false;
            this.pictureBox7.Click += new System.EventHandler(this.pictureBox7_Click);
            // 
            // pictureBox6
            // 
            this.pictureBox6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox6.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox6.Image = global::WaveCraft.Properties.Resources.incdec;
            this.pictureBox6.InitialImage = null;
            this.pictureBox6.Location = new System.Drawing.Point(88, 84);
            this.pictureBox6.Name = "pictureBox6";
            this.pictureBox6.Size = new System.Drawing.Size(70, 70);
            this.pictureBox6.TabIndex = 224;
            this.pictureBox6.TabStop = false;
            this.pictureBox6.Click += new System.EventHandler(this.pictureBox6_Click);
            // 
            // pictureBox5
            // 
            this.pictureBox5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox5.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox5.Image = global::WaveCraft.Properties.Resources.logdec;
            this.pictureBox5.InitialImage = null;
            this.pictureBox5.Location = new System.Drawing.Point(12, 312);
            this.pictureBox5.Name = "pictureBox5";
            this.pictureBox5.Size = new System.Drawing.Size(70, 70);
            this.pictureBox5.TabIndex = 223;
            this.pictureBox5.TabStop = false;
            this.pictureBox5.Click += new System.EventHandler(this.pictureBox5_Click);
            // 
            // pictureBox3
            // 
            this.pictureBox3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox3.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox3.Image = global::WaveCraft.Properties.Resources.loginc;
            this.pictureBox3.InitialImage = null;
            this.pictureBox3.Location = new System.Drawing.Point(12, 236);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(70, 70);
            this.pictureBox3.TabIndex = 222;
            this.pictureBox3.TabStop = false;
            this.pictureBox3.Click += new System.EventHandler(this.pictureBox3_Click);
            // 
            // pictureBox2
            // 
            this.pictureBox2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox2.Image = global::WaveCraft.Properties.Resources.dec;
            this.pictureBox2.InitialImage = null;
            this.pictureBox2.Location = new System.Drawing.Point(12, 160);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(70, 70);
            this.pictureBox2.TabIndex = 221;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.Click += new System.EventHandler(this.pictureBox2_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox1.Image = global::WaveCraft.Properties.Resources.inc;
            this.pictureBox1.InitialImage = null;
            this.pictureBox1.Location = new System.Drawing.Point(12, 84);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(70, 70);
            this.pictureBox1.TabIndex = 220;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // pictureBox4
            // 
            this.pictureBox4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox4.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox4.Image = global::WaveCraft.Properties.Resources.flat;
            this.pictureBox4.InitialImage = null;
            this.pictureBox4.Location = new System.Drawing.Point(12, 8);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(70, 70);
            this.pictureBox4.TabIndex = 219;
            this.pictureBox4.TabStop = false;
            this.pictureBox4.Click += new System.EventHandler(this.pictureBox4_Click);
            // 
            // textBoxNumSines
            // 
            this.textBoxNumSines.BackColor = System.Drawing.Color.Black;
            this.textBoxNumSines.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxNumSines.ForeColor = System.Drawing.Color.White;
            this.textBoxNumSines.Location = new System.Drawing.Point(58, 435);
            this.textBoxNumSines.Name = "textBoxNumSines";
            this.textBoxNumSines.Size = new System.Drawing.Size(24, 23);
            this.textBoxNumSines.TabIndex = 218;
            this.textBoxNumSines.Text = "1";
            // 
            // pictureBox12
            // 
            this.pictureBox12.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox12.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox12.Image = global::WaveCraft.Properties.Resources.sine;
            this.pictureBox12.InitialImage = null;
            this.pictureBox12.Location = new System.Drawing.Point(12, 388);
            this.pictureBox12.Name = "pictureBox12";
            this.pictureBox12.Size = new System.Drawing.Size(70, 70);
            this.pictureBox12.TabIndex = 230;
            this.pictureBox12.TabStop = false;
            this.pictureBox12.Click += new System.EventHandler(this.pictureBox12_Click);
            // 
            // textBoxNumIncSines
            // 
            this.textBoxNumIncSines.BackColor = System.Drawing.Color.Black;
            this.textBoxNumIncSines.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxNumIncSines.ForeColor = System.Drawing.Color.White;
            this.textBoxNumIncSines.Location = new System.Drawing.Point(134, 283);
            this.textBoxNumIncSines.Name = "textBoxNumIncSines";
            this.textBoxNumIncSines.Size = new System.Drawing.Size(24, 23);
            this.textBoxNumIncSines.TabIndex = 231;
            this.textBoxNumIncSines.Text = "1";
            // 
            // textBoxNumDecSines
            // 
            this.textBoxNumDecSines.BackColor = System.Drawing.Color.Black;
            this.textBoxNumDecSines.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxNumDecSines.ForeColor = System.Drawing.Color.White;
            this.textBoxNumDecSines.Location = new System.Drawing.Point(134, 359);
            this.textBoxNumDecSines.Name = "textBoxNumDecSines";
            this.textBoxNumDecSines.Size = new System.Drawing.Size(24, 23);
            this.textBoxNumDecSines.TabIndex = 232;
            this.textBoxNumDecSines.Text = "1";
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
            this.buttonCancel.Location = new System.Drawing.Point(710, 549);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(87, 22);
            this.buttonCancel.TabIndex = 160;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonApply
            // 
            this.buttonApply.Active = false;
            this.buttonApply.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.buttonApply.FlatAppearance.BorderSize = 2;
            this.buttonApply.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonApply.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonApply.ForeColor = System.Drawing.Color.Black;
            this.buttonApply.HorizontalGradient = false;
            this.buttonApply.Location = new System.Drawing.Point(485, 549);
            this.buttonApply.Name = "buttonApply";
            this.buttonApply.Size = new System.Drawing.Size(87, 22);
            this.buttonApply.TabIndex = 159;
            this.buttonApply.Text = "Apply";
            this.buttonApply.UseVisualStyleBackColor = true;
            this.buttonApply.Click += new System.EventHandler(this.buttonApply_Click);
            // 
            // pictureBox8
            // 
            this.pictureBox8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox8.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox8.Image = global::WaveCraft.Properties.Resources.ripples;
            this.pictureBox8.InitialImage = null;
            this.pictureBox8.Location = new System.Drawing.Point(88, 388);
            this.pictureBox8.Name = "pictureBox8";
            this.pictureBox8.Size = new System.Drawing.Size(70, 70);
            this.pictureBox8.TabIndex = 233;
            this.pictureBox8.TabStop = false;
            this.pictureBox8.Click += new System.EventHandler(this.pictureBox8_Click);
            // 
            // textBoxFlatValue
            // 
            this.textBoxFlatValue.BackColor = System.Drawing.Color.Black;
            this.textBoxFlatValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxFlatValue.ForeColor = System.Drawing.Color.White;
            this.textBoxFlatValue.Location = new System.Drawing.Point(38, 55);
            this.textBoxFlatValue.Name = "textBoxFlatValue";
            this.textBoxFlatValue.Size = new System.Drawing.Size(44, 23);
            this.textBoxFlatValue.TabIndex = 234;
            this.textBoxFlatValue.Text = "3.142";
            // 
            // pictureBoxFadeOutSines
            // 
            this.pictureBoxFadeOutSines.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBoxFadeOutSines.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBoxFadeOutSines.Image = global::WaveCraft.Properties.Resources.fadeoutsine;
            this.pictureBoxFadeOutSines.InitialImage = null;
            this.pictureBoxFadeOutSines.Location = new System.Drawing.Point(88, 464);
            this.pictureBoxFadeOutSines.Name = "pictureBoxFadeOutSines";
            this.pictureBoxFadeOutSines.Size = new System.Drawing.Size(70, 70);
            this.pictureBoxFadeOutSines.TabIndex = 237;
            this.pictureBoxFadeOutSines.TabStop = false;
            this.pictureBoxFadeOutSines.Click += new System.EventHandler(this.pictureBoxFadeOutSines_Click);
            // 
            // pictureBoxFadeInSines
            // 
            this.pictureBoxFadeInSines.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBoxFadeInSines.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBoxFadeInSines.Image = global::WaveCraft.Properties.Resources.fadeinsine;
            this.pictureBoxFadeInSines.InitialImage = null;
            this.pictureBoxFadeInSines.Location = new System.Drawing.Point(12, 464);
            this.pictureBoxFadeInSines.Name = "pictureBoxFadeInSines";
            this.pictureBoxFadeInSines.Size = new System.Drawing.Size(70, 70);
            this.pictureBoxFadeInSines.TabIndex = 236;
            this.pictureBoxFadeInSines.TabStop = false;
            this.pictureBoxFadeInSines.Click += new System.EventHandler(this.pictureBoxFadeInSines_Click);
            // 
            // textBoxFadeInSines
            // 
            this.textBoxFadeInSines.BackColor = System.Drawing.Color.Black;
            this.textBoxFadeInSines.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxFadeInSines.ForeColor = System.Drawing.Color.White;
            this.textBoxFadeInSines.Location = new System.Drawing.Point(58, 511);
            this.textBoxFadeInSines.Name = "textBoxFadeInSines";
            this.textBoxFadeInSines.Size = new System.Drawing.Size(24, 23);
            this.textBoxFadeInSines.TabIndex = 238;
            this.textBoxFadeInSines.Text = "1";
            // 
            // textBoxFadeOutSines
            // 
            this.textBoxFadeOutSines.BackColor = System.Drawing.Color.Black;
            this.textBoxFadeOutSines.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxFadeOutSines.ForeColor = System.Drawing.Color.White;
            this.textBoxFadeOutSines.Location = new System.Drawing.Point(134, 511);
            this.textBoxFadeOutSines.Name = "textBoxFadeOutSines";
            this.textBoxFadeOutSines.Size = new System.Drawing.Size(24, 23);
            this.textBoxFadeOutSines.TabIndex = 239;
            this.textBoxFadeOutSines.Text = "1";
            // 
            // FormPhase
            // 
            this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.ClientSize = new System.Drawing.Size(1254, 596);
            this.Controls.Add(this.textBoxFadeOutSines);
            this.Controls.Add(this.textBoxFadeInSines);
            this.Controls.Add(this.pictureBoxFadeOutSines);
            this.Controls.Add(this.pictureBoxFadeInSines);
            this.Controls.Add(this.textBoxFlatValue);
            this.Controls.Add(this.pictureBox8);
            this.Controls.Add(this.textBoxNumDecSines);
            this.Controls.Add(this.textBoxNumIncSines);
            this.Controls.Add(this.pictureBox11);
            this.Controls.Add(this.pictureBox10);
            this.Controls.Add(this.pictureBox9);
            this.Controls.Add(this.pictureBox7);
            this.Controls.Add(this.pictureBox6);
            this.Controls.Add(this.pictureBox5);
            this.Controls.Add(this.pictureBox3);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.pictureBox4);
            this.Controls.Add(this.textBoxNumSines);
            this.Controls.Add(this.pictureBox12);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonApply);
            this.Controls.Add(this.pictureBoxPhaseShape);
            this.Controls.Add(this.labelPhaseMin);
            this.Controls.Add(this.labelPhaseMax);
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormPhase";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.FormVolume_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPhaseShape)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox11)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox10)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox12)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxFadeOutSines)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxFadeInSines)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label labelPhaseMin;
        private System.Windows.Forms.Label labelPhaseMax;
        private System.Windows.Forms.PictureBox pictureBoxPhaseShape;
        private CustomControls.GradientButton buttonCancel;
        private CustomControls.GradientButton buttonApply;
        private System.Windows.Forms.PictureBox pictureBox11;
        private System.Windows.Forms.PictureBox pictureBox10;
        private System.Windows.Forms.PictureBox pictureBox9;
        private System.Windows.Forms.PictureBox pictureBox7;
        private System.Windows.Forms.PictureBox pictureBox6;
        private System.Windows.Forms.PictureBox pictureBox5;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.TextBox textBoxNumSines;
        private System.Windows.Forms.PictureBox pictureBox12;
        private System.Windows.Forms.TextBox textBoxNumIncSines;
        private System.Windows.Forms.TextBox textBoxNumDecSines;
        private System.Windows.Forms.PictureBox pictureBox8;
        private System.Windows.Forms.TextBox textBoxFlatValue;
        private System.Windows.Forms.PictureBox pictureBoxFadeOutSines;
        private System.Windows.Forms.PictureBox pictureBoxFadeInSines;
        private System.Windows.Forms.TextBox textBoxFadeInSines;
        private System.Windows.Forms.TextBox textBoxFadeOutSines;
    }
}
