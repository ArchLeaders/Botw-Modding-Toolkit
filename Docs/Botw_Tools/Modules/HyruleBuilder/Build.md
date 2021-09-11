# Botw.modules.HyruleBuilder.Build

Reads a Hyrule-Builder unbuilt directory structure and tries to build it into BotW files.

_Requires [Hyrule-Builder](https://github.com/NiceneNerd/Hyrule-Builder) by Caleb Smith. Pip: `pip install hyrule_builder`_

---

```cs
/// <summary>
/// <c>Botw.Modules.HyruleBuilder.UnBuild</c> Reads a Hyrule-Builder unbuilt directory structure and tries to build it into BotW files.
/// <para>
/// <see cref="Build(string, string, string)"/>
/// </para>
/// <see href="https://github.com/ArchLeaders/Breath-of-the-Wild-Basic-Mod-Creator/blob/master/Docs/Botw_Tools/Modules/HyruleBuilder/Build.md">GitHub Documentation</see>
/// <list type="bullet">
/// <item><description><para><paramref name="folder"/>: Full path to target dirctory</para></description></item>
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
```

## string folder

```
Full path to Hyrule-Builder unbuilt master folder
```

## string outFolder

```
Full path to build output folder
```

## string endian

```
 Endian type.
 
      -b (big) null (little)
```
