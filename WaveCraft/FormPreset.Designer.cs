
namespace WaveCraft
{
    partial class FormPreset
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
            this.buttonBulkEditOverTime = new WaveCraft.CustomControls.GradientButton();
            this.SuspendLayout();
            // 
            // buttonBulkEditOverTime
            // 
            this.buttonBulkEditOverTime.Active = false;
            this.buttonBulkEditOverTime.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.buttonBulkEditOverTime.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonBulkEditOverTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonBulkEditOverTime.ForeColor = System.Drawing.Color.Black;
            this.buttonBulkEditOverTime.HorizontalGradient = false;
            this.buttonBulkEditOverTime.Location = new System.Drawing.Point(578, 750);
            this.buttonBulkEditOverTime.Name = "buttonBulkEditOverTime";
            this.buttonBulkEditOverTime.Size = new System.Drawing.Size(124, 24);
            this.buttonBulkEditOverTime.TabIndex = 172;
            this.buttonBulkEditOverTime.Text = "cancel";
            this.buttonBulkEditOverTime.UseVisualStyleBackColor = true;
            this.buttonBulkEditOverTime.Click += new System.EventHandler(this.buttonBulkEditOverTime_Click);
            // 
            // FormPreset
            // 
            this.ClientSize = new System.Drawing.Size(1282, 794);
            this.Controls.Add(this.buttonBulkEditOverTime);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormPreset";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Select Preset";
            this.Load += new System.EventHandler(this.FormPreset_Load);
            this.ResumeLayout(false);

        }

        #endregion
        private CustomControls.GradientButton buttonBulkEditOverTime;
    }
}
