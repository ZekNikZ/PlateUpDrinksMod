using KitchenData;
using KitchenDrinksMod.Appliances;
using KitchenDrinksMod.ToMoveToLibraryModLater.Dishes;
using KitchenDrinksMod.Utils;
using KitchenLib.Utils;
using System.Collections.Generic;
using UnityEngine;

namespace KitchenDrinksMod.Items
{
    public class Cup : ModItem<CupProvider>
    {
        public override string UniqueNameID => "Cup";
        public override GameObject Prefab => Prefabs.Cup;
        public override ItemStorage ItemStorageFlags => ItemStorage.StackableFood;
        public override List<Item.ItemProcess> Processes => new()
        {
            new Item.ItemProcess()
            {
                Process = Refs.DispenseBlackTea,
                Result = Refs.BlackTea,
                Duration = 1f
            },
            new Item.ItemProcess()
            {
                Process = Refs.DispenseMatchaTea,
                Result = Refs.MatchaTea,
                Duration = 1f
            },
            new Item.ItemProcess()
            {
                Process = Refs.DispenseTaroTea,
                Result = Refs.TaroTea,
                Duration = 1f
            }
        };

        protected override void Modify(Item item)
        {
            MaterialUtils.ApplyMaterial<MeshRenderer>(Prefab, "Model/Cup", MaterialHelpers.GetMaterialArray("Cup Base"));
        }
    }
}
