using ApplianceLib.Api.Prefab;
using KitchenData;
using KitchenDrinksMod.Util;
using KitchenLib.Customs;
using KitchenLib.Utils;
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

    public class RootBeer : BaseSoda
    {
        protected override string Name => "RootBeer";
        protected override string LiquidMaterial => "drinkup_root_beer_liquid";
        public override string ColourBlindTag => "RB";
        public override Appliance DedicatedProvider => Refs.RootBeerProvider;
    }

    public abstract class BaseSoda : CustomItem, IColorblindLabelPositionOverride
    {
        protected abstract string Name { get; }
        protected abstract string LiquidMaterial { get; }

        public override string UniqueNameID => $"Soda - {Name}";
        public override GameObject Prefab => Prefabs.Create($"Soda{Name}");
        public override ItemCategory ItemCategory => ItemCategory.Generic;
        public override ItemStorage ItemStorageFlags => ItemStorage.StackableFood;
        public override ItemValue ItemValue => ItemValue.Small;
        public override Appliance DedicatedProvider => Refs.SodaProvider;

        public Vector3 ColorblindLabelPosition =>  new Vector3(0, 0.2f, 0);

        //public override List<Item.ItemProcess> Processes => new()
        //{
        //    new()
        //    {
        //        Process = Refs.Clean,
        //        Duration = 0.35f,
        //        Result = Refs.Cup
        //    }
        //};

        public override void SetupPrefab(GameObject prefab)
        {
            prefab.AttachCup(MaterialUtils.GetMaterialArray(LiquidMaterial)[0], true);
        }
    }
}
