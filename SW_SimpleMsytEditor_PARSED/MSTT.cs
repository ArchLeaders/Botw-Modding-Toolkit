using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using YamlDotNet.Serialization;

namespace SW_Msyt_Editor
{
    public class MSTT
    {
        #region NULL

        public static List<string[]> Parse(string[] inputText)
        {
            Editor editor = new();

            bool isSub = false;

            //Create a list to store the different text sequences.
            List<string[]> entries = new();
            List<string> entriesSub = new();

            //Loop the input array to find the text sequences.
            foreach (string line in inputText)
            {
                if (isSub == true)
                {
                    if (line.StartsWith("  ") && !line.StartsWith("   "))
                    {
                        entries.Add(entriesSub.ToArray());
                        entriesSub.Clear();
                        entriesSub.Add(line);
                    }
                    else
                    {
                        entriesSub.Add(line);
                    }
                }
                else  if (line.StartsWith("  ") && !line.StartsWith("   ")) //Find the header and collect Subitems;
                {
                    entriesSub.Add(line);
                    isSub = true;
                }
            }

            return entries;
        }

        private static string ToMsttString(string[] list)
        {
            string result = null;

            foreach (var item in list)
            {
                result = result + item + "\n";
            }

            return result;
        }

        #endregion

        public static void GetText()
        {
            var entry = new MSYT
            {
                group_count = 11,
                atr1_unknown = 4,
                entries = new Dictionary<string, EntriesClass>
                {
                    { "\"0001\"", new EntriesClass() {
                        attributes = "\"\"",
                        contents = new Dictionary<string, ControlClass>
                        {
                            {"- control", new ControlClass() {
                                kind = "\"pause\"",
                                frames = 456
                            }},
                        }
                    }}
                }
            };

            var serializer = new SerializerBuilder()
            .Build();

            var yaml = serializer.Serialize(entry);
            File.AppendAllText("wow.yml", yaml);
        }
    }
}
