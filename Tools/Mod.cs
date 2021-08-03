using System.IO;
using System.Threading.Tasks;

namespace Botw_Tools
{
    public class Mod
    {
        public static async Task Create(string name, string[] files = null)
        {
            string prior = BCML.ModCount();
            string path = Data.bcmlPath + "\\mods\\" + prior + "_" + name;
            await Task.Run(() => Directory.CreateDirectory(path + "\\content"));
            await Task.Run(() => File.WriteAllText(path + "\\Info.json", BCML.Info(name, Data.edition, BCML.ModPriority.ToString())));

            if (files != null)
            {
                foreach (var file in files)
                {
                    string sub = file.Replace("Update\\", "\\content\\")
                        .Replace(Data.dlcPath + "\\0012", "\\aoc\\")
                        .Replace(Data.dlcPath + "\\0010", "\\aoc\\")
                        .Replace("Base\\", "\\content\\");

                    await Task.Run(() => Directory.CreateDirectory(Data.GetPath(path + sub)));
                    await Task.Run(() => File.Copy(file, path + sub));
                }
            }
        }
        public static async Task Remove(string name)
        {
            string path = Data.bcmlPath + "\\mods\\" + name;
            await Task.Run(() => Directory.Delete(path, true));
        }
    }
}
