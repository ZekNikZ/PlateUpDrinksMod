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
    public class SodaProvider : CustomAppliance, IVariableProcessAppliance
    {
        public override string UniqueNameID => "Soda - Source";
        public override PriceTier PriceTier => PriceTier.Medium;
        public override bool SellOnlyAsDuplicate => true;
        public override bool IsPurchasable => true;
        public override ShoppingTags ShoppingTags => ShoppingTags.Cooking | ShoppingTags.Misc;
        public override GameObject Prefab => Prefabs.Find("SodaProvider", "Base");
        public override List<(Locale, ApplianceInfo)> InfoList => new()
        {
            (Locale.English, LocalisationUtils.CreateApplianceInfo("Fountain Drinks", "Provides soda", new(), new()))
        };
        public override List<IApplianceProperty> Properties => new()
        {
            new CItemHolder(),
            new CVariableProcessContainer()
            {
                Current = 0
            }
        };
        public List<Appliance.ApplianceProcesses> VariableApplianceProcesses => new()
        {
            new()
            {
                Process = Refs.DispenseRedSoda,
                Speed = 1.25f
            },
            new()
            {
                Process = Refs.DispenseGreenSoda,
                Speed = 1.25f
            },
            new()
            {
                Process = Refs.DispenseBlueSoda,
                Speed = 1.25f
            }
        };

        internal class SodaProviderProcessView : VariableProcessView
        {
            internal void Setup(GameObject prefab)
            {
                Animator = prefab.GetComponent<Animator>();
                HoldPointContainer = prefab.GetComponent<HoldPointContainer>();
                HoldPoints = new()
                {
                    prefab.GetChild("HoldPoint1").transform,
                    prefab.GetChild("HoldPoint2").transform,
                    prefab.GetChild("HoldPoint3").transform,
                };
            }
        }

        public override void OnRegister(Appliance appliance)
        {
            NotActuallyProviders.RemoveProvidersFrom(appliance);
            AutomatableAppliances.MakeAutomatable(AutomatableAppliances.Automator.Portioner, this);
        }

        public override void SetupPrefab(GameObject prefab)
        {
            prefab.AttachCounter(CounterType.Drawers);

            GameObject dispenser = prefab.GetChild("SodaDispenser");
            dispenser.ApplyMaterialToChild("Back", "DMBlackPlastic", "MetalLight", "MetalDark");
            dispenser.ApplyMaterialToChild("Base", "DMBlackPlastic", "MetalDark");
            dispenser.ApplyMaterialToChild("BumpOut", "MetalLight", "DMBlackPlastic");

            var indicatorMats = new string[] { "RedLiquid", "GreenLiquid", "BlueLiquid" };
            for (int i = 1; i <= 3; i++)
            {
                dispenser.ApplyMaterialToChild($"Flavor{i}", indicatorMats[i - 1]);
                dispenser.ApplyMaterialToChild($"Indicator{i}", indicatorMats[i - 1]);
                dispenser.ApplyMaterialToChild($"Nozzle{i}", "DMBlackPlastic");
            }

            prefab.AddComponent<HoldPointContainer>();
            var view = prefab.AddComponent<SodaProviderProcessView>();
            view.Setup(prefab);
        }
    }
}
