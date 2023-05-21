using KitchenData;
using KitchenDrinksMod.Util;
using KitchenLib.Customs;
using KitchenLib.Utils;
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

    public abstract class BaseServedMilkshake : CustomItem
    {
        protected abstract string Name { get; }
        protected abstract string LiquidMaterial { get; }

        public override string UniqueNameID => $"Milkshake - {Name} - Serving";
        public override GameObject Prefab => Prefabs.Find("Milkshake", $"{Name}Served");
        public override ItemCategory ItemCategory => ItemCategory.Generic;
        public override ItemStorage ItemStorageFlags => ItemStorage.StackableFood;
        public override ItemValue ItemValue => ItemValue.SideLarge;

        public override void SetupPrefab(GameObject prefab)
        {
            prefab.SetupMaterialsLikeMilkshake(LiquidMaterial);
            prefab.GetChild("MilkshakeCup/LiquidHalf").SetActive(false);
            prefab.GetChild("MilkshakeCup/IceCream1").SetActive(false);
            prefab.GetChild("MilkshakeCup/IceCream2").SetActive(false);
            prefab.GetChild("MilkshakeCup/IceCream3").SetActive(false);
        }
    }
}
