using System;

namespace SilentPrincessMapEditor
{
    class sp_sc
    {
        static void Main(string[] args)
        {
            Console.WriteLine("This is the Silent Princess Map Editor c# console application.\n" +
                "Commands:\n\n" +
                "\t'--ex' Extract Actor\n\t\tUsage: --ex HashId ActorName Field Map \"path\\to\\content_files\" \"path\\to\\out_folder/file\"\n\n" +
                "\tIf no actor exist with that HashID, attempt to create actor. Issue: HKX2 Reads '.obj' not '.sbfres'");

            Console.ReadLine();
        }
    }
}
