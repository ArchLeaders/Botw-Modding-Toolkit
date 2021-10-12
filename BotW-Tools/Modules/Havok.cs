using System.Collections.Generic;
using System.Threading.Tasks;

namespace Botw.Modules
{
    public class Havok
    {
        /// <summary>
        /// <c>Botw.Modules.Havok.Extract</c> Extracts model collision from the map physics files using HKRB-Extract by Kreny
        /// <para>
        /// <see cref="Extract(string, string, string, bool, bool, string, bool)"/>
        /// </para>
        /// <see href="">GitHub Documentation</see>
        /// <list type="bullet">
        /// <item><description><para>hashId: HashId of the actor to extract collision from</para></description></item>
        /// <item><description><para>field: Field in which the HashId resides</para></description></item>
        /// <item><description><para>pathToPhys: Path to <c>Update\content\Physics\StaticCompound\MainField</c> or 
        /// <c>DLC\content\0010\Physics\StaticCompound\AocField</c></para></description></item>
        /// <item><para><em>The Rest is Undocumented/Incomplete</em></para></item>
        /// </list>
        /// </summary>
        /// <param name="hashId"></param>
        /// <param name="field"></param>
        /// <param name="pathToPhys"></param>
        /// <param name="hidden"></param>
        /// <param name="wait"></param>
        /// <param name="workingDir"></param>
        /// <param name="admin"></param>
        /// <returns></returns>
        public static async Task Extract(string hashId, string field, string pathToPhys, bool hidden = true, bool wait = true, string workingDir = null, bool admin = false)
        {
            List<Task> tasks = new List<Task>();

            List<string> paths = new List<string>();

            for (int i = 0; i < 3; i++)
            {
                paths.Add(pathToPhys + "\\" + field + "_" + i + ".shksc");
            }

            foreach (var path in paths)
            {
                tasks.Add(_ = Data.Process("hkrb_extract.exe", path));
            }

            await Task.WhenAll(tasks);
        }
        public static async Task hk_to_json()
        {
            await Data.Process("hk_to_json.exe", "");
        }
        public static async Task json_to_hk()
        {
            await Data.Process("json_to_hk.exe", "");
        }
        public static async Task hk_compare()
        {
            await Data.Process("hk_compare.exe", "");
        }
        public static async Task hksc_to_hkrb()
        {
            await Data.Process("hksc_to_hkrb.exe", "");
        }
    }
}
