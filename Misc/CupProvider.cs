using KitchenData;
using KitchenDrinksMod.Registry;
using KitchenLib.Utils;
using System.Collections.Generic;
using UnityEngine;

namespace KitchenDrinksMod.Appliances
{
    public class CupProvider : ModAppliance
    {
        public override string UniqueNameID => "Source - Cups";
        public override string Name => "Cups";
        public override PriceTier PriceTier => PriceTier.Cheap;
        public override bool SellOnlyAsDuplicate => true;
        public override bool IsPurchasable => true;
        public override ShoppingTags ShoppingTags => ShoppingTags.Cooking | ShoppingTags.Misc;
        public override GameObject Prefab => Prefabs.CupProvider;
        public override List<IApplianceProperty> Properties => new()
        {
            KitchenPropertiesUtils.GetUnlimitedCItemProvider(Refs.Cup.ID)
        };
        public override IDictionary<Locale, ApplianceInfo> LocalisedInfo => new Dictionary<Locale, ApplianceInfo>()
        {
            { Locale.English, LocalisationUtils.CreateApplianceInfo("Cups", "Provides cups", new(), new()) }
        };

        public override void OnRegister(GameDataObject gdo)
        {
            gdo.name = "Source - Cups";
        }
    }
}
