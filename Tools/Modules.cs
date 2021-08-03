using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Botw_Tools
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
            string result = "0000";
            double count = Math.Floor(Math.Log10(Directory.GetDirectories(Data.bcmlPath + "\\mods").Length) + 1);
            string modCount = Directory.GetDirectories(Data.bcmlPath + "\\mods").Length.ToString();
            if (count == 1) { result = "000" + modCount; }
            else if (count == 2) { result = "00" + modCount; }
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
    public class Hyrule_Builder
    {
        public static async Task UnpackSarc(string file)
        {
            await Data.Process("unbuild_sarc.exe", "\"" + file + "\"");
        }
        public static async Task PackSarc(string folder, string outFile, string endian = null)
        {
            await Data.Process("build_sarc.exe", "\"" + folder + "\"" + endian + "\"" + outFile + "\"");
        }
        public static async Task Build(string folder, string out_folder = null, string endian = null)
        {
            await Data.Process("hyrule_builder.exe", "build " + endian + " \"" + folder + "\" \"" + out_folder + "\"");
        }
        public static async Task UnBuild(string folder, string out_folder = null, string endian = null)
        {
            await Data.Process("hyrule_builder.exe", "unbuild \"" + folder + "\" \"" + out_folder + "\"");
        }
    }
    public class BYML
    {
        public static async Task Byml_to_Yml(string file, string outPath = " !!.yml")
        {
            await Data.Process("byml_to_yml.exe", "\"" + file + "\"" + outPath);
        }
        public static async Task Yml_to_Byml(string file, string extension, string endian, string outPath = null)
        {
            outPath = " !!" + extension;
            await Data.Process("yml_to_byml.exe", endian + " \"" + file + "\"" + outPath);
        }
    }
    public class Botw_Havok
    {
        public static async Task hkrb_extract(string hashid, string field, string pathToPhys, bool hidden = true, bool wait = true, string workingDir = null, bool admin = false)
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
    public class HKX2
    {
        static string mtlFile = null;
        public static async Task Create(string obj, string type, string outFile = null)
        {
            if (outFile is null) { outFile = Data.GetPath(obj) + Data.GetName(obj, true) + Data.GetExtension(obj).Replace(".obj", ".hkrb"); }

            await Task.Run(() => File.Copy(obj, Data.path + "\\.HKX2\\" + Data.GetName(obj)));

            foreach (var item in File.ReadAllLines(obj))
            {
                if (item.StartsWith("mtllib"))
                {
                    mtlFile = Data.GetPath(obj) + item.Replace("mtllib ", "");
                    break;
                }
            }

            await Task.Run(() => File.Copy(mtlFile, Data.path + "\\.HKX2\\" + Data.GetName(mtlFile)));

            await Data.Process(Data.path + "\\.HKX2\\CCaNM.exe", "\"" + Data.path + "\\.HKX2\\" + Data.GetName(obj) +
                "\" " + type, false, false, Data.path + "\\.HKX2");

            await returnFile(obj, mtlFile);

            async Task returnFile(string obj, string mtlFile)
            {
                await Task.Run(() => File.Move(Data.path + "\\.HKX2\\" + Data.GetName(obj) + ".hkrb",
                    outFile));
                await Task.Run(() => File.Delete(Data.path + "\\.HKX2\\" + Data.GetName(obj)));
                await Task.Run(() => File.Delete(Data.path + "\\.HKX2\\" + Data.GetName(mtlFile)));
            }
        }
    }
}
