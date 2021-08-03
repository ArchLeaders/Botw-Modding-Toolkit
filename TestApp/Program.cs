using System;
using System.Threading.Tasks;
using Botw_Tools;

namespace TestApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            await Mod.Create("Name");
            //await Actor.Create(@"C:\Users\HP USER\Desktop\BotWBunkerDrawing\Collected Data\Assets\script-folder\Collision.obj");
        }
    }
}
