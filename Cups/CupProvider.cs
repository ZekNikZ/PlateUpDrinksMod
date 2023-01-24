using KitchenData;
using KitchenDrinksMod.Customs;
using KitchenDrinksMod.Util;
using KitchenLib.Utils;
using System.Collections.Generic;
using UnityEngine;

namespace KitchenDrinksMod.Cups
{
    public class CupProvider : ModAppliance
    {
        public override string UniqueNameID => "Cups - Source";
        public override string Name => "Cups";
        public override PriceTier PriceTier => PriceTier.Medium;
        public override bool SellOnlyAsDuplicate => true;
        public override bool IsPurchasable => true;
        public override ShoppingTags ShoppingTags => ShoppingTags.Cooking | ShoppingTags.Misc;
        public override GameObject Prefab => Prefabs.Find("CupProvider");
        public override IDictionary<Locale, ApplianceInfo> LocalisedInfo => new Dictionary<Locale, ApplianceInfo>()
        {
            { Locale.English, LocalisationUtils.CreateApplianceInfo("Cups", "Provides cups", new(), new()) }
        };
        public override List<IApplianceProperty> Properties => new()
        {
            KitchenPropertiesUtils.GetUnlimitedCItemProvider(Refs.Cup.ID)
        };

        protected override void Modify(Appliance appliance)
        {
            Prefab.SetupMaterialsLikeCounter();

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    Prefab.ApplyMaterialToChild($"Cups/CupStack{i}/Cup{j}/Model/Cup", "CupBase");
                }
            }
        }
    }
}
