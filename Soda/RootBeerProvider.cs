using ApplianceLib.Api;
using ApplianceLib.Api.Prefab;
using Kitchen;
using KitchenData;
using KitchenLib.Customs;
using KitchenLib.Utils;
using System.Collections.Generic;
using UnityEngine;

namespace KitchenDrinksMod.Soda
{
    public class RootBeerProvider : CustomAppliance
    {
        public override string UniqueNameID => "RootBeer - Source";
        public override PriceTier PriceTier => PriceTier.Medium;
        public override bool SellOnlyAsDuplicate => true;
        public override bool IsPurchasable => true;
        public override ShoppingTags ShoppingTags => ShoppingTags.Cooking | ShoppingTags.Misc;
        public override GameObject Prefab => Prefabs.Find("RootBeer");
        public override List<(Locale, ApplianceInfo)> InfoList => new()
        {
            (Locale.English, LocalisationUtils.CreateApplianceInfo("Root Beer Keg", "Provides root beer", new(), new()))
        };
        public override List<IApplianceProperty> Properties => new()
        {
            new CItemHolder(),
        };
        public override List<Appliance.ApplianceProcesses> Processes => new()
        {
            new()
            {
                Process = Refs.DispenseRootBeer,
                Speed = 1.25f
            },
        };

        public override void OnRegister(Appliance appliance)
        {
            NotActuallyProviders.RemoveProvidersFrom(appliance);
            AutomatableAppliances.MakeAutomatable(AutomatableAppliances.Automator.Portioner, this);
        }

        public override void SetupPrefab(GameObject prefab)
        {
            prefab.AttachCounter(CounterType.Drawers);

            prefab.ApplyMaterialToChild("barrel/barrel", "Metal - Dirty", "Wood 1", "Wood 2");

            //GameObject dispenser = prefab.GetChild("SodaDispenser");
            //dispenser.ApplyMaterialToChild("Back", "DMBlackPlastic", "MetalLight", "MetalDark");
            //dispenser.ApplyMaterialToChild("Base", "DMBlackPlastic", "MetalDark");
            //dispenser.ApplyMaterialToChild("BumpOut", "MetalLight", "DMBlackPlastic");

            //var indicatorMats = new string[] { "drinkup:root_beer_liquid", "drinkup:root_beer_liquid", "drinkup:root_beer_liquid" };
            //for (int i = 1; i <= 3; i++)
            //{
            //    dispenser.ApplyMaterialToChild($"Flavor{i}", indicatorMats[i - 1]);
            //    dispenser.ApplyMaterialToChild($"Indicator{i}", indicatorMats[i - 1]);
            //    dispenser.ApplyMaterialToChild($"Nozzle{i}", "DMBlackPlastic");
            //}

            var holdPointContainer = prefab.AddComponent<HoldPointContainer>();
            holdPointContainer.HoldPoint = prefab.GetChild("HoldPoint").transform;
        }
    }
}
