using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using static BasicModCreatorData.BMC;

namespace BasicModCreator
{
    class Program
    {
        static async Task Main(string[] args)
        {
            #region Filter Data
            if (args.Length == 0) 
            { args = new string[] { "-1" }; }

            string storeArgs = args[0];

            if (storeArgs == "-1")
            {
                Console.WriteLine("<Exception> - No arguments passed, open Basic Mod Creator UI?");

                await NoArguments_Exeption();

                return;
                
            }
            else
            {
                if (args[0].EndsWith(".bft")) { storeArgs = "BFT_File"; }
                if (args[0].EndsWith(".rtd")) { storeArgs = "RTD_File"; }
                if (args[0].EndsWith(".obj")) { storeArgs = "OBJ_File"; }
                if (args[0].EndsWith(".smubin") || args[0].EndsWith(".sbyml") || args[0].EndsWith(".byml") || args[0].EndsWith(".mubin")) { storeArgs = "BYML_File"; }
            }
            #endregion

            #region Handel Data
            switch (storeArgs)
            {
                //Show help
                case "-h": 
                case "help":
                    HelpConsole();
                    break;
                //Create BFT
                case "-b": 
                case "bft":
                    await CreateBFT(args);
                    break;
                //Read BFT and write data
                case "BFT_File":
                    await ReadBFT(args);
                    break;
                //Read data from folder
                case "-r": 
                case "readdata": 
                case "RTD_File":
                    await ReadData(args);
                    break;
                //Handle collision
                case "-c": 
                case "collision": 
                case "OBJ_File":
                    await collisionHandle(args);
                    break;
                //BYML Endian Changer
                case "-e":
                case "byml-endian-changer":
                case "BYML_File":
                    await BymlChanger(args);
                    break;
                //Creates a vanilla actor.
                case "-a":
                case "get-actor":
                    await GetActor(args);
                    break;
                //Yaz0 compress
                case "-y":
                case "yaz0":
                    break;
                //install misc files/data
                case "-i": 
                case "install":
                    await InstallData(args);
                    break;
                //Whilst args = true, but are undefined.
                default:
                    await CreateMod(args);
                    break;
            }
            #endregion
        }
    }
}
