# Botw.Modules.HyruleBuilder.PackSarc

Runs build_sarc.exe from [Hyrule-Builder](https://github.com/NiceneNerd/Hyrule-Builder) which reads a folder directory and encodes it into a single SARC file.

_Requires [Hyrule-Builder](https://github.com/NiceneNerd/Hyrule-Builder) by Caleb Smith. Pip: `pip install hyrule_builder`_ 

---

```cs
/// <summary>
/// <c>Botw.Modules.hyruleBuilder.PackSarc</c> Reads a folder directory and encodes it into a single SARC file.
/// <para>
/// <see cref="PackSarc(string, string, string)"/>
/// </para>
/// <see href="https://github.com/ArchLeaders/Breath-of-the-Wild-Basic-Mod-Creator/blob/master/Docs/Botw_Tools/Modules/
        HyruleBuilder/PackSarc.md">GitHub Documentation</see>

/// <list type="bullet">
/// <item><description><para><paramref name="folder"/>: Full path to target dirctory</para></description></item>
/// <item><description><para><paramref name="outFile"/>: Full path to SARC output file</para></description></item>
/// <item>
/// <description><para><paramref name="endian"/>: Endian type. <code>-b (big) null (little)</code></para></description>
/// </item>
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
```

## string folder

```
Full path to target dirctory
```

## string outFile

```
Full path to SARC output file
```

## string endian

```
Endian type. 

    -b (big) null (little)
```
