using BotwLib.Formats.Json;
using System.Text.Json;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace BotwLib.System
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

        public static async Task DownloadGitLatest(string apiLink, string outFile, int asset = 1)
        {
            HttpClient client = new();

            client.DefaultRequestHeaders.Add("user-agent", "test");
            var json = await client.GetStringAsync(apiLink);

            GitHub gitinfo = JsonSerializer.Deserialize<GitHub>(json); // Fails during this for no apparent reason.
            var link = gitinfo.assets[asset - 1].browser_download_url;

            await DownloadAsync(link, outFile);
        }
    }
}
