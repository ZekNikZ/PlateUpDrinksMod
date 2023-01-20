using KitchenData;
using KitchenDrinksMod.ToMoveToLibraryModLater.Registry;
using KitchenDrinksMod.Utils;
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
            MaterialUtils.ApplyMaterial<MeshRenderer>(Prefab, "Block/Counter2/Counter", MaterialHelpers.GetMaterialArray("Wood 4 - Painted"));
            MaterialUtils.ApplyMaterial<MeshRenderer>(Prefab, "Block/Counter2/Counter Doors", MaterialHelpers.GetMaterialArray("Wood 4 - Painted"));
            MaterialUtils.ApplyMaterial<MeshRenderer>(Prefab, "Block/Counter2/Counter Surface", MaterialHelpers.GetMaterialArray("Wood - Default"));
            MaterialUtils.ApplyMaterial<MeshRenderer>(Prefab, "Block/Counter2/Counter Top", MaterialHelpers.GetMaterialArray("Wood - Default"));
            MaterialUtils.ApplyMaterial<MeshRenderer>(Prefab, "Block/Counter2/Handles", MaterialHelpers.GetMaterialArray("Knob"));
            MaterialUtils.ApplyMaterial<MeshRenderer>(Prefab, "Block/Counter2/Handles", MaterialHelpers.GetMaterialArray("Knob"));
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    MaterialUtils.ApplyMaterial<MeshRenderer>(Prefab, $"Cups/CupStack{i}/Cup{j}/Model/Cup", MaterialHelpers.GetMaterialArray("Cup Base"));
                }
            }
        }
    }
}
