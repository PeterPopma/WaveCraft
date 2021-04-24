using Jacobi.Vst.Core;
using Jacobi.Vst.Plugin.Framework;
using Jacobi.Vst.Plugin.Framework.Common;
using System;
using System.Drawing;
using System.Linq;
using WaveCraft;

namespace WaveCraftVst
{
    /// <summary>
    /// This object manages the custom editor (UI) for your plugin.
    /// </summary>
    /// <remarks>
    /// When you do not implement a custom editor, 
    /// your Parameters will be displayed in an editor provided by the host.
    /// </remarks>
    internal sealed class PluginEditor : IVstPluginEditor
    {
        private readonly WinFormsControlWrapper<FormMain> _view;

        public PluginEditor()
        {
            _view = new WinFormsControlWrapper<FormMain>();
        }

        public Rectangle Bounds
        {
            get { return _view.Bounds; }
        }

        public void Close()
        {
            _view.Close();
        }

        public bool KeyDown(byte ascii, VstVirtualKey virtualKey, VstModifierKeys modifers)
        {
            // empty by design
            return false;
        }

        public bool KeyUp(byte ascii, VstVirtualKey virtualKey, VstModifierKeys modifers)
        {
            // empty by design
            return false;
        }

        public VstKnobMode KnobMode { get; set; }

        public void Open(IntPtr hWnd)
        {
            _view.Open(hWnd);
        }

        public void ProcessIdle()
        {
            // could be implemented in FormMain
//           _view.SafeInstance.ProcessIdle();
        }
    }
}
