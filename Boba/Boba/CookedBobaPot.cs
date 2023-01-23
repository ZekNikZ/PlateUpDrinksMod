using KitchenData;
using KitchenDrinksMod.ToMoveToLibraryModLater.Dishes;
using KitchenDrinksMod.Utils;
using KitchenLib.Utils;
using System.Collections.Generic;
using UnityEngine;

namespace KitchenDrinksMod.Boba
{
    public class CookedBobaPot : ModItem
    {
        public override string UniqueNameID => "Boba - Pot - Cooked";
        public override GameObject Prefab => Prefabs.CookedBobaPot;
        public override bool AllowSplitMerging => true;
        public override float SplitSpeed => 1f;
        public override int SplitCount => 3;
        public override Item SplitSubItem => Refs.CookedBoba;
        public override List<Item> SplitDepletedItems => new() { Refs.Pot };
        public override bool PreventExplicitSplit => true;
        public override Item DisposesTo => Refs.Pot;

        protected override void Modify(Item item)
        {
            MaterialUtils.ApplyMaterial<MeshRenderer>(Prefab, "Pot/Pot/Cylinder", MaterialHelpers.GetMaterialArray("Metal"));
            MaterialUtils.ApplyMaterial<MeshRenderer>(Prefab, "Pot/Pot/Cylinder.003", MaterialHelpers.GetMaterialArray("Metal Dark"));
            MaterialUtils.ApplyMaterial<MeshRenderer>(Prefab, "Water", MaterialHelpers.GetMaterialArray("Water"));

            Prefab.GetChildFromPath("BobaBalls").SetActive(true);

            foreach (var mesh in Prefab.GetChildFromPath("BobaBalls").GetComponentsInChildren<MeshRenderer>())
            {
                mesh.materials = MaterialHelpers.GetMaterialArray("CookedBoba");
            }
        }
    }
}
