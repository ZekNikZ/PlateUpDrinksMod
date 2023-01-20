using Kitchen;
using KitchenData;
using KitchenDrinksMod.Utils;
using KitchenLib.Customs;
using KitchenLib.Utils;
using System.Collections.Generic;
using UnityEngine;

namespace KitchenDrinksMod.Boba
{
    public class UncookedBobaPot : CustomItemGroup
    {
        public override string UniqueNameID => "Boba Pot - Uncooked";
        public override GameObject Prefab => Prefabs.UncookedBobaPot;
        public override ItemCategory ItemCategory => ItemCategory.Generic;
        public override ItemStorage ItemStorageFlags => ItemStorage.None;
        public override Item DisposesTo => Refs.Pot;

        public override List<ItemGroup.ItemSet> Sets => new()
        {
            new ItemGroup.ItemSet()
            {
                Min = 2,
                Max = 2,
                Items = new List<Item>
                {
                    Refs.UncookedBoba,
                    Refs.Water
                }
            },
            new ItemGroup.ItemSet()
            {
                Min = 1,
                Max = 1,
                IsMandatory = true,
                Items = new List<Item>
                {
                    Refs.Pot
                }
            }
        };

        public override List<Item.ItemProcess> Processes => new()
        {
            new Item.ItemProcess()
            {
                Process = Refs.Cook,
                Result = Refs.CookedBobaPot,
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
                        GameObject = GameObjectUtils.GetChildObject(prefab, "BobaBalls"),
                        Item = Refs.UncookedBoba
                    },
                    new ComponentGroup()
                    {
                        GameObject = GameObjectUtils.GetChildObject(prefab, "Water"),
                        Item = Refs.Water
                    }
                };
            }
        }

        public override void OnRegister(GameDataObject gameDataObject)
        {
            MaterialUtils.ApplyMaterial<MeshRenderer>(Prefab, "Pot/Pot/Cylinder", MaterialHelpers.GetMaterialArray("Metal"));
            MaterialUtils.ApplyMaterial<MeshRenderer>(Prefab, "Pot/Pot/Cylinder.003", MaterialHelpers.GetMaterialArray("Metal Dark"));
            MaterialUtils.ApplyMaterial<MeshRenderer>(Prefab, "Water", MaterialHelpers.GetMaterialArray("Water"));

            foreach (var mesh in Prefab.GetChildFromPath("BobaBalls").GetComponentsInChildren<MeshRenderer>())
            {
                mesh.materials = MaterialHelpers.GetMaterialArray("UncookedBoba");
            }

            var view = Prefab.AddComponent<View>();
            view.Setup(Prefab);
        }
    }
}
