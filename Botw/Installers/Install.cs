using Botw.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Botw.Installers
{
    public class Install
    {
        public static async Task Python(string version = "3.8.10", string installPath = "C:\\Python38", bool docs = false, bool silent = true)
        {
            throw new NotImplementedException();
        }

        public static async Task AscclemensMsyt()
        {
            await Web.DownloadGitLatest("https://api.github.com/repos/ascclemens/msyt/releases/latest", $"{Data.root}\\Formats\\Msyt\\msyt.zip", 3);
        }
    }
}
