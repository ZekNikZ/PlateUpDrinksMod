using KitchenData;
using KitchenDrinksMod.Utils;
using KitchenLib.Customs;
using KitchenLib.Utils;
using System.Collections.Generic;
using UnityEngine;

namespace KitchenDrinksMod.Items
{
    public class Cup : CustomItem
    {
        public override string UniqueNameID => "Cup";
        public override GameObject Prefab => Prefabs.Cup;
        public override ItemCategory ItemCategory => ItemCategory.Generic;
        public override ItemStorage ItemStorageFlags => ItemStorage.StackableFood;
        public override Appliance DedicatedProvider => Refs.CupProvider;
        public override List<Item.ItemProcess> Processes => new()
        {
            new Item.ItemProcess()
            {
                Process = Refs.DispenseBlackTea,
                Result = Refs.MilkshakeVanilla,
                Duration = 1f
            },
            new Item.ItemProcess()
            {
                Process = Refs.DispenseMatchaTea,
                Result = Refs.MilkshakeChocolate,
                Duration = 1f
            },
            new Item.ItemProcess()
            {
                Process = Refs.DispenseTaroTea,
                Result = Refs.MilkshakeStrawberry,
                Duration = 1f
            }
        };

        public override void OnRegister(GameDataObject gameDataObject)
        {
            MaterialUtils.ApplyMaterial<MeshRenderer>(Prefab, "Model/Cup", MaterialHelpers.GetMaterialArray("Cup Base"));
        }
    }
}
