# Botw.Modules.BYML.YmlToByml

Encodes a YAML file to the BYML format defined by the `extension` parameter.

_Requires [BYML](https://github.com/zeldamods/byml-v2) by Léo Lam. Pip: `pip install byml`_

---

```cs
/// <summary>
/// <c>Botw.Modules.BYML.BymlToYml</c> Encodes a YAML file to the BYML format defined with <paramref name="extension"/>.
/// <para>
/// <see cref="YmlToByml(string, string, string, string)"/>
/// </para>
/// <see href="https://github.com/ArchLeaders/Breath-of-the-Wild-Basic-Mod-Creator/blob/master/Docs/Botw_Tools/Modules/BYML
      /BymlToYml.md">GitHub Documentation</see>

/// <list type="bullet">
/// <item><description><para>file: BYML Input file full path.</para></description></item>
/// <item><description><para>extension: BYML Extension</para></description></item>
/// <item><description><para>endian: Endian type. <code>-b (big) null (little)</code></para></description></item>
/// <item><description><para>outpath: Full path to out YAML file.</para></description></item>
/// </list>
/// </summary>
/// <param name="file"></param>
/// <param name="extension"></param>
/// <param name="endian"></param>
/// <param name="outPath"></param>
/// <returns>Task</returns>
public static async Task YmlToByml(string file, string extension, string endian = null, string outPath = null)
{
    //Assigns outPath to satisfy BYML's arguments
    outPath = " !!" + extension;

    //Runs the python BYML package.
    await Data.Process("yml_to_byml.exe", endian + " \"" + file + "\"" + outPath);
}
```

## string file

```
BYML Input file full path
```

## string extension

```
BYML Extension

  .baischedule
  .baniminfo
  .bgdata
  .bgsvdata
  .bquestpack
  .byml
  .mubin
  
Procede any extension with 's' to use Yaz0 compression.
```

## string endian

```
Endian type.
  
  -b (big) null (little)
```

## string outPath

```
Full path to out YAML file.
```
