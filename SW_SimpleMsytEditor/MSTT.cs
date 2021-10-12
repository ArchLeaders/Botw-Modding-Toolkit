using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace SW_Msyt_Editor
{
    public class MSTT
    {
        public static string publicReturn = null;
        public static List<string> publicList = null;

        public static async Task Parse(string[] lines)
        {
            publicReturn = null;
            await Task.Run(() => {
                foreach (var line in lines)
                {
                    if (line.StartsWith("      - text: "))
                    {
                        publicReturn = publicReturn + line.Split(':')[1].Replace("\"", "").TrimStart() + "\n";
                        publicList.Add(line.Split(':')[1].Replace("\"", "").TrimStart() + "\n");
                    }
                }
            });
        }
    }
}
