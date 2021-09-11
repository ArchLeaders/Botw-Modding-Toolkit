# Botw.modules.HyruleBuilder.UnBuild

Reads a BotW mod directory structure and unbuilds any files it can.

_Requires [Hyrule-Builder](https://github.com/NiceneNerd/Hyrule-Builder) by Caleb Smith. Pip: `pip install hyrule_builder`_

---

```cs
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
/// <param name="folder">Target folder</param>
/// <param name="outFolder">Unbuild output folder</param>
/// <returns></returns>
public static async Task UnBuild(string folder, string outFolder = null)
{
    await Data.Process("hyrule_builder.exe", "unbuild \"" + folder + "\" \"" + outFolder + "\"");
}
```

## string folder

```
Full path to mod main folder (the folder containing 'content')
```

## string outFolder

```
Full path to unbuilt folder
```
