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

        private static GameObject FindAppliancePrefab(int id)
        {
            return ((Appliance)GDOUtils.GetExistingGDO(id)).Prefab;
        }

        private static GameObject FindModPrefab(string name)
        {
            return Mod.Bundle.LoadAsset<GameObject>(name);
        }
    }
}
