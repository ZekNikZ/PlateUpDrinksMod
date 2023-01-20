using KitchenData;
using KitchenLib.References;
using KitchenLib.Utils;
using UnityEngine;

namespace KitchenDrinksMod
{
    internal class Prefabs
    {
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

        private static GameObject FindExistingPrefab(int id)
        {
            return (GDOUtils.GetExistingGDO(id) as IHasPrefab)?.Prefab;
        }

        private static GameObject FindModPrefab(string name)
        {
            return Mod.Bundle.LoadAsset<GameObject>(name);
        }
    }
}
