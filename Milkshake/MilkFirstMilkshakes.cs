using KitchenData;
using KitchenDrinksMod.Util;
using KitchenLib.Customs;
using KitchenLib.Utils;
using System.Collections.Generic;
using UnityEngine;

namespace KitchenDrinksMod.Milkshakes
{
    public class ChocolateMilkFirstMilkshake : BaseMilkFirstMilkshake<ServedChocolateMilkshake>
    {
        protected override string Name => "Chocolate";
        protected override string IceCreamMaterial => "Chocolate";
        protected override string ColorblindLabel => "C";
        protected override Item BaseIceCream => Refs.IceCreamChocolate;
    }

    public class StrawberryMilkFirstMilkshake : BaseMilkFirstMilkshake<ServedStrawberryMilkshake>
    {
        protected override string Name => "Strawberry";
        protected override string IceCreamMaterial => "Strawberry";
        protected override string ColorblindLabel => "S";
        protected override Item BaseIceCream => Refs.IceCreamStrawberry;
    }

    public class VanillaMilkFirstMilkshake : BaseMilkFirstMilkshake<ServedVanillaMilkshake>
    {
        protected override string Name => "Vanilla";
        protected override string IceCreamMaterial => "Vanilla";
        protected override string ColorblindLabel => "V";
        protected override Item BaseIceCream => Refs.IceCreamVanilla;
    }

    public abstract class BaseMilkFirstMilkshake<T> : CustomItemGroup<MilkshakeItemGroupView>, IColorblindLabelPositionOverride where T : BaseServedMilkshake
    {
        protected abstract string Name { get; }
        protected abstract string IceCreamMaterial { get; }
        protected abstract string ColorblindLabel { get; }
        protected abstract Item BaseIceCream { get; }

        public override string UniqueNameID => $"Milkshake - {Name} (Milk First)";
        public override GameObject Prefab => Prefabs.Find("Milkshake", Name + "MilkFirst");
        public Vector3 ColorblindLabelPosition => new(0, 0.7f, 0);
        public override ItemCategory ItemCategory => ItemCategory.Generic;
        public override ItemStorage ItemStorageFlags => ItemStorage.StackableFood;

        public override List<ItemGroup.ItemSet> Sets => new()
        {
            new ItemGroup.ItemSet()
            {
                Min = 1,
                Max = 1,
                Items = new List<Item>
                {
                    BaseIceCream
                }
            },
            new ItemGroup.ItemSet()
            {
                Min = 1,
                Max = 1,
                IsMandatory = true,
                Items = new List<Item>
                {
                    Refs.MilkInCup,
                    Refs.Find<Item>("The Modded Kitchen", "Milk Glass") ?? Refs.MilkInCup
                }
            }
        };

        public override List<Item.ItemProcess> Processes => new()
        {
            new Item.ItemProcess()
            {
                Process = Refs.Shake,
                Result = Refs.Find<Item, T>(),
                Duration = 0.75f
            }
        };

        public override void SetupPrefab(GameObject prefab)
        {
            prefab.SetupMaterialsLikeMilkshake("Milk", IceCreamMaterial);
            prefab.GetChild("MilkshakeCup/LiquidFull").SetActive(false);
            prefab.GetChild("MilkshakeCup/Straw").SetActive(false);

            prefab.GetComponent<MilkshakeItemGroupView>()?.Setup(prefab, BaseIceCream, ColorblindLabel, Refs.Find<Item>("The Modded Kitchen", "Milk Glass") ?? Refs.MilkInCup);
        }
    }
}
