﻿using KitchenData;
using KitchenDrinksMod.Utils;
using KitchenLib.Customs;
using KitchenLib.Utils;
using UnityEngine;

namespace KitchenDrinksMod.Items
{
    public class MilkshakeVanilla : CustomItem
    {
        public override string UniqueNameID => "Milkshake - Vanilla";
        public override GameObject Prefab => Prefabs.MilkshakeVanilla;
        public override ItemCategory ItemCategory => ItemCategory.Generic;
        public override ItemStorage ItemStorageFlags => ItemStorage.StackableFood;

        public override void OnRegister(GameDataObject gameDataObject)
        {
            MaterialUtils.ApplyMaterial<MeshRenderer>(Prefab, "Model/Cup", MaterialHelpers.GetMaterialArray("Cup Base"));
            MaterialUtils.ApplyMaterial<MeshRenderer>(Prefab, "Model/Liquid", MaterialHelpers.GetMaterialArray("Vanilla"));
            MaterialUtils.ApplyMaterial<MeshRenderer>(Prefab, "Model/Straw", MaterialHelpers.GetMaterialArray("Straw"));
        }
    }
}
