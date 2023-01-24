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
        public override GameObject Prefab => Prefabs.Find("BobaProvider");
        public override IDictionary<Locale, ApplianceInfo> LocalisedInfo => new Dictionary<Locale, ApplianceInfo>()
        {
            { Locale.English, LocalisationUtils.CreateApplianceInfo("Boba Pearls", "Provides boba pearls", new(), new()) }
        };
        public override List<IApplianceProperty> Properties => new()
        {
            Utils.GetUnlimitedCItemProviderWithDirectInsert(Refs.UncookedBoba.ID)
        };

        protected override void Modify(Appliance appliance)
        {
            Prefab.SetupMaterialsLikeCounter()
                .ApplyMaterialToChild("BobaBag", "BobaBag");

            Prefab.GetChildFromPath("BobaBalls").ApplyMaterialToChildren("UncookedBoba");
        }
    }
}
