using KitchenData;
using KitchenDrinksMod.ToMoveToLibraryModLater.Registry;
using KitchenDrinksMod.Utils;
using KitchenLib.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace KitchenDrinksMod.Boba
{
    public class BobaProvider : ModAppliance
    {
        public override string UniqueNameID => "Source - Boba";
        public override string Name => "Boba Pearls";
        public override PriceTier PriceTier => PriceTier.Cheap;
        public override bool SellOnlyAsDuplicate => true;
        public override bool IsPurchasable => true;
        public override ShoppingTags ShoppingTags => ShoppingTags.Cooking | ShoppingTags.Misc;
        public override GameObject Prefab => Prefabs.BobaProvider;
        public override List<IApplianceProperty> Properties => new()
        {
            PropertyUtils.GetUnlimitedCItemProviderWithDirectInsert(Refs.UncookedBoba.ID)
        };
        public override IDictionary<Locale, ApplianceInfo> LocalisedInfo => new Dictionary<Locale, ApplianceInfo>()
        {
            { Locale.English, LocalisationUtils.CreateApplianceInfo("Boba Pearls", "Provides boba pearls", new(), new()) }
        };

        public override void OnRegister(GameDataObject gdo)
        {
            MaterialUtils.ApplyMaterial<MeshRenderer>(Prefab, "Block/Counter2/Counter", MaterialHelpers.GetMaterialArray("Wood 4 - Painted"));
            MaterialUtils.ApplyMaterial<MeshRenderer>(Prefab, "Block/Counter2/Counter Doors", MaterialHelpers.GetMaterialArray("Wood 4 - Painted"));
            MaterialUtils.ApplyMaterial<MeshRenderer>(Prefab, "Block/Counter2/Counter Surface", MaterialHelpers.GetMaterialArray("Wood - Default"));
            MaterialUtils.ApplyMaterial<MeshRenderer>(Prefab, "Block/Counter2/Counter Top", MaterialHelpers.GetMaterialArray("Wood - Default"));
            MaterialUtils.ApplyMaterial<MeshRenderer>(Prefab, "Block/Counter2/Handles", MaterialHelpers.GetMaterialArray("Knob"));
            MaterialUtils.ApplyMaterial<MeshRenderer>(Prefab, "Block/Counter2/Handles", MaterialHelpers.GetMaterialArray("Knob"));

            MaterialUtils.ApplyMaterial<MeshRenderer>(Prefab, "BobaBag", MaterialHelpers.GetMaterialArray("BobaBag"));
            foreach (var mesh in Prefab.GetChildFromPath("BobaBalls").GetComponentsInChildren<MeshRenderer>())
            {
                mesh.materials = MaterialHelpers.GetMaterialArray("UncookedBoba");
            }
        }
    }
}
