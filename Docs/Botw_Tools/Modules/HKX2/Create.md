# Botw_Tools.Modules.HKX2.Create

Creates a HKX2 file from a .obj and .mtl file.

---

```cs
/// <summary>
/// <c>HKX2.Create</c> creates a HKX2 type file from target obj file.
/// <para>
/// <see cref="Create(string, string, string)"/>
/// </para>
/// <see href="https://github.com">GitHub Documentation</see>
/// <list type="bullet">
/// <item><description><para>obj: Full path to .obj file</para></description></item>
/// <item><description><para>type: HKX2 Type. E.g. HKRB, HKSC, SHKSC, HKNM2, SHKNM2</para></description></item>
/// <item><description><para>outFile: Full path to output file</para></description></item>
/// </list>
/// </summary>
/// <param name="obj"></param>
/// <param name="type"> </param>
/// <param name="outFile"></param>
/// <returns></returns>
public static async Task Create(string obj, string type, string outFile = null)
{
    //Set out file when string outFile = null
    if (outFile is null)
    {
        outFile = Data.GetPath(obj) + Data.GetName(obj, true) + Data.GetExtension(obj).Replace(".obj", ".hkrb");
    }

    //Copy .obj file to working directory-
    await Task.Run(() => File.Copy(obj, Data.path + "\\.HKX2\\" + Data.GetName(obj)));

    //Get obj name from mtl file
    foreach (var item in File.ReadAllLines(obj))
    {
        //check if the line contains mtllib (this string is proceded by the obj name)
        //EDIT: Name should be aquired from .obj name. --CHECK--
        if (item.StartsWith("mtllib"))
        {
            //Filter name from line
            mtlFile = Data.GetPath(obj) + item.Replace("mtllib ", "");
            break;
        }
    }

    //Copy .mtl file to working directory.
    await Task.Run(() => File.Copy(mtlFile, Data.path + "\\.HKX2\\" + Data.GetName(mtlFile)));

    //Run CCaNM process to create HKX2 file
    await Data.Process(Data.path + "\\.HKX2\\CCaNM.exe", "\"" + Data.path + "\\.HKX2\\" + Data.GetName(obj) +
        "\" " + type, false, false, Data.path + "\\.HKX2");

    //Call ReturnFile
    await ReturnFile(obj, mtlFile);

    async Task ReturnFile(string obj, string mtlFile)
    {
        //Return new files and delete old
        await Task.Run(() => File.Move(Data.path + "\\.HKX2\\" + Data.GetName(obj) + ".hkrb",
            outFile));
        await Task.Run(() => File.Delete(Data.path + "\\.HKX2\\" + Data.GetName(obj)));
        await Task.Run(() => File.Delete(Data.path + "\\.HKX2\\" + Data.GetName(mtlFile)));
    }
}
```

## string obj

```
Full path to target .obj with coresponding .mtl in the smae directory.
```

## string type
```
Any HKX2 format.

HKRB, HKSC, SHKSC, HKNM2, SHKNM2
```

## string outFile

```
Full path to the new HKX2 file.
```