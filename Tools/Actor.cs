using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Botw_Tools
{
    public class Actor
    {
        public static async Task Extract()
        {

        }
        public static async Task Create(string obj, string name = null, string outloc = null, string edition = null)
        {
            if (name is null) { name = Data.GetName(obj, true); }
            if (outloc is null) { outloc = Data.GetPath(obj); }
            if (edition is null) { edition = Data.edition; }

            Console.WriteLine(Crc32.FromString(name));

            await Task.Run(() => Directory.CreateDirectory(Data.tempPath + "\\" + name + "\\Actor\\ActorLink"));
            await Task.Run(() => Directory.CreateDirectory(Data.tempPath + "\\" + name + "\\Actor\\Physics"));
            await Task.Run(() => Directory.CreateDirectory(Data.tempPath + "\\" + name + "\\Actor\\ModelList"));
            await Task.Run(() => Directory.CreateDirectory(Data.tempPath + "\\" + name + "\\Actor\\LifeCondition"));
            await Task.Run(() => Directory.CreateDirectory(Data.tempPath + "\\" + name + 
                "\\Physics\\RigidBody\\" + name));
            string pathToPhysics = name + "\\" + name + ".hkrb";

            await HKX2.Create(obj, "hkrb", Data.tempPath + "\\" + name + "\\Physics\\RigidBody\\" + name + "\\" + name + ".hkrb");

            string path = Data.tempPath + "\\" + name;

            await Task.Run(() => File.WriteAllText(path + "\\Actor\\Physics\\" + name + ".bphysics.yml", Physics(pathToPhysics)));
            await Task.Run(() => File.WriteAllText(path + "\\Actor\\ActorLink\\" + name + ".bxml.yml", BXML(name, name, "MapDynamicActive")));
            await Task.Run(() => File.WriteAllText(path + "\\Actor\\ModelList\\" + name + ".bmodellist.yml", ModelList(name, name)));
            await Task.Run(() => File.WriteAllText(path + "\\Actor\\LifeCondition\\Landmark" + LifeCondition()[1] +
                "m.blifecondition.yml", LifeCondition()[0]));
        }
        #region Absurdly long methods
        static string activeDistance = "500.0"; 
        public static string BXML(string ModelUser = "Dummy", string PhysicsUser = "Dummy", string ProfileUser = "Dummy", string ActorNameJpn = "俳優", string Priority = "Default", string AIProgramUser = "Dummy", string AIScheduleUser = "Dummy", string ASUser = "Dummy", string AttentionUser = "Dummy", string AwarenessUser = "Dummy", string BoneControlUser = "Dummy", string ActorCaptureUser = "Dummy", string ChemicalUser = "Dummy", string DamageParamUser = "Dummy", string DropTableUser = "Dummy", string ElinkUser = "Dummy", string GParamUser = "Dummy", string LifeConditionUser = null, string LODUser = "Dummy", string RgBlendWeightUser = "Dummy", string RgConfigListUser = "Dummy", string RecipeUser = "Dummy", string ShopDataUser = "Dummy", string SlinkUser = "Dummy", string UMiiUser = "Dummy", string XlinkUser = "Dummy", string AnimationInfo = "Dummy", string ActorScale = "1.0", string[] tags = null)
        {
            LifeConditionUser = "Landmark" + LifeCondition()[1] + "m";
            string tagsStr = null;
            if (tags != null)
            {
                tagsStr = "    Tags: !obj\n";
                for (int i = 0; i < tags.Length; i++)
                {
                    tagsStr = tagsStr + "      Tag" + i + ": " + tags[i] + "\n";
                }
            }
            return
                "!io\n" +
                "version: 0\n" +
                "type: xml\n" +
                "param_root: !list\n" +
                "  objects:\n" +
                "   LinkTarget: !obj\n" +
                "      ActorNameJpn: " + ActorNameJpn + "\n" +
                "      Priority: " + Priority + "\n" +
                "      AIProgramUser: " + AIProgramUser + "\n" +
                "      AIScheduleUser: " + AIScheduleUser + "\n" +
                "      ASUser: " + ASUser + "\n" +
                "      AttentionUser: " + AttentionUser + "\n" +
                "      AwarenessUser: " + AwarenessUser + "\n" +
                "      BoneControlUser: " + BoneControlUser + "\n" +
                "      ActorCaptureUser: " + ActorCaptureUser + "\n" +
                "      ChemicalUser: " + ChemicalUser + "\n" +
                "      DamageParamUser: " + DamageParamUser + "\n" +
                "      DropTableUser: " + DropTableUser + "\n" +
                "      ElinkUser: " + ElinkUser + "\n" +
                "      GParamUser: " + GParamUser + "\n" +
                "      LifeConditionUser: " + LifeConditionUser + "\n" +
                "      LODUser: " + LODUser + "\n" +
                "      ModelUser: " + ModelUser + "\n" +
                "      PhysicsUser: " + PhysicsUser + "\n" +
                "      ProfileUser: " + ProfileUser + "\n" +
                "      RgBlendWeightUser: " + RgBlendWeightUser + "\n" +
                "      RgConfigListUser: " + RgConfigListUser + "\n" +
                "      RecipeUser: " + RecipeUser + "\n" +
                "      ShopDataUser: " + ShopDataUser + "\n" +
                "      SlinkUser: " + SlinkUser + "\n" +
                "      UMiiUser: " + UMiiUser + "\n" +
                "      XlinkUser: " + XlinkUser + "\n" +
                "      AnimationInfo: " + AnimationInfo + "\n" +
                "      ActorScale: " + ActorScale + "\n" +
                    tagsStr +
                "   1115720914: !obj\n" +
                "      Tag0: DrcMapTerrain\n" +
                "  lists: {}\n";
        }

        public static string ModelList(string modelName, string unitName)
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

        public static string Physics(string pathToHKRB)
        {
            return "!io\nversion: 0\ntype: xml\nparam_root: !list\n  objects: {}\n  lists:\n    ParamSet: !list\n      objects:\n        1258832850: !obj\n          use_rigid_body_set_num: 1\n          use_ragdoll: false\n          use_cloth: false\n          use_support_bone: false\n          use_character_controller: false\n          use_contact_info: false\n          use_edge_rigid_body_num: 0\n          use_system_group_handler: true\n      lists:\n        RigidBodySet: !list\n          objects: {}\n          lists:\n            RigidBodySet_0: !list\n              objects:\n                4288596824: !obj\n                  set_name: !str32 Body\n                  type: !str32 from_resource\n                  num: 1" +
                "\n                  setup_file_path: !str256 " + pathToHKRB + "\n              lists:\n                RigidBody_0: !list\n                  objects:\n                    948250248: !obj\n                      rigid_body_name: !str64 Collision_IDK\n                      mass: 1.0\n                      inertia: !vec3 [0.741725981, 1.241768, 0.592725992]\n                      linear_damping: 0.0\n                      angular_damping: 0.0500000007\n                      max_impulse: -1.0\n                      col_impulse_scale: 1.0\n                      ignore_normal_for_impulse: false\n                      volume: 0.171292007\n                      toi: false\n                      center_of_mass: !vec3 [0.0, -0.0877700001, -0.131483003]\n                      bounding_center: !vec3 [0.0, 0.319700986, -0.0109369997]\n                      bounding_extents: !vec3 [2.936589, 1.159778, 2.91385007]\n                      max_linear_velocity: 200.0\n                      bone: !str64 ''\n                      water_buoyancy_scale: 1.0\n                      water_flow_effective_rate: 1.0\n                      link_matrix: ''\n                      layer: !str32 EntityGround\n                      no_hit_ground: false\n                      no_hit_water: false\n                      navmesh: !str32 NOT_USE\n                      navmesh_sub_material: !str32 ''\n                      magne_mass_scaling_factor: 1.0\n                      always_character_mass_scaling: false\n                      max_angular_velocity_rad: 198.967529\n                      motion_type: !str32 Keyframed\n                  lists: {}\n";
        }

        public static string[] LifeCondition()
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
                distance.Replace(".0", "") 
            };
        }
        #endregion
    }
}
