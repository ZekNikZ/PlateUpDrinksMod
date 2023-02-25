using ApplianceLib.Customs.GDO;
using KitchenData;
using KitchenDrinksMod.Util;
using System.Collections.Generic;
using UnityEngine;

namespace KitchenDrinksMod.Soda
{
    public class RedSoda : BaseSoda
    {
        protected override string Name => "Red";
        protected override string LiquidMaterial => "RedLiquid";
        public override string ColourBlindTag => "R";
    }

    public class GreenSoda : BaseSoda
    {
        protected override string Name => "Green";
        protected override string LiquidMaterial => "GreenLiquid";
        public override string ColourBlindTag => "G";
    }

    public class BlueSoda : BaseSoda
    {
        protected override string Name => "Blue";
        protected override string LiquidMaterial => "BlueLiquid";
        public override string ColourBlindTag => "B";
    }

    public abstract class BaseSoda : ModItem
    {
        protected abstract string Name { get; }
        protected abstract string LiquidMaterial { get; }

        public override string UniqueNameID => $"Soda - {Name}";
        public override GameObject Prefab => Prefabs.Find("SodaCupPrefab", $"{Name}");
        public override ItemCategory ItemCategory => ItemCategory.Generic;
        public override ItemStorage ItemStorageFlags => ItemStorage.StackableFood;
        public override ItemValue ItemValue => ItemValue.Small;
        public override Appliance DedicatedProvider => Refs.SodaProvider;

        public override List<Item.ItemProcess> Processes => new()
        {
            new()
            {
                Process = Refs.Clean,
                Duration = 0.35f,
                Result = Refs.Cup
            }
        };

        protected override void Modify(Item item)
        {
            Prefab.SetupMaterialsLikeSodaCup(LiquidMaterial);
        }
    }
}
