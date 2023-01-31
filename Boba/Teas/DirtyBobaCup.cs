using KitchenData;
using KitchenDrinksMod.Customs;
using KitchenDrinksMod.Util;
using UnityEngine;

namespace KitchenDrinksMod.Boba
{
    public class DirtyBobaCup : ModItem
    {
        public override string UniqueNameID => "Cup - Boba - Dirty";
        public override GameObject Prefab => Prefabs.Find("DirtyBobaCupPrefab");

        protected override void Modify(Item item)
        {
            Prefab
               .ApplyMaterialToChild("Cup", "BobaCup")
               .ApplyMaterialToChild("Liquid1", "Milk")
               .ApplyMaterialToChild("Lid", "CookedBoba")
               .ApplyMaterialToChild("Straw", "Straw");

            Prefab.GetChildFromPath("Boba").ApplyMaterialToChildren("Boba", "CookedBoba");
        }
    }
}
