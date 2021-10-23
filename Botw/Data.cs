﻿using Newtonsoft.Json;
using System;
using System.IO;

namespace Botw
{
    public class Data
    {
        //
        // The Data class stores all of the paths used by BMM
        //

        /// <summary>
        /// <list type="bullet">
        /// <item><description><para><c>%localappdata%\Botw-MM</c></para></description></item>
        /// </list>
        /// </summary>
        public static readonly string root = $"{Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)}\\Botw-MM";

        /// <summary>
        /// <c>Botw.Data.WriteSettings</c> writes the BMM settings file to %localappdata%\Botw-MM\settings.json
        /// <para><see cref="WriteSettings(string, string, string, string, string, string)"/></para>
        /// <see href="https://github.com/ArchLeaders/Breath-of-the-Wild-Basic-Mod-Creator/edit/master/Docs/Botw/Data/WriteSettings.md">GitHub Documentation</see>
        /// <list type="bullet">
        /// <item><description><para>string cemu: Path to folder containing Cemu.exe</para></description></item>
        /// <item><description><para>string baseG: Path to base game files. Must end in \content</para></description></item>
        /// <item><description><para>string updateG: Path to update files. Must end in \content</para></description></item>
        /// <item><description><para>string dlcG: Path to dlc files. Must end in \content</para></description></item>
        /// <item><description><para>string baseG_NX: Path to base game files for switch. Must end in \check</para></description></item>
        /// <item><description><para>string dlcG_NX: Path to dlc files for switch. Must end in \check</para></description></item>
        /// </list>
        /// </summary>
        /// <param name="cemu">Path to folder containing Cemu.exe</param>
        /// <param name="baseG">Path to base game files. Must end in \content</param>
        /// <param name="updateG">Path to update files. Must end in \content</param>
        /// <param name="dlcG">Path to dlc files. Must end in \content</param>
        /// <param name="gameG_NX">Path to base game files for switch. Must end in \check</param>
        /// <param name="dlcG_NX">Path to dlc files for switch. Must end in \check</param>
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

    }
}