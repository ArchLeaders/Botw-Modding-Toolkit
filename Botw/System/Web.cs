using Botw.Formats.Json;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Botw.System
{
    public class Web
    {
        public static async Task DownloadAsync(string link, string outFile)
        {
            using (WebClient client = new())
                await Task.Run(() => client.DownloadFile(link, outFile));
        }

        public static void DownloadSync(string link, string outFile)
        {
            using (WebClient client = new())
                client.DownloadFile(link, outFile);
        }

        public static async Task DownloadGitLatestAsync(string apiLink, string outFile)
        {
            HttpClient client = new();

            client.DefaultRequestHeaders.Add("user-agent", "test");
            var json = await client.GetStringAsync(apiLink);
            var gitinfo = JsonConvert.DeserializeObject<GitHub>(File.ReadAllText(json));
            var link = gitinfo.assets[0].browser_download_url;

            await DownloadAsync(link, outFile);
        }
    }
}
