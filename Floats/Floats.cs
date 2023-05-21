using KitchenData;
using KitchenDrinksMod.Customs;
using KitchenDrinksMod.Soda;
using KitchenDrinksMod.Util;
using KitchenLib.Customs;
using KitchenLib.Utils;
using System.Collections.Generic;
using UnityEngine;

namespace KitchenDrinksMod.Floats
{
    public class RedFloat : BaseFloat<RedSoda>
    {
        protected override string Name => "Red";
        protected override string LiquidMaterial => "RedLiquid";
        protected override string ColorblindLabel => "R";
    }

    public class GreenFloat : BaseFloat<GreenSoda>
    {
        protected override string Name => "Green";
        protected override string LiquidMaterial => "GreenLiquid";
        protected override string ColorblindLabel => "G";
    }
    public class BlueFloat : BaseFloat<BlueSoda>
    {
        protected override string Name => "Blue";
        protected override string LiquidMaterial => "BlueLiquid";
        protected override string ColorblindLabel => "B";
    }
    public class RootBeerFloat : BaseFloat<RootBeer>
    {
        protected override string Name => "RootBeer";
        protected override string LiquidMaterial => "drinkup:root_beer_liquid";
        protected override string ColorblindLabel => "RB";
    }

    public class FloatItemGroupView : CompletableItemGroupView
    {
        internal void Setup(GameObject prefab, string colorblindLabel, Item sodaItem)
        {
            ComponentGroups = new()
            {
                new()
                {
                    GameObject = prefab.GetChild("MilkshakeCup/LiquidFull"),
                    Item = sodaItem
                },
                new()
                {
                    Objects = new()
                    {
                        prefab.GetChild("MilkshakeCup/IceCream1"),
                        prefab.GetChild("MilkshakeCup/IceCream2"),
                        prefab.GetChild("MilkshakeCup/IceCream3")
                    },
                    DrawAll = true,
                    Item = Refs.IceCreamVanilla
                }
            };

            ComponentLabels = new()
            {
                new()
                {
                    Text = colorblindLabel,
                    Item = sodaItem
                },
                new()
                {
                    Text = "V",
                    Item = Refs.IceCreamVanilla
                },
            };

            CompletionObjects = new()
            {
                GameObjectUtils.GetChildObject(prefab, "MilkshakeCup/Straw")
            };

            CompletionLabel = colorblindLabel + "F";
        }

        protected override bool IsComplete(ItemList components)
        {
            bool foundSoda = false;
            bool foundIceCream = false;

            foreach (var itemId in components)
            {
                if (itemId == Refs.RedSoda.ID || itemId == Refs.GreenSoda.ID || itemId == Refs.BlueSoda.ID)
                {
                    foundSoda = true;
                }
                else if (itemId == Refs.IceCreamVanilla.ID)
                {
                    foundIceCream = true;
                }
            }

            return foundSoda && foundIceCream;
        }
    }

    public abstract class BaseFloat<T> : CustomItemGroup<FloatItemGroupView>, IColorblindLabelPositionOverride where T : BaseSoda
    {
        protected abstract string Name { get; }
        protected abstract string ColorblindLabel { get; }
        protected abstract string LiquidMaterial { get; }

        public override string UniqueNameID => $"Float - {Name}";
        public override GameObject Prefab => Prefabs.Find("Milkshake", Name);
        public Vector3 ColorblindLabelPosition => new(0, 0.45f, 0);
        public override ItemCategory ItemCategory => ItemCategory.Generic;
        public override ItemStorage ItemStorageFlags => ItemStorage.StackableFood;
        public override ItemValue ItemValue => ItemValue.SideMedium;

        public override List<ItemGroup.ItemSet> Sets => new()
        {
            new ItemGroup.ItemSet()
            {
                Min = 2,
                Max = 2,
                IsMandatory = true,
                Items = new List<Item>
                {
                    Refs.Find<Item, T>(),
                    Refs.IceCreamVanilla
                }
            }
        };

        public override void SetupPrefab(GameObject prefab)
        {
            prefab.SetupMaterialsLikeMilkshake(LiquidMaterial);
            prefab.GetChild("MilkshakeCup/LiquidHalf").SetActive(false);

            prefab.GetComponent<FloatItemGroupView>()?.Setup(prefab, ColorblindLabel, Refs.Find<Item, T>());
        }
    }
}
