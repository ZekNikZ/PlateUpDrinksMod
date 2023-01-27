using Kitchen;
using KitchenData;
using KitchenDrinksMod.Customs;
using KitchenDrinksMod.Util;
using System.Collections.Generic;
using UnityEngine;

namespace KitchenDrinksMod.Milkshake
{
    public class ChocolateMilkshake : BaseMilkshake<ServedChocolateMilkshake>
    {
        protected override string Name => "Chocolate";
        protected override string IceCreamMaterial => "Chocolate";
        protected override string ColorblindLabel => "C";
        protected override Item BaseIceCream => Refs.IceCreamChocolate;
    }

    public class StrawberryMilkshake : BaseMilkshake<ServedStrawberryMilkshake>
    {
        protected override string Name => "Strawberry";
        protected override string IceCreamMaterial => "Strawberry";
        protected override string ColorblindLabel => "S";
        protected override Item BaseIceCream => Refs.IceCreamStrawberry;
    }
    public class VanillaMilkshake : BaseMilkshake<ServedVanillaMilkshake>
    {
        protected override string Name => "Vanilla";
        protected override string IceCreamMaterial => "Vanilla";
        protected override string ColorblindLabel => "V";
        protected override Item BaseIceCream => Refs.IceCreamVanilla;
    }

    public class MilkshakeItemGroupView : ItemGroupView
    {
        internal void Setup(GameObject prefab, Item baseIceCream, string colorblindLabel)
        {
            ComponentGroups = new()
            {
                new()
                {
                    GameObject = prefab.GetChildFromPath("Model/Cup"),
                    Item = Refs.Cup
                },
                new()
                {
                    GameObject = prefab.GetChildFromPath("Model/Liquid"),
                    Item = Refs.MilkIngredient
                },
                new()
                {
                    Objects = new()
                    {
                        prefab.GetChildFromPath("Model/IceCream1"),
                        //prefab.GetChildFromPath("Model/IceCream2"),
                        prefab.GetChildFromPath("Model/IceCream3")
                    },
                    DrawAll = true,
                    Item = baseIceCream
                }
            };

            ComponentLabels = new()
            {
                new()
                {
                    Text = "Mi",
                    Item = Refs.MilkIngredient
                },
                new()
                {
                    Text = colorblindLabel,
                    Item = baseIceCream
                },
            };
        }
    }

    public abstract class BaseMilkshake<T> : ModItemGroup<MilkshakeItemGroupView> where T : BaseServedMilkshake
    {
        protected abstract string Name { get; }
        protected abstract string IceCreamMaterial { get; }
        protected abstract string ColorblindLabel { get; }
        protected abstract Item BaseIceCream { get; }

        public override string UniqueNameID => $"Milkshake - {Name}";
        public override GameObject Prefab => Prefabs.Find("Milkshake", Name);
        protected override Vector3 ColorblindLabelPosition => new(0, 0.7f, 0);
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
                    BaseIceCream,
                    Refs.MilkIngredient
                }
            },
            new ItemGroup.ItemSet()
            {
                Min = 1,
                Max = 1,
                IsMandatory = true,
                Items = new List<Item>
                {
                    Refs.Cup
                }
            }
        };

        public override List<Item.ItemProcess> Processes => new()
        {
            new Item.ItemProcess()
            {
                Process = Refs.Shake,
                Result = Refs.Find<Item, T>(),
                Duration = 1
            }
        };

        protected override void Modify(ItemGroup itemGroup)
        {
            Prefab.SetupMaterialsLikeMilkshake("Milk", IceCreamMaterial);
            Prefab.GetChildFromPath("Model/Straw").SetActive(false);

            Prefab.GetComponent<MilkshakeItemGroupView>()?.Setup(Prefab, BaseIceCream, ColorblindLabel);
        }
    }
}
