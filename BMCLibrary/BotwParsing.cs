using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using static BMCLibrary.BMCcontrol;

namespace BMCLibrary
{
    public class BotwParsing
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
            await QuickProcess("unbuild_sarc.exe", "\"" + file + "\"");
        }
        public static async Task SarcPacker(string folder, string outFile, string endian = null)
        {
            await QuickProcess("build_sarc.exe", "\"" + folder + "\"" + endian + "\"" + outFile + "\"");
        }
        public static async Task BymlDecoder(string file, string outPath = " !!.yml")
        {
            await QuickProcess("byml_to_yml.exe", "\"" + file + "\"" + outPath);
        }
        public static async Task YamlBymlEncoder(string file, string extension, string endian, string outPath = null)
        {
            outPath = " !!" + extension;
            await QuickProcess("yml_to_byml.exe", endian + " \"" + file + "\"" + outPath);
        }
        public static async Task Yaz0Compressor(string file, string level, string output = null)
        {
            await QuickProcess("yazit.exe", "\"" + file + "\"" + level + " " + output);
        }

        #endregion

        #region Basic Process

        public static async Task QuickProcess(string fileName, string args, bool hidden = true, bool dontWait = false)
        {
            Process proc = new Process();

            proc.StartInfo.FileName = fileName;
            proc.StartInfo.CreateNoWindow = !hidden;
            proc.StartInfo.Arguments = args;

            await Task.Run(() => proc.Start());
            if (dontWait == true)
            {
                await proc.WaitForExitAsync();
            }
        }

        #endregion

        #region BCML data

        /// <summary>
        /// 
        /// </summary>
        /// <returns>A four digit number representing the amount of mods in the users bcml.</returns>
        public static string BCMLPrior()
        {
            Directory.GetDirectories()
        }

        #endregion
    }
}
