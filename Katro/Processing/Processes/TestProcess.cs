using Cosmos.Kernel.System.Diagnostics;
using Katro.GUI.Widgets;
using Katro.GUI.Windows;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Katro.Processing.Processes
{
    public class TestProcess : Process
    {
        public override string? Name { get; set; }
        private int _tick = 0;

        public override void OnStart()
        {
            base.OnStart();
            Log.Write("[Test Process] Start\n");
        }

        public override void Update()
        {
            Log.Write($"[Test Process] Tick: {_tick}\n");
            _tick++;
        }

        public override void OnQuit()
        {
            base.OnQuit();
            Log.Write("[Test Process] Quit\n");
        }
    }
}
