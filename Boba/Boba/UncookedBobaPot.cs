using Kitchen;
using KitchenData;
using KitchenDrinksMod.ToMoveToLibraryModLater.Dishes;
using KitchenDrinksMod.Utils;
using KitchenLib.Customs;
using KitchenLib.Utils;
using System.Collections.Generic;
using UnityEngine;

namespace KitchenDrinksMod.Boba
{
    public class UncookedBobaPot : ModItemGroup
    {
        public override string UniqueNameID => "Boba - Pot - Uncooked";
        public override GameObject Prefab => Prefabs.UncookedBobaPot;
        public override Item DisposesTo => Refs.Pot;

        public override List<ItemGroup.ItemSet> Sets => new()
        {
            new()
            {
                Min = 2,
                Max = 2,
                Items = new List<Item>
                {
                    Refs.UncookedBoba,
                    Refs.Water
                }
            },
            new()
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
            new()
            {
                Process = Refs.Cook,
                Result = Refs.CookedBobaPot,
                Duration = 1
            }
        };

        private class UncookedBobaPotView : ItemGroupView
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

        protected override void Modify(ItemGroup itemGroup)
        {
            MaterialUtils.ApplyMaterial<MeshRenderer>(Prefab, "Pot/Pot/Cylinder", MaterialHelpers.GetMaterialArray("Metal"));
            MaterialUtils.ApplyMaterial<MeshRenderer>(Prefab, "Pot/Pot/Cylinder.003", MaterialHelpers.GetMaterialArray("Metal Dark"));
            MaterialUtils.ApplyMaterial<MeshRenderer>(Prefab, "Water", MaterialHelpers.GetMaterialArray("Water"));

            foreach (var mesh in Prefab.GetChildFromPath("BobaBalls").GetComponentsInChildren<MeshRenderer>())
            {
                mesh.materials = MaterialHelpers.GetMaterialArray("UncookedBoba");
            }

            var view = Prefab.AddComponent<UncookedBobaPotView>();
            view.Setup(Prefab);
        }
    }
}
