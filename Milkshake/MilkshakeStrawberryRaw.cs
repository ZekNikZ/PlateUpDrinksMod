using Kitchen;
using KitchenData;
using KitchenDrinksMod.Util;
using KitchenLib.Customs;
using KitchenLib.Utils;
using System.Collections.Generic;
using UnityEngine;

namespace KitchenDrinksMod.Milkshakes
{
    public class MilkshakeStrawberryRaw : CustomItemGroup
    {
        public override string UniqueNameID => "Milkshake - Strawberry - Raw";
        public override GameObject Prefab => Prefabs.MilkshakeStrawberryRaw;
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
                    Refs.IceCreamStrawberry,
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
                Result = Refs.MilkshakeStrawberry,
                Duration = 1
            }
        };

        internal class View : ItemGroupView
        {
            internal void Setup(GameObject prefab)
            {
                ComponentGroups = new()
                {
                    new ComponentGroup()
                    {
                        GameObject = GameObjectUtils.GetChildObject(prefab, "Model/Cup"),
                        Item = Refs.Cup
                    },
                    new ComponentGroup()
                    {
                        GameObject = GameObjectUtils.GetChildObject(prefab, "Model/Liquid"),
                        Item = Refs.MilkIngredient
                    },
                    new ComponentGroup()
                    {
                        Objects = new()
                        {
                            GameObjectUtils.GetChildObject(prefab, "Model/IceCream1"),
                            //GameObjectUtils.GetChildObject(prefab, "Model/IceCream2"),
                            GameObjectUtils.GetChildObject(prefab, "Model/IceCream3")
                        },
                        DrawAll = true,
                        Item = Refs.IceCreamStrawberry
                    }
                };
            }
        }

        public override void OnRegister(GameDataObject gameDataObject)
        {
            MaterialUtils.ApplyMaterial<MeshRenderer>(Prefab, "Model/Cup", MaterialHelpers.GetMaterialArray("Cup Base"));
            MaterialUtils.ApplyMaterial<MeshRenderer>(Prefab, "Model/Liquid", MaterialHelpers.GetMaterialArray("Milk"));
            MaterialUtils.ApplyMaterial<MeshRenderer>(Prefab, "Model/IceCream1", MaterialHelpers.GetMaterialArray("Strawberry"));
            MaterialUtils.ApplyMaterial<MeshRenderer>(Prefab, "Model/IceCream2", MaterialHelpers.GetMaterialArray("Strawberry"));
            MaterialUtils.ApplyMaterial<MeshRenderer>(Prefab, "Model/IceCream3", MaterialHelpers.GetMaterialArray("Strawberry"));

            var view = Prefab.AddComponent<View>();
            view.Setup(Prefab);
        }
    }
}
