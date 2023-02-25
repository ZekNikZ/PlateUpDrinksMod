using ApplianceLib.Customs.GDO;
using KitchenData;
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

        protected override void SetupPrefab(GameObject prefab)
        {
            prefab.ApplyMaterialToChild("BobaBag", "BobaBag");
            prefab.GetChildFromPath("BobaBalls").ApplyMaterialToChildren("Ball", "UncookedBoba");
        }
    }
}
