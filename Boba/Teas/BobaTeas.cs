using ApplianceLib.Customs.GDO;
using KitchenData;
using KitchenDrinksMod.Util;
using System.Collections.Generic;
using UnityEngine;

namespace KitchenDrinksMod.Boba
{
    public class BlackBobaTea : BobaTea
    {
        protected override string Name => "Black";
        protected override string LiquidMaterial => "BlackTeaLiquid";
        public override string ColourBlindTag => "Bl";
    }

    public class MatchaBobaTea : BobaTea
    {
        protected override string Name => "Matcha";
        protected override string LiquidMaterial => "MatchaTeaLiquid";
        public override string ColourBlindTag => "Ma";
    }

    public class TaroBobaTea : BobaTea
    {
        protected override string Name => "Taro";
        protected override string LiquidMaterial => "TaroTeaLiquid";
        public override string ColourBlindTag => "T";
    }

    public abstract class BobaTea : ModItem
    {
        protected abstract string Name { get; }
        protected abstract string LiquidMaterial { get; }

        public override string UniqueNameID => $"Boba Tea - {Name}";
        public override GameObject Prefab => Prefabs.Find("BobaCupPrefab", $"{Name}Base");
        public override ItemCategory ItemCategory => ItemCategory.Generic;
        public override ItemStorage ItemStorageFlags => ItemStorage.StackableFood;
        public override ItemValue ItemValue => ItemValue.Small;
        public override Appliance DedicatedProvider => Refs.TeaProvider;

        public override List<Item.ItemProcess> Processes => new()
        {
            new()
            {
                Process = Refs.Clean,
                Duration = 0.35f,
                Result = Refs.Cup
            }
        };

        protected override void SetupPrefab(GameObject prefab)
        {
            prefab.SetupMaterialsLikeBobaCup(LiquidMaterial);

            var disabledChildObjects = new List<string>()
            {
                "Boba",
                "Liquid2",
                "Lid",
                "Straw"
            };
            foreach (var childPath in disabledChildObjects)
            {
                prefab.GetChildFromPath(childPath).SetActive(false);
            }
        }
    }
}
