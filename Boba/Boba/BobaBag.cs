using KitchenData;
using KitchenLib.Customs;
using KitchenLib.Utils;
using UnityEngine;

namespace KitchenDrinksMod.Boba
{
    public class BobaBag : CustomItem
    {
        public override string UniqueNameID => "Boba Bag";
        public override bool IsIndisposable => true;
        public override Item SplitSubItem => Refs.UncookedBoba;
        public override float SplitSpeed => 1;
        public override int SplitCount => 999;
        public override bool AllowSplitMerging => true;
        public override bool PreventExplicitSplit => true;
        public override GameObject Prefab => Prefabs.Find("BobaBagPrefab", "Base");

        public override void SetupPrefab(GameObject prefab)
        {
            prefab.ApplyMaterialToChild("BobaBag", "BobaBag");
            prefab.GetChild("BobaBalls").ApplyMaterialToChildren("Ball", "UncookedBoba");
        }
    }
}
