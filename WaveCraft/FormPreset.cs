using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using System.Windows.Forms;
using WaveCraft.CustomControls;
using WaveCraft.Synth;

namespace WaveCraft
{
    public partial class FormPreset : Form
    {
        private FormMain myParent = null;

        public FormMain MyParent { get => myParent; set => myParent = value; }

        public FormPreset()
        {
            InitializeComponent();
        }

        private void buttonBulkEditOverTime_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void listViewPresets_SelectedIndexChanged(object sender, EventArgs e)
        {

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

        private void presetbutton_Click(object sender, EventArgs e)
        {
            GradientButton2 myButton = (GradientButton2)sender;
            Cursor = Cursors.WaitCursor;
            myParent.ChangedPresetData = false;
            myParent.CurrentPreset = myParent.FindPresetByName(myButton.Text);
            myParent.labelPreset.Text = myButton.Text;
            try
            {
                myParent.Preset.Load(myParent.SynthGenerator, myButton.Text);
            }
            catch (Exception)
            {

            }
            myParent.UpdateWaveControls();
            myParent.SynthGenerator.UpdateAllWaveData();
            Cursor = Cursors.Default;
            Close();
        }

        private void FormPreset_Load(object sender, EventArgs e)
        {
            List<string> categories = myParent.GetAllCategories();
            int x = 20;
            int[] y = new int[] { 20, 20 };
            for (int i=0; i < categories.Count; i++)
            {
                string category = categories[i];
                int x_offset = i%2 * (Width / 2);
                Label label = new Label();
                label.BackColor = Color.FromArgb(0, 0, 0, 0);
                label.ForeColor = Color.FromArgb(192, 192, 255);
                label.Font = new Font("Serif", 16, FontStyle.Bold);
                label.Text = category;
                label.Left = x + x_offset;
                label.Top = y[i % 2];
                label.Height = 30;
                label.Width = Width/2 - 40;
                Controls.Add(label);
                y[i % 2] += label.Height + 4;
                foreach (PresetItem presetItem in myParent.Presets)
                {
                    if (presetItem.Category.Equals(category))
                    {
                        if (x > Width / 2 - 150)       // next row of presets
                        {
                            y[i % 2] += 30;
                            x = 20;
                        }
                        GradientButton2 button = new GradientButton2();
                        button.Left = x + x_offset;
                        button.Top = y[i % 2];
                        button.Text = presetItem.Name;
                        button.Width = 120; 
                        button.ForeColor = Color.White;
                        button.Font = new Font("Serif", 8.25f, FontStyle.Bold);
                        button.FlatStyle = FlatStyle.Standard;
                        button.Click += new EventHandler(presetbutton_Click);
                        Controls.Add(button);
                        x += button.Width + 8;
                    }
                }
                x = 20;
                y[i % 2] += 40;
            }
        }
    }
}
