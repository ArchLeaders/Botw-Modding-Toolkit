using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotwLib.System.StringManipulation
{
    class FilePaths
    {
        public static string RemoveFolders(string path, int removeCount)
        {
            string rt = null;
            string[] a1 = path.Split('\\');

            int i = 0;

            foreach (var item in a1)
            {
                i++;
                if (i >= removeCount)
                    rt = $"{rt}\\{item}";
            }

            return rt;
        }
    }
}
