using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace BasiModCreator
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
            //string[] args = { @"-r" };
            //string[] args = { @"C:\Users\HP USER\source\repos\Breath of the Wild File Gathering Tool\BotWFileGatherTool\bin\Debug\net5.0\something.bft" };
            //string[] args = { "-b", "-m", "Glass,Eye,LineLight", "-t", "DgnObj_DLC_BattleRoom_Parts_B.Tex1.sbfres, Animal_Boar_Big.sbactorpack", "-fm", @"C:\Users\HP USER\source\repos\Breath of the Wild File Gathering Tool\BotWFileGatherTool\bin\Debug\net5.0\ToolTest", "-fs", "Actor!, Pack, Map_MainField_J-8, Model" };

            string AppPath = System.Reflection.Assembly.GetEntryAssembly().Location.Replace("BasicModCreator.dll", "");
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
                    File.WriteAllText(AppPath + "data\\paths.txt", args[1] + "\n" + args[2] + "\n" + dlcPath + "\n");
                }
                else
                {
                    File.WriteAllText(AppPath + "data\\paths.txt", args[1] + "\n" + args[2] + "\n" + dlcPath + "\n");
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
            }//???
            else if (args[0] == "-r" || args[0] == "readdata")
            {
                
                string contentPath = null;
                string ActorName = null;
                Console.WriteLine("Collecting Data...");
                Directory.CreateDirectory(ActivePath() + "\\~Build\\HyRe");
                foreach (var item in Directory.GetFiles(AppPath + "\\collision"))
                {
                    string[] fileName = item.Split('\\');
                    File.Copy(item, ActivePath() + "\\" + fileName[fileName.Length - 1]);
                }
                int i = 0;
                foreach (var file in Directory.GetFiles(ActivePath(), ".", SearchOption.AllDirectories))
                {
                    i = i + 1;
                    Process proc = new Process();
                    proc.StartInfo.FileName = "cmd.exe";
                    if (file.Contains(".obj"))
                    {
                        Console.WriteLine("Writting HKRB...");
                        string[] filePath = file.Split('\\');
                        string fileName = filePath[filePath.Length - 1];
                        proc.StartInfo.Arguments = "/c .\\CreateCollisionAndNavmesh.exe \".\\" + fileName + "\" hkrb";

                        await Task.Run(() => proc.Start());
                        await proc.WaitForExitAsync();
                    }
                }
                try
                {
                    File.Delete(ActivePath() + "\\BakeTool.exe");
                    File.Delete(ActivePath() + "\\CreateCollisionAndNavmesh.exe");
                    File.Delete(ActivePath() + "\\libgcc_s_seh-1.dll");
                    File.Delete(ActivePath() + "\\libgomp-1.dll");
                    File.Delete(ActivePath() + "\\libstdc++-6.dll");
                    File.Delete(ActivePath() + "\\libwinpthread-1.dll");
                }
                catch
                {

                }
                foreach (var folder in Directory.GetDirectories(ActivePath(), ".", SearchOption.AllDirectories))
                {
                    if (folder.Contains("\\content"))
                    {
                        contentPath = folder;
                        if (Directory.Exists(folder + "\\Actor\\Pack"))
                        {
                            Console.WriteLine("Transfering Actorpacks...");
                            Directory.CreateDirectory(ActivePath() + "\\~Build\\HyRe\\content\\Actor\\Pack");
                            File.Copy(File.ReadAllLines(AppPath + "\\data\\paths.txt")[1] + "\\Actor\\ActorInfo.product.sbyml", ActivePath() + "\\~Build\\HyRe\\content\\Actor\\ActorInfo.product.sbyml");
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
                Console.WriteLine("Unbuilding...");
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
                    File.Move(ActivePath() + "\\" + CurrentActorName + ".obj.hkrb", ActivePath() + "\\~Build\\HyRe\\content\\Physics\\RigidBody\\HKRB\\" + CurrentActorName + ".hkrb");
                    File.Copy(AppPath + "\\data\\physics.yml", ActivePath() + "\\~Build\\HyRe\\content\\Actor\\Physics\\" + CurrentActorName + ".bphysics.yml");

                    Console.WriteLine("Writting Info For: " + CurrentActorName + "...");
                    editInfo(ActivePath() + "\\~Build\\HyRe\\content\\Actor\\ActorInfo\\" + CurrentActorName + ".info.yml", CurrentActorName);
                    Console.WriteLine("Writting ActorLinks For: " + CurrentActorName + "...");
                    editLink(ActivePath() + "\\~Build\\HyRe\\content\\Actor\\ActorLink\\" + CurrentActorName + ".bxml.yml", CurrentActorName);
                    Console.WriteLine("Writting ModelList For: " + CurrentActorName + "...");
                    editModelList(ActivePath() + "\\~Build\\HyRe\\content\\Actor\\ModelList\\" + CurrentActorName + ".bmodellist.yml", CurrentActorName);
                    Console.WriteLine("Writting BPhysics For: " + CurrentActorName + "...");
                    editBPhysics(ActivePath() + "\\~Build\\HyRe\\content\\Actor\\Physics\\" + CurrentActorName + ".bphysics.yml", CurrentActorName);
                }

                Console.WriteLine("Rebuilding Files...");
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

                    File.AppendAllText(ActivePath() + "\\" + CurrentActorName + "_Info.txt", "For: " + CurrentActorName + ".\n\tSBFRES File/Folder Name: " + CurrentActorName.Replace("_01", "") + ".sbfres\n\tModel Unit Name: " +
                        CurrentActorName + "\n\tActor Name: " + CurrentActorName + "\n");
                }
                File.Move(ActivePath() + "\\~Build\\HyRe\\build\\content\\Actor\\ActorInfo.product.sbyml", ActivePath() + "\\Build\\content\\Actor\\ActorInfo.product.sbyml");
                File.Move(ActivePath() + "\\~Build\\HyRe\\build\\content\\System\\Resource\\ResourceSizeTable.product.srsizetable", ActivePath() + "\\Build\\content\\System\\Resource\\ResourceSizeTable.product.srsizetable");

                Directory.Delete(ActivePath() + "\\~Build", true);

                Console.WriteLine("Process Complete!");
            }
            else if (args[0] == "-rp" || args[0] == "rebuild-pack")
            {
                string[] paths = File.ReadAllLines(AppPath + "\\data\\paths.txt");
                string packInt = args[1];
                string packID = "Dungeon" + args[1] + ".pack";
                //Requirements: Model (SBFRES), Collision (SHKSC), Navmesh (OBJ, shknm2)

                //First, get desired pack and Unbuild
                Directory.CreateDirectory(ActivePath() + "\\~ShrineBuild\\content\\Pack");
                try
                {
                    File.Copy(paths[0] + "\\Pack\\" + packID, ActivePath() + "\\~ShrineBuild\\content\\Pack\\" + packID);
                    File.Copy(paths[1] + "\\Actor\\ActorInfo.product.sbyml", ActivePath() + "\\~ShrineBuild\\content\\Actor\\ActorInfo.product.sbyml");
                }
                catch
                {
                    Console.WriteLine("Existing build found in this directory. Aborting...");
                    return;
                }
                Process proc = new Process();
                proc.StartInfo.FileName = "cmd.exe";
                proc.StartInfo.Arguments = "/c hyrule_builder unbuild \"" + ActivePath() + "\\~ShrineBuild\"";

                await Task.Run(() => proc.Start());
                await proc.WaitForExitAsync();

                Directory.Move(ActivePath() + "\\~ShrineBuild", ActivePath() + "\\~ShrineBuild_CollectedData");
                Directory.Move(ActivePath() + "\\~ShrineBuild_unbuilt", ActivePath() + "\\~ShrineBuild");
                //Create and Replace: Model, Physics, Navmesh;
                //Model: check for Tex1 and Tex2. If exist, move to pack and rename.

                try
                {
                    bool SBFRES = false;
                    bool Tex1 = false;
                    bool Tex2 = false;
                    foreach (var file in Directory.GetFiles(ActivePath(), ".", SearchOption.AllDirectories))
                    {
                        //SBFRES
                        if (file.Contains(".sbfres") && SBFRES == false)
                        {
                            Console.WriteLine("Model file found. Moving...");
                            File.Copy(file, ActivePath() + "\\~ShrineBuild\\content\\Pack\\" + packID + "\\Model\\DgnMrgPrt_Dungeon" + packInt + ".sbfres", true);
                            SBFRES = true;
                        }
                        //Tex1
                        if (file.Contains(".Tex1.sbfres") && Tex1 == false)
                        {
                            Console.WriteLine("Tex1 found. Moving...");
                            Directory.CreateDirectory(ActivePath() + "\\~ShrineBuild\\content\\Model");
                            File.Copy(file, ActivePath() + "\\~ShrineBuild\\content\\Model\\DgnMrgPrt_Dungeon" + packInt + ".Tex1.sbfres", true);
                            Tex1 = true;
                        }
                        //Tex2
                        if (file.Contains(".Tex2.sbfres") && Tex2 == false)
                        {
                            Console.WriteLine("Tex2 found. Moving...");
                            File.Copy(file, ActivePath() + "\\~ShrineBuild\\content\\Pack\\" + packID + "\\Model\\DgnMrgPrt_Dungeon" + packInt + ".Tex2.sbfres", true);
                            Tex2 = true;
                        }
                        
                    }
                    //Checks
                    if (SBFRES == false) { Console.WriteLine("Model file not found. Skipping..."); }
                    if (Tex1 == false) { Console.WriteLine("Tex1 not found. Skipping..."); }
                    if (Tex2 == false) { Console.WriteLine("Tex2 not found. Skipping..."); }

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                //try
                //{
                    Process proc2 = new Process();
                    proc2.StartInfo.FileName = "cmd.exe";
                    Process proc3 = new Process();
                    proc3.StartInfo.FileName = "cmd.exe";
                    bool readyCollision = false;
                    bool readyNavmesh = false;
                    string navMeshArgs = null;
                    string collisionArgs = null;
                    string fileName = null;
                    foreach (var item in Directory.GetFiles(AppPath + "\\collision"))
                    {
                        string[] fileSlpit = item.Split('\\');
                        File.Copy(item, ActivePath() + "\\" + fileSlpit[fileSlpit.Length - 1]);
                    }
                    foreach (var file in Directory.GetFiles(ActivePath())) //Handle navmesh and collision
                    {
                        string[] x = file.Split('\\');
                        fileName = x[x.Length - 1];
                        if (file.Contains("NavMesh.obj"))
                        {
                            navMeshArgs = "/c .\\CreateCollisionAndNavmesh.exe \".\\NavMesh.obj\" hknm2 && yazit \".\\NavMesh.obj.hknm2\"";
                        }
                        else if (file.Contains("Collision.obj"))
                        {
                            collisionArgs = "/c .\\CreateCollisionAndNavmesh.exe \".\\Collision.obj\" hksc && yazit \".\\Collision.obj.hksc\"";
                        }
                        else if (file.Contains(".shknm2"))
                        {
                            File.Move(file, ActivePath() + "\\~ShrineBuild\\content\\Pack\\" + packID + "\\NavMesh\\CDungeon\\" + packID.Replace(".pack", ".shknm2"), true);
                        }
                        else if (file.Contains(".hknm2"))
                        {
                            navMeshArgs = "/c yazit \".\\NavMesh.obj.hknm2\"";
                        }
                        else if (file.Contains(".shksc"))
                        {
                            File.Move(file, ActivePath() + "\\~ShrineBuild\\content\\Pack\\" + packID + "\\Physics\\StaticCompound\\CDungeon\\" + packID.Replace(".pack", ".shksc"), true);
                        }
                        else if (file.Contains(".hksc"))
                        {
                            collisionArgs = "/c yazit \".\\Collision.hksc\"";
                        }
                        else
                        {

                        }
                    }

                    if (collisionArgs == null) { readyCollision = true; }
                    if (readyCollision != true)
                    {
                        proc2.StartInfo.Arguments = collisionArgs;
                        await Task.Run(() => proc2.Start());
                        await proc2.WaitForExitAsync();

                        File.Move(ActivePath() + "\\Collision.obj.shksc", ActivePath() + "\\~ShrineBuild\\content\\Pack\\" + packID + "\\Physics\\StaticCompound\\CDungeon\\" + packID.Replace(".pack", ".shksc"), true);
                        File.Delete(ActivePath() + "\\Collision.obj.hksc");
                    }

                    if (navMeshArgs == null) { readyNavmesh = true; }
                    if (readyNavmesh != true)
                    {
                        proc3.StartInfo.Arguments = navMeshArgs;
                        await Task.Run(() => proc3.Start());
                        await proc3.WaitForExitAsync();

                        File.Move(ActivePath() + "\\NavMesh.obj.shknm2", ActivePath() + "\\~ShrineBuild\\content\\Pack\\" + packID + "\\NavMesh\\CDungeon\\" + packID.Replace(".pack", "") + "\\" + packID.Replace(".pack", ".shknm2"), true);
                        File.Delete(ActivePath() + "\\NavMesh.obj.hknm2");
                    }

                    try //Cleanup
                    {
                        File.Delete(ActivePath() + "\\BakeTool.exe");
                        File.Delete(ActivePath() + "\\CreateCollisionAndNavmesh.exe");
                        File.Delete(ActivePath() + "\\libgcc_s_seh-1.dll");
                        File.Delete(ActivePath() + "\\libgomp-1.dll");
                        File.Delete(ActivePath() + "\\libstdc++-6.dll");
                        File.Delete(ActivePath() + "\\libwinpthread-1.dll");
                    }
                    catch
                    {

                    }
                    
                //}
                //catch
                //{
                //    Console.WriteLine("Process failed. No navmesh or collision.");
                //}
                try //Repacking
                {
                    Process proc4 = new Process();
                    proc4.StartInfo.FileName = "cmd.exe";
                    proc4.StartInfo.Arguments = "/c hyrule_builder build --be \".\\~ShrineBuild\"";

                    proc4.Start();
                    proc4.WaitForExit();

                    Directory.Move(ActivePath() + "\\~ShrineBuild\\build", ActivePath() + "\\Built_" + packID.Replace(".pack", ""));

                    Directory.Delete(ActivePath() + "\\~ShrineBuild", true);
                    Directory.Delete(ActivePath() + "\\~ShrineBuild_CollectedData", true);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }//Experimental
            else if (args[0] == "-c" || args[0] == "quick-collision" || args[0].Contains(".obj"))
            {
                if (args[0].Contains(".obj"))
                {
                    string[] targetFile = args[0].Split('\\');
                    string targetFilePath = args[0].Replace(targetFile[targetFile.Length - 1], "");
                    string targetFileName = targetFile[targetFile.Length - 1];

                    foreach (var file in Directory.GetFiles(AppPath + "collision"))
                    {
                        string[] fileName = file.Split('\\');
                        File.Copy(file, targetFilePath + "\\" + fileName[fileName.Length - 1]);
                    }

                    Console.WriteLine("Writting HKRB...");
                    Process proc = new Process();
                    proc.StartInfo.FileName = "cmd.exe";
                    proc.StartInfo.Arguments = "/c .\\CreateCollisionAndNavmesh.exe \".\\" + targetFileName + "\" hkrb";

                    proc.Start();
                    proc.WaitForExit();
                    Console.WriteLine("Cleaning Files...");
                    try
                    {
                        File.Delete(targetFilePath + "\\BakeTool.exe");
                        File.Delete(targetFilePath + "\\CreateCollisionAndNavmesh.exe");
                        File.Delete(targetFilePath + "\\libgcc_s_seh-1.dll");
                        File.Delete(targetFilePath + "\\libgomp-1.dll");
                        File.Delete(targetFilePath + "\\libstdc++-6.dll");
                        File.Delete(targetFilePath + "\\libwinpthread-1.dll");
                    }
                    catch
                    {

                    }
                    Console.WriteLine("Process Complete!");
                }
                else
                {
                    foreach (var file in Directory.GetFiles(AppPath + "\\collision"))
                    {
                        string[] fileName = file.Split('\\');
                        File.Copy(file, ActivePath() + "\\" + fileName[fileName.Length - 1]);
                    }
                    foreach (var file in Directory.GetFiles(ActivePath()))
                    {
                        if (file.Contains(".obj"))
                        {
                            Console.WriteLine("Writting HKRB...");
                            string[] filePath = file.Split('\\');
                            string fileName = filePath[filePath.Length - 1];

                            Process proc = new Process();
                            proc.StartInfo.FileName = "cmd.exe";
                            proc.StartInfo.Arguments = "/c .\\CreateCollisionAndNavmesh.exe \".\\" + fileName + "\" hkrb";

                            proc.Start();
                            proc.WaitForExit();
                        }
                    }
                    Console.WriteLine("Cleaning Files...");
                    try
                    {
                        File.Delete(ActivePath() + "\\BakeTool.exe");
                        File.Delete(ActivePath() + "\\CreateCollisionAndNavmesh.exe");
                        File.Delete(ActivePath() + "\\libgcc_s_seh-1.dll");
                        File.Delete(ActivePath() + "\\libgomp-1.dll");
                        File.Delete(ActivePath() + "\\libstdc++-6.dll");
                        File.Delete(ActivePath() + "\\libwinpthread-1.dll");
                    }
                    catch
                    {

                    }
                    Console.WriteLine("Process Complete!");
                }
            }

            //HKRB Handling - Create Actor? Get size for OBJ ref. 
            //XXX.SHKNM2 Handling - Create shrine pack? Issue: Collision data, Object Data.
            //HKSC Handling - Yaz0 Compress
            //HKNM2 handling - Yaz0 Compress
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
                    if (!File.Exists(ApplicationPath + "\\data\\paths.txt"))
                    {
                        Console.WriteLine("Error\n\nGame paths not set, set them with \"-p \"path\\to\\game\\content\" \"path\\to\\update\\content\" \"path\\to\\DLC\\0010\" (optianal)\"");
                    }
                    else
                    {
                        #region Files

                        #endregion

                        #region Folders + Files
                        string[] GamePaths = File.ReadAllLines(ApplicationPath + "\\data\\paths.txt");
                        foreach (var line in dataFile)
                        {
                            if (line.Contains("!"))
                            {
                                contentPath = line.Replace("\t!", "");
                            }
                            else if (line.Contains("m~"))
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
                            else if (line.Contains("\tGame"))
                            {
                                string[] splitLine = line.Split('\\');
                                Directory.CreateDirectory(ActivePath() + "\\" + contentPath + "\\" + line.Replace("\tGame\\", "").Replace(splitLine[splitLine.Length - 1], ""));
                                File.Copy(GamePaths[0] + "\\" + line.Replace("\tGame\\", ""), ActivePath() + "\\" + contentPath + "\\" + line.Replace("\tGame\\", ""), true);

                            }
                            else if (line.Contains("\tUpdate"))
                            {
                                string[] splitLine = line.Split('\\');
                                Directory.CreateDirectory(ActivePath() + "\\" + contentPath + "\\" + line.Replace("\tUpdate\\", "").Replace(splitLine[splitLine.Length - 1], ""));
                                File.Copy(GamePaths[1] + "\\" + line.Replace("\tUpdate\\", ""), ActivePath() + "\\" + contentPath + "\\" + line.Replace("\tUpdate\\", ""), true);
                            }
                            else if (line.Contains("\tDLC"))
                            {
                                string[] splitLine = line.Split('\\');
                                Directory.CreateDirectory(ActivePath() + "\\" + contentPath.Replace("content", "aoc") + "\\" + line.Replace("\tDLC\\", "").Replace(splitLine[splitLine.Length - 1], ""));
                                File.Copy(GamePaths[2] + "\\" + line.Replace("\tDLC\\", ""), ActivePath() + "\\" + contentPath.Replace("content", "aoc") + "\\" + line.Replace("\tDLC\\", ""), true);

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
                        optionT = "\n\t" + optionT.Replace(" : ", "\n\t");
                    }

                    Console.WriteLine("Done!");
                    #endregion
                }

                string[] bftCount = Directory.GetFiles(ActivePath(), "*.bft");

                File.WriteAllText(ActivePath() + "\\DirectoryData_" + (bftCount.Length + 1) + ".bft", "#Folder Data File\n\nKey Folders; \n\t!" + contentPath + "\n\tm~" + materialPath + "\n\ts~" + modelPath + "\n\tt~" + texturePath + "\n\ta~" + animPath + "\nFolders; " + optionF + "\nBotW Folders; " + optionS.Replace("!", "") + "\nMaterials; " + optionM + "\nBotW Files; " + optionT);
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
                if (lineSplit[0] == "                  setup_file_path")
                {
                    lineArgs = "                  setup_file_path: " + lineSplit[1].Replace("path_to/the_file.hkrb", "HKRB/" + ActorName + ".hkrb");
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
