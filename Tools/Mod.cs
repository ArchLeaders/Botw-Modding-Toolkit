using System.IO;
using System.Threading.Tasks;

namespace Botw
{
    public class Mod
    {
        public static async Task Create(string name, string[] files = null)
        {
            string prior = BCML.ModCount();
            string path = Data.bcmlPath + "\\mods\\" + prior + "_" + name + "\\";
            if (Data.edition == "wiiu")
            {
                await Task.Run(() => Directory.CreateDirectory(path + "content"));
            }
            else if (Data.edition == "switch")
            {
                await Task.Run(() => Directory.CreateDirectory(path + "01007EF00011E000\\romfs"));
            }
            await Task.Run(() => File.WriteAllText(path + "Info.json", BCML.Info(name, Data.edition, BCML.ModPriority.ToString())));

            if (files != null)
            {
                foreach (var file in files)
                {
                    if (Data.edition == "wiiu")
                    {
                        string sub = file.Replace(Data.updatePath, "content")
                            .Replace(Data.dlcPath + "0012", "\\aoc\\")
                            .Replace(Data.dlcPath + "0010", "\\aoc")
                            .Replace(Data.basePath, "content");

                        await Task.Run(() => Directory.CreateDirectory(Data.GetPath(path + sub)));
                        await Task.Run(() => File.Copy(file, path + sub));
                    }
                    else if (Data.edition == "switch")
                    {
                        string sub = file.Replace(Data.updatePath, "01007EF00011E000\\romfs\\")
                            .Replace(Data.dlcPath, "01007EF00011F001\\romfs")
                            .Replace(Data.basePath, "01007EF00011E000\\romfs");

                        await Task.Run(() => Directory.CreateDirectory(Data.GetPath(path + sub)));
                        await Task.Run(() => File.Copy(file, path + sub));
                    }
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
