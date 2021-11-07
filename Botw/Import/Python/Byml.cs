using BotwLib.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotwLib.Import.Python
{
    public class Byml
    {
        public static async Task BymlToYml(string input, string output = null, string endian = "-b", bool yaz0 = false)
        {
            string _yaz0 = "";
            if (yaz0) _yaz0 = "s";

            if (output == null)
                output = $"!!.{_yaz0}{input.Split('.')[input.Split('.').Length - 1]}";
            else
            {
                string extension = $"{ _yaz0}{input.Split('.')[input.Split('.').Length - 1]}";
                string file = output.Replace($".{input.Split('.')[input.Split('.').Length - 1]}", "");
                output = $"{file}.{extension}";
            }


            await Proc.Async("byml_to_yml.exe", $"\"{input}\" \"{output}\" {endian}");
        }

        public static async Task YmlToByml(string input, string output = null)
        {
            if (output == null) output = $"!!.yml";

            await Proc.Async("byml_to_yml.exe", $"\"{input}\" \"{output}\"");
        }
    }
}
