using Jacobi.Vst.Core;
using Jacobi.Vst.Plugin.Framework;
using Jacobi.Vst.Plugin.Framework.Plugin;
using Microsoft.Extensions.DependencyInjection;

namespace WaveCraftVst
{
    /// <summary>
    /// The Plugin root class that derives from the framework provided base class that also include the interface manager.
    /// </summary>
    internal sealed class Plugin : VstPluginWithServices
    {
        /// <summary>
        /// Constructs a new instance.
        /// </summary>
        public Plugin()
            : base("Wavecraft VST plugin", 36373435,
                new VstProductInfo("Wavecraft VST plugin", "© Peter Popma 2021", 2000),
                VstPluginCategory.Synth)
        { }

        protected override void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<SampleManager>()
                .AddSingletonAll<AudioProcessor>()
                .AddSingletonAll<MidiProcessor>()
                .AddSingletonAll<PluginEditor>();
        }
    }
}
