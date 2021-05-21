using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using System.Windows.Forms;
using WaveCraft.Synth;

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
            if (myParent.FindPresetByName(textBoxPresetName.Text)!=null)
            {
                MessageBox.Show("Preset does already exist!");
            }
            string category = comboBoxCategories.Text;
            if (category.Length==0)
            {
                category = "[default]";
            }
            if (textBoxPresetName.Text.Length > 0)
            {
                PresetItem presetItem = new PresetItem(textBoxPresetName.Text, category);
                myParent.Presets.Add(presetItem);
                myParent.CurrentPreset = presetItem;
                myParent.labelPreset.Text = presetItem.Name;
                myParent.Preset.Save(myParent.SynthGenerator, presetItem);

                textBoxPresetName.Text = "";
            }
            Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void FormAddPreset_Load(object sender, EventArgs e)
        {
            comboBoxCategories.Items.Clear();
            List<string> categories = myParent.GetAllCategories();
            comboBoxCategories.Items.AddRange(categories.ToArray());
        }
    }
}
