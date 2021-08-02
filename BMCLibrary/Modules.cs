using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace BMCLibrary
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

            string[] filedata = await System.IO.File.ReadAllLinesAsync(file);
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
                    await System.IO.File.AppendAllTextAsync(file + ".tmp", indent + type + ": " + value + "\n");

                    if (newValues.Length != hold + 1)
                    {
                        hold = hold + 1;
                    }
                }
                else
                {
                    await System.IO.File.AppendAllTextAsync(file + ".tmp", line + "\n");
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
        #region BCML data

        /// <summary>
        /// 
        /// </summary>
        /// <returns>A four digit number representing the amount of mods in the users bcml.</returns>
        public static string ModCount()
        {
            string result = "0000";
            double count = Math.Floor(Math.Log10(Directory.GetDirectories(BMC.bcmlPath + "\\mods").Length) + 1);
            string modCount = Directory.GetDirectories(BMC.bcmlPath + "\\mods").Length.ToString();
            if (count == 1) { result = "000" + modCount; }
            else if (count == 2) { result = "00" + modCount; }
            else if (count == 3) { result = "0" + modCount; }
            else if (count == 4) { result = modCount; }
            else { Console.WriteLine("Error: BCML mod limit reached."); }

            return result;
        }
        public static async Task ModTemplate(string name, string type, string[] files, bool openWhenDone = true)
        {

        }

        #endregion
    }
    public class Simple
    {
        public static async Task Process(string fileName, string args, bool hidden = true, bool dontWait = false, string workingDir = null)
        {
            if (workingDir == null)
            {
                workingDir = AppContext.BaseDirectory;
            }
            Process proc = new Process();

            proc.StartInfo.WorkingDirectory = workingDir;
            proc.StartInfo.FileName = fileName;
            proc.StartInfo.CreateNoWindow = hidden;
            proc.StartInfo.Arguments = args;

            proc.Start();
            if (dontWait == false)
            {
                await proc.WaitForExitAsync();
            }
        }
    }
    public class Hyrule_Builder
    {
        public static async Task UnpackSarc(string file)
        {
            await Simple.Process("unbuild_sarc.exe", "\"" + file + "\"");
        }
        public static async Task PackSarc(string folder, string outFile, string endian = null)
        {
            await Simple.Process("build_sarc.exe", "\"" + folder + "\"" + endian + "\"" + outFile + "\"");
        }
        public static async Task Build(string folder, string out_folder = null, string endian = null)
        {
            await Simple.Process("hyrule_builder.exe", "build " + endian + " \"" + folder + "\" \"" + out_folder + "\"");
        }
        public static async Task UnBuild(string folder, string out_folder = null, string endian = null)
        {
            await Simple.Process("hyrule_builder.exe", "unbuild \"" + folder + "\" \"" + out_folder + "\"");
        }
    }
    public class BYML
    {
        public static async Task Byml_to_Yml(string file, string outPath = " !!.yml")
        {
            await Simple.Process("byml_to_yml.exe", "\"" + file + "\"" + outPath);
        }
        public static async Task Yml_to_Byml(string file, string extension, string endian, string outPath = null)
        {
            outPath = " !!" + extension;
            await Simple.Process("yml_to_byml.exe", endian + " \"" + file + "\"" + outPath);
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
                tasks.Add(_ = Simple.Process("hkrb_extract.exe", path));
            }

            await Task.WhenAll(tasks);
        }
        public static async Task UnBuild(string folder, string outFile, string endian = null)
        {
            await Simple.Process("build_sarc.exe", "\"" + folder + "\"" + endian + "\"" + outFile + "\"");
        }
    }
    public class HKX2
    {
        public static async Task Create(string obj, string type)
        {
            string mtlFile = null;

            await GetObj(obj, mtlFile);

            await Simple.Process(BMC.path + "\\.HKX2\\CCaNM.exe", "\"" + BMC.path + "\\.HKX2\\" + Files.GetName(obj) +
                "\" " + type, false, false, BMC.path + "\\.HKX2");

            await returnFile(obj, mtlFile);

            static async Task GetObj(string obj, string mtlFile)
            {
                await Task.Run(() => System.IO.File.Copy(obj, BMC.path + "\\.HKX2\\" + Files.GetName(obj)));

                foreach (var item in System.IO.File.ReadAllLines(obj))
                {
                    if (item.StartsWith("mtllib"))
                    {
                        mtlFile = Files.GetPath(obj) + item.Replace("mtllib ", "");
                        break;
                    }
                }

                await Task.Run(() => System.IO.File.Copy(mtlFile, BMC.path + "\\.HKX2\\" + Files.GetName(mtlFile)));
            }
            static async Task returnFile(string obj, string mtlFile)
            {
                await Task.Run(() => System.IO.File.Move(BMC.path + "\\.HKX2\\" + Files.GetName(obj) + ".hkrb", Files.GetPath(obj) + Files.GetName(obj, true) + Files.GetExtension(obj).Replace(".obj", ".hkrb")));
                await Task.Run(() => System.IO.File.Delete(BMC.path + "\\.HKX2\\" + Files.GetName(obj)));
                await Task.Run(() => System.IO.File.Delete(BMC.path + "\\.HKX2\\" + Files.GetName(mtlFile)));
            }
        }
    }
}
