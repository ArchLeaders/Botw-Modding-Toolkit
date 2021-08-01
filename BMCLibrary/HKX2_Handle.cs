using System;
using System.IO;
using System.Threading.Tasks;
using static BMCLibrary.DataAccesFiles;

namespace BMCLibrary
{
    public class HKX2_Handle
    {
        public static string mtlFile = null;
        public static async Task HKX2(string obj, string type)
        {
            await GetObj(obj);
            await BotwParsing.QuickProcess(BMCcontrol.path + "\\.HKX2\\CCaNM.exe", "\"" + BMCcontrol.path + "\\.HKX2\\" + GetName(obj) +
                "\" " + type, false, false, BMCcontrol.path + "\\.HKX2");
            await returnFile(obj);
        }
        static async Task GetObj(string obj)
        {
            await Task.Run(() => File.Copy(obj, BMCcontrol.path + "\\.HKX2\\" + GetName(obj)));

            foreach (var item in File.ReadAllLines(obj))
            {
                if (item.StartsWith("mtllib"))
                {
                    mtlFile = GetPath(obj) + item.Replace("mtllib ", "");
                    break;
                }
            }

            await Task.Run(() => File.Copy(mtlFile, BMCcontrol.path + "\\.HKX2\\" + GetName(mtlFile)));
        }
        static async Task returnFile(string obj)
        {
            await Task.Run(() => File.Move(BMCcontrol.path + "\\.HKX2\\" + GetName(obj) + ".hkrb", GetPath(obj) + GetName(obj, true) + GetExtension(obj).Replace(".obj", ".hkrb")));
            await Task.Run(() => File.Delete(BMCcontrol.path + "\\.HKX2\\" + GetName(obj)));
            await Task.Run(() => File.Delete(BMCcontrol.path + "\\.HKX2\\" + GetName(mtlFile)));
        }
    }
}
