using System.Threading.Tasks;
using BMCLibrary;

namespace extract_actor
{
    class extract_actor
    {
        static async Task Main(string[] args)
        {
            await BMC.ExtractActor(args, false);
        }
    }
}
