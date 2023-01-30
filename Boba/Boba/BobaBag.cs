using KitchenData;
using KitchenDrinksMod.Customs;
using KitchenDrinksMod.Util;
using UnityEngine;

namespace KitchenDrinksMod.Boba
{
    public class BobaBag : ModItem
    {
        public override string UniqueNameID => "Boba Bag";
        public override bool IsIndisposable => true;
        public override Item SplitSubItem => Refs.UncookedBoba;
        public override float SplitSpeed => 1;
        public override int SplitCount => 999;
        public override bool AllowSplitMerging => true;
        public override bool PreventExplicitSplit => true;
        public override GameObject Prefab => Prefabs.Find("BobaBagPrefab", "Base");

        protected override void Modify(Item item)
        {
            Prefab.ApplyMaterialToChild("BobaBag", "BobaBag");
            Prefab.GetChildFromPath("BobaBalls").ApplyMaterialToChildren("Ball", "UncookedBoba");
        }
    }
}
