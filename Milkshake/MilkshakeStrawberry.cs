using KitchenData;
using KitchenDrinksMod.Util;
using KitchenLib.Customs;
using KitchenLib.Utils;
using UnityEngine;

namespace KitchenDrinksMod.Milkshakes
{
    public class MilkshakeStrawberry : CustomItem
    {
        public override string UniqueNameID => "Milkshake - Strawberry - Serving";
        public override GameObject Prefab => Prefabs.Find("Milkshake", "StrawberryServed");
        public override ItemCategory ItemCategory => ItemCategory.Generic;
        public override ItemStorage ItemStorageFlags => ItemStorage.StackableFood;
        public override ItemValue ItemValue => ItemValue.Small;

        public override void OnRegister(GameDataObject gameDataObject)
        {
            MaterialUtils.ApplyMaterial<MeshRenderer>(Prefab, "Model/Cup", MaterialHelpers.GetMaterialArray("Cup Base"));
            MaterialUtils.ApplyMaterial<MeshRenderer>(Prefab, "Model/Liquid", MaterialHelpers.GetMaterialArray("Strawberry"));
            MaterialUtils.ApplyMaterial<MeshRenderer>(Prefab, "Model/Straw", MaterialHelpers.GetMaterialArray("Straw"));
        }
    }
}
