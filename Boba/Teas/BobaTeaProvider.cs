using Kitchen;
using KitchenData;
using KitchenDrinksMod.Customs;
using KitchenDrinksMod.Util;
using KitchenLib.Utils;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace KitchenDrinksMod.Boba
{
    public class BobaTeaProvider : ModAppliance
    {
        public override string UniqueNameID => "Boba Tea - Source";
        public override string Name => "Boba Teas";
        public override PriceTier PriceTier => PriceTier.Medium;
        public override bool SellOnlyAsDuplicate => true;
        public override bool IsPurchasable => true;
        public override ShoppingTags ShoppingTags => ShoppingTags.Cooking | ShoppingTags.Misc;
        public override GameObject Prefab => Prefabs.Find("TeaProvider", "Base");
        public override IDictionary<Locale, ApplianceInfo> LocalisedInfo => new Dictionary<Locale, ApplianceInfo>()
        {
            { Locale.English, LocalisationUtils.CreateApplianceInfo("Boba Teas", "Provides teas for boba", new(), new()) }
        };
        public override List<IApplianceProperty> Properties => new()
        {
            new CItemHolder(),
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
                Items = new ItemList(Refs.Cup.ID, Refs.MilkInCup.ID),
                Processes = new()
                {
                    Refs.DispenseBlackTeaApplianceProcess,
                    Refs.DispenseMatchaTeaApplianceProcess,
                    Refs.DispenseTaroTeaApplianceProcess
                }
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
                    prefab.GetChildFromPath("TeaDispenser1/HoldPoint1").transform,
                    prefab.GetChildFromPath("TeaDispenser2/HoldPoint2").transform,
                    prefab.GetChildFromPath("TeaDispenser3/HoldPoint3").transform,
                };
                HoldPointContainer.HoldPoint = HoldPoints.First();
            }
        }

        protected override void Modify(Appliance appliance)
        {
            Prefab.SetupMaterialsLikeCounter();
            var indicatorMats = new string[] { "BlackTeaLiquid", "MatchaTeaLiquid", "TaroTeaLiquid" };
            for (int i = 1; i <= 3; i++)
            {
                Prefab.ApplyMaterialToChild($"TeaDispenser{i}", "DMAluminum", "DMBlackPlastic", indicatorMats[i - 1]);
                Prefab.ApplyMaterialToChild($"TeaDispenser{i}/Indicator{i}", indicatorMats[i - 1]);
            }

            Prefab.AddComponent<HoldPointContainer>();
            var view = Prefab.AddComponent<TeaProviderProcessView>();
            view.Setup(Prefab);
        }
    }
}
