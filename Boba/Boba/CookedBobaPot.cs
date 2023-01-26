using KitchenData;
using KitchenDrinksMod.Customs;
using KitchenDrinksMod.Util;
using System.Collections.Generic;
using UnityEngine;

namespace KitchenDrinksMod.Boba
{
    public class CookedBobaPot : ModItem
    {
        public override string UniqueNameID => "Boba - Pot - Cooked";
        public override GameObject Prefab => Prefabs.Find("BobaPot", "Cooked");
        public override bool AllowSplitMerging => true;
        public override float SplitSpeed => 1f;
        public override int SplitCount => 3;
        public override Item SplitSubItem => Refs.CookedBoba;
        public override List<Item> SplitDepletedItems => new() { Refs.Pot };
        public override bool PreventExplicitSplit => true;
        public override Item DisposesTo => Refs.Pot;

        protected override void Modify(Item item)
        {
            Prefab.SetupMaterialsLikePot();
            Prefab.GetChildFromPath("BobaBalls").ApplyMaterialToChildren("Ball", "CookedBoba");

            Prefab.GetChildFromPath("BobaBalls").SetActive(true);
        }
    }
}
