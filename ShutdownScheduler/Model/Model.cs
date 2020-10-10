using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace ShutdownScheduler.Model
{
    public static class Model
    {
        private static void PerformCommand(params string[] parameters)
        {
            Process process = new Process()
            {
                StartInfo =
                {
                    Arguments = string.Join(" ", parameters.Select(x => $"-{x}")),
                    CreateNoWindow = true,
                    FileName = "shutdown.exe"
                }
            };
            process.Start();
        }

        public static void CancelShutdownOrRestart()
        {
            PerformCommand("a");
        }

        public static void PerformAction(bool isRestart, ushort seconds)
        {
            PerformCommand(isRestart ? "r" : "s", "f", $"t {seconds}");
        }

        public static void PerformAction(bool isRestart, DateTime targetDateTime)
        {
            TimeSpan diff = targetDateTime - DateTime.Now;
            PerformAction(isRestart, (ushort)diff.TotalSeconds);
        }

        public static void PerformAction(bool isRestart, ushort hours, byte minutes, byte seconds)
        {
            PerformAction(isRestart, (ushort)(hours*3600 + minutes*60 + seconds));
        }

    }
}
