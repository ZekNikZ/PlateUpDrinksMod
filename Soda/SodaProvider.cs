using Kitchen;
using KitchenData;
using KitchenDrinksMod.Customs;
using KitchenDrinksMod.Util;
using KitchenLib.Utils;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace KitchenDrinksMod.Soda
{
    public class SodaProvider : ModAppliance
    {
        public override string UniqueNameID => "Soda - Source";
        public override string Name => "Fountain Drinks";
        public override PriceTier PriceTier => PriceTier.Medium;
        public override bool SellOnlyAsDuplicate => true;
        public override bool IsPurchasable => true;
        public override ShoppingTags ShoppingTags => ShoppingTags.Cooking | ShoppingTags.Misc;
        public override GameObject Prefab => Prefabs.Find("SodaProvider", "Base");
        public override IDictionary<Locale, ApplianceInfo> LocalisedInfo => new Dictionary<Locale, ApplianceInfo>()
        {
            { Locale.English, LocalisationUtils.CreateApplianceInfo("Fountain Drinks", "Provides soda", new(), new()) }
        };
        public override List<IApplianceProperty> Properties => new()
        {
            new CItemHolder(),
            new CItemProvider(),
            new CVariableProcessContainer()
            {
                Current = 0,
                Max = 3
            }
        };
        public override List<VariableApplianceProcess> VariableApplianceProcesses => new()
        {
            new VariableApplianceProcess
            {
                Items = new ItemList(Refs.Cup.ID),
                Processes = new()
                {
                    Refs.DispenseRedSodaApplianceProcess,
                    Refs.DispenseGreenSodaApplianceProcess,
                    Refs.DispenseBlueSodaApplianceProcess
                }
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
                    prefab.GetChildFromPath("HoldPoint1").transform,
                    prefab.GetChildFromPath("HoldPoint2").transform,
                    prefab.GetChildFromPath("HoldPoint3").transform,
                };
                HoldPointContainer.HoldPoint = HoldPoints.First();
            }
        }

        protected override void Modify(Appliance appliance)
        {
            Prefab.SetupMaterialsLikeCounter();
            GameObject dispenser = Prefab.GetChildFromPath("SodaDispenser");
            dispenser.ApplyMaterialToChild("Back", "DMBlackPlastic", "MetalLight", "MetalDark")
                .ApplyMaterialToChild("Base", "DMBlackPlastic", "MetalDark")
                .ApplyMaterialToChild("BumpOut", "MetalLight", "DMBlackPlastic");

            var indicatorMats = new string[] { "RedLiquid", "GreenLiquid", "BlueLiquid" };
            for (int i = 1; i <= 3; i++)
            {
                dispenser.ApplyMaterialToChild($"Flavor{i}", indicatorMats[i - 1]);
                dispenser.ApplyMaterialToChild($"Indicator{i}", indicatorMats[i - 1]);
                dispenser.ApplyMaterialToChild($"Nozzle{i}", "DMBlackPlastic");
            }

            Prefab.AddComponent<HoldPointContainer>();
            var view = Prefab.AddComponent<SodaProviderProcessView>();
            view.Setup(Prefab);
        }
    }
}
