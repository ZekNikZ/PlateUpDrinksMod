using KitchenData;
using KitchenDrinksMod.Utils;
using KitchenLib.Customs;
using KitchenLib.Utils;
using UnityEngine;

namespace KitchenDrinksMod.Boba
{
    public class CookedBobaPot : CustomItem
    {
        public override string UniqueNameID => "Boba Pot - Cooked";
        public override ItemCategory ItemCategory => ItemCategory.Generic;
        public override ItemStorage ItemStorageFlags => ItemStorage.StackableFood;
        public override ItemValue ItemValue => ItemValue.Small;
        public override float SplitSpeed => 1f;
        public override Item SplitSubItem => Refs.CookedBoba;
        public override int SplitCount => 3;
        public override GameObject Prefab => Prefabs.CookedBobaPot;
        public override Item DisposesTo => Refs.Pot;

        public override void OnRegister(GameDataObject gameDataObject)
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
