using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using static BMCLibrary.BotwParsing;
using static BMCLibrary.DataAccesFiles;
using static BMCLibrary.HashIDs;

namespace BMCLibrary
{
    public class BMCcontrol
    {
        #region General Paths and Strings/Data
        public static string path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\BMC";
        public static string dataPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\BMC\\data";
        public static string tempPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\BMC\\.temp";

        //Paths
        public static string basePath = File.ReadAllLines(dataPath + "\\paths.txt")[0];
        public static string updatePath = File.ReadAllLines(dataPath + "\\paths.txt")[1];
        public static string dlcPath = File.ReadAllLines(dataPath + "\\paths.txt")[2];
        public static string bcmlPath = File.ReadAllLines(dataPath + "\\paths.txt")[3];
        public static string pyPath = File.ReadAllLines(dataPath + "\\paths.txt")[6];

        //Paths
        public static string edition = File.ReadAllLines(dataPath + "\\paths.txt")[4];
        public static string pyVersion = File.ReadAllLines(dataPath + "\\paths.txt")[5];

        #endregion

        public static async Task BymlSwitcher(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("" +
                    "  Positional Arguments or Open BYML File With byml_switch.exe\n" +
                    "      { path\\to\\file | path\\to\\dir }\n" +
                    "\n" +
                    "  Optional Arguments\n" +
                    "      -#               yaz0 compresion, # is compresion level, can be any number from 1-9.\n" +
                    "      -b, --be         Make Big Endian.\n" +
                    "      path\\to\\out    Output folder.\n" +
                    "\n" +
                    "  BYML Formats\n" +
                    "      .baischedule |  .sbaischedule\n" +
                    "      .baniminfo   |  .sbaniminfo\n" +
                    "      .bgdata      |  .sbgdata\n" +
                    "      .bgsvdata    |  .sbgsvdata\n" +
                    "      .bquestpack  |  .sbquestpack\n" +
                    "      .byml        |  .sbyml\n" +
                    "      .mubin       |  .smubin" +
                    "");
                Console.WriteLine("\n\nPress any key to continue...");
                Console.ReadLine();
                return;
            }

            List<string> formats = new List<string>
            {
                ".baischedule",
                ".baniminfo",
                ".bgdata",
                ".bgsvdata",
                ".bquestpack",
                ".byml",
                ".mubin",
                ".sbaischedule",
                ".sbaniminfo",
                ".sbgdata",
                ".sbgsvdata",
                ".sbquestpack",
                ".sbyml",
                ".smubin"
            };
            string format = null;

            string endian = null;
            int yaz0 = -1;
            string output = null;
            string file = args[0];

            foreach (var argument in args)
            {
                if (argument == "-b" || argument == "--be") { endian = "-b"; }
                else if (argument == "-o" || argument == "--output") { output = argument; }
                else if (int.TryParse(argument, out yaz0)) { }
                else if (argument.Contains('\\')) { file = argument; }
                else
                {

                }
            }

            await BymlDecoder(file, dataPath + GetName(file));

            await YamlBymlEncoder(dataPath + GetName(file), GetExtension(file), endian);

            string extension = GetExtension(file);
            if (yaz0 != -1)
            {
                await Yaz0Compressor(dataPath + GetName(file), yaz0.ToString());
            }

            File.Move(dataPath + GetName(file), output + "\\" + GetName(file, true) + extension);
        }
        public static async Task ExtractActor(string[] args, bool staticArgs)
        {
            #region Strings & Bools
            //Actor Data
            string hashId = null;
            string actorName = null;
            string field = null;
            string map = null;

            //Botw 
            string update = null;
            string dlc = null;

            //Output
            string outFile = null;

            //Type
            bool isSwitch = false;

            #endregion

            #region Assign Strings & Bools if normal expressions
            if (staticArgs == true)
            {
                //Actor Data
                hashId = args[1];
                actorName = args[2];
                field = args[3];
                map = args[4];

                //Botw 
                update = args[5];
                dlc = args[6];

                //Output
                outFile = null;
                if (Directory.Exists(args[7]))
                {
                    outFile = args[7] + "\\content\\Actor\\Pack\\" + actorName + "C.sbactorpack";
                }
                else
                {
                    outFile = GetPath(args[0]) + "\\" +
                     GetName(args[0], true) + GetExtension(args[7]).Replace(".sbactorpack", "C.sbactorpack");
                }

                //Type
                isSwitch = false;

                if (GetName(update) == "romfs") { isSwitch = true; outFile.Replace("content", "01007EF00011E000\\romfs"); }
                else if (GetName(update) == "content") { isSwitch = false; }
            }
            else
            {
                string[] actorData = await HashId(args[0]);

                hashId = actorData[0];
                actorName = actorData[1];
                field = actorData[2];
                map = actorData[3];

                update = updatePath;
                dlc = dlcPath;

                //Outfile
                if (args.Length >= 2)
                {
                    if (Directory.Exists(args[1]))
                    {
                        outFile = args[1] + "\\content\\Actor\\Pack\\" + actorName + "C.sbactorpack";
                    }
                    else if (args[1] == "bcml_mod" || args[1] == "-b" || args[1] == "--bcml")
                    {
                        outFile = bcmlPath + "\\mods\\" + BCMLPrior() + "_" + actorName + "\\content\\Actor\\Pack\\" + actorName + "C.sbactorpack";
                    }
                }
                else { outFile = Directory.GetCurrentDirectory() + "\\" + actorName + "_Build\\content\\Actor\\Pack\\" + actorName + "C.sbactorpack"; }

                //IsSwitch
                if (edition == "switch")
                {
                    isSwitch = true;
                    outFile.Replace("content", "01007EF00011E000\\romfs");
                }
                else if (edition == "wiiu")
                {
                    isSwitch = false;
                }

                Console.WriteLine(BCMLPrior());
            }
            #endregion
        }
    }
}
