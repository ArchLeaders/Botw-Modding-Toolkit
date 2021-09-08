# Botw.Modules.BYML.BymlToYml

Converts a BYML file to YAML.

---

```cs
/// <summary>
/// <c>Botw.Modules.BYML.BymlToYml</c> converts a BYML file to YAML.
/// <para>
/// <see cref="BymlToYml(string, string)"/>
/// </para>
/// <see href=""/>
/// </summary>
/// <param name="file"></param>
/// <param name="outPath"></param>
/// <returns></returns>
public static async Task BymlToYml(string file, string outPath = "!!.yml")
{
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
