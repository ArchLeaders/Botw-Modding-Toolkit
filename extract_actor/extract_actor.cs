using System.Threading.Tasks;
using Botw_Tools;

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
