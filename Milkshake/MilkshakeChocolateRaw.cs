using Kitchen;
using KitchenData;
using KitchenDrinksMod.Util;
using KitchenLib.Customs;
using KitchenLib.Utils;
using System.Collections.Generic;
using UnityEngine;

namespace KitchenDrinksMod.Milkshakes
{
    public class MilkshakeChocolateRaw : CustomItemGroup
    {
        public override string UniqueNameID => "Milkshake - Chocolate - Raw";
        public override GameObject Prefab => Prefabs.MilkshakeChocolateRaw;
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
                    Refs.IceCreamChocolate,
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
                Result = Refs.MilkshakeChocolate,
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
                        Item = Refs.IceCreamChocolate
                    }
                };
            }
        }

        public override void OnRegister(GameDataObject gameDataObject)
        {
            MaterialUtils.ApplyMaterial<MeshRenderer>(Prefab, "Model/Cup", MaterialHelpers.GetMaterialArray("Cup Base"));
            MaterialUtils.ApplyMaterial<MeshRenderer>(Prefab, "Model/Liquid", MaterialHelpers.GetMaterialArray("Milk"));
            MaterialUtils.ApplyMaterial<MeshRenderer>(Prefab, "Model/IceCream1", MaterialHelpers.GetMaterialArray("Chocolate"));
            MaterialUtils.ApplyMaterial<MeshRenderer>(Prefab, "Model/IceCream2", MaterialHelpers.GetMaterialArray("Chocolate"));
            MaterialUtils.ApplyMaterial<MeshRenderer>(Prefab, "Model/IceCream3", MaterialHelpers.GetMaterialArray("Chocolate"));

            var view = Prefab.AddComponent<View>();
            view.Setup(Prefab);
        }
    }
}
