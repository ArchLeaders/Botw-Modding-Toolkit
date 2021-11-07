using System;
using System.Threading.Tasks;

namespace TEST_NET5
{
    class Program
    {
        static async Task Main(string[] args)
        {
            await BotwLib.Installers.Install.AscclemensMsyt();
        }
    }
}
