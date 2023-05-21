using ApplianceLib.Api;
using ApplianceLib.Api.Prefab;
using Kitchen;
using KitchenData;
using KitchenLib.Customs;
using KitchenLib.Utils;
using System.Collections.Generic;
using UnityEngine;

namespace KitchenDrinksMod.Boba
{
    public class BobaTeaProvider : CustomAppliance, IVariableProcessAppliance
    {
        public override string UniqueNameID => "Boba Tea - Source";
        public override PriceTier PriceTier => PriceTier.Medium;
        public override bool SellOnlyAsDuplicate => true;
        public override bool IsPurchasable => true;
        public override ShoppingTags ShoppingTags => ShoppingTags.Cooking | ShoppingTags.Misc;
        public override GameObject Prefab => Prefabs.Find("TeaProvider", "Base");
        public override List<(Locale, ApplianceInfo)> InfoList => new()
        {
            (Locale.English, LocalisationUtils.CreateApplianceInfo("Boba Teas", "Provides teas for boba", new(), new()))
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
                Process = Refs.DispenseBlackTea,
                Speed = 1.25f
            },
            new()
            {
                Process = Refs.DispenseMatchaTea,
                Speed = 1.25f
            },
            new()
            {
                Process = Refs.DispenseTaroTea,
                Speed = 1.25f
            }
        };

        internal class TeaProviderProcessView : VariableProcessView
        {
            internal void Setup(GameObject prefab)
            {
                Animator = prefab.GetComponent<Animator>();
                HoldPointContainer = prefab.GetComponent<HoldPointContainer>();
                HoldPoints = new()
                {
                    prefab.GetChild("TeaDispenser1/HoldPoint1").transform,
                    prefab.GetChild("TeaDispenser2/HoldPoint2").transform,
                    prefab.GetChild("TeaDispenser3/HoldPoint3").transform,
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

            var indicatorMats = new string[] { "BlackTeaLiquid", "MatchaTeaLiquid", "TaroTeaLiquid" };
            for (int i = 1; i <= 3; i++)
            {
                prefab.ApplyMaterialToChild($"TeaDispenser{i}", "DMAluminum", "DMBlackPlastic", indicatorMats[i - 1]);
                prefab.ApplyMaterialToChild($"TeaDispenser{i}/Indicator{i}", indicatorMats[i - 1]);
            }

            prefab.AddComponent<HoldPointContainer>();
            var view = prefab.AddComponent<TeaProviderProcessView>();
            view.Setup(Prefab);
        }
    }
}
