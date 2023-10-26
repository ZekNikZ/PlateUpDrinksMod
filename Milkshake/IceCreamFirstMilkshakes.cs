using Kitchen;
using KitchenData;
using KitchenDrinksMod.Cups;
using KitchenDrinksMod.Util;
using KitchenLib.Customs;
using KitchenLib.Utils;
using System.Collections.Generic;
using UnityEngine;

namespace KitchenDrinksMod.Milkshakes
{
    public class ChocolateMilkshake : BaseMilkshake<ServedChocolateMilkshake, ChocolateIceCreamInCup>
    {
        protected override string Name => "Chocolate";
        protected override string IceCreamMaterial => "Chocolate";
        protected override string ColorblindLabel => "C";
    }

    public class StrawberryMilkshake : BaseMilkshake<ServedStrawberryMilkshake, StrawberryIceCreamInCup>
    {
        protected override string Name => "Strawberry";
        protected override string IceCreamMaterial => "Strawberry";
        protected override string ColorblindLabel => "S";
    }
    public class VanillaMilkshake : BaseMilkshake<ServedVanillaMilkshake, VanillaIceCreamInCup>
    {
        protected override string Name => "Vanilla";
        protected override string IceCreamMaterial => "Vanilla";
        protected override string ColorblindLabel => "V";
    }

    public class MilkshakeItemGroupView : ItemGroupView
    {
        internal void Setup(GameObject prefab, Item iceCreamInCup, string colorblindLabel, Item milkIngredient=null)
        {
            ComponentGroups = new()
            {
                new()
                {
                    GameObject = prefab.GetChild("MilkshakeCup/LiquidHalf"),
                    Item = milkIngredient ?? Refs.Milk
                },
                new()
                {
                    Objects = new()
                    {
                        prefab.GetChild("MilkshakeCup/Cup"),
                        prefab.GetChild("MilkshakeCup/IceCream1"),
                        prefab.GetChild("MilkshakeCup/IceCream2"),
                        prefab.GetChild("MilkshakeCup/IceCream3")
                    },
                    DrawAll = true,
                    Item = iceCreamInCup
                }
            };

            ComponentLabels = new()
            {
                new()
                {
                    Text = "Mi",
                    Item = Refs.Milk
                },
                new()
                {
                    Text = "Mi",
                    Item = Refs.MilkInCup
                },
                new()
                {
                    Text = colorblindLabel,
                    Item = iceCreamInCup
                },
            };
        }
    }

    public abstract class BaseMilkshake<T, I> : CustomItemGroup<MilkshakeItemGroupView>, IColorblindLabelPositionOverride where T : BaseServedMilkshake where I : BaseIceCreamInCup
    {
        protected abstract string Name { get; }
        protected abstract string IceCreamMaterial { get; }
        protected abstract string ColorblindLabel { get; }

        public override string UniqueNameID => $"Milkshake - {Name}";
        public override GameObject Prefab => Prefabs.Find("Milkshake", Name);
        public Vector3 ColorblindLabelPosition => new(0, 0.7f, 0);
        public override ItemCategory ItemCategory => ItemCategory.Generic;
        public override ItemStorage ItemStorageFlags => ItemStorage.StackableFood;

        public override List<ItemGroup.ItemSet> Sets => new()
        {
            new ItemGroup.ItemSet()
            {
                Min = 2,
                Max = 2,
                Items = new List<Item>
                {
                    Refs.Milk,
                    Refs.Find<ItemGroup, I>()
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
            prefab.GetChild("MilkshakeCup/Straw").SetActive(false);
            prefab.GetChild("MilkshakeCup/LiquidFull").SetActive(false);

            prefab.GetComponent<MilkshakeItemGroupView>()?.Setup(prefab, Refs.Find<Item, I>(), ColorblindLabel);
        }
    }
}
