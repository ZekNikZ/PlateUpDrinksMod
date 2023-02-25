using ApplianceLib.Customs.GDO;
using KitchenData;
using KitchenDrinksMod.Util;
using UnityEngine;

namespace KitchenDrinksMod.Milkshakes
{
    public class ServedChocolateMilkshake : BaseServedMilkshake
    {
        protected override string Name => "Chocolate";
        protected override string LiquidMaterial => "Chocolate";
        public override string ColourBlindTag => "C";
    }

    public class ServedStrawberryMilkshake : BaseServedMilkshake
    {
        protected override string Name => "Strawberry";
        protected override string LiquidMaterial => "Strawberry";
        public override string ColourBlindTag => "S";
    }

    public class ServedVanillaMilkshake : BaseServedMilkshake
    {
        protected override string Name => "Vanilla";
        protected override string LiquidMaterial => "Vanilla";
        public override string ColourBlindTag => "V";
    }

    public abstract class BaseServedMilkshake : ModItem
    {
        protected abstract string Name { get; }
        protected abstract string LiquidMaterial { get; }

        public override string UniqueNameID => $"Milkshake - {Name} - Serving";
        public override GameObject Prefab => Prefabs.Find("Milkshake", $"{Name}Served");
        public override ItemCategory ItemCategory => ItemCategory.Generic;
        public override ItemStorage ItemStorageFlags => ItemStorage.StackableFood;
        public override ItemValue ItemValue => ItemValue.SideLarge;

        protected override void SetupPrefab(GameObject prefab)
        {
            prefab.SetupMaterialsLikeMilkshake(LiquidMaterial);
            prefab.GetChildFromPath("MilkshakeCup/LiquidHalf").SetActive(false);
            prefab.GetChildFromPath("MilkshakeCup/IceCream1").SetActive(false);
            prefab.GetChildFromPath("MilkshakeCup/IceCream2").SetActive(false);
            prefab.GetChildFromPath("MilkshakeCup/IceCream3").SetActive(false);
        }
    }
}
