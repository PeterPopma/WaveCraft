using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaveCraft.Synth
{
    public class WaveInfo
    {
        string name;                // unique name
        int startPosition;          // delay in seconds * 44100 (start sample)
        double minFrequency;        // in Hz
        double maxFrequency;        // in Hz
        int minVolume;              // 0..1000
        int maxVolume;              // 0..1000
        int minWeight;              // weight compared to other waves (0..1000)
        int maxWeight;              // weight compared to other waves (0..1000)
        int channel;                // 2=both, 0=left, 1=right
        string waveForm;            // sine, saw, square, triangle, noise, or .wav file
        string waveFile;            // .wav file data
        double[] waveData;          // resulting data after applying all properties (freq, vol, waveshape, etc.)
        int[] waveFileData = new int[0];            // data read from .wav file
        int[] shapeWave = new int[0];               // shape of the custom waveform. this consists of 1000 items with value between 0 and SHAPE_MAX_VALUE
        int[] shapeWaveEnd = new int[0];            // end shape of the custom waveform. this consists of 1000 items with value between 0 and SHAPE_MAX_VALUE
        int[] shapeFrequency = new int[0];          // shape of the frequency. this consists of 1000 items with value between 0 and SHAPE_MAX_VALUE
        int[] shapeVolume = new int[0];             // shape of the volume. this consists of 1000 items with value between 0 and SHAPE_MAX_VALUE
        int[] shapeWeight = new int[0];             // shape of the weight. this consists of 1000 items with value between 0 and SHAPE_MAX_VALUE

        public double MinFrequency { get => minFrequency; set => minFrequency = value; }
        public double MaxFrequency { get => maxFrequency; set => maxFrequency = value; }
        public string WaveForm { get => waveForm; set => waveForm = value; }
        public int Channel { get => channel; set => channel = value; }
        public int MinVolume { get => minVolume; set => minVolume = value; }
        public int MaxVolume { get => maxVolume; set => maxVolume = value; }

        public string WaveFile { get => waveFile; set => waveFile = value; }
        public int StartPosition { get => startPosition; set => startPosition = value; }
        public string Name { get => name; set => name = value; }
        public double[] WaveData { get => waveData; set => waveData = value; }
        public int[] WaveFileData { get => waveFileData; set => waveFileData = value; }
        public int[] ShapeVolume { get => shapeVolume; set => shapeVolume = value; }
        public int[] ShapeFrequency { get => shapeFrequency; set => shapeFrequency = value; }
        public int[] ShapeWave { get => shapeWave; set => shapeWave = value; }
        public int MinWeight { get => minWeight; set => minWeight = value; }
        public int MaxWeight { get => maxWeight; set => maxWeight = value; }
        public int[] ShapeWeight { get => shapeWeight; set => shapeWeight = value; }
        public int[] ShapeWaveEnd { get => shapeWaveEnd; set => shapeWaveEnd = value; }

        public WaveInfo(int samplesPerSecond)
        {
            this.name = "Wave1";
            this.startPosition = 0;
            this.minFrequency = 440;
            this.maxFrequency = 440;
            this.minVolume = 0;
            this.maxVolume = SynthGenerator.MAX_VOLUME;
            this.minWeight = 0;
            this.maxWeight = SynthGenerator.MAX_WEIGHT;
            this.channel = 2;
            this.waveForm = "Sine";
            this.waveFile = "";
            this.waveData = new double[samplesPerSecond * 2];          // default 1 sec.

            ShapeVolume = new int[SynthGenerator.SHAPE_NUMPOINTS];
            ArrayUtils.Populate(ShapeVolume, SynthGenerator.SHAPE_MAX_VALUE / 2);
            ShapeFrequency = new int[SynthGenerator.SHAPE_NUMPOINTS];
            ArrayUtils.Populate(ShapeFrequency, SynthGenerator.SHAPE_MAX_VALUE / 2);
            ShapeWeight = new int[SynthGenerator.SHAPE_NUMPOINTS];
            ArrayUtils.Populate(ShapeWeight, SynthGenerator.SHAPE_MAX_VALUE / 2);
        }

        public WaveInfo(string name, int numSamples, int startPosition, double minFrequency, double maxFrequency, int minVolume, int maxVolume, int minWeight, int maxWeight, int channel, string waveForm, string waveFile)
        {
            this.name = name;
            waveData = new double[numSamples * 2];
            this.startPosition = startPosition;
            this.minFrequency = minFrequency;
            this.maxFrequency = maxFrequency;
            this.minVolume = minVolume;
            this.maxVolume = maxVolume;
            this.minWeight = minWeight;
            this.maxWeight = maxWeight;
            this.channel = channel;
            this.waveForm = waveForm;
            this.waveFile = waveFile;
        }

        // stereo samples are counted as one (so 1 second contains 44100 samples)
        public int NumSamples()
        {
            return waveData.Length / 2;
        }

        public string DisplayName()
        {
            string info = Name + " - " + startPosition + ":" + NumSamples();
            if (!waveForm.Equals("Noise") && !waveForm.Equals("WavFile"))
            {
                info += " - " + string.Format("{0:0.00}", minFrequency);
                info += ":" + string.Format("{0:0.00}", maxFrequency);
            }
            info += " - " + waveForm;

            if (waveForm.Equals("WavFile"))
            {
                info += " - " + Path.GetFileName(waveFile);
            }
            return info;
        }

        public void SetNumSamples(int numSamples)
        {
            waveData = new double[(int)(2 * numSamples)];
        }
    }
}
