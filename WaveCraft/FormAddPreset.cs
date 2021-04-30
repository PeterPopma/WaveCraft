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
    public partial class FormAddPreset : Form
    {
        private FormMain myParent = null;

        public FormMain MyParent { get => myParent; set => myParent = value; }

        public FormAddPreset()
        {
            InitializeComponent();
        }
        protected override void OnPaintBackground(PaintEventArgs e)
        {
            if (this.ClientRectangle.Width == 0 || this.ClientRectangle.Height == 0)
            {
                return;
            }
            using (LinearGradientBrush brush = new LinearGradientBrush(this.ClientRectangle,
                                                                       Color.FromArgb(70, 77, 95),
                                                                       Color.Black,
                                                                       90F))
            {
                e.Graphics.FillRectangle(brush, this.ClientRectangle);
            }
        }

        private void buttonApply_Click(object sender, EventArgs e)
        {
            if (textBoxPresetName.Text.Length > 0 && !myParent.comboBoxPresets.Items.Contains(textBoxPresetName.Text))
            {
                myParent.Preset.Save(myParent.SynthGenerator, textBoxPresetName.Text);
                myParent.comboBoxPresets.Items.Add(textBoxPresetName.Text);
                myParent.comboBoxPresets.SelectedIndex = myParent.comboBoxPresets.FindStringExact(textBoxPresetName.Text);
                myParent.CurrentPreset = myParent.comboBoxPresets.Text;
                textBoxPresetName.Text = "";
            }
            Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
