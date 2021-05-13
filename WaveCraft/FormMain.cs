using WaveCraft.Synth;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Windows.Forms;
using System.Collections.Generic;

namespace WaveCraft
{
    public partial class FormMain : Form
    {
        Timer aTimer = new Timer();
        bool generatorEnabled = false;
        bool changedPresetData = false;
        bool isPlaying = false;
//        private int[] waveData;

        Random random = new Random();
        SynthGenerator synthGenerator;
        Preset preset = new Preset();
        string currentPreset = "";

        internal SynthGenerator SynthGenerator { get => synthGenerator; set => synthGenerator = value; }
        public bool ChangedPresetData { get => changedPresetData; set => changedPresetData = value; }
        internal Preset Preset { get => preset; set => preset = value; }
        public string CurrentPreset { get => currentPreset; set => currentPreset = value; }

        public FormMain()
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

        private void GroupBoxPaint(object sender, PaintEventArgs e)
        {
            Control control = (Control)sender;

            using (LinearGradientBrush brush = new LinearGradientBrush(control.ClientRectangle,
                                                                       Color.Black,
                                                                       Color.FromArgb(70, 77, 95),
                                                                       90F))
            {
                e.Graphics.FillRectangle(brush, control.ClientRectangle);
                ControlPaint.DrawBorder(e.Graphics, control.ClientRectangle, Color.Gray, ButtonBorderStyle.Solid);
            }
        }

        public void UpdateWaveFormPicture()
        {
            switch (synthGenerator.CurrentWave.WaveForm)
            {
                case "Sine":
                    pictureBoxWaveForm.Image = Properties.Resources.sine;
                    break;
                case "Square":
                    pictureBoxWaveForm.Image = Properties.Resources.square;
                    break;
                case "Triangle":
                    pictureBoxWaveForm.Image = Properties.Resources.triangle;
                    break;
                case "Sawtooth":
                    pictureBoxWaveForm.Image = Properties.Resources.sawtooth;
                    break;
                case "Noise":
                    pictureBoxWaveForm.Image = Properties.Resources.random;
                    break;
                case "WavFile":
                    pictureBoxWaveForm.Image = Properties.Resources.wav;
                    break;
                case "Custom":
                    pictureBoxWaveForm.Image = Properties.Resources.custom;
                    break;
                case "CustomBeginEnd":
                    pictureBoxWaveForm.Image = Properties.Resources.custom2;
                    break;
            }
        }
        public void UpdateWaveControls()
        {
            generatorEnabled = false;

            colorSliderDuration.Value = (decimal)(10000.0 * synthGenerator.CurrentWave.NumSamples() / synthGenerator.SamplesPerSecond);
            colorSliderDelay.Value = (decimal)(10000.0 * synthGenerator.CurrentWave.StartPosition / synthGenerator.SamplesPerSecond);
            textBoxDuration.Text = string.Format("{0:0.0000}", DurationSliderToText());
            textBoxDelay.Text = string.Format("{0:0.0000}", DelaySliderToText());
            UpdateChannelText();
            UpdateWaveFormPicture();

            if (synthGenerator.CurrentWave.WaveFile.Length > 0)
            {
                labelFileName.Text = Path.GetFileName(SynthGenerator.CurrentWave.WaveFile);
                labelFileName.ForeColor = Color.Yellow;
            }
            else
            {
                labelFileName.Text = "select .wav file";
                labelFileName.ForeColor = Color.White;
            }

            colorSliderAttack.Value = (decimal)synthGenerator.EnvelopAttack * 100;
            colorSliderHold.Value = (decimal)synthGenerator.EnvelopHold * 100;
            colorSliderDecay.Value = (decimal)synthGenerator.EnvelopDecay * 100;
            colorSliderSustain.Value = (decimal)synthGenerator.EnvelopSustain * 100;
            colorSliderRelease.Value = (decimal)synthGenerator.EnvelopRelease * 100;

            labelVolumeMin.Text = synthGenerator.CurrentWave.MinVolume.ToString();
            labelVolumeMax.Text = synthGenerator.CurrentWave.MaxVolume.ToString();
            labelFrequencyMin.Text = synthGenerator.CurrentWave.MinFrequency.ToString("0.00");
            labelFrequencyMax.Text = synthGenerator.CurrentWave.MaxFrequency.ToString("0.00");
            labelWeightMin.Text = synthGenerator.CurrentWave.MinWeight.ToString();
            labelWeightMax.Text = synthGenerator.CurrentWave.MaxWeight.ToString();

            UpdateVisibility();

            generatorEnabled = true;
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
//            waveData = new int[pictureBoxCustomWave.Width];

            groupBox1.Paint += new PaintEventHandler(GroupBoxPaint);
            groupBox1.Refresh();

            groupBox2.Paint += new PaintEventHandler(GroupBoxPaint);
            groupBox2.Refresh();
            
            groupBox3.Paint += new PaintEventHandler(GroupBoxPaint);
            groupBox3.Refresh();

            groupBox5.Paint += new PaintEventHandler(GroupBoxPaint);
            groupBox5.Refresh();

            groupBox6.Paint += new PaintEventHandler(GroupBoxPaint);
            groupBox6.Refresh();

            groupBox4.Paint += new PaintEventHandler(GroupBoxPaint);
            groupBox4.Refresh();

            groupBox7.Paint += new PaintEventHandler(GroupBoxPaint);
            groupBox7.Refresh();

            groupBox8.Paint += new PaintEventHandler(GroupBoxPaint);
            groupBox8.Refresh();
            
            synthGenerator = new SynthGenerator(this);

            // Init generators
            synthGenerator.Waves.Add(new WaveInfo(synthGenerator.SamplesPerSecond));
            synthGenerator.CurrentWave = synthGenerator.Waves[0];

            UpdateWaveControls();

            chartAHDSR.ChartAreas[0].AxisY.Minimum = 0;
            chartAHDSR.ChartAreas[0].AxisY.Maximum = 1;

            chartCurrentWave.ChartAreas[0].AxisY.Maximum = 40000;
            chartCurrentWave.ChartAreas[0].AxisY.Minimum = -40000;

            chartResultLeft.ChartAreas[0].AxisY.Maximum = 40000;
            chartResultLeft.ChartAreas[0].AxisY.Minimum = -40000;
            chartResultRight.ChartAreas[0].AxisY.Maximum = 40000;
            chartResultRight.ChartAreas[0].AxisY.Minimum = -40000;

            generatorEnabled = false;

            // find all presets
            try
            {
                string[] fileEntries = Directory.GetFiles("presets\\");
                foreach (string fileName in fileEntries)
                {
                    if (fileName.EndsWith(".pst"))
                    {
                        comboBoxPresets.Items.Add(fileName.Substring(8, fileName.Length - 12));
                    }
                }
            }
            catch (System.IO.DirectoryNotFoundException)
            {
                Directory.CreateDirectory("presets");
            }
            if (comboBoxPresets.Items.Count > 0)
            {
                comboBoxPresets.SelectedIndex = 0;
                currentPreset = comboBoxPresets.Items[0].ToString();
            }

            RecreateWavesLists();

            synthGenerator.UpdateAllWaveData();
            generatorEnabled = true;
        }

        private void UpdateChannelText()
        {
            switch (synthGenerator.CurrentWave.Channel)
            {
                case 0:
                    labelChannel.Text = "left";
                    break;
                case 1:
                    labelChannel.Text = "right";
                    break;
                case 2:
                    labelChannel.Text = "both";
                    break;
            }
        }

        public void UpdateMixedSound()
        {
            if (generatorEnabled)
            {
                ChangedPresetData = true;
                synthGenerator.UpdateMixedSound();
            }
        }

        private void TimerEventProcessor(Object myObject,
                                                   EventArgs myEventArgs)
        {
            synthGenerator.Play();
        }

        private void labelChannel1_Click(object sender, EventArgs e)
        {
            synthGenerator.CurrentWave.Channel++;
            if (synthGenerator.CurrentWave.Channel == 3)
            {
                synthGenerator.CurrentWave.Channel = 0;
            }
            UpdateChannelText();
            synthGenerator.UpdateCurrentWaveData();
        }

        private void colorSliderAttack_ValueChanged_1(object sender, EventArgs e)
        {
            labelAttack.Text = string.Format("{0:0.0} %", (double)colorSliderAttack.Value);
            synthGenerator.EnvelopAttack = (float)(colorSliderAttack.Value / 100);
            synthGenerator.UpdateADSRChart();
        }

        private void colorSliderDecay_ValueChanged_1(object sender, EventArgs e)
        {
            labelDecay.Text = string.Format("{0:0.0} %", (double)colorSliderDecay.Value);
            synthGenerator.EnvelopDecay = (float)(colorSliderDecay.Value / 100);
            synthGenerator.UpdateADSRChart();
        }

        private void colorSliderSustain_ValueChanged_1(object sender, EventArgs e)
        {
            labelSustain.Text = string.Format("{0:0.0} %", (double)colorSliderSustain.Value);
            synthGenerator.EnvelopSustain = (float)(colorSliderSustain.Value / 100);
            synthGenerator.UpdateADSRChart();
        }

        private void colorSliderRelease_ValueChanged_1(object sender, EventArgs e)
        {
            labelRelease.Text = string.Format("{0:0.0} %", (double)colorSliderRelease.Value);
            synthGenerator.EnvelopRelease = (float)(colorSliderRelease.Value / 100);
            synthGenerator.UpdateADSRChart();
        }

        private void colorSliderAttack_MouseUp(object sender, MouseEventArgs e)
        {
            UpdateMixedSound();
        }

        private void colorSliderDecay_MouseUp(object sender, MouseEventArgs e)
        {
            UpdateMixedSound();
        }

        private void colorSliderSustain_MouseUp(object sender, MouseEventArgs e)
        {
            UpdateMixedSound();
        }

        private void colorSliderRelease_MouseUp(object sender, MouseEventArgs e)
        {
            UpdateMixedSound();
        }

        private void colorSliderDuration1_MouseUp(object sender, MouseEventArgs e)
        {
            UpdateDuration();
            textBoxDuration.Text = string.Format("{0:0.0000}", DurationSliderToText());
        }

        private int DurationToNumSamples()
        {
            return (int)(synthGenerator.SamplesPerSecond * Convert.ToDouble(colorSliderDuration.Value) / 10000.0) * SynthGenerator.NUM_AUDIO_CHANNELS;
        }

        private double DurationSliderToText()
        {
            return Convert.ToDouble(colorSliderDuration.Value) / 10000.0;
        }

        private int DelayToStartPosition()
        {
            return (int)(synthGenerator.SamplesPerSecond * Convert.ToDouble(colorSliderDelay.Value) / 10000.0);
        }

        private double DelaySliderToText()
        {
            return Convert.ToDouble(colorSliderDelay.Value) / 10000.0;
        }

        private void UpdateDuration()
        {
            if (synthGenerator.CurrentWave.WaveForm == "WavFile")
            {
                // The wav file is selected; stretch its wavefile data to the new duration
                // TODO
            }
            else
            {
                synthGenerator.CurrentWave.WaveData = new double[DurationToNumSamples()];
            }
            synthGenerator.UpdateCurrentWaveData();
        }

        private void colorSliderDelay1_MouseUp(object sender, MouseEventArgs e)
        {
            synthGenerator.CurrentWave.StartPosition = DelayToStartPosition();
            synthGenerator.UpdateCurrentWaveData();
            textBoxDelay.Text = string.Format("{0:0.0000}", DelaySliderToText());
        }

        private void colorSliderWeight1_MouseUp(object sender, MouseEventArgs e)
        {
            synthGenerator.UpdateCurrentWaveData();
        }

        private void comboBoxPresets_SelectedIndexChanged(object sender, EventArgs e)
        {
            ChangedPresetData = false;
            currentPreset = comboBoxPresets.Text;
            try
            {
                preset.Load(synthGenerator, currentPreset);
            } catch (Exception)
            {

            }
            RecreateWavesLists();
            UpdateWaveControls();
            synthGenerator.UpdateAllWaveData();
        }

        private void colorSliderHold_ValueChanged(object sender, EventArgs e)
        {
            labelHold.Text = string.Format("{0:0.0} %", (double)colorSliderHold.Value);
            synthGenerator.EnvelopHold = (float)(colorSliderHold.Value / 100);
            synthGenerator.UpdateADSRChart();
        }

        public void UpdateVisibility()
        {
            if (synthGenerator.CurrentWave.WaveForm.Equals("WavFile") || synthGenerator.CurrentWave.WaveForm.Equals("Noise"))
            {
                labelFrequencyMax.Visible = false;
                labelFrequencyMin.Visible = false;
                buttonCreatePartials.Enabled = false;
                labelFrequencyTitle.Visible = false;
                pictureBoxFrequencyShape.Visible = false;
                labelFreqMaxTitle.Visible = false;
                labelFreqMinTitle.Visible = false;
                labelFrequencyMax.Visible = false;
                labelFrequencyMin.Visible = false;
            }
            else
            {
                labelFrequencyMax.Visible = true;
                labelFrequencyMin.Visible = true;
                buttonCreatePartials.Enabled = true;
                labelFrequencyTitle.Visible = true;
                pictureBoxFrequencyShape.Visible = true;
                labelFreqMaxTitle.Visible = true;
                labelFreqMinTitle.Visible = true;
                labelFrequencyMax.Visible = true;
                labelFrequencyMin.Visible = true;
            }
            if (synthGenerator.CurrentWave.WaveForm.Equals("WavFile"))
            {
                labelFileName.Visible = true;
                if (synthGenerator.CurrentWave.WaveFile.Length > 0)
                {
                    buttonConvertToWaves.Visible = true;
                }
            }
            else
            {
                labelFileName.Visible = false;
                buttonConvertToWaves.Visible = false;
            }
            if(synthGenerator.CurrentWave.WaveForm.Equals("Custom") || synthGenerator.CurrentWave.WaveForm.Equals("CustomBeginEnd"))
            {
                buttonChange.Visible = true;
                pictureBoxCustomWave.Visible = true;
            }
            else
            {
                buttonChange.Visible = false;
                pictureBoxCustomWave.Visible = false;
            }
            if (synthGenerator.CurrentWave.WaveForm.Equals("CustomBeginEnd"))
            {
                pictureBoxCustomWave.Width = 178;
                pictureBoxCustomWaveEnd.Visible = true;
            }
            else
            {
                pictureBoxCustomWave.Width = 360;
                pictureBoxCustomWaveEnd.Visible = false;
            }
        }

        private void LoadWavFile()
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "Wave file|*.wav";
            openFileDialog1.Title = "Load .wav file";
            openFileDialog1.ShowDialog();

            // If the file name is not an empty string open it for saving.
            if (openFileDialog1.FileName != "")
            {
                synthGenerator.LoadWaveFile(openFileDialog1.FileName);
                SynthGenerator.UpdateCurrentWaveData();

                // update duration
                colorSliderDuration.Value = (decimal)(10000.0 * SynthGenerator.CurrentWave.NumSamples() / (double)SynthGenerator.SamplesPerSecond);
                textBoxDuration.Text = string.Format("{0:0.0000}", DurationSliderToText());

                labelFileName.Text = Path.GetFileName(SynthGenerator.CurrentWave.WaveFile);
                labelFileName.ForeColor = Color.Yellow;
                buttonConvertToWaves.Visible = true;
            }
        }

        private void pictureBoxWaveForm_Click(object sender, EventArgs e)
        {
            FormWaveForm formWaveForm = new FormWaveForm();
            formWaveForm.MyParent = this;
            formWaveForm.Location = new Point(this.Location.X + 600, this.Location.Y + 50);
            formWaveForm.ShowDialog();
        }

        private void chartResultLeft_Click(object sender, EventArgs e)
        {
            FormAmplitudeChart formAmplitude = new FormAmplitudeChart();
            formAmplitude.MyParent = this;
            formAmplitude.ShowDialog();
        }

        private void chartResultRight_Click(object sender, EventArgs e)
        {
            FormAmplitudeChart formAmplitude = new FormAmplitudeChart();
            formAmplitude.MyParent = this;
            formAmplitude.ShowDialog();
        }

        private void chartResultLeft_MouseMove(object sender, MouseEventArgs e)
        {
            chartResultLeft.Cursor = new Cursor(Properties.Resources.magnifying_glass.Handle);
        }

        private void chartResultRight_MouseMove(object sender, MouseEventArgs e)
        {
            chartResultRight.Cursor = new Cursor(Properties.Resources.magnifying_glass.Handle);
        }

        private void chartFFTLeft_MouseMove(object sender, MouseEventArgs e)
        {
            chartFFTLeft.Cursor = new Cursor(Properties.Resources.magnifying_glass.Handle);
        }

        private void chartFFTRight_MouseMove(object sender, MouseEventArgs e)
        {
            chartFFTRight.Cursor = new Cursor(Properties.Resources.magnifying_glass.Handle);
        }

        private void chartFFTLeft_Click(object sender, EventArgs e)
        {
            FormFFTChart formFFT = new FormFFTChart();
            formFFT.MyParent = this;
            formFFT.ShowDialog();
        }

        private void chartFFTRight_Click(object sender, EventArgs e)
        {
            FormFFTChart formFFT = new FormFFTChart();
            formFFT.MyParent = this;
            formFFT.ShowDialog();
        }

        private void colorSliderHold_MouseUp(object sender, MouseEventArgs e)
        {
            UpdateMixedSound();
        }

        private void buttonSavePreset2_Click(object sender, EventArgs e)
        {
            if (currentPreset.Length > 0)
            {
                ChangedPresetData = false;
                preset.Save(synthGenerator, currentPreset);
            }
        }

        private void buttonAddPreset2_Click(object sender, EventArgs e)
        {
            FormAddPreset formAddPreset = new FormAddPreset();
            formAddPreset.MyParent = this;
            formAddPreset.ShowDialog();
        }

        public void RecreateWavesLists()
        {
            listBoxWaves.Items.Clear();
            foreach (WaveInfo wave in synthGenerator.Waves)
            {
                listBoxWaves.Items.Add(wave.DisplayName);
            }
            listBoxWaves.SelectedIndex = listBoxWaves.FindStringExact(synthGenerator.CurrentWave.DisplayName);
        }

        private void AddToListBoxWaves(WaveInfo newWave)
        {
            listBoxWaves.Items.Add(newWave.DisplayName);
        }

        private void RemoveFromListBoxWaves(WaveInfo waveInfo)
        {
            listBoxWaves.Items.RemoveAt(listBoxWaves.FindStringExact(waveInfo.DisplayName));
        }

        private void buttonAddNewWave_Click(object sender, EventArgs e)
        {
            synthGenerator.CurrentWave = synthGenerator.CloneWave();
            AddToListBoxWaves(synthGenerator.CurrentWave);
            listBoxWaves.SelectedIndex = listBoxWaves.FindStringExact(synthGenerator.CurrentWave.DisplayName);
            UpdateMixedSound();
        }

        private void buttonDeleteWave_Click(object sender, EventArgs e)
        {
            int index = listBoxWaves.SelectedIndex;

            while (listBoxWaves.Items.Count > 1 && listBoxWaves.SelectedItems.Count>0)
            {
                String item = listBoxWaves.SelectedItems[0].ToString();
                synthGenerator.SetCurrentWaveByDisplayName(item);
                RemoveFromListBoxWaves(synthGenerator.CurrentWave);
                synthGenerator.RemoveCurrentWave();
            }

            if (listBoxWaves.Items.Count > index)
            {
                listBoxWaves.SelectedIndex = index;
            }
            else
            {
                listBoxWaves.SelectedIndex = index - 1;
            }
            synthGenerator.SetCurrentWaveByDisplayName(listBoxWaves.SelectedItem.ToString());
            synthGenerator.UpdateCurrentWaveData();
            UpdateWaveControls();
        }

        private void buttonDeleteAll_Click(object sender, EventArgs e)
        {
            while (synthGenerator.Waves.Count > 1)
            {
                synthGenerator.Waves.RemoveAt(synthGenerator.Waves.Count - 1);
            }
            synthGenerator.CurrentWave = synthGenerator.Waves[0];
            listBoxWaves.SelectedIndex = 0;
            synthGenerator.UpdateAllWaveData();
            UpdateWaveControls();
        }

        private void chartResultLeft_Paint(object sender, PaintEventArgs e)
        {
            if (synthGenerator.NumSamples() == 0 || synthGenerator.CurrentWave.Channel == 1)
            {
                return;     // skip if no samples or only right channel
            }
            double widthPercentage = synthGenerator.CurrentWave.NumSamples() / (double)synthGenerator.NumSamples();
            double startPercentage = synthGenerator.CurrentWave.StartPosition / (double)synthGenerator.NumSamples();
            Rectangle rect = new Rectangle(29 + (int)(346 * startPercentage), 4, (int)(346 * widthPercentage), 110);
            Pen myBrush = new Pen(Color.White);
            e.Graphics.DrawRectangle(myBrush, rect);
        }

        private void chartResultRight_Paint(object sender, PaintEventArgs e)
        {
            if (synthGenerator.NumSamples()==0 || synthGenerator.CurrentWave.Channel == 0)
            {
                return;     // skip if no samples or only left channel
            }
            double withPercentage = synthGenerator.CurrentWave.NumSamples() / (double)synthGenerator.NumSamples();
            double startPercentage = synthGenerator.CurrentWave.StartPosition / (double)synthGenerator.NumSamples();
            Rectangle rect = new Rectangle(29 + (int)(346 * startPercentage), 4, (int)(346 * withPercentage), 110);
            System.Drawing.Pen myBrush = new System.Drawing.Pen(System.Drawing.Color.White);
            e.Graphics.DrawRectangle(myBrush, rect);
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (ChangedPresetData)
            {
                DialogResult dialogResult = MessageBox.Show("You made changes to the Preset. Do you want to save them?", "Save Changes", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    preset.Save(synthGenerator, currentPreset);
                }
            }
        }

        private void pictureBoxCustomWave_MouseMove(object sender, MouseEventArgs e)
        {
            pictureBoxCustomWave.Cursor = new Cursor(Properties.Resources.pencil.Handle);
        }

        private void pictureBoxCustomWave_Click(object sender, EventArgs e)
        {
            FormCustomShape formCustomWave = new FormCustomShape();
            formCustomWave.MyParent = this;
            if(synthGenerator.CurrentWave.WaveForm.Equals("Custom"))
            {
                formCustomWave.Text = "Wave Shape";
            }
            else
            {
                formCustomWave.Text = "Wave Shape Begin";
            }

            formCustomWave.ShowDialog();
        }

        private void pictureBoxCustomWave_Paint(object sender, PaintEventArgs e)
        {
            Control control = (Control)sender;
            using (LinearGradientBrush brush = new LinearGradientBrush(control.ClientRectangle,
                                                                       Color.FromArgb(195, 70, 70),
                                                                       Color.FromArgb(15, 0, 0),
                                                                       90F))
            {
                e.Graphics.FillRectangle(brush, control.ClientRectangle);
                ControlPaint.DrawBorder(e.Graphics, control.ClientRectangle, Color.Gray, ButtonBorderStyle.Solid);
            }

            Pen pen = new Pen(Color.White);

            if (synthGenerator.CurrentWave.ShapeWave.Length == SynthGenerator.SHAPE_NUMPOINTS)
            {
                for (int x = 0; x < pictureBoxCustomWave.Width; x++)
                {
                    int position = (int)(x / (double)pictureBoxCustomWave.Width * SynthGenerator.SHAPE_NUMPOINTS);
                    int next_position = (int)((x + 1) / (double)pictureBoxCustomWave.Width * SynthGenerator.SHAPE_NUMPOINTS);
                    if (next_position < SynthGenerator.SHAPE_NUMPOINTS)
                    {
                        int value1 = (int)((SynthGenerator.SHAPE_MAX_VALUE - synthGenerator.CurrentWave.ShapeWave[position]) * (pictureBoxCustomWave.Height / (double)SynthGenerator.SHAPE_MAX_VALUE));
                        int value2 = (int)((SynthGenerator.SHAPE_MAX_VALUE - synthGenerator.CurrentWave.ShapeWave[next_position]) * (pictureBoxCustomWave.Height / (double)SynthGenerator.SHAPE_MAX_VALUE));
                        e.Graphics.DrawLine(pen, new Point(x, value1), new Point(x + 1, value2));
                    }
                }
            }
        }

        private void buttonDeletePreset2_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are you sure you want to delete this Preset?", "Delete Preset", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                preset.Delete(comboBoxPresets.Text);
                comboBoxPresets.Items.RemoveAt(comboBoxPresets.SelectedIndex);
                if (comboBoxPresets.Items.Count > 0)
                {
                    comboBoxPresets.SelectedIndex = 0;
                }
            }
        }

        private void buttonPlayA_Click(object sender, EventArgs e)
        {
            synthGenerator.Play();
        }

        private void buttonPlayG_Click(object sender, EventArgs e)
        {
            synthGenerator.Play(392.00 / 440.0);
        }

        private void buttonPlayB_Click(object sender, EventArgs e)
        {
            synthGenerator.Play(493.88 / 440.0);
        }

        private void buttonPlayBFlat_Click(object sender, EventArgs e)
        {
            synthGenerator.Play(466.16 / 440.0);
        }

        private void buttonPlayGSharp_Click(object sender, EventArgs e)
        {
            synthGenerator.Play(415.30 / 440.0);
        }

        private void buttonPlayFSharp_Click(object sender, EventArgs e)
        {
            synthGenerator.Play(369.99 / 440.0);
        }

        private void buttonPlayF_Click(object sender, EventArgs e)
        {
            synthGenerator.Play(349.23 / 440.0);
        }

        private void buttonPlayE_Click(object sender, EventArgs e)
        {
            synthGenerator.Play(329.63 / 440.0);
        }

        private void buttonPlayC1_Click(object sender, EventArgs e)
        {
            synthGenerator.Play(523.25 / 440.0);
        }

        private void buttonPlayD1_Click(object sender, EventArgs e)
        {
            synthGenerator.Play(587.33 / 440.0);
        }

        private void buttonPlayC5Sharp_Click(object sender, EventArgs e)
        {
            synthGenerator.Play(554.37 / 440.0);
        }

        private void buttonPlayContinuous(object sender, EventArgs e)
        {
            if (!isPlaying)
            {
                synthGenerator.Play();
                buttonPlay.Image = WaveCraft.Properties.Resources.pausebutton;
                isPlaying = true;
                aTimer.Interval = (int)(1000 * synthGenerator.CurrentWave.NumSamples()/synthGenerator.SamplesPerSecond);
                aTimer.Tick += new EventHandler(TimerEventProcessor);
                aTimer.Enabled = true;
            }
            else
            {
                buttonPlay.Image = WaveCraft.Properties.Resources.playbutton;
                isPlaying = false;
                aTimer.Enabled = false;
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "Wave file|*.wav";
            saveFileDialog1.Title = "Save to .wav file";
            saveFileDialog1.ShowDialog();

            // If the file name is not an empty string open it for saving.
            if (saveFileDialog1.FileName != "")
            {
                synthGenerator.Save(saveFileDialog1.FileName);
            }
        }

        private void buttonPlayWav_Click(object sender, EventArgs e)
        {
            synthGenerator.Play(1, false);
            synthGenerator.UpdateMixedSound();     // play single wave overwrites mix buffer
        }

        private void FormMain_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.A:
                    buttonPlayD4.PerformClick();
                    break;
                case Keys.S:
                    buttonPlayE4.PerformClick();
                    break;
                case Keys.D:
                    buttonPlayF4.PerformClick();
                    break;
                case Keys.F:
                    buttonPlayG4.PerformClick();
                    break;
                case Keys.G:
                    buttonPlayA4.PerformClick();
                    break;
                case Keys.H:
                    buttonPlayB4.PerformClick();
                    break;
                case Keys.J:
                    buttonPlayC5.PerformClick();
                    break;
                case Keys.K:
                    buttonPlayD5.PerformClick();
                    break;
                case Keys.L:
                    buttonPlayE5.PerformClick();
                    break;
                case Keys.P:
                    buttonPlayA4.PerformClick();
                    break;
                case Keys.W:
                    buttonPlayD4Sharp.PerformClick();
                    break;
                case Keys.R:
                    buttonPlayF4Sharp.PerformClick();
                    break;
                case Keys.T:
                    buttonPlayB4Flat.PerformClick();
                    break;
                case Keys.Y:
                    buttonPlayA4.PerformClick();
                    break;
                case Keys.I:
                    buttonPlayC5Sharp.PerformClick();
                    break;
                case Keys.Q:
                    buttonPlayD5Sharp.PerformClick();
                    break;
            }
        }

        private void buttonPlayD5Sharp_Click(object sender, EventArgs e)
        {
            synthGenerator.Play(622.25 / 440.0);
        }

        private void buttonPlayE5_Click(object sender, EventArgs e)
        {
            synthGenerator.Play(659.25 / 440.0);
        }

        private void buttonPlayD4Sharp_Click(object sender, EventArgs e)
        {
            synthGenerator.Play(311.13 / 440.0);
        }

        private void buttonPlayD4_Click(object sender, EventArgs e)
        {
            synthGenerator.Play(293.66 / 440.0);
        }

        private void comboBoxPresets_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void buttonDurationMinus1_Click(object sender, EventArgs e)
        {
            if (colorSliderDuration.Value > 1)
            {
                colorSliderDuration.Value -= 1;
                synthGenerator.CurrentWave.WaveData = new double[DurationToNumSamples()];
                synthGenerator.UpdateCurrentWaveData();
                textBoxDuration.Text = string.Format("{0:0.0000}", DurationSliderToText());
            }
        }

        private void buttonDurationMinus10_Click(object sender, EventArgs e)
        {
            if (colorSliderDuration.Value > 99)
            {
                colorSliderDuration.Value -= 100;
                synthGenerator.CurrentWave.WaveData = new double[DurationToNumSamples()];
                synthGenerator.UpdateCurrentWaveData();
                textBoxDuration.Text = string.Format("{0:0.0000}", DurationSliderToText());
            }
        }

        private void buttonDurationPlus10_Click(object sender, EventArgs e)
        {
            if (colorSliderDuration.Value + 100 <= colorSliderDuration.Maximum)
            {
                colorSliderDuration.Value += 100;
                synthGenerator.CurrentWave.WaveData = new double[DurationToNumSamples()];
                synthGenerator.UpdateCurrentWaveData();
                textBoxDuration.Text = string.Format("{0:0.0000}", DurationSliderToText());
            }
        }

        private void buttonDurationPlus1_Click(object sender, EventArgs e)
        {
            if (colorSliderDuration.Value + 1 <= colorSliderDuration.Maximum)
            {
                colorSliderDuration.Value += 1;
                synthGenerator.CurrentWave.WaveData = new double[DurationToNumSamples()];
                synthGenerator.UpdateCurrentWaveData();
                textBoxDuration.Text = string.Format("{0:0.0000}", DurationSliderToText());
            }
        }

        private void buttonDelayMinus10_Click(object sender, EventArgs e)
        {
            if (colorSliderDelay.Value > 99)
            {
                colorSliderDelay.Value -= 100;
                synthGenerator.CurrentWave.StartPosition = DelayToStartPosition();
                synthGenerator.UpdateCurrentWaveData();
                textBoxDelay.Text = string.Format("{0:0.0000}", DelaySliderToText());

            }
        }

        private void buttonDelayMinus1_Click(object sender, EventArgs e)
        {
            if (colorSliderDelay.Value > 1)
            {
                colorSliderDelay.Value -= 1;
                synthGenerator.CurrentWave.StartPosition = DelayToStartPosition();
                synthGenerator.UpdateCurrentWaveData();
                textBoxDelay.Text = string.Format("{0:0.0000}", DelaySliderToText());
            }
        }

        private void buttonDelayPlus1_Click(object sender, EventArgs e)
        {
            if (colorSliderDelay.Value + 1 <= colorSliderDelay.Maximum)
            {
                colorSliderDelay.Value += 1;
                synthGenerator.CurrentWave.StartPosition = DelayToStartPosition();
                synthGenerator.UpdateCurrentWaveData();
                textBoxDelay.Text = string.Format("{0:0.0000}", DelaySliderToText());
            }
        }

        private void buttonDelayPlus10_Click(object sender, EventArgs e)
        {
            if (colorSliderDelay.Value + 100 <= colorSliderDelay.Maximum)
            {
                colorSliderDelay.Value += 100;
                synthGenerator.CurrentWave.StartPosition = DelayToStartPosition();
                synthGenerator.UpdateCurrentWaveData();
                textBoxDelay.Text = string.Format("{0:0.0000}", DelaySliderToText());
            }
        }

        private void pictureBoxFrequencyShape_Click(object sender, EventArgs e)
        {
            FormFrequency formFrequency = new FormFrequency();
            formFrequency.MyParent = this;
            formFrequency.ShowDialog();
        }

        private void pictureBoxVolumeShape_Click(object sender, EventArgs e)
        {
            FormVolume formVolume = new FormVolume();
            formVolume.MyParent = this;
            formVolume.ShowDialog();
        }

        private void pictureBoxVolumeShape_Paint(object sender, PaintEventArgs e)
        {
            Control control = (Control)sender;
            using (LinearGradientBrush brush = new LinearGradientBrush(control.ClientRectangle,
                                                                       Color.FromArgb(70, 87, 195),
                                                                       Color.FromArgb(0, 0, 15),
                                                                       90F))
            {
                e.Graphics.FillRectangle(brush, control.ClientRectangle);
                ControlPaint.DrawBorder(e.Graphics, control.ClientRectangle, Color.Gray, ButtonBorderStyle.Solid);
            }

            Pen pen = new Pen(Color.White);

            if (synthGenerator.CurrentWave.ShapeVolume.Length == SynthGenerator.SHAPE_NUMPOINTS)
            {
                for (int x = 0; x < pictureBoxVolumeShape.Width; x++)
                {
                    int position = (int)(x / (double)pictureBoxVolumeShape.Width * SynthGenerator.SHAPE_NUMPOINTS);
                    int next_position = (int)((x + 1) / (double)pictureBoxVolumeShape.Width * SynthGenerator.SHAPE_NUMPOINTS);
                    if (next_position < SynthGenerator.SHAPE_NUMPOINTS)
                    {
                        int value1 = (int)((SynthGenerator.SHAPE_MAX_VALUE - synthGenerator.CurrentWave.ShapeVolume[position]) * (pictureBoxVolumeShape.Height / (double)SynthGenerator.SHAPE_MAX_VALUE));
                        int value2 = (int)((SynthGenerator.SHAPE_MAX_VALUE - synthGenerator.CurrentWave.ShapeVolume[next_position]) * (pictureBoxVolumeShape.Height / (double)SynthGenerator.SHAPE_MAX_VALUE));
                        e.Graphics.DrawLine(pen, new Point(x, value1), new Point(x + 1, value2));
                    }
                }
            }
        }

        private void pictureBoxFrequencyShape_Paint(object sender, PaintEventArgs e)
        {
            Control control = (Control)sender;
            using (LinearGradientBrush brush = new LinearGradientBrush(control.ClientRectangle,
                                                                       Color.FromArgb(70, 195, 87),
                                                                       Color.FromArgb(0, 15, 0),
                                                                       90F))
            {
                e.Graphics.FillRectangle(brush, control.ClientRectangle);
                ControlPaint.DrawBorder(e.Graphics, control.ClientRectangle, Color.Gray, ButtonBorderStyle.Solid);
            }

            Pen pen = new Pen(Color.White);

            if (synthGenerator.CurrentWave.ShapeFrequency.Length == SynthGenerator.SHAPE_NUMPOINTS)
            {
                for (int x = 0; x < pictureBoxFrequencyShape.Width; x++)
                {
                    int position = (int)(x / (double)pictureBoxFrequencyShape.Width * SynthGenerator.SHAPE_NUMPOINTS);
                    int next_position = (int)((x + 1) / (double)pictureBoxFrequencyShape.Width * SynthGenerator.SHAPE_NUMPOINTS);
                    if (next_position < SynthGenerator.SHAPE_NUMPOINTS)
                    {
                        int value1 = (int)((SynthGenerator.SHAPE_MAX_VALUE - synthGenerator.CurrentWave.ShapeFrequency[position]) * (pictureBoxFrequencyShape.Height / (double)SynthGenerator.SHAPE_MAX_VALUE));
                        int value2 = (int)((SynthGenerator.SHAPE_MAX_VALUE - synthGenerator.CurrentWave.ShapeFrequency[next_position]) * (pictureBoxFrequencyShape.Height / (double)SynthGenerator.SHAPE_MAX_VALUE));
                        e.Graphics.DrawLine(pen, new Point(x, value1), new Point(x + 1, value2));
                    }
                }
            }
        }

        private void CreateInharmonics()
        {
            if(!radioButtonUnderTones.Checked)
            {
                CreateInharmonics(false);
            }
            if (!radioButtonOverTones.Checked)
            {
                CreateInharmonics(true);
            }
        }

        private void CreateInharmonics(bool undertones)
        {
            for (int inharmonic_number = 1; inharmonic_number <= numericUpDownAmount.Value; inharmonic_number++)
            {
                double durationFactor = Math.Pow(((double)numericUpDownDuration.Value / 100.0), inharmonic_number);
                double deviation = inharmonic_number / (double)numericUpDownAmount.Value * (double)numericUpDownSpread.Value / 100.0;
                if (checkBoxRandomFrequency.Checked)
                {
                    // choose a random spot between this and the previous deviation
                    double previous_deviation = (inharmonic_number - 1) / (double)numericUpDownAmount.Value * (double)numericUpDownSpread.Value / 100.0;
                    double place = random.Next(1000) / 1000.0;
                    deviation = (previous_deviation * place) + (deviation * (1 - place));
                }
                double frequency_factor = 1 + deviation;
                if (undertones)
                {
                    frequency_factor = 1 / frequency_factor;
                }

                if ((frequency_factor * synthGenerator.CurrentWave.MinFrequency) < SynthGenerator.MAX_FREQUENCY && (frequency_factor * synthGenerator.CurrentWave.MaxFrequency) < SynthGenerator.MAX_FREQUENCY)
                {
                    WaveInfo newWave = synthGenerator.CloneWave(frequency_factor);

                    // Note: this only works if the fundamental wave has a flat weight-shape and a max. weight of > 10
                    if ((double)numericUpDownWeightChange.Value / 100.0 > 0)       // increase in weight
                    {
                        int[] WaveData = new int[SynthGenerator.SHAPE_NUMPOINTS];
                        double factor = 1 - (double)Math.Abs(numericUpDownWeightChange.Value) / 100.0;
                        newWave.MinWeight = (int)(factor * newWave.MaxWeight);
                        Shapes.IncreasingLineair(WaveData);
                        newWave.ShapeWeight = WaveData;
                    }
                    if ((double)numericUpDownWeightChange.Value / 100.0 < 0)       // decrease in weight
                    {
                        int[] WaveData = new int[SynthGenerator.SHAPE_NUMPOINTS];
                        double factor = 1 - (double)Math.Abs(numericUpDownWeightChange.Value) / 100.0;
                        newWave.MinWeight = (int)(factor * newWave.MaxWeight);
                        Shapes.DecreasingLineair(WaveData);
                        newWave.ShapeWeight = WaveData;
                    }

                    double weightFactor = Math.Pow(((double)numericUpDownWeight.Value / 100.0), inharmonic_number);
                    newWave.MinWeight = (int)(newWave.MinWeight * weightFactor);
                    newWave.MaxWeight = (int)(newWave.MaxWeight * weightFactor);

                    newWave.SetNumSamples((int)(newWave.NumSamples() * durationFactor));
                    if (numericUpDownTimeShift.Value > 0)
                    {
                        newWave.StartPosition += (int)(newWave.NumSamples() * Convert.ToDouble(inharmonic_number * numericUpDownTimeShift.Value) / 100.0);
                    }
                    newWave.UpdateDisplayName();
                    AddToListBoxWaves(newWave);
                }
            }
        }

        private void CreateHarmonic(int harmonic_number, double frequency_factor)
        {
            double durationFactor = Math.Pow(((double)numericUpDownDuration.Value / 100.0), harmonic_number);

            WaveInfo newWave = synthGenerator.CloneWave(frequency_factor);

            // Note: this only works if the fundamental wave has a flat weight-shape and a max. weight of > 10
            if ((double)numericUpDownWeightChange.Value / 100.0 > 0)       // increase in weight
            {
                double factor = 1 - (double)Math.Abs(numericUpDownWeightChange.Value) / 100.0;
                newWave.MinWeight = (int)(factor * newWave.MaxWeight);
                Shapes.IncreasingLineair(newWave.ShapeWeight);
            }
            if ((double)numericUpDownWeightChange.Value / 100.0 < 0)       // decrease in weight
            {
                double factor = 1 - (double)Math.Abs(numericUpDownWeightChange.Value) / 100.0;
                newWave.MinWeight = (int)(factor * newWave.MaxWeight);
                Shapes.DecreasingLineair(newWave.ShapeWeight);
            }

            double weightFactor = Math.Pow(((double)numericUpDownWeight.Value / 100.0), harmonic_number);
            newWave.MinWeight = (int)(newWave.MinWeight * weightFactor);
            newWave.MaxWeight = (int)(newWave.MaxWeight * weightFactor);

            newWave.SetNumSamples((int)(newWave.NumSamples() * durationFactor));
            if(numericUpDownTimeShift.Value>0)
            {
                newWave.StartPosition += (int)(newWave.NumSamples()* Convert.ToDouble(harmonic_number*numericUpDownTimeShift.Value)/100.0);
            }
            newWave.UpdateDisplayName();
            AddToListBoxWaves(newWave);
        }

        private void CreateHarmonics(int startFactor)
        {
            int num_undertones = radioButtonOverTones.Checked ? 0 : (int)numericUpDownAmount.Value;
            int num_overtones = radioButtonUnderTones.Checked ? 0 : (int)numericUpDownAmount.Value;

            int harmonic_factor = startFactor;
            for (int harmonic_number=1; harmonic_number<=num_undertones; harmonic_number++)
            {
                CreateHarmonic(harmonic_number, 1 / (double)harmonic_factor);
                if (radioButtonEvenOddHarmonics.Checked == false)
                {
                    harmonic_factor += 2;
                }
                else
                {
                    harmonic_factor++;
                }
            }

            harmonic_factor = startFactor;
            for (int harmonic_number = 1; harmonic_number <= num_overtones; harmonic_number++)
            {
                CreateHarmonic(harmonic_number, (double)harmonic_factor);
                if (radioButtonEvenOddHarmonics.Checked == false)
                {
                    harmonic_factor += 2;
                }
                else
                {
                    harmonic_factor++;
                }
            }
        }

        private void buttonCreatePartials_Click(object sender, EventArgs e)
        {
            if (radioButtonOddHarmonics.Checked)
            {
                CreateHarmonics(3);
            }
            if (radioButtonEvenHarmonics.Checked || radioButtonEvenOddHarmonics.Checked)
            {
                CreateHarmonics(2);
            }
            if (radioButtonInharmonics.Checked)
            {
                CreateInharmonics();
            }

            synthGenerator.UpdateAllWaveData();
            UpdateSelectedWave();
        }

        private void pictureBoxFrequencyShape_MouseMove(object sender, MouseEventArgs e)
        {
            pictureBoxFrequencyShape.Cursor = new Cursor(Properties.Resources.pencil.Handle);
        }

        private void pictureBoxVolumeShape_MouseMove(object sender, MouseEventArgs e)
        {
            pictureBoxVolumeShape.Cursor = new Cursor(Properties.Resources.pencil.Handle);
        }

        private void textBoxDuration_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                colorSliderDuration.Value = (int)(Convert.ToDecimal(textBoxDuration.Text) * 10000);
                UpdateDuration();
            }
            catch (Exception)
            {
            }
        }

        private void UpdateSelectedWave()
        {
            if (listBoxWaves.SelectedItem != null && generatorEnabled)
            {
                synthGenerator.SetCurrentWaveByDisplayName(listBoxWaves.SelectedItem.ToString());
                synthGenerator.UpdateWaveGraph();
                pictureBoxCustomWave.Refresh();
                pictureBoxCustomWaveEnd.Refresh();
                pictureBoxFrequencyShape.Refresh();
                pictureBoxVolumeShape.Refresh();
                pictureBoxWeightShape.Refresh();
                chartResultLeft.Refresh();
                chartResultRight.Refresh();
                UpdateWaveControls();
            }
        }

        private void listBoxWaves_MouseUp(object sender, MouseEventArgs e)
        {
            UpdateSelectedWave();
        }

        private void textBoxDelay_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                colorSliderDelay.Value = (int)(Convert.ToDecimal(textBoxDelay.Text) * 10000);
                synthGenerator.CurrentWave.StartPosition = DelayToStartPosition();
                synthGenerator.UpdateCurrentWaveData();
            }
            catch (Exception)
            {
            }
        }

        private void buttonChange_Click(object sender, EventArgs e)
        {
            FormWaveForm formWaveForm = new FormWaveForm();
            formWaveForm.MyParent = this;
            formWaveForm.Location = new Point(this.Location.X + 600, this.Location.Y + 50);
            formWaveForm.ShowDialog();
        }

        private void listBoxWaves_KeyUp(object sender, KeyEventArgs e)
        {
            UpdateSelectedWave();
        }

        private void ChangePartialsVisibility()
        {
            if (radioButtonEvenHarmonics.Checked || radioButtonOddHarmonics.Checked)
            {
                numericUpDownSpread.Enabled = false;
                checkBoxRandomFrequency.Enabled = false;
            }
            else
            {
                numericUpDownSpread.Enabled = true;
                checkBoxRandomFrequency.Enabled = true;
            }
        }

        private void radioButtonEvenHarmonics_CheckedChanged(object sender, EventArgs e)
        {
            ChangePartialsVisibility();
        }

        private void radioButtonOddHarmonics_CheckedChanged(object sender, EventArgs e)
        {
            ChangePartialsVisibility();
        }

        private void radioButtonInharmonics_CheckedChanged(object sender, EventArgs e)
        {
            ChangePartialsVisibility();
        }

        private void radioButtonSplit_CheckedChanged(object sender, EventArgs e)
        {
            ChangePartialsVisibility();
        }

        private void radioButtonMerge_CheckedChanged(object sender, EventArgs e)
        {
            ChangePartialsVisibility();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FormSettings formSettings = new FormSettings();
            formSettings.MyParent = this;
            formSettings.ShowDialog();
        }

        private void labelFileName_Click(object sender, EventArgs e)
        {
            LoadWavFile();
        }

        private void buttonConvertToWaves_Click(object sender, EventArgs e)
        {
            FormFileToWaves formFileToWaves = new FormFileToWaves();
            formFileToWaves.MyParent = this;
            formFileToWaves.ShowDialog();
        }

        private void buttonAdjustFrequencies_Click(object sender, EventArgs e)
        {
            FormBulkChange formFrequency2 = new FormBulkChange();
            formFrequency2.MyParent = this;
            formFrequency2.ShowDialog();
        }

        private void pictureBoxWaveForm_MouseMove(object sender, MouseEventArgs e)
        {
            if (synthGenerator.CurrentWave.WaveForm == "WavFile")
            {
                pictureBoxWaveForm.Cursor = Cursors.Hand;
            }
            else
            {
                pictureBoxWaveForm.Cursor = Cursors.Default;
            }

        }

        private void SetDuration(WaveInfo wave, int numSamples)
        {
            int newNumSamples = numSamples - wave.StartPosition;
            if (newNumSamples < 0)
            {
                newNumSamples = 0;
            }
            wave.WaveData = new double[(newNumSamples) * 2];
        }

        private void SetAllDurations()
        {
            int numSamples = (int)(synthGenerator.SamplesPerSecond * Convert.ToDouble(textBoxDurationAll.Text));
            if (listBoxWaves.SelectedItems.Count > 1)
            {
                foreach (string item in listBoxWaves.SelectedItems)
                {
                    WaveInfo wave = synthGenerator.GetCurrentWaveByDisplayName(item);
                    SetDuration(wave, numSamples);
                }
            }
            else
            {

                foreach (WaveInfo wave in synthGenerator.Waves)
                {
                    SetDuration(wave, numSamples);
                }
            }
        }

        private void SetAllDelays()
        {
            int startPosition = (int)(synthGenerator.SamplesPerSecond * Convert.ToDouble(textBoxDelayAll.Text));
            if (listBoxWaves.SelectedItems.Count > 1)
            {
                foreach (string item in listBoxWaves.SelectedItems)
                {
                    WaveInfo wave = synthGenerator.GetCurrentWaveByDisplayName(item);
                    wave.StartPosition = startPosition;
                }
            }
            else
            {
                foreach (WaveInfo wave in synthGenerator.Waves)
                {
                    wave.StartPosition = startPosition;
                }
            }
        }

        private void ChangeAllWeights()
        {
            double weightFactor = Convert.ToDouble(textBoxChangeWeightAll.Text) / 100.0;
            if (weightFactor <= 0)
                return;

            if (listBoxWaves.SelectedItems.Count > 1)
            {
                foreach (string item in listBoxWaves.SelectedItems)
                {
                    WaveInfo wave = synthGenerator.GetCurrentWaveByDisplayName(item);
                    wave.MinWeight = (int)(wave.MinWeight * weightFactor);
                    wave.MaxWeight = (int)(wave.MaxWeight * weightFactor);
                    if (wave.MinWeight > SynthGenerator.MAX_WEIGHT)
                    {
                        wave.MinWeight = SynthGenerator.MAX_WEIGHT;
                    }
                    if (wave.MaxWeight > SynthGenerator.MAX_WEIGHT)
                    {
                        wave.MaxWeight = SynthGenerator.MAX_WEIGHT;
                    }
                }
            }
            else
            {
                foreach (WaveInfo wave in synthGenerator.Waves)
                {
                    wave.MinWeight = (int)(wave.MinWeight * weightFactor);
                    wave.MaxWeight = (int)(wave.MaxWeight * weightFactor);
                    if (wave.MinWeight > SynthGenerator.MAX_WEIGHT)
                    {
                        wave.MinWeight = SynthGenerator.MAX_WEIGHT;
                    }
                    if (wave.MaxWeight > SynthGenerator.MAX_WEIGHT)
                    {
                        wave.MaxWeight = SynthGenerator.MAX_WEIGHT;
                    }
                }
            }
        }

        private void ChangeAllVolumes()
        {
            double volumeFactor = Convert.ToDouble(textBoxChangeVolumeAll.Text) / 100.0;
            if (volumeFactor <= 0)
                return;
            if (listBoxWaves.SelectedItems.Count > 1)
            {
                foreach (string item in listBoxWaves.SelectedItems)
                {
                    WaveInfo wave = synthGenerator.GetCurrentWaveByDisplayName(item);
                    wave.MinVolume = (int)(wave.MinVolume * volumeFactor);
                    wave.MaxVolume = (int)(wave.MaxVolume * volumeFactor);
                    if (wave.MinVolume > SynthGenerator.MAX_WEIGHT)
                    {
                        wave.MinVolume = SynthGenerator.MAX_WEIGHT;
                    }
                    if (wave.MaxVolume > SynthGenerator.MAX_WEIGHT)
                    {
                        wave.MaxVolume = SynthGenerator.MAX_WEIGHT;
                    }
                }
            }
            else
            {
                foreach (WaveInfo wave in synthGenerator.Waves)
                {
                    wave.MinVolume = (int)(wave.MinVolume * volumeFactor);
                    wave.MaxVolume = (int)(wave.MaxVolume * volumeFactor);
                    if (wave.MinVolume > SynthGenerator.MAX_WEIGHT)
                    {
                        wave.MinVolume = SynthGenerator.MAX_WEIGHT;
                    }
                    if (wave.MaxVolume > SynthGenerator.MAX_WEIGHT)
                    {
                        wave.MaxVolume = SynthGenerator.MAX_WEIGHT;
                    }
                }
            }
        }

        private void buttonSetAllDurations_Click(object sender, EventArgs e)
        {
            try
            {
                SetAllDurations();
                synthGenerator.UpdateAllWaveData();
                UpdateWaveControls();
            }
            catch (System.FormatException)
            {

            }
        }

        private void buttonLuckyPick_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor; 
            synthGenerator.Waves.Clear();
            for(int k=0; k<random.Next(12)+1; k++)
            {
                // create wave
                WaveInfo waveInfo = new WaveInfo(synthGenerator.SamplesPerSecond);
                waveInfo.Name = synthGenerator.CreateUniqueName();
                waveInfo.MinFrequency = 10 + random.Next(50) * 10;
                waveInfo.MinFrequency *= random.Next(4)+1;
                waveInfo.MaxFrequency = 10 + random.Next(50) * 10;
                waveInfo.MaxFrequency *= random.Next(4) + 1;
                if (waveInfo.MinFrequency>waveInfo.MaxFrequency)
                {
                    double temp = waveInfo.MinFrequency;
                    waveInfo.MinFrequency = waveInfo.MaxFrequency;
                    waveInfo.MaxFrequency = temp;
                }
                waveInfo.MinVolume = random.Next(SynthGenerator.MAX_VOLUME) + 1;
                waveInfo.MaxVolume = (int)(random.Next(SynthGenerator.MAX_VOLUME)/2.0 + (SynthGenerator.MAX_VOLUME / 2.0) + 1);
                if (waveInfo.MinVolume > waveInfo.MaxVolume)
                {
                    int temp = waveInfo.MinVolume;
                    waveInfo.MinVolume = waveInfo.MaxVolume;
                    waveInfo.MaxVolume = temp;
                }
                waveInfo.MinWeight = random.Next(SynthGenerator.MAX_WEIGHT) + 1;
                waveInfo.MaxWeight = (int)(random.Next(SynthGenerator.MAX_WEIGHT) / 2.0 + (SynthGenerator.MAX_WEIGHT / 2.0) + 1);
                if (waveInfo.MinWeight > waveInfo.MaxWeight)
                {
                    int temp = waveInfo.MinWeight;
                    waveInfo.MinWeight = waveInfo.MaxWeight;
                    waveInfo.MaxWeight = temp;
                }
                waveInfo.WaveData = new double[(int)(SynthGenerator.SamplesPerSecond * (random.Next(20)+1) / 5.0)];       // duration 0.2 - 4 seconds
                if (random.Next(10)<2)
                {
                    waveInfo.StartPosition = (int)(SynthGenerator.SamplesPerSecond * (random.Next(30) / 30.0));       // startposition 0-3 seconds
                }
                int rand = random.Next(16);
                switch(rand)
                {
                    case 0:
                    case 1:
                        waveInfo.WaveForm = "Triangle";
                        break;
                    case 2:
                    case 3:
                        waveInfo.WaveForm = "Sawtooth";
                        break;
                    case 4:
                    case 5:
                        waveInfo.WaveForm = "Square";
                        break;
                    case 6:
                        waveInfo.WaveForm = "Noise";
                        break;
                    case 7:
                        waveInfo.WaveForm = "Custom";
                        waveInfo.ShapeWave = new int[SynthGenerator.SHAPE_NUMPOINTS];
                        break;
                    case 8:
                        waveInfo.WaveForm = "CustomBeginEnd";
                        waveInfo.ShapeWave = new int[SynthGenerator.SHAPE_NUMPOINTS];
                        waveInfo.ShapeWaveEnd = new int[SynthGenerator.SHAPE_NUMPOINTS];
                        break;

                    default:
                        waveInfo.WaveForm = "Sine";
                        break;
                }
                waveInfo.ShapeVolume = RandomShape();
                waveInfo.ShapeFrequency = RandomShape();
                waveInfo.ShapeWeight = RandomShape();
                if(waveInfo.WaveForm.Equals("Custom") || waveInfo.WaveForm.Equals("CustomBeginEnd"))
                {
                    rand = random.Next(4);
                    switch (rand)
                    {
                        default:
                        case 0:
                            Shapes.Sines(waveInfo.ShapeWave);
                            break;
                        case 1:
                            Shapes.IncDec(waveInfo.ShapeWave);
                            break;
                        case 2:
                            Shapes.Square(waveInfo.ShapeWave);
                            break;
                        case 3:
                            Shapes.IncreasingLineair(waveInfo.ShapeWave);
                            break;
                    }
                }
                if (waveInfo.WaveForm.Equals("CustomBeginEnd"))
                {
                    rand = random.Next(4);
                    switch (rand)
                    {
                        default:
                        case 0:
                            Shapes.Sines(waveInfo.ShapeWaveEnd);
                            break;
                        case 1:
                            Shapes.IncDec(waveInfo.ShapeWaveEnd);
                            break;
                        case 2:
                            Shapes.Square(waveInfo.ShapeWaveEnd);
                            break;
                        case 3:
                            Shapes.IncreasingLineair(waveInfo.ShapeWaveEnd);
                            break;
                    }
                }
                waveInfo.UpdateDisplayName();
                synthGenerator.Waves.Add(waveInfo);
                if (waveInfo.WaveForm != "Noise")
                {
                    if (random.Next(10) < 2)
                    {
                        RandomToneMode();
                        numericUpDownAmount.Value = random.Next(4) + 1;
                        CreateHarmonics(3);
                    }
                    if (random.Next(10) < 2)
                    {
                        RandomToneMode();
                        numericUpDownAmount.Value = random.Next(4) + 1;
                        CreateHarmonics(2);
                    }
                    if (random.Next(10) < 2)
                    {
                        RandomToneMode();
                        numericUpDownAmount.Value = random.Next(4) + 1;
                        CreateInharmonics();
                    }
                }
            }
            if(random.Next(6)==0)
            {
                synthGenerator.AmountBulkCreate = random.Next(30) + 1;
                synthGenerator.MinFrequencyBulkCreate = 10 * Math.Pow(1.0076298626466613, random.Next(999));        // 10 .. 20000
                synthGenerator.MaxFrequencyBulkCreate = 10 * Math.Pow(1.0076298626466613, random.Next(999));        // 10 .. 20000
                synthGenerator.BulkOtherFrequency = 10 * Math.Pow(1.0076298626466613, random.Next(999));        // 10 .. 20000
                for (int k=0; k<SynthGenerator.SHAPE_NUMPOINTS; k++)
                {
                    synthGenerator.ShapeBulkCreate[k] = random.Next(SynthGenerator.SHAPE_MAX_VALUE);
                }
                int rand = random.Next(2);
                if (rand==0)
                {
                    synthGenerator.CreateBulkWaves();
                }
                else if (rand == 1)
                {
                    synthGenerator.CreateBulkWaves(true, false);
                }
                else
                {
                    synthGenerator.CreateBulkWaves(false, true);
                }
            }

            synthGenerator.CurrentWave = synthGenerator.Waves[0];

            synthGenerator.EnvelopAttack = random.Next(3) * 5 / 100.0f;
            synthGenerator.EnvelopRelease = random.Next(4) / 10.0f;
            synthGenerator.EnvelopHold = 1 - synthGenerator.EnvelopRelease - synthGenerator.EnvelopAttack;

            synthGenerator.UpdateAllWaveData();
            UpdateWaveControls();
            Cursor = Cursors.Default;
        }

        private void RandomToneMode()
        {
            int mode = random.Next(3);
            switch (mode)
            {
                case 0:
                    radioButtonOverTones.Checked = true;
                    break;
                case 1:
                    radioButtonUnderTones.Checked = true;
                    break;
                case 2:
                    radioButtonBothTones.Checked = true;
                    break;
            }
        }

        private int[] RandomShape()
        {
            int[] WaveData = new int[SynthGenerator.SHAPE_NUMPOINTS];

            int rand = random.Next(16);
            switch (rand)
            {
                case 0:
                case 15:
                case 12:
                case 14:
                case 13:
                    Shapes.Flat(WaveData);
                    break;
                case 1:
                    Shapes.RandomWaves(WaveData);
                    break;
                case 2:
                    Shapes.DecreasingLineair(WaveData);
                    break;
                case 3:
                    Shapes.IncreasingLineair(WaveData);
                    break;
                case 4:
                    Shapes.IncDec(WaveData);
                    break;
                case 5:
                    Shapes.DecInc(WaveData);
                    break;
                case 6:
                    Shapes.DecreasingLogarithmic(WaveData);
                    break;
                case 7:
                    Shapes.IncreasingLogarithmic(WaveData);
                    break;
                case 8:
                    Shapes.IncSines(WaveData, random.Next(6) + 1);
                    break;
                case 9:
                    Shapes.DecSines(WaveData, random.Next(6) + 1);
                    break;
                case 10:
                    Shapes.Sines(WaveData, random.Next(6) + 1);
                    break;
                case 11:
                    Shapes.Spikes(WaveData);
                    break;
            }

            return WaveData;
        }

        private void buttonSetAllDelays_Click(object sender, EventArgs e)
        {
            try
            {
                SetAllDelays();
                synthGenerator.UpdateAllWaveData();
                UpdateWaveControls();
            }
            catch (System.FormatException)
            {

            }
        }

        private void pictureBoxWeightShape_Paint(object sender, PaintEventArgs e)
        {
            Control control = (Control)sender;
            using (LinearGradientBrush brush = new LinearGradientBrush(control.ClientRectangle,
                                                                       Color.FromArgb(195, 195, 70),
                                                                       Color.FromArgb(15, 0, 0),
                                                                       90F))
            {
                e.Graphics.FillRectangle(brush, control.ClientRectangle);
                ControlPaint.DrawBorder(e.Graphics, control.ClientRectangle, Color.Gray, ButtonBorderStyle.Solid);
            }
            Pen pen = new Pen(Color.White);

            if (synthGenerator.CurrentWave.ShapeWeight.Length == SynthGenerator.SHAPE_NUMPOINTS)
            {
                for (int x = 0; x < pictureBoxWeightShape.Width; x++)
                {
                    int position = (int)(x / (double)pictureBoxWeightShape.Width * SynthGenerator.SHAPE_NUMPOINTS);
                    int next_position = (int)((x + 1) / (double)pictureBoxWeightShape.Width * SynthGenerator.SHAPE_NUMPOINTS);
                    if (next_position < SynthGenerator.SHAPE_NUMPOINTS)
                    {
                        int value1 = (int)((SynthGenerator.SHAPE_MAX_VALUE - synthGenerator.CurrentWave.ShapeWeight[position]) * (pictureBoxWeightShape.Height / (double)SynthGenerator.SHAPE_MAX_VALUE));
                        int value2 = (int)((SynthGenerator.SHAPE_MAX_VALUE - synthGenerator.CurrentWave.ShapeWeight[next_position]) * (pictureBoxWeightShape.Height / (double)SynthGenerator.SHAPE_MAX_VALUE));
                        e.Graphics.DrawLine(pen, new Point(x, value1), new Point(x + 1, value2));
                    }
                }
            }
        }

        private void pictureBoxWeightShape_MouseMove(object sender, MouseEventArgs e)
        {
            pictureBoxWeightShape.Cursor = new Cursor(Properties.Resources.pencil.Handle);
        }

        private void pictureBoxWeightShape_Click(object sender, EventArgs e)
        {
            FormWeight formWeight = new FormWeight();
            formWeight.MyParent = this;
            formWeight.ShowDialog();
        }

        private void listBoxWaves_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxWaves.SelectedItems.Count > 1)
            {
                labelBulkEdit.Text = "Bulk edit selection";
            }
            else
            {
                labelBulkEdit.Text = "Bulk edit all";
            }
        }

        private void buttonAddToVault_Click(object sender, EventArgs e)
        {
            int index = listBoxWaves.SelectedIndex;
            while (listBoxWaves.Items.Count > 1 && listBoxWaves.SelectedItems.Count > 0)
            {
                String item = listBoxWaves.SelectedItems[0].ToString();
                listBoxWaves.Items.RemoveAt(listBoxWaves.FindStringExact(item));
                listBoxWavesVault.Items.Add(item);
                synthGenerator.AddToVault(synthGenerator.GetCurrentWaveByDisplayName(item));
            }

            if (listBoxWaves.Items.Count > index)
            {
                listBoxWaves.SelectedIndex = index;
            }
            else
            {
                listBoxWaves.SelectedIndex = index - 1;
            }
            synthGenerator.SetCurrentWaveByDisplayName(listBoxWaves.SelectedItem.ToString());
            synthGenerator.UpdateAllWaveData();
            UpdateWaveControls();
        }

        private void buttonRemoveFromVault_Click(object sender, EventArgs e)
        {
            int index = listBoxWavesVault.SelectedIndex;
            while (listBoxWavesVault.SelectedItems.Count > 0)
            {
                String item = listBoxWavesVault.SelectedItems[0].ToString();
                listBoxWavesVault.Items.RemoveAt(listBoxWavesVault.FindStringExact(item));
                WaveInfo wave = synthGenerator.GetVaultedWaveByDisplayName(item);
                synthGenerator.RemoveFromVault(wave);
                listBoxWaves.Items.Add(wave.DisplayName);
            }

            synthGenerator.UpdateAllWaveData();
        }

        private void gradientListBox1_DrawItem(object sender, DrawItemEventArgs e)
        {
            Rectangle rect = new Rectangle(e.Bounds.Y - e.Bounds.Height, e.Bounds.Y, e.Bounds.Width, e.Bounds.Height);
            // Draw the background of the ListBox control for each item.
            using (LinearGradientBrush brush = new LinearGradientBrush(e.Bounds,
                                               Color.FromArgb(70, 77, 95),
                                               Color.Black,
                                               0F))
            {
                e.Graphics.FillRectangle(brush, e.Bounds);
            }

            // Define the default color of the brush as black.
            Brush myBrush = Brushes.White;
            // Draw the current item text based on the current Font 
            // and the custom brush settings.
            e.Graphics.DrawString(listBoxWaves.Items[e.Index].ToString(), e.Font, myBrush, e.Bounds, StringFormat.GenericDefault);
            // If the ListBox has focus, draw a focus rectangle around the selected item.
            e.DrawFocusRectangle();
        }

        private void listBoxWaves_DrawItem(object sender, DrawItemEventArgs e)
        {
            Rectangle rect = new Rectangle(e.Bounds.Y - e.Bounds.Height, e.Bounds.Y, e.Bounds.Width, e.Bounds.Height);
            // Draw the background of the ListBox control for each item.
            using (LinearGradientBrush brush = new LinearGradientBrush(e.Bounds,
                                               Color.FromArgb(70, 77, 95),
                                               Color.Black,
                                               0F))
            {
                e.Graphics.FillRectangle(brush, e.Bounds);
            }

            // Define the default color of the brush as black.
            Brush myBrush = Brushes.White;
            // Draw the current item text based on the current Font 
            // and the custom brush settings.
            e.Graphics.DrawString(listBoxWaves.Items[e.Index].ToString(), e.Font, myBrush, e.Bounds, StringFormat.GenericDefault);
            // If the ListBox has focus, draw a focus rectangle around the selected item.
            e.DrawFocusRectangle();
        }

        private void listBoxWaves_MouseUp_1(object sender, MouseEventArgs e)
        {
            UpdateSelectedWave();
        }

        private void pictureBoxCustomWaveEnd_MouseMove(object sender, MouseEventArgs e)
        {
            pictureBoxCustomWaveEnd.Cursor = new Cursor(Properties.Resources.pencil.Handle);
        }

        private void pictureBoxCustomWaveEnd_Paint(object sender, PaintEventArgs e)
        {
            Control control = (Control)sender;
            using (LinearGradientBrush brush = new LinearGradientBrush(control.ClientRectangle,
                                                                       Color.FromArgb(195, 70, 70),
                                                                       Color.FromArgb(15, 0, 0),
                                                                       90F))
            {
                e.Graphics.FillRectangle(brush, control.ClientRectangle);
                ControlPaint.DrawBorder(e.Graphics, control.ClientRectangle, Color.Gray, ButtonBorderStyle.Solid);
            }

            Pen pen = new Pen(Color.White);

            if (synthGenerator.CurrentWave.ShapeWaveEnd.Length == SynthGenerator.SHAPE_NUMPOINTS)
            {
                for (int x = 0; x < pictureBoxCustomWaveEnd.Width; x++)
                {
                    int position = (int)(x / (double)pictureBoxCustomWaveEnd.Width * SynthGenerator.SHAPE_NUMPOINTS);
                    int next_position = (int)((x + 1) / (double)pictureBoxCustomWaveEnd.Width * SynthGenerator.SHAPE_NUMPOINTS);
                    if (next_position < SynthGenerator.SHAPE_NUMPOINTS)
                    {
                        int value1 = (int)((SynthGenerator.SHAPE_MAX_VALUE - synthGenerator.CurrentWave.ShapeWaveEnd[position]) * (pictureBoxCustomWaveEnd.Height / (double)SynthGenerator.SHAPE_MAX_VALUE));
                        int value2 = (int)((SynthGenerator.SHAPE_MAX_VALUE - synthGenerator.CurrentWave.ShapeWaveEnd[next_position]) * (pictureBoxCustomWaveEnd.Height / (double)SynthGenerator.SHAPE_MAX_VALUE));
                        e.Graphics.DrawLine(pen, new Point(x, value1), new Point(x + 1, value2));
                    }
                }
            }
        }

        private void pictureBoxCustomWaveEnd_Click(object sender, EventArgs e)
        {
            FormCustomShape formCustomWave = new FormCustomShape();
            formCustomWave.MyParent = this;
            formCustomWave.Text = "Wave Shape End";
            formCustomWave.ShowDialog();
        }

        private void buttonBulkCreate_Click(object sender, EventArgs e)
        {
            FormBulkCreate formBulkCreate = new FormBulkCreate();
            formBulkCreate.MyParent = this;
            formBulkCreate.ShowDialog();
        }

        private void buttonChangeAllWeights_Click(object sender, EventArgs e)
        {
            try
            {
                ChangeAllWeights();
                synthGenerator.UpdateAllWaveData();
                UpdateWaveControls();
            }
            catch (System.FormatException)
            {

            }
        }

        private void buttonChangeAllVolumes_Click(object sender, EventArgs e)
        {
            try
            {
                ChangeAllVolumes();
                synthGenerator.UpdateAllWaveData();
                UpdateWaveControls();
            }
            catch (System.FormatException)
            {

            }
        }
    }
}
