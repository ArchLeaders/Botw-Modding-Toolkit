using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;

namespace Resource
{
    public class Extract
    {
        public static async Task Embed(string fileName, string output)
        {
            await Task.Run(() => {
                Assembly assembly = Assembly.GetCallingAssembly();

                using (Stream stream = assembly.GetManifestResourceStream("Resource.Resource." + fileName))
                using (BinaryReader binaryReader = new(stream))
                using (FileStream fileStream = new(output, FileMode.OpenOrCreate))
                using (BinaryWriter binaryWriter = new(fileStream))
                    binaryWriter.Write(binaryReader.ReadBytes((int)stream.Length));

            });
        }
    }
}
