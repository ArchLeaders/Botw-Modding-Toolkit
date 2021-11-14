using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotwLib.Formats.Writer
{
    public class Json
    {
        public static async Task BcmlSettings(string cemuDir, string baseGame, string update, string storeDir, string dlc, string exportDir, string lang)
        {
            await Task.Run(() => File.WriteAllText($"{Data.temp}\\Settings.json",
                                "{\n" +
                                $"  \"cemu_dir\": \"{cemuDir.Replace("\\", "\\\\")}\",\n" +
                                $"  \"game_dir\": \"{baseGame.Replace("\\", "\\\\")}\",\n" +
                                "  \"game_dir_nx\": \"\",\n" +
                                $"  \"update_dir\": \"{update.Replace("\\", "\\\\")}\",\n" +
                                $"  \"dlc_dir\": \"{dlc.Replace("\\", "\\\\")}\",\n" +
                                "  \"dlc_dir_nx\": \"\",\n" +
                                $"  \"store_dir\": \"{storeDir.Replace("\\", "\\\\")}\",\n" +
                                $"  \"export_dir\": \"{exportDir.Replace("\\", "\\\\")}\",\n" +
                                "  \"export_dir_nx\": \"\",\n" +
                                "  \"load_reverse\": false,\n" +
                                "  \"site_meta\": \"\",\n" +
                                "  \"no_guess\": false,\n" +
                                $"  \"lang\": \"{lang}\",\n" +
                                "  \"no_cemu\": false,\n" +
                                "  \"wiiu\": true,\n" +
                                "  \"no_hardlinks\": false,\n" +
                                "  \"force_7z\": false,\n" +
                                "  \"suppress_update\": true,\n" +
                                "  \"loaded\": true,\n" +
                                "  \"nsfw\": false,\n" +
                                "  \"changelog\": true,\n" +
                                "  \"strip_gfx\": false,\n" +
                                "  \"auto_gb\": true,\n" +
                                "  \"dark_theme\": false,\n" +
                                "  \"last_version\": \"3.6.1\",\n" +
                                "  \"show_gb\": true\n" +
                                "}"));
        }
    }
}
