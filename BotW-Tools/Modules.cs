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
        public static int ModPriority = Directory.GetDirectories(Data.bcmlPath + "\\mods").Length + 100 - 2;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="platform"></param>
        /// <param name="priority"></param>
        /// <returns></returns>
        public static string Info(string name, string platform)
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
            "\n    \"id\": \"\"" +
            "\n}";
        }
    }
}
