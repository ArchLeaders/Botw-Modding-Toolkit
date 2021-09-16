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

            //MSTT File format is: A compressed ZIP with yml, meta and txt snippets inside it.
            //MSYT to MSTT: Get each text sequence and convert it to a single yml file. Various other things.
            //How do we read it: Foreach snippet create a list of string[] where the string[] is the snippet and the List is each snippet.
            //What is the txt file: The text file is each snippets pure in-game text combined. (Each yml snippet contains a corresponding text file. Yes? No?)
            //Meta file: Sequence count for ease of reading.
        }

        #endregion
    }
}
