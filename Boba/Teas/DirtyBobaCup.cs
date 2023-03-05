using ApplianceLib.Customs.GDO;
using KitchenDrinksMod.Util;
using UnityEngine;

namespace KitchenDrinksMod.Boba
{
    public class DirtyBobaCup : ModItem
    {
        public override string UniqueNameID => "Cup - Boba - Dirty";
        public override GameObject Prefab => Prefabs.Find("DirtyBobaCupPrefab");

        protected override void SetupPrefab(GameObject prefab)
        {
            prefab
               .ApplyMaterialToChild("Cup", "BobaCup")
               .ApplyMaterialToChild("Liquid1", "Milk")
               .ApplyMaterialToChild("Lid", "CookedBoba")
               .ApplyMaterialToChild("Straw", "Straw");

            prefab.GetChildFromPath("Boba").ApplyMaterialToChildren("Boba", "CookedBoba");
        }
    }
}
