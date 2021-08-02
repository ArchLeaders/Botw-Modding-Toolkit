using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace BMCLibrary
{
    public class BMC
    {
        #region General Paths and Strings/Data
        public static string path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\BMC";
        public static string dataPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\BMC\\data";
        public static string tempPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\BMC\\.temp";
        public static string appPath = System.IO.File.ReadAllLines(dataPath + "\\paths.txt")[7];

        //Paths
        public static string basePath = System.IO.File.ReadAllLines(dataPath + "\\paths.txt")[0];
        public static string updatePath = System.IO.File.ReadAllLines(dataPath + "\\paths.txt")[1];
        public static string dlcPath = System.IO.File.ReadAllLines(dataPath + "\\paths.txt")[2];
        public static string bcmlPath = System.IO.File.ReadAllLines(dataPath + "\\paths.txt")[3];
        public static string pyPath = System.IO.File.ReadAllLines(dataPath + "\\paths.txt")[6];

        //Paths
        public static string edition = System.IO.File.ReadAllLines(dataPath + "\\paths.txt")[4];
        public static string pyVersion = System.IO.File.ReadAllLines(dataPath + "\\paths.txt")[5];

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

            await BYML.Byml_to_Yml(file, dataPath + Files.GetName(file));

            await BYML.Yml_to_Byml(dataPath + Files.GetName(file), Files.GetExtension(file), endian);

            string extension = Files.GetExtension(file);
            if (yaz0 != -1)
            {
                await Simple.Process("yaz.exe", "\"" + file + "\" " + yaz0, true, false, tempPath);
            }

            System.IO.File.Move(tempPath + "\\" +  Files.GetName(file), output + "\\" + Files.GetName(file, true) + extension);
        }
        public static async Task ExtractActor(string[] args, bool staticArgs)
        {
            if (args[0] == "--c")
            {
                foreach (var line in File.ReadAllLines(dataPath + "\\info.cache"))
                {
                    Console.WriteLine(line);
                }
            }
            #region Strings & Bools
            //Actor Data
            string hashId = null;
            string actorName = null;
            string field = null;

            //Botw 
            string pathToPhys = null;
            string pathToActors = null;

            //Output
            string of1 = null;
            string outFile = null;

            //Static Only
            string Info = null;

            //Dynamic Only
            bool cache = true;

            #endregion

            #region Assign Strings & Bools if normal expressions
            if (staticArgs == true)
            {
                //Actor Data
                hashId = args[1];
                actorName = args[2];
                field = args[3];

                //Botw
                if (Files.GetName(args[4]) == "0010" || args[4].EndsWith("01007EF00011F001\\romfs"))
                {
                    pathToPhys = args[4] + "\\Physics\\StaticCompound\\AocField";
                }
                else if (Files.GetName(args[4]) == "content" || args[4].EndsWith("01007EF00011E000\\romfs"))
                {
                    pathToPhys = args[4] + "\\Physics\\StaticCompound\\MainField";
                    pathToActors = args[4] + "\\Actor";
                }

                //Output
                if (Directory.Exists(args[5]))
                {
                    of1 = args[5] + "\\content\\Actor\\Pack\\" + actorName + "C.sbactorpack";
                }
                else
                {
                    of1 = Files.GetPath(args[5]) +
                    Files.GetName(args[5], true) + Files.GetExtension(args[5]).Replace(".sbactorpack", "C.sbactorpack");
                }

                //Type
                if (args[4].EndsWith("romfs")) { outFile = of1.Replace("\\content", "\\01007EF00011E000\\romfs"); }
                else { outFile = of1; }

                Info = path + "\\.info\\0001\\content\\Actor\\Info\\"; //Possibly static path, otherwise the last argument.
            }
            else
            {
                string[] actorData = await Botw.HashId(args[0]);
                if (actorData[0] == "-1") { return; }

                hashId = actorData[0];
                actorName = actorData[1];
                field = actorData[2];

                //Path to Physics
                if (actorData[3] == "MainField")
                {
                    pathToPhys = updatePath + "\\Physics\\StaticCompound\\MainField";
                    pathToActors = args[4] + "\\Actor";
                }
                else if (actorData[3] == "AocField")
                {
                    pathToPhys = dlcPath + "0010\\Physics\\StaticCompound\\AocField";
                }

                //Outfile
                if (args.Length >= 2)
                {
                    if (Files.GetExtension(args[1]) == ".sbactorpack")
                    {
                        of1 = args[1];
                    }
                    else if (args[1] == "bcml_mod" || args[1] == "-b" || args[1] == "--bcml")
                    {
                        of1 = bcmlPath + "\\mods\\" + BCML.ModCount() + "_" + actorName + "\\content\\Actor\\Pack\\" + actorName + "C.sbactorpack";
                    }
                    else if (Directory.Exists(args[1]))
                    {
                        of1 = args[1] + "\\" + actorName + "_Actor\\content\\Actor\\Pack\\" + actorName + "C.sbactorpack";
                    }
                }
                else { of1 = Directory.GetCurrentDirectory() + "\\" + actorName + "_Build\\content\\Actor\\Pack\\" + actorName + "C.sbactorpack"; }

                //IsSwitch
                if (edition == "switch")
                {
                    outFile = of1.Replace("\\content", "\\01007EF00011E000\\romfs");
                }
                else { outFile = of1; }
            }
            #endregion

            //Availible data: HashID, ActorName, Field, PathToPhysics (update or dlc), output path. Needed, temp path.

            #region Preparation...

            Console.WriteLine("Getting files...");
            Directory.CreateDirectory(Files.GetPath(outFile));
            Directory.CreateDirectory(tempPath + "\\" + actorName + "content\\Actor\\Pack");

            System.IO.File.Copy(pathToActors + "\\Pack\\" + actorName + ".sbactorpack",
                tempPath + "\\" + actorName + "_SARC\\" + actorName + "C.sbactorpack");

            #endregion

        }
    }
}
