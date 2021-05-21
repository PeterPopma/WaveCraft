using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaveCraft.Synth
{
    class Preset
    {
        NumberFormatInfo providerDecimalPoint = new NumberFormatInfo();

        public Preset()
        {
            providerDecimalPoint.NumberDecimalSeparator = ".";
        }

        public void Save(SynthGenerator synthGenerator, PresetItem presetItem)
        {
            if (presetItem.Name.Length==0)
            {
                presetItem.Name = "default";
                presetItem.Category = "[default]";
                //                throw new Exception("preset name not set!");
            }
            using (System.IO.StreamWriter file =
                new System.IO.StreamWriter(@".\presets\" + presetItem.Name + ".pst"))
            {
                file.WriteLine(presetItem.Category);
                file.WriteLine(synthGenerator.EnvelopAttack.ToString(providerDecimalPoint));
                file.WriteLine(synthGenerator.EnvelopHold.ToString(providerDecimalPoint));
                file.WriteLine(synthGenerator.EnvelopDecay.ToString(providerDecimalPoint));
                file.WriteLine(synthGenerator.EnvelopSustain.ToString(providerDecimalPoint));
                file.WriteLine(synthGenerator.EnvelopRelease.ToString(providerDecimalPoint));
                file.WriteLine(synthGenerator.Waves.Count);
                for (int i = 0; i < synthGenerator.Waves.Count; i++)
                {
                    WaveInfo waveInfo = synthGenerator.Waves[i];
                    file.WriteLine(waveInfo.Name);
                    file.WriteLine(waveInfo.Channel);
                    file.WriteLine(waveInfo.NumSamples());
                    file.WriteLine(waveInfo.StartPosition);
                    file.WriteLine(waveInfo.WaveFile);
                    file.WriteLine(waveInfo.WaveForm);
                    file.WriteLine(waveInfo.MinFrequency.ToString(providerDecimalPoint));
                    file.WriteLine(waveInfo.MaxFrequency.ToString(providerDecimalPoint));
                    file.WriteLine(waveInfo.MinVolume);
                    file.WriteLine(waveInfo.MaxVolume);
                    file.WriteLine(waveInfo.MinWeight);
                    file.WriteLine(waveInfo.MaxWeight);
                    file.WriteLine(waveInfo.ShapeWave.Length);
                    for (int j = 0; j < waveInfo.ShapeWave.Length; j++)
                    {
                        file.WriteLine(waveInfo.ShapeWave[j]);
                    }
                    file.WriteLine(waveInfo.ShapeFrequency.Length);
                    for (int j = 0; j < waveInfo.ShapeFrequency.Length; j++)
                    {
                        file.WriteLine(waveInfo.ShapeFrequency[j]);
                    }
                    file.WriteLine(waveInfo.ShapeVolume.Length);
                    for (int j = 0; j < waveInfo.ShapeVolume.Length; j++)
                    {
                        file.WriteLine(waveInfo.ShapeVolume[j]);
                    }
                    file.WriteLine(waveInfo.ShapeWeight.Length);
                    for (int j = 0; j < waveInfo.ShapeWeight.Length; j++)
                    {
                        file.WriteLine(waveInfo.ShapeWeight[j]);
                    }
                    file.WriteLine(waveInfo.ShapePhase.Length);
                    for (int j = 0; j < waveInfo.ShapePhase.Length; j++)
                    {
                        file.WriteLine(waveInfo.ShapePhase[j]);
                    }
                    file.WriteLine(waveInfo.WaveFileData.Length);
                    for (int j = 0; j < waveInfo.WaveFileData.Length; j++)
                    {
                        file.WriteLine(waveInfo.WaveFileData[j]);
                    }
                    if (waveInfo.WaveForm.Equals("CustomBeginEnd"))
                    {
                        file.WriteLine(waveInfo.ShapeWaveEnd.Length);
                        for (int j = 0; j < waveInfo.ShapeWaveEnd.Length; j++)
                        {
                            file.WriteLine(waveInfo.ShapeWaveEnd[j]);
                        }
                    }
                    else
                    {
                        file.WriteLine(0);          // end wave 0 length
                    }
                }
            }
        }

        public string ReadCategory(string name)
        {
            using (StreamReader srFile = new StreamReader(@".\presets\" + name + ".pst"))
            {
                return srFile.ReadLine();      // category
            }
        }

        public void Load(SynthGenerator synthGenerator, string name)
        {
            using (StreamReader srFile = new StreamReader(@".\presets\" + name + ".pst"))
            {
                srFile.ReadLine();      // category we already know
                synthGenerator.EnvelopAttack = float.Parse(srFile.ReadLine(), providerDecimalPoint);
                synthGenerator.EnvelopHold = float.Parse(srFile.ReadLine(), providerDecimalPoint);
                synthGenerator.EnvelopDecay = float.Parse(srFile.ReadLine(), providerDecimalPoint);
                synthGenerator.EnvelopSustain = float.Parse(srFile.ReadLine(), providerDecimalPoint);
                synthGenerator.EnvelopRelease = float.Parse(srFile.ReadLine(), providerDecimalPoint);
                int numWaves = int.Parse(srFile.ReadLine());
                synthGenerator.Waves.Clear();
                for (int i = 0; i < numWaves; i++)
                {
                    WaveInfo newWave = new WaveInfo(synthGenerator.SamplesPerSecond);
                    newWave.Name = srFile.ReadLine();
                    newWave.Channel = int.Parse(srFile.ReadLine());
                    newWave.WaveData = new double[int.Parse(srFile.ReadLine()) * 2];
                    newWave.StartPosition = int.Parse(srFile.ReadLine());
                    newWave.WaveFile = srFile.ReadLine();
                    newWave.WaveForm = srFile.ReadLine();
                    newWave.MinFrequency = double.Parse(srFile.ReadLine(), providerDecimalPoint);
                    newWave.MaxFrequency = double.Parse(srFile.ReadLine(), providerDecimalPoint);
                    newWave.MinVolume = int.Parse(srFile.ReadLine());
                    newWave.MaxVolume = int.Parse(srFile.ReadLine());
                    newWave.MinWeight = int.Parse(srFile.ReadLine());
                    newWave.MaxWeight = int.Parse(srFile.ReadLine());

                    int length = int.Parse(srFile.ReadLine());
                    newWave.ShapeWave = new int[length];
                    for (int j = 0; j < length; j++)
                    {
                        newWave.ShapeWave[j] = int.Parse(srFile.ReadLine());
                    }
                    length = int.Parse(srFile.ReadLine());
                    newWave.ShapeFrequency = new int[length];
                    for (int j = 0; j < length; j++)
                    {
                        newWave.ShapeFrequency[j] = int.Parse(srFile.ReadLine());
                    }
                    length = int.Parse(srFile.ReadLine());
                    newWave.ShapeVolume = new int[length];
                    for (int j = 0; j < length; j++)
                    {
                        newWave.ShapeVolume[j] = int.Parse(srFile.ReadLine());
                    }
                    length = int.Parse(srFile.ReadLine());
                    newWave.ShapeWeight = new int[length];
                    for (int j = 0; j < length; j++)
                    {
                        newWave.ShapeWeight[j] = int.Parse(srFile.ReadLine());
                    }
                    length = int.Parse(srFile.ReadLine());
                    newWave.ShapePhase = new int[length];
                    for (int j = 0; j < length; j++)
                    {
                        newWave.ShapePhase[j] = int.Parse(srFile.ReadLine());
                    }
                    length = int.Parse(srFile.ReadLine());
                    newWave.WaveFileData = new int[length];
                    for (int j = 0; j < length; j++)
                    {
                        newWave.WaveFileData[j] = int.Parse(srFile.ReadLine());
                    }
                    length = int.Parse(srFile.ReadLine());
                    newWave.ShapeWaveEnd = new int[length];
                    for (int j = 0; j < length; j++)
                    {
                        newWave.ShapeWaveEnd[j] = int.Parse(srFile.ReadLine());
                    }
                    newWave.UpdateDisplayName();
                    synthGenerator.Waves.Add(newWave);
                }

                synthGenerator.CurrentWave = synthGenerator.Waves.Last();
            }
        }

        public void Delete(string name)
        {
            File.Delete((@".\presets\" + name + ".pst"));
        }
    }
}
