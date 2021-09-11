using System.Threading.Tasks;

namespace Botw
{
    public class HyruleBuilder
    {
        /// <summary>
        /// <c>Botw.Modules.hyruleBuilder.UnpackSarc</c> Hyrule Builders default SARC unpacking process.
        /// <para>
        /// <see cref="UnpackSarc(string)"/>
        /// </para>
        /// <see href="https://github.com/ArchLeaders/Breath-of-the-Wild-Basic-Mod-Creator/blob/master/Docs/Botw_Tools/Modules/HyruleBuilder/UnpackSarc.md">GitHub Documentation</see>
        /// <list type="bullet">
        /// <item><description><para><paramref name="file"/>: Full path to SARC file</para></description></item>
        /// </list>
        /// </summary>
        /// <returns>Task</returns>
        public static async Task UnpackSarc(string file)
        {
            await Data.Process("unbuild_sarc.exe", "\"" + file + "\"");
        }

        /// <summary>
        /// <c>Botw.Modules.hyruleBuilder.PackSarc</c> Reads a folder directory and encodes it into a single SARC file.
        /// <para>
        /// <see cref="PackSarc(string, string, string)"/>
        /// </para>
        /// <see href="https://github.com/ArchLeaders/Breath-of-the-Wild-Basic-Mod-Creator/blob/master/Docs/Botw_Tools/Modules/HyruleBuilder/PackSarc.md">GitHub Documentation</see>
        /// <list type="bullet">
        /// <item><description><para><paramref name="folder"/>: Full path to target dirctory</para></description></item>
        /// <item><description><para><paramref name="outFile"/>: Full path to SARC output file</para></description></item>
        /// <item><description><para><paramref name="endian"/>: Endian type. <code>-b (big) null (little)</code></para></description></item>
        /// </list>
        /// </summary>
        /// <param name="endian">Endian type. <code>-b (big) null (little)</code></param>
        /// <param name="folder">Full path to target dirctory</param>
        /// <param name="outFile">Full path to SARC output file</param>
        /// <returns>Task</returns>
        public static async Task PackSarc(string folder, string outFile, string endian = null)
        {
            await Data.Process("build_sarc.exe", "\"" + folder + "\"" + endian + "\"" + outFile + "\"");
        }

        /// <summary>
        /// <c>Botw.Modules.HyruleBuilder.UnBuild</c> Reads a Hyrule-Builder unbuilt directory structure and tries to build it into BotW files.
        /// <para>
        /// <see cref="Build(string, string, string)"/>
        /// </para>
        /// <see href="https://github.com/ArchLeaders/Breath-of-the-Wild-Basic-Mod-Creator/blob/master/Docs/Botw_Tools/Modules/HyruleBuilder/Build.md">GitHub Documentation</see>
        /// <list type="bullet">
        /// <item><description><para><paramref name="folder"/>: Full path to Hyrule-Builder unbuilt master folder</para></description></item>
        /// <item><description><para><paramref name="outFolder"/>: Full path to build output folder</para></description></item>
        /// <item><description><para><paramref name="endian"/>: Endian type. <code>-b (big) null (little)</code></para></description></item>
        /// </list>
        /// </summary>
        /// <param name="endian">Endian type. <code>-b (big) null (little)</code></param>
        /// <param name="folder">Target folder</param>
        /// <param name="outFolder">Build output folder</param>
        /// <returns>Task</returns>
        public static async Task Build(string folder, string outFolder = null, string endian = null)
        {
            await Data.Process("hyrule_builder.exe", "build " + endian + " \"" + folder + "\" \"" + outFolder + "\"");
        }

        /// <summary>
        /// <c>Botw.Modules.HyruleBuilder.UnBuild</c> Reads a BotW mod directory structure and unbuilds any files it can.
        /// <para>
        /// <see cref="UnBuild(string, string, string)"/>
        /// </para>
        /// <see href="https://github.com/ArchLeaders/Breath-of-the-Wild-Basic-Mod-Creator/blob/master/Docs/Botw_Tools/Modules/HyruleBuilder/UnBuild.md">GitHub Documentation</see>
        /// <list type="bullet">
        /// <item><description><para><paramref name="folder"/>: Full path to target dirctory</para></description></item>
        /// <item><description><para><paramref name="outFolder"/>: Full path to build output folder</para></description></item>
        /// <item><description><para><paramref name="endian"/>: Endian type. <code>-b (big) null (little)</code></para></description></item>
        /// </list>
        /// </summary>
        /// <param name="endian">Endian type. <code>-b (big) null (little)</code></param>
        /// <param name="folder">Target folder</param>
        /// <param name="outFolder">Unbuild output folder</param>
        /// <returns>Task</returns>
        public static async Task UnBuild(string folder, string outFolder = null)
        {
            await Data.Process("hyrule_builder.exe", "unbuild \"" + folder + "\" \"" + outFolder + "\"");
        }
    }
}
