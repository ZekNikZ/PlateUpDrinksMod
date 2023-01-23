using KitchenData;
using KitchenLib.References;
using KitchenLib.Utils;
using System.Collections.Generic;
using UnityEngine;

namespace KitchenDrinksMod
{
    internal class Prefabs
    {
        private static readonly Dictionary<string, GameObject> PrefabCache = new();

        public static GameObject MilkshakeVanilla => FindModPrefab("Milkshake Vanilla");
        public static GameObject MilkshakeChocolate => FindModPrefab("Milkshake Chocolate");
        public static GameObject MilkshakeStrawberry => FindModPrefab("Milkshake Strawberry");
        public static GameObject MilkshakeVanillaRaw => FindModPrefab("Milkshake Vanilla Raw");
        public static GameObject MilkshakeChocolateRaw => FindModPrefab("Milkshake Chocolate Raw");
        public static GameObject MilkshakeStrawberryRaw => FindModPrefab("Milkshake Strawberry Raw");
        public static GameObject Cup => FindModPrefab("Cup");
        public static GameObject CupProvider => FindModPrefab("Cup Provider");
        public static GameObject TeaProvider => FindModPrefab("Tea Provider");
        public static GameObject DistributedTea => FindExistingPrefab(ItemReferences.Sugar);
        public static GameObject BobaProvider => FindModPrefab("Boba Provider");
        public static GameObject UncookedBobaPot => FindModPrefab("Uncooked Boba Pot");
        public static GameObject CookedBobaPot => FindModPrefab("Cooked Boba Pot");
        public static GameObject BlackTeaCup => FindModPrefab("BobaCupPrefab", "Black");
        public static GameObject MatchaTeaCup => FindModPrefab("BobaCupPrefab", "Matcha");
        public static GameObject TaroTeaCup => FindModPrefab("BobaCupPrefab", "Taro");
        public static GameObject BlackTeaBase => FindModPrefab("BobaCupPrefab", "BlackBase");
        public static GameObject MatchaTeaBase => FindModPrefab("BobaCupPrefab", "MatchaBase");
        public static GameObject TaroTeaBase => FindModPrefab("BobaCupPrefab", "TaroBase");
        public static GameObject BobaIcon => FindModPrefab("BobaCupPrefab", "BobaIcon");

        private static GameObject FindExistingPrefab(int id)
        {
            return (GDOUtils.GetExistingGDO(id) as IHasPrefab)?.Prefab;
        }

        private static GameObject FindModPrefab(string name, string copyName = "")
        {
            if (!PrefabCache.ContainsKey(name + copyName))
            {
                var prefab = Mod.Bundle.LoadAsset<GameObject>(name);
                if (copyName != "")
                {
                    var copy = Object.Instantiate(prefab);
                    copy.transform.localPosition = Vector3.positiveInfinity;
                    PrefabCache.Add(name + copyName, copy);
                }
                else
                {
                    PrefabCache.Add(name, prefab);
                }
            }

            return PrefabCache[name + copyName];
        }
    }
}
