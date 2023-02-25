using ApplianceLib.Customs.GDO;
using Kitchen;
using KitchenData;
using KitchenDrinksMod.Util;
using KitchenLib.Utils;
using System.Collections.Generic;
using UnityEngine;

namespace KitchenDrinksMod.Boba
{
    public class BobaPotItemView : PositionSplittableView
    {
        internal void Setup(GameObject prefab)
        {
            var fFullPosition = ReflectionUtils.GetField<PositionSplittableView>("FullPosition");
            fFullPosition.SetValue(this, new Vector3(0, -0.009f, 0));

            var fEmptyPosition = ReflectionUtils.GetField<PositionSplittableView>("EmptyPosition");
            fEmptyPosition.SetValue(this, new Vector3(0, -0.194f, 0));

            var fObjects = ReflectionUtils.GetField<PositionSplittableView>("Objects");
            fObjects.SetValue(this, new List<GameObject>() { prefab.GetChild("BobaBalls") });
        }
    }

    public class CookedBobaPot : ModItem
    {
        public override string UniqueNameID => "Boba - Pot - Cooked";
        public override GameObject Prefab => Prefabs.Find("BobaPot", "Cooked");
        public override bool AllowSplitMerging => true;
        public override float SplitSpeed => 0.75f;
        public override int SplitCount => 5;
        public override Item SplitSubItem => Refs.CookedBoba;
        public override List<Item> SplitDepletedItems => new() { Refs.Pot };
        public override bool PreventExplicitSplit => true;
        public override Item DisposesTo => Refs.Pot;

        protected override void Modify(Item item)
        {
            Prefab.SetupMaterialsLikePot();
            Prefab.GetChildFromPath("BobaBalls").ApplyMaterialToChildren("Ball", "CookedBoba");

            Prefab.GetChildFromPath("BobaBalls").SetActive(true);

            if (!Prefab.HasComponent<BobaPotItemView>())
            {
                var view = Prefab.AddComponent<BobaPotItemView>();
                view.Setup(Prefab);
            }
        }
    }
}
