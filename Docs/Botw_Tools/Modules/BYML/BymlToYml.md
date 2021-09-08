# Botw.Modules.BYML.BymlToYml

Converts a BYML file to YAML using [BYML](https://github.com/zeldamods/byml-v2).

_Requires [BYML](https://github.com/zeldamods/byml-v2) by Leoetlino. Pip: `pip install byml`_

---

```cs
/// <summary>
/// <c>Botw.Modules.BYML.BymlToYml</c> converts a BYML file to YAML.
/// <para>
/// <see cref="BymlToYml(string, string)"/>
/// </para>
/// <see href="https://github.com/ArchLeaders/Breath-of-the-Wild-Basic-Mod-Creator/blob/master/Docs/Botw_Tools/Modules/
        BYML/BymlToYml.md">GitHub Documentation</see>
/// <list type="bullet">
/// <item><description><para>file: BYML Input file full path.</para></description></item>
/// <item><description><para>outPath: Full path to out YAML file.</para></description></item>
/// </list>
/// </summary>
/// <param name="file"></param>
/// <param name="outPath"></param>
/// <returns>Task</returns>
public static async Task BymlToYml(string file, string outPath = "!!.yml")
{
    //Runs the python BYML package.
    await Data.Process("byml_to_yml.exe", "\"" + file + "\" " + outPath);
}
```

## string file

```
BYML Input file full path.
```

## string outPath

```
Full path to out YAML file.
```
