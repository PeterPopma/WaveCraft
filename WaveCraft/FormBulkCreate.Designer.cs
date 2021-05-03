
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
            this.buttonCancel = new WaveCraft.CustomControls.GradientButton();
            this.buttonCreate = new WaveCraft.CustomControls.GradientButton();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxAmount = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCustomWave)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBoxCustomWave
            // 
            this.pictureBoxCustomWave.Location = new System.Drawing.Point(69, 12);
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
            this.textBoxFrequency1.Location = new System.Drawing.Point(173, 519);
            this.textBoxFrequency1.Name = "textBoxFrequency1";
            this.textBoxFrequency1.Size = new System.Drawing.Size(63, 23);
            this.textBoxFrequency1.TabIndex = 163;
            // 
            // textBoxFrequency2
            // 
            this.textBoxFrequency2.BackColor = System.Drawing.Color.DimGray;
            this.textBoxFrequency2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxFrequency2.ForeColor = System.Drawing.Color.White;
            this.textBoxFrequency2.Location = new System.Drawing.Point(1006, 519);
            this.textBoxFrequency2.Name = "textBoxFrequency2";
            this.textBoxFrequency2.Size = new System.Drawing.Size(63, 23);
            this.textBoxFrequency2.TabIndex = 164;
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
            this.buttonCancel.Location = new System.Drawing.Point(612, 631);
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
            this.buttonCreate.Location = new System.Drawing.Point(390, 631);
            this.buttonCreate.Name = "buttonCreate";
            this.buttonCreate.Size = new System.Drawing.Size(87, 22);
            this.buttonCreate.TabIndex = 159;
            this.buttonCreate.Text = "Create";
            this.buttonCreate.UseVisualStyleBackColor = true;
            this.buttonCreate.Click += new System.EventHandler(this.buttonApply_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 17);
            this.label2.TabIndex = 165;
            this.label2.Text = "weight";
            // 
            // textBoxAmount
            // 
            this.textBoxAmount.BackColor = System.Drawing.Color.DimGray;
            this.textBoxAmount.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxAmount.ForeColor = System.Drawing.Color.White;
            this.textBoxAmount.Location = new System.Drawing.Point(584, 544);
            this.textBoxAmount.Name = "textBoxAmount";
            this.textBoxAmount.Size = new System.Drawing.Size(84, 38);
            this.textBoxAmount.TabIndex = 166;
            this.textBoxAmount.Text = "10";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(474, 547);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(104, 31);
            this.label3.TabIndex = 167;
            this.label3.Text = "amount";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(896, 519);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(104, 17);
            this.label1.TabIndex = 168;
            this.label1.Text = "max. frequency";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(66, 519);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(101, 17);
            this.label4.TabIndex = 169;
            this.label4.Text = "min. frequency";
            // 
            // FormBulkCreate
            // 
            this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.ClientSize = new System.Drawing.Size(1090, 673);
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
    }
}
