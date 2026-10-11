using Cosmos.Kernel.System.Diagnostics;
using Katro.GUI.Widgets;
using Katro.GUI.Windows;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Katro.Processing.Processes
{
    public class TestWindowProcess : Process
    {
        public override string? Name { get; set; }
        public override Window? Window { get; set; }

        public TestWindowProcess()
        {
            var testWindow = new Window()
            {
                X = 10,
                Y = 10,
                Width = 300,
                Height = 300,
                Title = "Test Window"
            };

            var label = new Label()
            {
                X = 10,
                Y = 10,
                Text = "Hello, World!",
                Foreground = Color.White
            };

            testWindow.Widgets.Add(label);

            Window = testWindow;
        }

        public override void OnStart()
        {
            base.OnStart();
            Log.Write("[Test Process] Start\n");
        }

        public override void Update()
        {
            if (Window != null)
                Log.Write($"[Test Window Process] Window X: {Window.X}, Window Y: {Window.Y}, Window Width: {Window.Width}, Window Height: {Window.Height}\n");
        }

        public override void OnQuit()
        {
            base.OnQuit();
            Log.Write("[Test Process] Quit\n");
        }
    }
}
