using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotwLib.System
{
    public class Proc
    {
        public static async Task Async(string exe, string args = null, bool wait = true, bool silent = true, ProcessWindowStyle style = ProcessWindowStyle.Normal)
        {
            Process process = new();

            process.StartInfo.FileName = exe;
            process.StartInfo.Arguments = args;
            process.StartInfo.CreateNoWindow = silent;
            process.StartInfo.WindowStyle = style;
            if (silent) process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;

            await Task.Run(() => process.Start());
            if (wait) await process.WaitForExitAsync();
        }

        public static void Syncronous(string exe, string args = null, bool wait = true, bool silent = true, ProcessWindowStyle style = ProcessWindowStyle.Normal)
        {
            Process process = new();

            process.StartInfo.FileName = exe;
            process.StartInfo.Arguments = args;
            process.StartInfo.CreateNoWindow = silent;
            process.StartInfo.WindowStyle = style;
            if (silent) process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;

            process.Start();
            if (wait) process.WaitForExit();
        }
    }
}
