using System.Threading.Tasks;
using static BMCLibrary.BMCcontrol;
using static BMCLibrary.HKX2_Handle;

namespace extract_actor
{
    class extract_actor
    {
        static async Task Main(string[] args)
        {
            await ExtractActor(args, false);
        }
    }
}
