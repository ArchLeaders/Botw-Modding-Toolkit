using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace BMCLibrary
{
    class BotwParsing
    {
        #region Actor Files, bxml, bmodellist, info, bphysics, etc...
        /// <summary>
        /// <c>BxmlParse</c> edits the values of a BXML
        /// <list type="bullet">
        /// <item><description><para>Type: NewValue.</para></description></item>
        /// <item><description><para>ModelUser: Obj_MyActorModel_A_01.</para></description></item>
        /// </list>
        /// <returns><em>Returns:</em><strong> An edited BXML file.</strong></returns>
        /// </summary>
        public static async Task BxmlParse(string file, string[] newValues)
        {
            string indent = "      ";

            string[] filedata = await File.ReadAllLinesAsync(file);
            string[] fields = new string[] { 
                "ActorNameJpn", 
                "Priority", 
                "AIProgramUser", 
                "AIScheduleUser", 
                "ASUser", 
                "AttentionUser", 
                "AwarenessUser", 
                "BoneControlUser", 
                "ActorCaptureUser", 
                "ChemicalUser", 
                "DamageParamUser", 
                "DropTableUser", 
                "ElinkUser", 
                "GParamUser", 
                "LifeConditionUser", 
                "LODUser", 
                "ModelUser", 
                "PhysicsUser", 
                "ProfileUser", 
                "RgBlendWeightUser", 
                "RgConfigListUser", 
                "RecipeUser", 
                "ShopDataUser", 
                "SlinkUser", 
                "UMiiUser", 
                "XlinkUser", 
                "AnimationInfo", 
                "ActorScale",
                "tag*"
            };
            int hold = 0;

            foreach (var line in filedata)
            {
                string[] split = newValues[hold].Split(':');
                string type = split[0];
                string value = split[1];

                if (line.Split(':')[0].Replace(" ", "") == type)
                {
                    await File.AppendAllTextAsync(file + ".tmp", indent + type + ": " + value + "\n");

                    if (newValues.Length != hold + 1)
                    {
                        hold = hold + 1;
                    }
                }
                else
                {
                    await File.AppendAllTextAsync(file + ".tmp", line + "\n");
                }
            }
        }
        public static async Task InfoParse(string file, string newValues)
        {

        }
        public static async Task BmodellistParse(string file, string folderName, string unitName)
        {

        }
        public static async Task MystParse(string file)
        {

        }
        #endregion

        #region Python Application Commands

        public static async Task SarcUnpacker(string file)
        {
            Process hyre = new Process();

            hyre.StartInfo.FileName = "unbuild_sarc.exe";
            hyre.StartInfo.Arguments = "\"" + file + "\"";

            await Task.Run(() => hyre.Start());
            await hyre.WaitForExitAsync();
        }
        public static async Task SarcPacker(string folder, string outFile, string endian = null)
        {
            Process hyre = new Process();

            hyre.StartInfo.FileName = "build_sarc.exe";
            hyre.StartInfo.Arguments = "\"" + folder + "\"" + endian + "\"" + outFile + "\"";

            await Task.Run(() => hyre.Start());
            await hyre.WaitForExitAsync();
        }
        public static async Task BymlDecoder(string file, string outPath = " !!.yml")
        {
            Process byml = new Process();

            byml.StartInfo.FileName = "byml_to_yml.exe";
            byml.StartInfo.Arguments = "\"" + file + "\"" + outPath;

            await Task.Run(() => byml.Start());
            await byml.WaitForExitAsync();

            await Task.Run(() => File.Delete(file));
        }
        public static async Task YamlBymlEncoder(string file, string format, string outPath = null)
        {
            if (outPath == null)
            {
                outPath = " !!." + format;
            }

            Process byml = new Process();

            byml.StartInfo.FileName = "yml_to_byml.exe";
            byml.StartInfo.Arguments = "\"" + file + "\"" + outPath;

            await Task.Run(() => byml.Start());
            await byml.WaitForExitAsync();

            await Task.Run(() => File.Delete(file));
        }
        public static async Task Yaz0Compressor(string file, string level)
        {
            Process byml = new Process();

            byml.StartInfo.FileName = "yazit.exe";
            byml.StartInfo.Arguments = "\"" + file + "\"" + level;

            await Task.Run(() => byml.Start());
            await byml.WaitForExitAsync();

            await Task.Run(() => File.Delete(file));
        }

        #endregion
    }
}
