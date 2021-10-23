using Newtonsoft.Json;
using System;
using System.IO;

namespace Botw
{
    public class Data
    {
        // The Data class stores all of the paths used by BMM

        public static void WriteSettings(string cemu, string baseG, string updateG, string dlcG = null, string gameG_NX = null, string dlcG_NX = null)
        {
            Json.Settings settings = new();
            JsonSerializer serializer = new();

            settings.cemu_dir = cemu;
            settings.game_dir = baseG;
            settings.update_dir = updateG;
            settings.dlc_dir = dlcG;
            settings.game_dir_nx = gameG_NX;
            settings.dlc_dir_nx = dlcG_NX;

            using (StreamWriter streamWriter = new StreamWriter($"{root}\\settings.json"))
                using (JsonWriter jsonWriter = new JsonTextWriter(streamWriter))
                    serializer.Serialize(jsonWriter, settings);
        }

        public static string root = $"{Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)}\\Botw-MM";
    }
}
