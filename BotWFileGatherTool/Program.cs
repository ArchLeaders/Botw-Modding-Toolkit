using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace BotWFileGatherTool
{
    class Program
    {
        public static bool isFile;
        public static bool isFullLine;
        public static bool isCommand;

        public static bool optionM_Yes;
        public static bool optionF_Yes;
        public static bool optionT_Yes;
        public static bool optionS_Yes;

        public static string ActivePath()
        {
            return Directory.GetCurrentDirectory();
        }
        static async Task Main(string[] args)
        {
            //string[] args = { @"-c" };
            //string[] args = { @"C:\Users\HP USER\source\repos\Breath of the Wild File Gathering Tool\BotWFileGatherTool\bin\Debug\net5.0\something.bft" };
            //string[] args = { "-b", "-m", "Glass,Eye,LineLight", "-t", "DgnObj_DLC_BattleRoom_Parts_B.Tex1.sbfres, Animal_Boar_Big.sbactorpack", "-fm", @"C:\Users\HP USER\source\repos\Breath of the Wild File Gathering Tool\BotWFileGatherTool\bin\Debug\net5.0\ToolTest", "-fs", "Actor!, Pack, Map_MainField_J-8, Model" };

            string AppPath = System.Reflection.Assembly.GetEntryAssembly().Location.Replace("BotWFileGatherTool.dll", "");
            foreach (string argument in args)
            {
                if (argument != null) { isCommand = true; }

                if (argument == "-h" || argument == "help")
                {

                }

                if (argument == "-c") { isFullLine = true; }
                if (argument == "-m") { optionM_Yes = true; }
                if (argument == "-t") { optionT_Yes = true; }
                if (argument == "-fm") { optionF_Yes = true; }
                if (argument == "-fs") { optionS_Yes = true; }
            }

            if (args[0] == "bft" || args[0] == "-b")
            {
                if (isFullLine == true)
                {
                    await createdata(AppPath, args);
                }
                else if (isCommand == true)
                {
                    await createdata(AppPath, args, false);
                }
                else
                {
                    Console.WriteLine("Error\n\tInvalid arguments.\n\t Use 'help' [-h] for more information.");
                }
            }
            else if (args[0] == "paths" || args[0] == "-p")
            {
                string dlcPath = null;
                if (args[3] != null) { dlcPath = args[3]; }
                if (!Directory.Exists(AppPath + "data"))
                {
                    Directory.CreateDirectory(AppPath + "data");
                    File.WriteAllText(AppPath + "data\\paths.dat", args[1] + "\n" + args[2] + "\n" + dlcPath + "\n");
                }
                else
                {
                    File.WriteAllText(AppPath + "data\\paths.dat", args[1] + "\n" + args[2] + "\n" + dlcPath + "\n");
                }
            }
            else if (args[0] == "createdata" || args[0].Contains(".bft"))
            {
                isFile = true;
                await createdata(AppPath, args);
            }
            else if (args[0] == "build" || args[0] == "--be" || args[0].Contains(".btfb"))
            {
                string[] dir = Directory.GetDirectories(AppPath);

                foreach (var folder in dir)
                {

                }
            }
            else if (args[0] == "-r" || args[0] == "readdata")
            {
                string contentPath = null;
                string ActorName = null;
                Console.WriteLine("Collecting Data...");
                Directory.CreateDirectory(ActivePath() + "\\~Build\\HyRe");
                foreach (var folder in Directory.GetDirectories(ActivePath(), ".", SearchOption.AllDirectories))
                {
                    if (folder.Contains("c~"))
                    {
                        int i = 0;
                        foreach (var file in Directory.GetFiles(folder))
                        {
                            i = i + 1;
                            Process proc = new Process();
                            proc.StartInfo.FileName = "cmd.exe";
                            if (file.Contains("shksc"))
                            {
                                Console.WriteLine("Writting HKRB...\n");
                                string[] filePath = file.Split('\\');
                                string fileName = filePath[filePath.Length - 1];
                                proc.StartInfo.Arguments = "/c hksc_to_hkrb \"" + file + "\" \"" + ActivePath() + "\\~Build\\" + fileName.Replace(".shksc", ".hkrb");
                                await Task.Run(() => proc.Start());
                            }
                            else if (file.Contains(".hksc"))
                            {
                                Console.WriteLine("Writting HKRB...\n");
                                string[] filePath = file.Split('\\');
                                string fileName = filePath[filePath.Length - 1];
                                proc.StartInfo.Arguments = "/c hksc_to_hkrb \"" + file + "\" \"" + ActivePath() + "\\~Build\\" + fileName.Replace(".hksc", ".hkrb");
                                await Task.Run(() => proc.Start());
                            }
                            else if (file.Contains(".hkrb"))
                            {
                                Console.WriteLine("Moving HKRB...");
                                File.Copy(file, ActivePath() + "\\~Build\\" + file.Split('\\')[file.Length - 1]);
                            }
                            await proc.WaitForExitAsync();
                        }
                    }
                    else if (folder.Contains("\\content"))
                    {
                        contentPath = folder;
                        if (Directory.Exists(folder + "\\Actor\\Pack"))
                        {
                            Console.WriteLine("Transfering Actorpacks...");
                            Directory.CreateDirectory(ActivePath() + "\\~Build\\HyRe\\content\\Actor\\Pack");
                            File.Copy(File.ReadAllLines(AppPath + "\\data\\paths.dat")[1] + "\\Actor\\ActorInfo.product.sbyml", ActivePath() + "\\~Build\\HyRe\\content\\Actor\\ActorInfo.product.sbyml");
                            foreach (var file in Directory.GetFiles(folder + "\\Actor\\Pack"))
                            {
                                string[] filePath = file.Split('\\');
                                string fileName = filePath[filePath.Length - 1];
                                ActorName = filePath[filePath.Length - 1].Replace(".sbactorpack", "");
                                File.Copy(file, ActivePath() + "\\~Build\\HyRe\\content\\Actor\\Pack\\" + fileName);
                            }
                        }
                    }
                }
                Console.WriteLine("Unbuilding...\n");
                Process HyRe = new Process();
                HyRe.StartInfo.FileName = "cmd.exe";
                HyRe.StartInfo.Arguments = "/c hyrule_builder unbuild \"" + ActivePath() + "\\~Build\\HyRe\"";
                await Task.Run(() => HyRe.Start());

                await HyRe.WaitForExitAsync();
                Console.WriteLine("Moving Collision Data...");
                await Task.Run(() => Directory.Move(ActivePath() + "\\~Build\\HyRe", ActivePath() + "\\~Build\\HyRe_CollectionData"));
                await Task.Run(() => Directory.Move(ActivePath() + "\\~Build\\HyRe_unbuilt", ActivePath() + "\\~Build\\HyRe"));

                foreach (var file in Directory.GetFiles(contentPath))
                {
                    string CurrentActorName = file.Replace(contentPath + "\\", "").Replace(".sbactorpack", "");

                    Directory.CreateDirectory(ActivePath() + "\\~Build\\HyRe\\content\\Physics\\RigidBody\\HKRB\\");
                    File.Move(ActivePath() + "\\~Build\\" + CurrentActorName + ".hkrb", ActivePath() + "\\~Build\\HyRe\\content\\Physics\\RigidBody\\HKRB\\" + CurrentActorName + ".hkrb");
                    File.Move(ActivePath() + "\\~Build\\" + CurrentActorName + ".yml", ActivePath() + "\\~Build\\HyRe\\content\\Actor\\Physics\\" + CurrentActorName + ".bphysics.yml");

                    Console.WriteLine("Writting Info For: " + CurrentActorName + "...");
                    editInfo(ActivePath() + "\\~Build\\HyRe\\content\\Actor\\ActorInfo\\" + CurrentActorName + ".info.yml", CurrentActorName);
                    Console.WriteLine("Writting ActorLinks For: " + CurrentActorName + "...");
                    editLink(ActivePath() + "\\~Build\\HyRe\\content\\Actor\\ActorLink\\" + CurrentActorName + ".bxml.yml", CurrentActorName);
                    Console.WriteLine("Writting ModelList For: " + CurrentActorName + "...");
                    editModelList(ActivePath() + "\\~Build\\HyRe\\content\\Actor\\ModelList\\" + CurrentActorName + ".bmodellist.yml", CurrentActorName);
                    Console.WriteLine("Writting BPhysics For: " + CurrentActorName + "...");
                    editBPhysics(ActivePath() + "\\~Build\\HyRe\\content\\Actor\\Physics\\" + CurrentActorName + ".bphysics.yml", CurrentActorName);
                }

                Console.WriteLine("Rebuilding Files...\n");
                HyRe.StartInfo.Arguments = "/c hyrule_builder build --be \"" + ActivePath() + "\\~Build\\HyRe\"";
                await Task.Run(() => HyRe.Start());

                await HyRe.WaitForExitAsync();

                Console.WriteLine("Cleaning Files...");

                Directory.CreateDirectory(ActivePath() + "\\Build\\content\\Actor\\Pack");
                Directory.CreateDirectory(ActivePath() + "\\Build\\content\\System\\Resource");
                foreach (var file in Directory.GetFiles(contentPath))
                {
                    string CurrentActorName = file.Replace(contentPath + "\\", "").Replace(".sbactorpack", "");

                    File.Move(ActivePath() + "\\~Build\\HyRe\\build\\content\\Actor\\Pack\\" + CurrentActorName + ".sbactorpack", ActivePath() + "\\Build\\content\\Actor\\Pack\\" + CurrentActorName + ".sbactorpack");

                    File.AppendAllText(ActivePath() + " Info.txt", "For: " + CurrentActorName + ".\n\tSBFRES File/Folder Name: " + CurrentActorName.Replace("_01", "") + ".sbfres\n\tModel Unit Name: " +
                        CurrentActorName + "\n\tActor Name: " + CurrentActorName + "\n");
                }
                File.Move(ActivePath() + "\\~Build\\HyRe\\build\\content\\Actor\\ActorInfo.product.sbyml", ActivePath() + "\\Build\\content\\Actor\\ActorInfo.product.sbyml");
                File.Move(ActivePath() + "\\~Build\\HyRe\\build\\content\\System\\Resource\\ResourceSizeTable.product.srsizetable", ActivePath() + "\\Build\\content\\System\\Resource\\ResourceSizeTable.product.srsizetable");

                Directory.Delete(ActivePath() + "\\~Build", true);

                Console.WriteLine("Process Complete!");
            }
            else if (args[0] == "-c" || args[0] == "quick-collision")
            {
                foreach (var file in Directory.GetFiles(ActivePath()))
                {
                    Process proc = new Process();
                    //proc.StartInfo.CreateNoWindow = true;
                    proc.StartInfo.FileName = "cmd.exe";
                    proc.StartInfo.Arguments = "/k hksc_to_hkrb \"" + file + "\" \"" + file.Replace("shksc", "hkrb").Replace("hksc", "hkrb") + "\"";

                    proc.Start();
                }
            }
        }

        public static async Task createdata(string ApplicationPath, string[] args, bool writeFile = false)
        {
            string contentPath = null;
            string materialPath = null;
            string modelPath = null;
            string texturePath = null;
            string animPath = null;

            string optionM = null;
            string optionT = null;
            string optionF = null;
            string optionS = null;

            if (isFile == true)
            {
                string[] dataFile = File.ReadAllLines(args[0]);

                try
                {
                    if (!File.Exists(ApplicationPath + "\\data\\paths.dat"))
                    {
                        Console.WriteLine("Error\n\nGame paths not set, set them with \"-p \"path\\to\\game\\content\" \"path\\to\\update\\content\" \"path\\to\\DLC\\0010\" (optianal)\"");
                    }
                    else
                    {
                        #region Files

                        #endregion

                        #region Folders + Files

                        foreach (var line in dataFile)
                        {
                            if (line.Contains("m~"))
                            {
                                materialPath = line.Replace("\tm~", "");
                            }
                            else if (line.Contains("s~"))
                            {
                                modelPath = line.Replace("\ts~", "");
                            }
                            else if (line.Contains("t~"))
                            {
                                texturePath = line.Replace("\tt~", "");
                            }
                            else if (line.Contains("a~"))
                            {
                                animPath = line.Replace("\ta~", "");
                            }
                            //Read Folder data
                            else if (line.Split('\\')[0] == "\t")
                            {
                                Directory.CreateDirectory(ActivePath() + line.Replace("\t", ""));
                            }
                            //Read Material Data
                            else if (line.Split('\\')[0].Contains("\tdata"))
                            {
                                string[] types = line.Replace("\t", "").Split(';');

                                if (types[0] != "") //Materials
                                {
                                    File.Copy(ApplicationPath + types[0], ActivePath() + materialPath + "\\" + types[0].Replace("data\\MT\\", "").Replace(".bin", ".bfmat"), true);
                                }
                                if (types[2] != "") //Models
                                {
                                    File.Copy(ApplicationPath + types[2], ActivePath() + modelPath + "\\" + types[2].Replace("data\\SB\\", "").Replace(".bin", ".sbfres"), true);
                                    File.Copy(ApplicationPath + types[2].Replace(".bin", "_.bin"), ActivePath() + modelPath + "\\" + types[2].Replace("data\\SB\\", "").Replace(".bin", ".Tex1.sbfres"), true);
                                }
                                if (types[1] != "") //Textures
                                {
                                    string[] files = Directory.GetFiles(ApplicationPath + "\\" + types[1], ".", SearchOption.AllDirectories);

                                    foreach (var file in files)
                                    {
                                        string[] fileName = file.Replace(".bin", ".dds").Split('\\');

                                        Directory.CreateDirectory(ActivePath() + texturePath + "\\" + fileName[fileName.Length - 2]);
                                        File.Copy(file, ActivePath() + texturePath + "\\" + fileName[fileName.Length - 2] + "\\" + fileName[fileName.Length - 1]);
                                    }
                                }
                                if (types[3] != "") //Animations
                                {
                                    Directory.CreateDirectory(animPath + "\\" + types[3]);
                                    string[] files = Directory.GetFiles(ApplicationPath + "\\" + types[3], ".", SearchOption.AllDirectories);

                                    foreach (var file in files)
                                    {
                                        string[] fileName = file.Replace(".bin", ".dds").Split('\\');

                                        File.Copy(file, texturePath + "\\" + "");
                                    }
                                }
                            }
                            //Read BotW Files
                            else if (line == "BotW Files")
                            {
                                string[] values = line.Split(',');
                            }
                        }

                        #endregion

                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            else //Making a BFT
            {
                //-b (args[0]), -m (args[1]), "string" (args[2]), -t (args[3]), "string" (args[4]), -fm (args[5]), "string" (args[6]), -fs (args[7]), "string (args[8], -c (args[9]))
                if (isFullLine == true)
                {
                    optionM = args[2];
                    optionT = args[4];
                    optionF = args[6];
                    optionS = args[8];
                }
                else
                {
                    #region Getting arguments
                    string data = null;
                    int aa = 0;

                    string option1 = null;
                    string option2 = null;
                    string option3 = null;
                    string option4 = null;
                    string option5 = null;
                    string option6 = null;
                    string option7 = null;
                    string option8 = null;

                    foreach (string argument in args)
                    {
                        aa = aa + 1;
                        if (aa == 2) { option1 = argument + "|"; }
                        if (aa == 3) { option2 = argument + "|"; }
                        if (aa == 4) { option3 = argument + "|"; }
                        if (aa == 5) { option4 = argument + "|"; }
                        if (aa == 6) { option5 = argument + "|"; }
                        if (aa == 7) { option6 = argument + "|"; }
                        if (aa == 8) { option7 = argument + "|"; }
                        if (aa == 9) { option8 = argument + "|"; }
                    }

                    data = data + option1;
                    data = data + option2;
                    data = data + option3;
                    data = data + option4;
                    data = data + option5;
                    data = data + option6;
                    data = data + option7;
                    data = data + option8;

                    string[] options = data.Split('|');

                    bool m = false;
                    bool t = false;
                    bool f = false;
                    bool s = false;

                    foreach (var option in options)
                    {
                        if (m == true) { optionM = option; m = false; }
                        if (t == true) { optionT = option; t = false; }
                        if (f == true) { optionF = option; f = false; }
                        if (s == true) { optionS = option; s = false; }
                        if (option == "-m") { m = true; }
                        if (option == "-t") { t = true; }
                        if (option == "-fm") { f = true; }
                        if (option == "-fs") { s = true; }
                    }
                    #endregion

                    #region Converting arguments
                    //OptionF - Folders
                    if (optionF_Yes == true)
                    {
                        bool checkM = false;
                        bool checkS = false;
                        bool checkT = false;
                        bool checkA = false;

                        string searchPath = optionF;
                        string[] directories = Directory.GetDirectories(optionF, ".", SearchOption.AllDirectories);

                        optionF = optionF.Replace(optionF, "").Replace("!", "");
                        foreach (var folder in directories)
                        {
                            optionF = optionF + "\n\t" + folder.Replace("!", "").Replace(searchPath, "").Replace("m~", "").Replace("t~", "").Replace("s~", "").Replace("a~", "");
                            if (folder.Contains("!"))
                            {
                                contentPath = folder.Replace("m~", "").Replace("t~", "").Replace("s~", "").Replace("a~", "").Replace(searchPath, "");
                            }
                            else if (folder.Contains("m~") && checkM == false)
                            {
                                materialPath = folder.Replace("m~", "").Replace("t~", "").Replace("s~", "").Replace("a~", "").Replace(searchPath, "");
                                checkM = true;
                            }
                            else if (folder.Contains("s~") && checkS == false)
                            {
                                modelPath = folder.Replace("m~", "").Replace("t~", "").Replace("s~", "").Replace("a~", "").Replace(searchPath, "");
                                checkS = true;
                            }
                            else if (folder.Contains("t~") && checkT == false)
                            {
                                texturePath = folder.Replace("m~", "").Replace("t~", "").Replace("s~", "").Replace("a~", "").Replace(searchPath, "");
                                checkT = true;
                            }
                            else if (folder.Contains("a~") && checkA == false)
                            {
                                animPath = folder.Replace("m~", "").Replace("t~", "").Replace("s~", "").Replace("a~", "").Replace(searchPath, "");
                                checkA = true;
                            }
                        }
                    }
                    //OptionS - BotW Specific Directories
                    if (optionS_Yes == true)
                    {
                        string[] paths = optionS.Replace(" ", "").Split(',');
                        string[] accptedValues = { "content", "aoc", "Actor", "Event", "Font", "Layout", "Map", "Model", "Movie", "Pack", "Physics", "Sound", "System", "Terrain", "UI", "Voice" };

                        optionS = null;
                        foreach (string param in paths)
                        {
                            foreach (string item in accptedValues)
                            {
                                if (param.Replace("!", "").Replace("_", "").Replace("~", "") == item)
                                {
                                    if (param.Contains("!"))
                                    {
                                        for (int i = 0; i < 3; i++)
                                        {
                                            try
                                            {
                                                string[] filePaths = File.ReadAllLines(ApplicationPath + "\\data\\paths.dat");
                                                string[] pathsInGame = Directory.GetDirectories(filePaths[i] + "\\" + item, ".", SearchOption.AllDirectories);

                                                if (pathsInGame == null)
                                                {
                                                    optionS = optionS + "\n\t" + contentPath + "\\" + item;
                                                }
                                                foreach (var folder in pathsInGame)
                                                {
                                                    optionS = optionS + "\n\t" + contentPath + "\\" + folder.Replace(filePaths[i] + "\\", "");
                                                }

                                                if (item == "Actor")
                                                {
                                                    optionT = optionT.Replace("\n\tActor\\ActorInfo.product.sbyml", "") + "\n\tActor\\ActorInfo.product.sbyml";
                                                }
                                            }
                                            catch
                                            {

                                            }
                                        }
                                    }
                                    else if (item.Contains("~"))
                                    {

                                    }
                                    else if (item == "Map")
                                    {
                                        optionS = optionS + "\n\t" + contentPath.Replace("content", "aoc") + "\\" + item;
                                    }
                                    else
                                    {
                                        if (item == "Actor")
                                        {
                                            optionT = optionT.Replace("\n\tActor\\ActorInfo.product.sbyml", "") + "\n\tActor\\ActorInfo.product.sbyml";
                                        }
                                        optionS = optionS + "\n\t" + contentPath + "\\" + item;
                                    }
                                }
                                else if (param.Contains("_") && item == "Map")
                                {
                                    string[] datavalue = param.Split('_');

                                    optionS = optionS + "\n\t" + contentPath.Replace("content", "aoc\\0010") + "\\" + item + "\\" + datavalue[1] + "\\" + datavalue[2];
                                }
                            }
                        }
                    }
                    //OptioM - Materials
                    if (optionM_Yes == true)
                    {
                        string optionMData = optionM;
                        optionM = null;
                        foreach (string param in optionMData.Replace(" ", "").Split(','))
                        {
                            string[] validValues = { "Alpha", "Eye", "Glass", "Ice", "Light", "LineLight", "Malice", "Metal", "ShrineWall", "Wall", "Water" };

                            foreach (string value in validValues)
                            {
                                string optionMString = null;
                                if (File.Exists(ApplicationPath + "\\data\\MT\\" + value + ".bfmat"))
                                {
                                    optionMString = optionMString + "data\\MT\\" + value + ".bfmat;";
                                }
                                else
                                {
                                    optionMString = optionMString + ";";
                                }

                                if (Directory.Exists(ApplicationPath + "\\data\\TX\\" + value))
                                {
                                    optionMString = optionMString + "\\data\\TX\\" + value + ";";
                                }
                                else
                                {
                                    optionMString = optionMString + ";";
                                }

                                if (File.Exists(ApplicationPath + "\\data\\SB\\" + value + ".bin"))
                                {
                                    optionMString = optionMString + "\\data\\SB\\" + value + ".bin;";
                                }
                                else
                                {
                                    optionMString = optionMString + ";";
                                }

                                if (Directory.Exists(ApplicationPath + "\\data\\AN\\" + value))
                                {
                                    optionMString = optionMString + "\\data\\AN\\" + value;
                                }
                                else
                                {
                                    optionMString = optionMString + ";";
                                }

                                if (param == value)
                                {
                                    optionM = optionM + "\n\t" + optionMString;
                                }
                            }
                        }
                    }
                    //OptioT - Type(should this even exist?) ~~Files
                    if (optionT_Yes == true)
                    {
                        optionT = "\n\t" + optionT.Replace(", ", "\n\t");
                    }

                    Console.WriteLine("Done!");
                    #endregion
                }

                string[] bftCount = Directory.GetFiles(ActivePath(), "*.bft");

                File.WriteAllText(ActivePath() + "\\DirectoryData_" + (bftCount.Length + 1) + ".bft", "#Folder Data File\n\nKey Folders; \n\tm~" + materialPath + "\n\ts~" + modelPath + "\n\tt~" + texturePath + "\n\ta~" + animPath + "\nFolders; " + optionF + "\nBotW Folders; " + optionS.Replace("!", "") + "\nMaterials; " + optionM + "\nBotW Files; " + optionT);
            }
        }
        public static async Task editInfo(string infoFile, string ActorName)
        {
            if (!File.Exists(infoFile))
            {
                File.Copy(infoFile.Replace(ActorName, "TwnObj_TempleOfTime_A_01"), infoFile);
            }
            string path = infoFile.Replace("\\Actor\\ActorInfo\\" + ActorName + ".info.yml", "");
            string ActiveInstSize = null;
            foreach (var modual in File.ReadAllLines(infoFile))
            {
                string[] data = modual.Split(':');
                if (data[0] == "instSize")
                {
                    ActiveInstSize = data[1];
                }
            }

            long infodata_1 = new FileInfo(path + "\\Physics\\RigidBody\\HKRB\\" + ActorName + ".hkrb").Length;
            int infodata_2 = (int)infodata_1 / 1000;
            int InstSize = 0;
            if (ActiveInstSize != null) { InstSize = infodata_2 + int.Parse(ActiveInstSize); }
            string mapProfile = "MapDynamicActive";

            foreach (var modual in File.ReadAllLines(infoFile))
            {
                bool info1 = false;
                bool info2 = false;
                bool info3 = false;
                string[] lineSplit = modual.Split(':');
                string lineArgs = null;
                if (lineSplit[0] == "bfres")
                {
                    lineArgs = "bfres: " + ActorName.Replace("_01", "");
                    info2 = true;
                }
                else if (lineSplit[0] == "instSize")
                {
                    lineArgs = "instSize: " + InstSize.ToString();
                    info1 = true;
                }
                else if (lineSplit[0] == "name")
                {
                    lineArgs = "name: " + ActorName;
                    info2 = true;
                }
                else if (lineSplit[0] == "mainModel")
                {
                    lineArgs = "mainmodel: " + ActorName;
                    info2 = true;
                }
                else if (lineSplit[0] == "profile")
                {
                    lineArgs = "profile: " + mapProfile;
                    info3 = true;
                }
                else
                {
                    lineArgs = modual;
                }
                File.AppendAllTextAsync(infoFile + ".temp", lineArgs + "\n");

            }
            File.Move(infoFile + ".temp", infoFile, true);
        }
        public static async Task editLink(string linkFile, string ActorName)
        {
            string path = linkFile.Replace("\\Actor\\ActorLink\\" + ActorName + ".bxml.yml", "");
            string mapProfile = "MapDynamicActive";

            string[] oldFile = Directory.GetFiles(path + "\\Actor\\ActorLink\\", ".");
            File.Copy(oldFile[0], linkFile);

            foreach (var modual in File.ReadAllLines(path + "\\Actor\\ActorLink\\" + ActorName + ".bxml.yml"))
            {
                string[] lineSplit = modual.Split(':');
                string lineArgs = null;

                if (lineSplit[0] == "      ModelUser")
                {
                    lineArgs = "      ModelUser: " + ActorName;
                }
                else if (lineSplit[0] == "      PhysicsUser")
                {
                    lineArgs = "      PhysicsUser: " + ActorName;
                }
                else if (lineSplit[0] == "      ProfileUser")
                {
                    lineArgs = "      ProfileUser: " + mapProfile;
                }
                else
                {
                    lineArgs = modual;
                }
                File.AppendAllTextAsync(linkFile + ".temp", lineArgs + "\n");
            }
            File.Move(linkFile + ".temp", linkFile, true);
        }
        public static async Task editBPhysics(string physicsFile, string ActorName)
        {
            string path = physicsFile.Replace("\\Actor\\ActorLink\\" + ActorName + ".bphysics.yml", "");
            foreach (var modual in File.ReadAllLines(physicsFile))
            {
                string[] lineSplit = modual.Split(':');
                string lineArgs = null;
                if (lineSplit[0] == "                  num")
                {
                    lineArgs = "                  num:" + lineSplit[1] + ":" + lineSplit[2].Replace("path_to/the_file.hkrb", "HKRB/" + ActorName + ".hkrb");
                }
                else
                {
                    lineArgs = modual;
                }
                File.AppendAllTextAsync(physicsFile + ".temp", lineArgs + "\n");
            }
            File.Move(physicsFile + ".temp", physicsFile, true);
        }
        public static async Task editModelList(string modelFile, string ActorName)
        {
            string path = modelFile.Replace("\\Actor\\ModelList\\" + ActorName + ".bmodellist.yml", "");

            string[] oldFile = Directory.GetFiles(path + "\\Actor\\ModelList\\", ".");
            File.Copy(oldFile[0], modelFile);

            foreach (var modual in File.ReadAllLines(path + "\\Actor\\ModelList\\" + ActorName + ".bmodellist.yml"))
            {
                string[] lineSplit = modual.Split(':');
                string lineArgs = null;

                if (lineSplit[0] == "              Folder")
                {
                    lineArgs = "              Folder: " + ActorName.Replace("_01", "");
                }
                else if (lineSplit[0] == "                  UnitName")
                {
                    lineArgs = "                  UnitName: " + ActorName;
                }
                else
                {
                    lineArgs = modual;
                }
                File.AppendAllTextAsync(modelFile + ".temp", lineArgs + "\n");
            }
            File.Move(modelFile + ".temp", modelFile, true);
        }
    }
}
