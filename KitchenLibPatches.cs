using HarmonyLib;
using KitchenData;
using KitchenLib;
using KitchenLib.Colorblind;
using KitchenLib.Customs;
using KitchenLib.Utils;
using KitchenMods;
using UnityEngine;

// This file contains some patches to fix bugs in KitchenLib until it is updated.
namespace KitchenDrinksMod.Patches
{
    [HarmonyPatch(typeof(BaseMod), "PostActivate")]
    class JsonMaterialLoadingPatch
    {
        static bool Prefix(KitchenMods.Mod mod, ref BaseMod __instance)
        {
            foreach (AssetBundleModPack pack in mod.GetPacks<AssetBundleModPack>())
            {
                foreach (AssetBundle bundle in pack.AssetBundles)
                {
                    JSONManager.LoadAllJsons(bundle);
                }
            }

            foreach (CustomBaseMaterial material in JSONManager.LoadedJsons)
            {
                material.ConvertMaterial(out Material mat);
                __instance.AddMaterial(mat);
            }
            var mOnPostActivate = ReflectionUtils.GetMethod<BaseMod>("OnPostActivate");
            mOnPostActivate.Invoke(__instance, new object[] { mod });

            return false;
        }
    }

    [HarmonyPatch(typeof(GDOUtils), "SetupGDOIndex")]
    class ColorblindInitPatch
    {
        static void Prefix(GameData gameData)
        {
            ColorblindUtils.Init(gameData);
        }
    }
}
