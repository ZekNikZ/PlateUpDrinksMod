using KitchenLib.Customs;
using KitchenLib.Utils;
using UnityEngine;

namespace KitchenDrinksMod.Boba
{
    public class DirtyBobaCup : CustomItem
    {
        public override string UniqueNameID => "Cup - Boba - Dirty";
        public override GameObject Prefab => Prefabs.Find("DirtyBobaCupPrefab");

        public override void SetupPrefab(GameObject prefab)
        {
            prefab.ApplyMaterialToChild("Cup", "BobaCup");
            prefab.ApplyMaterialToChild("Liquid1", "Milk");
            prefab.ApplyMaterialToChild("Lid", "CookedBoba");
            prefab.ApplyMaterialToChild("Straw", "Straw");

            prefab.GetChild("Boba").ApplyMaterialToChildren("Boba", "CookedBoba");
        }
    }
}
