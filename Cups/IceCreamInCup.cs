﻿using Kitchen;
using KitchenData;
using KitchenDrinksMod.Util;
using KitchenLib.Customs;
using KitchenLib.Utils;
using System.Collections.Generic;
using UnityEngine;

namespace KitchenDrinksMod.Cups
{
    public class ChocolateIceCreamInCup : BaseIceCreamInCup
    {
        protected override string Name => "Chocolate";
        protected override string IceCreamMaterial => "Chocolate";
        protected override string ColorblindLabel => "C";
        protected override Item BaseIceCream => Refs.IceCreamChocolate;
    }

    public class StrawberryIceCreamInCup : BaseIceCreamInCup
    {
        protected override string Name => "Strawberry";
        protected override string IceCreamMaterial => "Strawberry";
        protected override string ColorblindLabel => "S";
        protected override Item BaseIceCream => Refs.IceCreamStrawberry;
    }
    public class VanillaIceCreamInCup : BaseIceCreamInCup
    {
        protected override string Name => "Vanilla";
        protected override string IceCreamMaterial => "Vanilla";
        protected override string ColorblindLabel => "V";
        protected override Item BaseIceCream => Refs.IceCreamVanilla;

        public override List<Item.ItemProcess> Processes => new()
        {
            new Item.ItemProcess()
            {
                Process = Refs.DispenseRedSoda,
                Result = Refs.RedSodaWithIceCream,
                Duration = 1f
            },
            new Item.ItemProcess()
            {
                Process = Refs.DispenseGreenSoda,
                Result = Refs.GreenSodaWithIceCream,
                Duration = 1f
            },
            new Item.ItemProcess()
            {
                Process = Refs.DispenseBlueSoda,
                Result = Refs.BlueSodaWithIceCream,
                Duration = 1f
            },
            new Item.ItemProcess()
            {
                Process = Refs.DispenseRootBeer,
                Result = Refs.RootBeerWithIceCream,
                Duration = 1f
            }
        };
    }
    public class IceCreamInCupItemGroupView : ItemGroupView
    {
        internal void Setup(GameObject prefab, Item baseIceCream, string colorblindLabel)
        {
            ComponentGroups = new()
            {
                new()
                {
                    GameObject = prefab.GetChild("MilkshakeCup/Cup"),
                    Item = Refs.Cup
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
                    Item = baseIceCream
                }
            };

            ComponentLabels = new()
            {
                new()
                {
                    Text = colorblindLabel,
                    Item = baseIceCream
                },
            };
        }
    }

    public abstract class BaseIceCreamInCup : CustomItemGroup<IceCreamInCupItemGroupView>, IColorblindLabelPositionOverride
    {
        protected abstract string Name { get; }
        protected abstract string IceCreamMaterial { get; }
        protected abstract string ColorblindLabel { get; }
        protected abstract Item BaseIceCream { get; }

        public override string UniqueNameID => $"Ice Cream In Cup - {Name}";
        public override GameObject Prefab => Prefabs.Find("Milkshake", $"{Name}InCup");
        public Vector3 ColorblindLabelPosition => new(0, 0.7f, 0);
        public override ItemCategory ItemCategory => ItemCategory.Generic;
        public override ItemStorage ItemStorageFlags => ItemStorage.StackableFood;

        public override List<ItemGroup.ItemSet> Sets => new()
        {
            new ItemGroup.ItemSet()
            {
                Min = 2,
                Max = 2,
                IsMandatory = true,
                Items = new List<Item>
                {
                    BaseIceCream,
                    Refs.Cup
                }
            }
        };

        public override void SetupPrefab(GameObject prefab)
        {
            prefab.SetupMaterialsLikeMilkshake("Milk", IceCreamMaterial);
            prefab.GetChild("MilkshakeCup/Straw").SetActive(false);
            prefab.GetChild("MilkshakeCup/LiquidHalf").SetActive(false);
            prefab.GetChild("MilkshakeCup/LiquidFull").SetActive(false);

            prefab.GetComponent<IceCreamInCupItemGroupView>()?.Setup(prefab, BaseIceCream, ColorblindLabel);
        }
    }
}
