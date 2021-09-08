using System;
using System.IO;
using System.Threading.Tasks;

namespace Botw
{
    public class Parse
    {
        /// <summary>
        /// <c>BxmlParse</c> edits the values of a BXML
        /// <list type="bullet">
        /// <item><description><para>Type: NewValue.</para></description></item>
        /// <item><description><para>ModelUser: Obj_MyActorModel_A_01.</para></description></item>
        /// </list>
        /// <returns><em>Returns:</em><strong> An edited BXML file.</strong></returns>
        /// </summary>
        public static async Task BXML(string file, string[] newValues)
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
        public static async Task Info(string file, string newValues)
        {

        }
        public static async Task ModelList(string file, string folderName, string unitName)
        {

        }
        public static async Task Myst(string file)
        {

        }
    }
    public class BCML
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns>A four digit number representing the amount of mods in the users bcml.</returns>
        public static string ModCount()
        {
            string result = "0100";
            double count = Math.Floor(Math.Log10(Directory.GetDirectories(Data.bcmlPath + "\\mods").Length) + 1);
            int mods = Directory.GetDirectories(Data.bcmlPath + "\\mods").Length - 1;
            string modCount = mods.ToString();
            if (count == 1) { result = "010" + modCount; }
            else if (count == 2) { result = "01" + modCount; }
            else if (count == 3) { result = "0" + modCount; }
            else if (count == 4) { result = modCount; }
            else { Console.WriteLine("Error: BCML mod limit reached."); }

            return result;
        }
        public static int ModPriority = Directory.GetDirectories(Data.bcmlPath + "\\mods").Length + 100 - 2;
        public static string Info(string name, string platform, string priority)
        {
            return "{" +
            "\n    \"name\": \"" + name + "\"," +
            "\n    \"image\": \"\"," +
            "\n    \"url\": \"\"," +
            "\n    \"desc\": \"\"," +
            "\n    \"version\": \"1.0.0\"," +
            "\n    \"options\": {}," +
            "\n    \"depends\": []," +
            "\n    \"showCompare\": false," +
            "\n    \"showConvert\": false," +
            "\n    \"platform\": \"" + platform + "\"," +
            "\n    \"priority\": \"" + priority + "\"," +
            "\n    \"id\": \"\"" +
            "\n}";
        }
    }
    public class HyruleBuilder
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public static async Task UnpackSarc(string file)
        {
            await Data.Process("unbuild_sarc.exe", "\"" + file + "\"");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="folder"></param>
        /// <param name="outFile"></param>
        /// <param name="endian"></param>
        /// <returns></returns>
        public static async Task PackSarc(string folder, string outFile, string endian = null)
        {
            await Data.Process("build_sarc.exe", "\"" + folder + "\"" + endian + "\"" + outFile + "\"");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="folder"></param>
        /// <param name="out_folder"></param>
        /// <param name="endian"></param>
        /// <returns></returns>
        public static async Task Build(string folder, string out_folder = null, string endian = null)
        {
            await Data.Process("hyrule_builder.exe", "build " + endian + " \"" + folder + "\" \"" + out_folder + "\"");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="folder"></param>
        /// <param name="out_folder"></param>
        /// <param name="endian"></param>
        /// <returns></returns>
        public static async Task UnBuild(string folder, string out_folder = null, string endian = null)
        {
            await Data.Process("hyrule_builder.exe", "unbuild \"" + folder + "\" \"" + out_folder + "\"");
        }
    }
}
