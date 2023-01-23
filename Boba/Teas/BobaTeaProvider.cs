﻿using Kitchen;
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
        public override GameObject Prefab => Prefabs.Find("TeaProvider");
        public override IDictionary<Locale, ApplianceInfo> LocalisedInfo => new Dictionary<Locale, ApplianceInfo>()
        {
            { Locale.English, LocalisationUtils.CreateApplianceInfo("Boba Teas", "Provides teas for boba", new(), new()) }
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
                Item = Refs.Cup.ID,
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
            MaterialUtils.ApplyMaterial<MeshRenderer>(Prefab, "Block/Counter2/Counter", MaterialHelpers.GetMaterialArray("Wood 4 - Painted"));
            MaterialUtils.ApplyMaterial<MeshRenderer>(Prefab, "Block/Counter2/Counter Doors", MaterialHelpers.GetMaterialArray("Wood 4 - Painted"));
            MaterialUtils.ApplyMaterial<MeshRenderer>(Prefab, "Block/Counter2/Counter Surface", MaterialHelpers.GetMaterialArray("Wood - Default"));
            MaterialUtils.ApplyMaterial<MeshRenderer>(Prefab, "Block/Counter2/Counter Top", MaterialHelpers.GetMaterialArray("Wood - Default"));
            MaterialUtils.ApplyMaterial<MeshRenderer>(Prefab, "Block/Counter2/Handles", MaterialHelpers.GetMaterialArray("Knob"));
            MaterialUtils.ApplyMaterial<MeshRenderer>(Prefab, "Block/Counter2/Handles", MaterialHelpers.GetMaterialArray("Knob"));
            var indicatorMats = new string[] { "BlackIndicator", "MatchaIndicator", "TaroIndicator" };
            for (int i = 1; i <= 3; i++)
            {
                MaterialUtils.ApplyMaterial<MeshRenderer>(Prefab, $"TeaDispenser{i}", MaterialHelpers.GetMaterialArray("DMAluminum", "DMBlackPlastic", indicatorMats[i - 1]));
                MaterialUtils.ApplyMaterial<MeshRenderer>(Prefab, $"TeaDispenser{i}/Indicator{i}", MaterialHelpers.GetMaterialArray("DMMatchaIndicator"));
            }

            Prefab.AddComponent<HoldPointContainer>();
            var view = Prefab.AddComponent<TeaProviderProcessView>();
            view.Setup(Prefab);
        }
    }
}
