using KitchenData;
using KitchenDrinksMod.Customs;
using KitchenDrinksMod.Util;
using UnityEngine;

namespace KitchenDrinksMod.Milkshake
{
    public class ServedChocolateMilkshake : BaseServedMilkshake
    {
        protected override string Name => "Chocolate";
        protected override string LiquidMaterial => "Chocolate";
    }

    public class ServedStrawberryMilkshake : BaseServedMilkshake
    {
        protected override string Name => "Strawberry";
        protected override string LiquidMaterial => "Strawberry";
    }

    public class ServedVanillaMilkshake : BaseServedMilkshake
    {
        protected override string Name => "Vanilla";
        protected override string LiquidMaterial => "Vanilla";
    }

    public abstract class BaseServedMilkshake : ModItem
    {
        protected abstract string Name { get; }
        protected abstract string LiquidMaterial { get; }

        public override string UniqueNameID => $"Milkshake - {Name} - Serving";
        public override GameObject Prefab => Prefabs.Find("Milkshake", $"{Name}Served");
        public override ItemCategory ItemCategory => ItemCategory.Generic;
        public override ItemStorage ItemStorageFlags => ItemStorage.StackableFood;
        public override ItemValue ItemValue => ItemValue.Medium;

        protected override void Modify(Item item)
        {
            Prefab.SetupMaterialsLikeMilkshake(LiquidMaterial);
        }
    }
}
