using ApplianceLib.Api.Prefab;
using Kitchen;
using KitchenData;
using KitchenLib.Customs;
using KitchenLib.Utils;
using System.Collections.Generic;
using UnityEngine;

namespace KitchenDrinksMod.Boba
{
    public class BobaProvider : CustomAppliance
    {
        public override string UniqueNameID => "Boba - Source";
        public override PriceTier PriceTier => PriceTier.Medium;
        public override bool SellOnlyAsDuplicate => true;
        public override bool IsPurchasable => true;
        public override ShoppingTags ShoppingTags => ShoppingTags.Cooking | ShoppingTags.Misc;
        public override GameObject Prefab => Prefabs.Find("BobaProvider", "Base");
        public override List<(Locale, ApplianceInfo)> InfoList => new()
        {
            (Locale.English, LocalisationUtils.CreateApplianceInfo("Boba Pearls", "Provides boba pearls", new(), new()))
        };
        public override List<IApplianceProperty> Properties => new()
        {
            new CItemHolder(),
            KitchenPropertiesUtils.GetCItemProvider(Refs.BobaBag.ID, 1, 1, false, false, true, false, false, true, false)
        };

        public override void SetupPrefab(GameObject prefab)
        {
            prefab.AttachCounter(CounterType.DoubleDoors);

            prefab.ApplyMaterialToChild("HoldPoint/BobaBagPrefab/BobaBag", "BobaBag");
            prefab.GetChild("HoldPoint/BobaBagPrefab/BobaBalls").ApplyMaterialToChildren("Ball", "UncookedBoba");

            var holdTransform = prefab.GetChild("HoldPoint").transform;
            var holdPoint = prefab.AddComponent<HoldPointContainer>();
            holdPoint.HoldPoint = holdTransform;
            var sourceView = prefab.AddComponent<LimitedItemSourceView>();
            sourceView.HeldItemPosition = holdTransform;
            ReflectionUtils.GetField<LimitedItemSourceView>("Items").SetValue(sourceView, new List<GameObject>()
            {
                GameObjectUtils.GetChildObject(prefab, "HoldPoint/BobaBagPrefab")
            });
        }
    }
}
