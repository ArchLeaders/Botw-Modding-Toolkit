# Botw.Formats.Byml.Bxml

Returns a Botw BXML file. Write with <c>Botw.Formats.Writer.Byml.Bxml<c/>.

---

```cs
/// <summary>
/// <c>Botw.Formats.Byml.Bxml</c> Returns a Botw BXML file. Write with <c>Botw.Formats.Writer.Byml.Bxml</c>.
/// <para><see cref="Bxml(string, string, string, string, string[], string[])"/></para>
/// <see href="https://github.com/ArchLeaders/Breath-of-the-Wild-Basic-Mod-Creator/blob/master/Docs/Botw/Formats/Byml/Bxml.md">GitHub Documentation</see>
/// <list type="bullet">
/// <item><description><para>Bxml ModelUser parameter</para></description></item>
/// <item><description><para>Bxml PhysicsUser parameter</para></description></item>
/// <item><description><para>Bxml GParamUser parameter</para></description></item>
/// <item><description><para>Bxml LifeConditionUser parameter</para></description></item>
/// <item><description><para>Bxml tags</para></description></item>
/// <item><description><para>Other bxml parameters. <para>Syntax: <c>PropertyName = PropertyValue</c></para></para></description></item>
/// </list>
/// </summary>
/// <param name="ModelUser">Bxml ModelUser parameter</param>
/// <param name="PhysicsUser">Bxml PhysicsUser parameter</param>
/// <param name="GParamUser">Bxml GParamUser parameter</param>
/// <param name="LifeConditionUser">Bxml LifeConditionUser parameter</param>
/// <param name="tags">Bxml tags</param>
/// <param name="param">Other bxml parameters. <para>Syntax: <c>PropertyName = PropertyValue</c></para></param>
public static string Bxml(string ModelUser = "Dummy", string PhysicsUser = "Dummy", string GParamUser = "Dummy", string LifeConditionUser = null, string[] tags = null, string[] param = null)
{
    #region Strings & string arrays holding parameter data.

            string ProfileUser = "Dummy";
            string ActorNameJpn = "ダミー";
            string Priority = "Default";
            string AIProgramUser = "Dummy";
            string AIScheduleUser = "Dummy";
            string ASUser = "Dummy";
            string AttentionUser = "Dummy";
            string AwarenessUser = "Dummy";
            string BoneControlUser = "Dummy";
            string ActorCaptureUser = "Dummy";
            string ChemicalUser = "Dummy";
            string DamageParamUser = "Dummy";
            string DropTableUser = "Dummy";
            string ElinkUser = "Dummy";
            string LODUser = "Dummy";
            string RgBlendWeightUser = "Dummy";
            string RgConfigListUser = "Dummy";
            string RecipeUser = "Dummy";
            string ShopDataUser = "Dummy";
            string SlinkUser = "Dummy";
            string UMiiUser = "Dummy";
            string XlinkUser = "Dummy";
            string AnimationInfo = "Dummy";
            string ActorScale = "1.0";

            #endregion

    if (param != null)
        foreach (string item in param)
            Loop(item.Split('=')[0].Trim(), item.Split('=')[1].Trim());

    // Format LifeConditionUser
    if (LifeConditionUser != null)
        activeDistance = LifeConditionUser;

    LifeConditionUser = "Landmark" + LifeCondition()[1] + "m";

    // Format tags
    string tagsStr = null;

    if (tags != null)
    {
        tagsStr = "    Tags: !obj\n";
        for (int i = 0; i < tags.Length; i++)
        {
            tagsStr = tagsStr + "      Tag" + i + ": " + tags[i] + "\n";
        }
    }

    // Return BXML
    return
        "!io\n" +
        "version: 0\n" +
        "type: xml\n" +
        "param_root: !list\n" +
        "  objects:\n" +
        "   LinkTarget: !obj\n" +
        $"      ActorNameJpn: {ActorNameJpn}\n" +
        $"      Priority: {Priority}\n" +
        $"      AIProgramUser: {AIProgramUser}\n" +
        $"      AIScheduleUser: {AIScheduleUser}\n" +
        $"      ASUser: {ASUser}\n" +
        $"      AttentionUser: {AttentionUser}\n" +
        $"      AwarenessUser: {AwarenessUser}\n" +
        $"      BoneControlUser: {BoneControlUser}\n" +
        $"      ActorCaptureUser: {ActorCaptureUser}\n" +
        $"      ChemicalUser: {ChemicalUser}\n" +
        $"      DamageParamUser: {DamageParamUser}\n" +
        $"      DropTableUser: {DropTableUser}\n" +
        $"      ElinkUser: {ElinkUser}\n" +
        $"      GParamUser: {GParamUser}\n" +
        $"      LifeConditionUser: {LifeConditionUser}\n" +
        $"      LODUser: {LODUser}\n" +
        $"      ModelUser: {ModelUser}\n" +
        $"      PhysicsUser: {PhysicsUser}\n" +
        $"      ProfileUser: {ProfileUser}\n" +
        $"      RgBlendWeightUser: {RgBlendWeightUser}\n" +
        $"      RgConfigListUser: {RgConfigListUser}\n" +
        $"      RecipeUser: {RecipeUser}\n" +
        $"      ShopDataUser: {ShopDataUser}\n" +
        $"      SlinkUser: {SlinkUser}\n" +
        $"      UMiiUser: {UMiiUser}\n" +
        $"      XlinkUser: {XlinkUser}\n" +
        $"      AnimationInfo: {AnimationInfo}\n" +
        $"      ActorScale: {ActorScale}\n" +
            tagsStr +
        "   1115720914: !obj\n" +
        "      Tag0: DrcMapTerrain\n" +
        "  lists: {}\n";

    // Format LifeCondition method.
    static string[] LifeCondition()
            {
                string distance = activeDistance;
                return new string[] {
                    "!io\n" +
                    "version: 0\n" +
                    "type: xml\n" +
                    "param_root: !list\n" +
                    "  objects:\n" +
                    "    DisplayDistance: !obj\n" +
                    "      Item: " + distance + "\n" +
                    "    AutoDisplayDistanceAlgorithm: !obj\n" +
                    "      Item: Bounding.Y\n" +
                    "    YLimitAlgorithm: !obj\n" +
                    "      Item: NoLimit\n" +
                    "  lists: {}\n",
                    distance.Replace(".0", "")};
            }

    // Assign each string value.
    void Loop(string prm, string value)
    {
        if (prm == "ActorNameJpn")
            ActorNameJpn = value;
        if (prm == "Priority")
            Priority = value;
        if (prm == "AIProgramUser")
            AIProgramUser = value;
        if (prm == "AIScheduleUser")
            AIScheduleUser = value;
        if (prm == "ASUser")
            ASUser = value;
        if (prm == "AttentionUser")
            AttentionUser = value;
        if (prm == "AwarenessUser")
            AwarenessUser = value;
        if (prm == "BoneControlUser")
            BoneControlUser = value;
        if (prm == "ActorCaptureUser")
            ActorCaptureUser = value;
        if (prm == "ChemicalUser")
            ChemicalUser = value;
        if (prm == "DamageParamUser")
            DamageParamUser = value;
        if (prm == "DropTableUser")
            DropTableUser = value;
        if (prm == "ElinkUser")
            ElinkUser = value;
        if (prm == "LODUser")
            LODUser = value;
        if (prm == "ProfileUser")
            ProfileUser = value;
        if (prm == "RgBlendWeightUser")
            RgBlendWeightUser = value;
        if (prm == "RgConfigListUser")
            RgConfigListUser = value;
        if (prm == "RecipeUser")
            RecipeUser = value;
        if (prm == "ShopDataUser")
            ShopDataUser = value;
        if (prm == "SlinkUser")
            SlinkUser = value;
        if (prm == "UMiiUser")
            UMiiUser = value;
        if (prm == "XlinkUser")
            XlinkUser = value;
        if (prm == "AnimationInfo")
            AnimationInfo = value;
        if (prm == "ActorScale")
            ActorScale = value;
    }
}
```

## Bxml(string ModelUser

```
Bxml ModelUser parameter
```

##  string PhysicsUser

```
Bxml PhysicsUser parameter
```

##  string GParamUser

```
Bxml GParamUser parameter
```

##  string LifeConditionUser

```
Bxml LifeConditionUser parameter
```

##  string[] tags

```
Bxml tags
```

##  string[] param

```
Other bxml parameters.
Syntax:	PropertyName = PropertyValue
```

