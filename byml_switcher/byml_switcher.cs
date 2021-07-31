using System.Threading.Tasks;
using static BMCLibrary.BMCcontrol;

namespace byml_switcher
{
    class byml_switcher
    {
        static void Main(string[] args)
        {
            _ = Call(args);   
        }

        public static async Task Call(string[] args)
        {
            await BymlSwitcher(args);
        }
    }
}
