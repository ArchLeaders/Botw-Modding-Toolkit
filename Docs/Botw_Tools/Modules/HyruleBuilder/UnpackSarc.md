# Botw.Modules.hyruleBuilder.UnpackSarc

Hyrule Builders default SARC unpacking process.

_Requires Hyrule-Builder by Caleb Smith. Pip: `pip install hyrule_builder`_

```cs
/// <summary>
/// <c>Botw.Modules.hyruleBuilder.UnpackSarc</c> Hyrule Builders default unpacking process.
/// <para>
/// <see cref="UnpackSarc(string)"/>
/// </para>
/// <see href="https://github.com/ArchLeaders/Breath-of-the-Wild-Basic-Mod-Creator/blob/master/Docs/Botw_Tools/Modules/
        HyruleBuilder/UnpackSarc.md">GitHub Documentation</see>

/// <list type="bullet">
/// <item><description><para><paramref name="file"/>: Full path to SARC file</para></description></item>
/// </list>
/// </summary>
/// <param name="file"></param>
/// <returns>Task</returns>
public static async Task UnpackSarc(string file)
{
  await Data.Process("unbuild_sarc.exe", "\"" + file + "\"");
}

/// <summary>
/// 
/// </summary>
/// <param name="folder"></param>
/// <param name="outFile"></param>
/// <param name="endian"></param>
/// <returns></returns>
public static async Task PackSarc(string folder, string outFile, string endian = null)
{
  await Data.Process("build_sarc.exe", "\"" + folder + "\"" + endian + "\"" + outFile + "\"");
}
```

## string file

```
Full path to SARC file
```
