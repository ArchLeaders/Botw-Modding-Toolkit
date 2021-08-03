using System;
using System.Threading.Tasks;
using Botw_Tools;

namespace SilentPrincessMapEditor
{
    class sp_sc
    {
        static async Task Main(string[] args)
        {
            switch (args[0])
            {
                case "-e":
                    await BMC.ExtractActor(args, true);
                    break;
            }
        }
    }
}
