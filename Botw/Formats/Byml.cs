using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Botw.Formats
{
    class Byml
    {
        private static string activeDistance = "500.0";

        public static string BModelList(string modelName, string unitName)
        {
            return
                "!io\n" +
                "version: 0\n" +
                "type: xml\n" +
                "param_root: !list\n" +
                "  objects:\n" +
                "    ControllerInfo: !obj\n" +
                "      452881839: true\n" +
                "      MulColor: !color [1.0, 1.0, 1.0, 1.0]\n" +
                "      AddColor: !color [0.0, 0.0, 0.0, 0.0]\n" +
                "      2335622703: !str64 ''\n" +
                "      468054209: !str64 Fill\n" +
                "      3089169744: !str64 MapUnitShape\n" +
                "      2592531187: !str64 ''\n" +
                "      3331358099: !str64 ''\n" +
                "      3142504426: !str64 ''\n" +
                "      BaseScale: !vec3 [1.0, 1.0, 1.0]\n" +
                "      VariationMatAnim: !str64 ''\n" +
                "      VariationMatAnimFrame: 0\n" +
                "      VariationShaderAnim: !str64 ''\n" +
                "      VariationShaderAnimFrame: 0\n" +
                "      1528658372: !str32 Auto\n" +
                "      FarModelCullingCenter: !vec3 [0.0, 0.0, 0.0]\n" +
                "      FarModelCullingRadius: 0.0\n" +
                "      FarModelCullingHeight: 0.0\n" +
                "      CalcAABBASKey: !str64 Wait\n" +
                "    Attention: !obj\n" +
                "      IsEnableAttention: true\n" +
                "      LookAtBone: !str32 ''\n" +
                "      LookAtOffset: !vec3 [0.0, 0.0, 0.0]\n" +
                "      CursorOffsetY: 0.0\n" +
                "      AIInfoOffsetY: 0.0\n" +
                "      CutTargetBone: !str32 ''\n" +
                "      CutTargetOffset: !vec3 [0.0, 0.0, 0.0]\n" +
                "      GameCameraBone: !str32 ''\n" +
                "      GameCameraOffset: !vec3 [0.0, 0.0, 0.0]\n" +
                "      BowCameraBone: !str32 ''\n" +
                "      BowCameraOffset: !vec3 [0.0, 0.0, 0.0]\n" +
                "      AttackTargetBone: !str32 ''\n" +
                "      AttackTargetOffset: !vec3 [0.0, 0.0, 0.0]\n" +
                "      AttackTargetOffsetBack: 0.0\n" +
                "      AtObstacleChkOffsetBone: !str32 ''\n" +
                "      AtObstacleChkOffset: !vec3 [0.0, 0.0, 0.0]\n" +
                "      AtObstacleChkUseLookAtPos: true\n" +
                "      CursorAIInfoBaseBone: !str32 ''\n" +
                "      CursorAIInfoBaseOffset: !vec3 [0.0, 0.0, 0.0]\n" +
                "  lists:\n" +
                "    ModelData: !list\n" +
                "      objects: {}\n" +
                "      lists:\n" +
                "        ModelData_0: !list\n" +
                "          objects:\n" +
                "            Base: !obj\n" +
                "              Folder: !str64 " + modelName + "\n" +
                "          lists:\n" +
                "            Unit: !list\n" +
                "              objects:\n" +
                "                Unit_0: !obj\n" +
                "                  UnitName: !str64 " + unitName + "\n" +
                "                  BindBone: !str64 ''\n" +
                "              lists: {}\n" +
                "    AnmTarget: !list\n" +
                "      objects: {}\n" +
                "      lists:\n" +
                "        AnmTarget_0: !list\n" +
                "          objects:\n" +
                "            Base: !obj\n" +
                "              NumASSlot: 1\n" +
                "              IsParticalEnable: true\n" +
                "              TargetType: 0\n" +
                "          lists: {}\n";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ModelUser">Bxml ModelUser parameter</param>
        /// <param name="PhysicsUser">Bxml PhysicsUser parameter</param>
        /// <param name="GParamUser">Bxml GParamUser parameter</param>
        /// <param name="LifeConditionUser">Bxml LifeConditionUser parameter</param>
        /// <param name="tags">Bxml tags</param>
        /// <param name="param">Other bxml parameters. <para><c>Syntax: PropertyName = PropertyValue</c></para></param>
        /// <returns></returns>
        public static string Bxml(string ModelUser = "Dummy", string PhysicsUser = "Dummy", string GParamUser = "Dummy", string LifeConditionUser = null, string[] tags = null, string[] param = null)
        {
            #region Strings & string arrays holding parameter data.

            string ProfileUser = "Dummy";
            string ActorNameJpn = "俳優";
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

            foreach (string item in param)
            {
                Loop(item.Split('=')[0].Trim(), item.Split('=')[1].Trim());
            }

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

    }
}
