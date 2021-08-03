using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace Botw_Tools
{
    public class Data
    {
        #region General Paths and Strings/Data
        public static string path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\BMC";
        public static string dataPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\BMC\\data";
        public static string tempPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\BMC\\.temp";
        public static string appPath = File.ReadAllLines(dataPath + "\\paths.txt")[7];

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

        #region File Paths, Files: Get > Name, Extension, Path, FolderInPath, Path -X Folders/Paths, Endian

        public static string GetExtension(string file)
        {
            string[] nm1 = file.Split('.');
            return "." + nm1[nm1.Length - 1];
        }
        public static string GetName(string file, bool removeExtension = false)
        {
            string[] nm1 = file.Split('\\');
            string returnName = nm1[nm1.Length - 1];
            if (removeExtension == true)
            {
                returnName = returnName.Replace(GetExtension(file), "");
            }
            return returnName;
        }
        public static string GetPath(string file)
        {
            string[] nm1 = file.Split('\\');
            return file.Replace(nm1[nm1.Length - 1], "");
        }
        public static string GetFolder(string file, string folder, bool isolateFolder = false, bool KeepFileInPath = false)
        {
            string[] nm1 = file.Split('\\');

            List<string> nmL1 = new List<string>();
            bool found = false;

            foreach (string fd1 in nm1)
            {
                if (fd1 == folder)
                {
                    nmL1.Add(fd1);
                    found = true;
                }
                else if (found != true && isolateFolder != true)
                {
                    nmL1.Add(fd1 + "\\");
                }
            }
            string result = nmL1.ToString();
            if (KeepFileInPath == true)
            {
                result = result + "\\" + GetName(file);
            }

            return result;
        }
        public static string GetEndianType(string file)
        {
            byte[] bytes = File.ReadAllBytes(file);
            string endianArgumentsType = null;

            if (bytes[0] == 89 || bytes[17] == 89)
            {
                endianArgumentsType = ""; //Little Endian
            }
            else if (bytes[0] == 66 || bytes[17] == 66)
            {
                endianArgumentsType = "-b"; //Big Endian
            }
            return endianArgumentsType;
        }
        #endregion

        #region Copy Directories with files, Move/Copy File(s), Delete Files/Directories, Create Directries, Open Directories

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
        public static void Copy_MoveFiles(string[] files, string destDir, bool move = false)
        {
            List<Task> tasks = new List<Task>();

            foreach (var file in files)
            {
                if (move == false)
                {
                    tasks.Add(Task.Run(() => File.Copy(file, destDir + "\\" + GetName(file))));
                }
                else
                {
                    tasks.Add(Task.Run(() => File.Move(file, destDir + "\\" + GetName(file))));
                }
            }

            Task.WaitAll(tasks.ToArray());
        }
        public static void DeleteFiles(string[] files)
        {
            List<Task> tasks = new List<Task>();

            foreach (var file in files)
            {
                tasks.Add(Task.Run(() => File.Delete(file)));
            }

            Task.WaitAll(tasks.ToArray());
        }
        public static async Task OpenDir(Process proc, string folderPath)
        {
            proc.StartInfo.FileName = "explorer.exe";
            proc.StartInfo.Arguments = folderPath;

            await Task.Run(() => proc.Start());
        }

        #endregion

        #region Lists<string>: Get a list of Files, LinesInFile, Directories, string[] to List<string>, string to List<string>.

        public static List<string> StringtoList(string str, char split)
        {
            string[] array = str.Split(split);
            List<string> list = new List<string>();

            foreach (var item in array)
            {
                list.Add(item);
            }

            return list;
        }
        public static List<string> ArrayToList(string[] array)
        {
            List<string> list = new List<string>();

            foreach (var item in array)
            {
                list.Add(item);
            }

            return list;
        }
        public static List<string> ListOfFiles(string dir, SearchOption readSub = SearchOption.AllDirectories, bool removePath = false)
        {
            List<string> list = new List<string>();

            foreach (var file in Directory.GetFiles(dir, ".", readSub))
            {
                if (removePath == true)
                {
                    list.Add(GetName(file));
                }
                else
                {
                    list.Add(file);
                }
            }

            return list;
        }

        public static List<string> ListOfFolder(string dir, SearchOption readSub = SearchOption.AllDirectories, bool removePath = true)
        {
            List<string> list = new List<string>();

            foreach (var folder in Directory.GetDirectories(dir, ".", readSub))
            {
                if (removePath == true)
                {
                    list.Add(GetName(folder));
                }
                else
                {
                    list.Add(folder);
                }
            }

            return list;
        }
        public static async Task<List<string>> FileLines(string file)
        {
            List<string> list = new List<string>();

            foreach (var line in await File.ReadAllLinesAsync(file))
            {
                list.Add(line);
            }

            return list;
        }
        public static async Task EditFile(string file, int[] line, string[] replaceWith)
        {
            string[] filedata = await File.ReadAllLinesAsync(file);
            int hold = 0;

            for (int i = 0; i < filedata.Length; i++)
            {
                if (i == line[hold] - 1)
                {
                    await File.AppendAllTextAsync(tempPath + "\\" + GetName(file) + ".tmp", replaceWith[hold] + "\n");

                    if (line.Length != hold + 1)
                    {
                        hold = hold + 1;
                    }
                }
                else
                {
                    await File.AppendAllTextAsync(tempPath + "\\" + GetName(file) + ".tmp", filedata[i] + "\n");
                }
            }

            File.Move(file + ".tmp", file, true);
        }
        /// <summary>
        /// Replaces the first line that is equal to lineText[x] with replaceWith[x]. 
        /// </summary>
        /// <returns>The new file with edited lines.</returns>
        public static async Task EditFileStr(string file, string[] lineText, string[] replaceWith)
        {
            string[] filedata = await File.ReadAllLinesAsync(file);
            int hold = 0;
            string done = null;

            foreach (var line in filedata)
            {
                if (line != done && line == lineText[hold])
                {
                    await File.AppendAllTextAsync(tempPath + "\\" + GetName(file) + ".tmp", replaceWith[hold] + "\n");

                    done = lineText[hold];

                    if (lineText.Length != hold + 1)
                    {
                        hold = hold + 1;
                    }
                }
                else
                {
                    await File.AppendAllTextAsync(tempPath + "\\" + GetName(file) + ".tmp", line + "\n");
                }
            }

            File.Move(file + ".tmp", file, true);
        }
        #endregion

        #region Process

        public static async Task Process(string fileName, string args, bool hidden = true, bool dontWait = false, string workingDir = null)
        {
            if (workingDir == null)
            {
                workingDir = AppContext.BaseDirectory;
            }
            Process proc = new Process();

            proc.StartInfo.WorkingDirectory = workingDir;
            proc.StartInfo.FileName = fileName;
            proc.StartInfo.CreateNoWindow = hidden;
            proc.StartInfo.Arguments = args;

            proc.Start();
            if (dontWait == false)
            {
                await proc.WaitForExitAsync();
            }
        }

        #endregion
    }
}
