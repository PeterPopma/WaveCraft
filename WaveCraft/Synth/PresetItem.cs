using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaveCraft.Synth
{
    public class PresetItem
    {
        string name;
        string category;

        public string Name { get => name; set => name = value; }
        public string Category { get => category; set => category = value; }

        public PresetItem(string name, string category)
        {
            this.name = name;
            this.category = category;
        }
    }
}
