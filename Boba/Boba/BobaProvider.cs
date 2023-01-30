using Kitchen;
using KitchenData;
using KitchenDrinksMod.Customs;
using KitchenDrinksMod.Util;
using KitchenLib.Utils;
using System.Collections.Generic;
using UnityEngine;

namespace KitchenDrinksMod.Boba
{
    public class BobaProvider : ModAppliance
    {
        public override string UniqueNameID => "Boba - Source";
        public override string Name => "Boba Pearls";
        public override PriceTier PriceTier => PriceTier.Medium;
        public override bool SellOnlyAsDuplicate => true;
        public override bool IsPurchasable => true;
        public override ShoppingTags ShoppingTags => ShoppingTags.Cooking | ShoppingTags.Misc;
        public override GameObject Prefab => Prefabs.Find("BobaProvider", "Base");
        public override IDictionary<Locale, ApplianceInfo> LocalisedInfo => new Dictionary<Locale, ApplianceInfo>()
        {
            { Locale.English, LocalisationUtils.CreateApplianceInfo("Boba Pearls", "Provides boba pearls", new(), new()) }
        };
        public override List<IApplianceProperty> Properties => new()
        {
            new CItemHolder(),
            KitchenPropertiesUtils.GetCItemProvider(Refs.BobaBag.ID, 1, 1, false, false, true, false, false, true, false)
        };

        protected override void Modify(Appliance appliance)
        {
            Prefab.SetupMaterialsLikeCounter();

            Prefab.ApplyMaterialToChild("HoldPoint/BobaBagPrefab/BobaBag", "BobaBag");
            Prefab.GetChildFromPath("HoldPoint/BobaBagPrefab/BobaBalls").ApplyMaterialToChildren("Ball", "UncookedBoba");

            var holdTransform = Prefab.GetChildFromPath("HoldPoint").transform;
            var holdPoint = Prefab.AddComponent<HoldPointContainer>();
            holdPoint.HoldPoint = holdTransform;
            var sourceView = Prefab.AddComponent<LimitedItemSourceView>();
            sourceView.HeldItemPosition = holdTransform;
            ReflectionUtils.GetField<LimitedItemSourceView>("Items").SetValue(sourceView, new List<GameObject>()
            {
                GameObjectUtils.GetChildObject(Prefab, "HoldPoint/BobaBagPrefab")
            });
        }
    }
}
