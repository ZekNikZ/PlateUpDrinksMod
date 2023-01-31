using KitchenData;
using KitchenDrinksMod.Customs;
using KitchenDrinksMod.Soda;
using KitchenDrinksMod.Util;
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

    public class FloatItemGroupView : CompletableItemGroupView
    {
        internal void Setup(GameObject prefab, string colorblindLabel, Item sodaItem)
        {
            ComponentGroups = new()
            {
                new()
                {
                    GameObject = prefab.GetChildFromPath("MilkshakeCup/LiquidFull"),
                    Item = sodaItem
                },
                new()
                {
                    Objects = new()
                    {
                        prefab.GetChildFromPath("MilkshakeCup/IceCream1"),
                        prefab.GetChildFromPath("MilkshakeCup/IceCream2"),
                        prefab.GetChildFromPath("MilkshakeCup/IceCream3")
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
            return components.Count == 2;
        }
    }

    public abstract class BaseFloat<T> : ModItemGroup<FloatItemGroupView> where T: BaseSoda
    {
        protected abstract string Name { get; }
        protected abstract string ColorblindLabel { get; }
        protected abstract string LiquidMaterial { get; }

        public override string UniqueNameID => $"Float - {Name}";
        public override GameObject Prefab => Prefabs.Find("Milkshake", Name);
        protected override Vector3 ColorblindLabelPosition => new(0, 0.45f, 0);
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

        protected override void Modify(ItemGroup itemGroup)
        {
            Prefab.SetupMaterialsLikeMilkshake(LiquidMaterial);
            Prefab.GetChildFromPath("MilkshakeCup/LiquidHalf").SetActive(false);

            Prefab.GetComponent<FloatItemGroupView>()?.Setup(Prefab, ColorblindLabel, Refs.Find<Item, T>());
        }
    }
}
