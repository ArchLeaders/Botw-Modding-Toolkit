using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;

namespace BasicModCreatorData
{
    class BMC
    {
        static string applicationPath = System.Reflection.Assembly.GetEntryAssembly().Location.Replace("BasicModCreator.dll", "");
        static string workingDir = Environment.CurrentDirectory;
        static string[] gamePaths = File.ReadAllLines(applicationPath + "\\data\\paths.txt");
        static string BCMLPath = gamePaths[3];
        static string type = File.ReadAllLines(applicationPath + "\\data\\paths.txt")[4];
        static int handleKey = 0;

        #region Primary Methods
        public static async Task ReadData(string[] args)
        {
            //Reads data from local path and creates a actor from it.
        }
        public static async Task CreateBFT(string[] args)
        {
            //creates a bft with defines information.
        }
        public static async  Task ReadBFT(string[] args)
        {
            //Writes data from a bft file.
        }
        public static async Task BuildShrine(string[] args)
        {
            //Builds a shrine from NavMesh and Collision.
        }
        public static async Task collisionHandle(string[] args)
        {
            //Creates HKRB from target .obj file. Creates HKSC from target sc.obj file. Creates HKNM2 from target nm.obj file.
            //Creates SHKSC from target ssc.obj file. Creates SHKNM2 from target snm.obj file.

                 if (args[0] == "-c") { handleKey = 1; }
            else if (args[0] == "collision") { handleKey = 2; }
            //File Formats
            else if (args[0].EndsWith(".obj")) { handleKey = 3; }
            else if (args[0].EndsWith(".sc.obj")) { handleKey = 4; }
            else if (args[0].EndsWith(".ssc.obj")) { handleKey = 5; }
            else if (args[0].EndsWith(".nm.obj")) { handleKey = 6; }
            else if (args[0].EndsWith(".snm.obj")) { handleKey = 7; }
            else 
            {
                try
                {
                    await HKX2("hkrb", args[0]);
                }
                catch { }
            }

            await CollisionHandler(handleKey, args);
        }
        public static async Task BymlChanger(string[] args)
        {
            //Converts target BYML to Big or Little endian
            try
            {
                await BYMLHandler(args[1], args[2], args[3], true, args[4], args[5]);
            }
            catch { }
            try
            {
                await BYMLHandler(args[1], args[2], args[3], true, args[4]);
            }
            catch { }
            try
            {
                await BYMLHandler(args[1], args[2], args[3], true);
            }
            catch { }
            try
            {
                await BYMLHandleUnkownE(args[0]);
            }
            catch { }
        }
        public static async Task GetActor(string[] args)
        {
            await CreateDirectories(new string[] { applicationPath + "temp\\collision.HKRB", "temp\\" + args[0] + "C\\" + GetName(gamePaths[1]) + "\\Actor\\Pack"});
        }
        public static async Task InstallData(string[] args)
        {

        }
        public static void HelpConsole()
        {
            Console.WriteLine("\n" +
                "Basic Mod Creator Commands:\n" +
                "-h, help: Displays the help. (Console Only)\n" +
                "    usage: -h\n\n" +
                "-b, bft: Writes a bft file.\n" +
                "    usage: -b \"path\\to\\read\\directory\" \"path\\to\\file.bft\" " +
                    "[-r \"Wood, Metal\"] [-f \"ActorInfo, RSTB, #sound, #map_a-1_main-field, " +
                    "Update\\Actor\\Pack\\TwnObj_TempleOfTime_A_01\"]\n\n" +
                "*.bft: Writes data from the target BFT file.\n" +
                "    usage: \"path\\to\\bft\" (Open .bft file with the console executable)\n\n" +
                "-r, readdata, *.rdt: Creates a \".sbactorpack\" from data source.\n" +
                "    usage: -r \"path\\to\\folder\" [\"Actor Name\"]\n\n" +
                "-c, collision, *.obj: Creates collision from corresponding .obj and .mtl files.\n" +
                "    usage: -c \"path\\to\\*.obj\" { hkrb | hksc | shksc | hknm2 | shknm2}\n" +
                "       OBJ file syntax: .obj = hkrb; .sc.obj = hksc; .ssc.obj = shksc; .nm.obj = hknm2; .snm.obj = shknm2;\n\n" +
                "-e byml-changer, Endians {0 = Little Endian | 1 = Big Endian}, Yaz0 {-1 = Don't Change | 0 = Un-compress | 1 = Compress}" +
                "    usage: -e {0|1} {-1|0|1} [path\\to\\input] [path\\to\\output]" +
                "-a get-actor, Gets collision and attaches it to a specified actor." +
                "    usage: -a 'ActorName' 'HashID' 'Field' [path\\to\\out folder] [Extension]" +
                "-y yaz0, Compresses target file." +
                "    usage: -y \"path\\to\\file\" [compression level (1-9)]" +
                "-i, install: Installs various Basic Mod Creator components.\n" +
                "    usage: -i { python | c++ | hyrule-builder | yaz-it | byml | hkx2-blender | obj-lib | all | required }");
        }
        #endregion
        public static async Task CreateMod(string[] args)
        {
            Console.WriteLine("Do you wish to create a new mod named: " + GetName(args[0]) + "[yes/no/rename]");
            string result = Console.ReadLine();
            if (result == "yes" || result == "y" || result == "Yes" || result == "Y" || result == "true")
            {
                await Create(GetName(args[0]).Replace(GetExtension(args[0]), ""));
            }
            else if (result == "rename" || result == "r" || result == "R" || result == "Rename")
            {
                Console.WriteLine("Please enter the new name: \n");
                string name = Console.ReadLine();

                await Create(name);
            }
            else if (result == "no" || result == "n" || result == "No" || result == "No")
            {
                return;
            }
            else
            {
                Console.WriteLine("Error, invalid arguments");
                await CreateMod(args);

                return;
            }

            static async Task Create(string name, bool openWhenDone = true)
            {
                Directory.CreateDirectory(BCMLPath + "\\mods\\" + name + "\\" + GetName(gamePaths[1]));
                string info = File.ReadAllText(applicationPath + "\\data\\info.json");
                string infojson = info.Replace("%MODNAME%", name).Replace("%PLATFORM%", type);
                File.WriteAllText(BCMLPath + "\\mods\\" + name + "\\info.json", infojson);

                if (openWhenDone == true)
                {
                    Process proc = new Process();
                    proc.StartInfo.FileName = "explorer.exe";
                    proc.StartInfo.Arguments = BCMLPath + "\\mods\\" + name;

                    proc.Start();
                }
            }
        }

        #region Edit text documents methods

        private static async Task EditInfo(string[] args, string build, string actorname, int hkrb, string bfresName = null)
        {

        }
        private static async Task EditLinks(string[] args, string build, string actorname)
        {

        }
        private static async Task EditPhysics(string[] args, string build, string actorname, string folderName = null, string hkrbName = null)
        {

        }
        private static async Task EditModelList(string[] args, string build, string actorname, string unitName = null, string folderName = null)
        {

        }

        #endregion

        #region Process data

        public static async Task CollisionHandler(int key, string[] args)
        {
            switch (key)
            {
                case 1: //Syntax: -c "file.obj" "type"
                case 2:
                    if (!args[1].EndsWith(".obj")) { return; }
                    await HKX2(args[2], args[1], true);
                    break;
                case 3: //.hkrb
                    await HKX2("hkrb", args[0], true);
                    break;
                case 4: //.sc.obj
                    await HKX2("hksc", args[0], true);
                    break;
                case 5: //.ssc.obj
                    await HKX2("shksc", args[0], true);
                    break;
                case 6: //.nm.obj
                    await HKX2("hknm2", args[0], true);
                    break;
                case 7: //.snm.obj
                    await HKX2("shknm2", args[0], true);
                    break;
            }
        }

        public static async Task BYMLHandler(string file, string type, string yaz0, bool deleteOriginal = true, string pathIn = null, string pathOut = null, bool silent = false, bool admin = false)
        {
            if (pathIn != null) { file = pathIn; }
            string endian = type.Replace("1", " -b ").Replace("0", "");
            pathOut = applicationPath + "\\" + GetName(file);

            string yaz0extents = yaz0.Replace("-1", ".").Replace("0", ".s").Replace("1", ".");
            string yaz0extension = yaz0.Replace("-1", ".").Replace("1", ".s").Replace("0", ".");
            string extension = GetExtension(file).Replace(yaz0extents, yaz0extension);

            Process BYML = new Process();
            BYML.StartInfo.FileName = "cmd.exe";
            BYML.StartInfo.UseShellExecute = admin;
            BYML.StartInfo.CreateNoWindow = !silent;
            BYML.StartInfo.Arguments = "/c byml_to_yml \"" + file + "\" \"" + applicationPath + GetName(file).Replace(GetExtension(file), ".yml") + 
                "\" && yml_to_byml " + endian + "\"" + applicationPath + GetNameNoExtension(file) + ".yml\" \"" + file.Replace(GetExtension(file), extension) + "\" && EXIT";

            await Task.Run(() => BYML.Start());
            await BYML.WaitForExitAsync();

            if (file != file.Replace(GetExtension(file), extension) && deleteOriginal == true) File.Delete(file);
            File.Delete(file.Replace(GetExtension(file), ".yml"));
        }

        public static async Task BYMLHandleUnkownE(string file, bool silent = false, bool admin = false)
        {
            byte[] bytes = File.ReadAllBytes(file);

            if (bytes[0] == 89) { await isCompressed(bytes, file); }
            else if (bytes[0] == 66 || bytes[0] == 89) { await isNotCompressed(bytes, file); }

            static async Task isCompressed(byte[] bytes, string file)
            {
                if (bytes[17] == 89) //Little Endian
                {
                    await BYMLHandler(file, "1", "-1", false, null, null, true);
                }
                else if (bytes[17] == 66) //Big Endian
                {
                    await BYMLHandler(file, "0", "-1", false, null, null, true);
                }
            }
            static async Task isNotCompressed(byte[] bytes, string file)
            {
                if (bytes[0] == 89) //Little Endian
                {
                    await BYMLHandler(file, "1", "-1", false);
                }
                else if (bytes[0] == 66) //Big Endian
                {
                    await BYMLHandler(file, "0", "-1", false);
                }
            }
        }
        #endregion

        #region HKX2
        public static async Task HKX2(string type, string file, bool silent = true, bool admin = false)
        {
            string yazIt = " && EXIT";
            if (type == "shksc" || type == "shknm2") { yazIt = "&& yaz-it \"" + file + "\" && EXIT"; }
            CopyDirectory(applicationPath + "\\collision", workingDir);

            Process HKX2 = new Process();
            HKX2.StartInfo.FileName = "cmd.exe";
            HKX2.StartInfo.UseShellExecute = admin;
            HKX2.StartInfo.CreateNoWindow = !silent;
            HKX2.StartInfo.Arguments = "/c CreateCollisionAndNavmesh.exe " + file + " " + type + yazIt;

            await Task.Run(() => HKX2.Start());
            await HKX2.WaitForExitAsync();

            DeleteFiles(null, workingDir, ".dll");
            File.Delete(applicationPath + "\\CreateCollisionAndNavmesh.exe");
        }

        #endregion

        #region Misc Methods

        public static void CopyDirectory(string sourceDir, string destDir)
        {
            List<Task> tasks = new List<Task>();
            foreach (var file in Directory.GetFiles(sourceDir))
            {
                string[] data = file.Split('\\');

                tasks.Add(Task.Run(() => File.Copy(file, destDir + "\\" + data[data.Length - 1])));
            }

            Task.WaitAll(tasks.ToArray());
        }

        public static void DeleteFiles(string[] files = null, string directory = null, string filter = null)
        {
            if (files != null)
            {
                foreach (var file in files)
                {
                    if (file.EndsWith(filter))
                    {
                        File.Delete(file);
                    }
                }
            }
            else if (directory != null)
            {
                foreach (var file in Directory.GetFiles(directory))
                {
                    if (file.EndsWith(filter))
                    {
                        File.Delete(file);
                    }
                }
            }
        }
        public static async Task<Process> AsyncConsoleProcess(Process proc, string args, string fileName, bool dontExit = false, bool UsShellExecute = false, bool CreateNoWindow = true, bool dontStart = false, bool dontWait = false)
        {
            if (dontExit == false) { args = args + " && EXIT"; }
            proc = new Process();

            proc.StartInfo.FileName = fileName;
            proc.StartInfo.Arguments = args;
            proc.StartInfo.CreateNoWindow = CreateNoWindow;
            proc.StartInfo.UseShellExecute = UsShellExecute;

            if (dontStart == false) { await Task.Run(() => proc.Start()); }
            if (dontStart == false && dontWait == false) { await proc.WaitForExitAsync(); }

            return proc;
        }
        public static async Task CreateDirectories(string[] Directories)
        {
            List<Task<DirectoryInfo>> tasks = new List<Task<DirectoryInfo>>();

            foreach (string directory in Directories)
            {
                tasks.Add(Task.Run(() => Directory.CreateDirectory(directory)));
            }

            var result = await Task.WhenAll(tasks);
        }
        public static string GetExtension(string file)
        {
            string[] nm1 = file.Split('.');
            return "." + nm1[nm1.Length - 1]; 
        }
        public static string GetName(string file)
        {
            string[] nm1 = file.Split('\\');
            return nm1[nm1.Length - 1];
        }
        public static string GetNameNoExtension(string file)
        {
            string[] nm1 = file.Split('\\');
            return nm1[nm1.Length - 1].Replace(GetExtension(file), "");
        }
        public static string GetPath(string file)
        {
            string[] nm1 = file.Split('\\');
            return file.Replace(nm1[nm1.Length - 1], "");
        }
        public static string GetLastInString(Char split, string targetString, string replaceChar = null, string replaceWith = "")
        {
            string[] ls = targetString.Split(split);
            return ls[ls.Length - 1].Replace(replaceChar, replaceWith);
        }
        #endregion

        #region Console Command Methods

        public static async Task Yaz0Compress(string file, bool deCompress = false, int compresionLevel = 7)
        {
            if (deCompress == false)
            {
                string arguments = "yazit \"" + file + "\"" + compresionLevel.ToString();

                await AsyncConsoleProcess(new Process(), arguments, "cmd.exe");
            }
        }
        public static async Task HKRB_Extract(string actorName, string useArguments = null, string outFile = null, string hashID = null, string fieldname = null, string maptype = null)
        {
            string pathTo = null;
            if (useArguments != "-p")
            {
                foreach (var line in File.ReadAllLines(applicationPath + "\\data\\ActorData.yml"))
                {
                    string[] paramaters = line.Split(',');

                    hashID = GetLastInString(':', paramaters[4]);
                    fieldname = GetLastInString(':', paramaters[2], "\"", "");
                    maptype = GetLastInString(':', paramaters[1], "\"", "");

                    outFile = applicationPath + "\\temp\\collision.HKRB\\" + actorName + "C.hkrb";
                }
            }
            switch(maptype)
            {
                case "MainField":
                    pathTo = gamePaths[1] + "\\Physics\\StaticCompound\\MainField\\";
                    break;
                case "AocField":
                    pathTo = gamePaths[2] + "\\0010\\Physics\\StaticCompound\\AocField\\";
                    break;
            }

            List<Task<Process>> tasks = new List<Task<Process>>();
            for (int i = 0; i < 3; i++)
            {
                string arguments = "\"" + pathTo + fieldname + "-" + i.ToString() + ".shksc\" " + hashID + " \"" + outFile + "\"";

                tasks.Add(AsyncConsoleProcess(new Process(), arguments, "cmd.exe"));
                
            }

            var results = await Task.WhenAll(tasks);

            Console.WriteLine("You should have collision now...");
        }
        #endregion
    }
}
