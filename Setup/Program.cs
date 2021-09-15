using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;

namespace Setup
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Extracting executable...");
            await Task.Run(() => ExtractEmbed("executable.exe", "SW - Msyt Editor.exe"));

            Console.WriteLine("Extracting x64 files...");
            await Task.Run(() => ExtractEmbed("x64.zip", "x64.arc"));

            Console.WriteLine("Extracting font files...");
            string aaa = Environment.GetFolderPath(Environment.SpecialFolder.Fonts);
            await Task.Run(() => ExtractEmbed("font.otf", Environment.GetFolderPath(Environment.SpecialFolder.Fonts) + "\\Calamity-Regular.otf"));

            Console.WriteLine("Unpacking x64 files...");
            await Task.Run(() => ZipFile.ExtractToDirectory("x64.arc", "x64"));

            Console.WriteLine("Cleaning source directory...");
            File.Delete("x64.arc");

            Console.WriteLine("Adding font to registry...");

            #pragma warning disable CA1416 // Validate platform compatibility

            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Fonts", "Calamity (OpenType)", "Calamity-Regular.otf");

            #pragma warning restore CA1416 // Validate platform compatibility

            Console.WriteLine("\nInstall Complete");
            Console.ReadLine();
        }

        private static void ExtractEmbed(string fileName, string output)
        {
            Assembly assembly = Assembly.GetCallingAssembly();

            using (Stream stream = assembly.GetManifestResourceStream("Setup.SOURCE." + fileName))
            using (BinaryReader binaryReader = new(stream))
            using (FileStream fileStream = new(output, FileMode.OpenOrCreate))
            using (BinaryWriter binaryWriter = new(fileStream))
                binaryWriter.Write(binaryReader.ReadBytes((int)stream.Length));
        }
    }
}