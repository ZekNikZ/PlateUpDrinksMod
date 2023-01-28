using KitchenData;
using KitchenDrinksMod.Customs;
using KitchenDrinksMod.Util;
using System.Collections.Generic;
using UnityEngine;

namespace KitchenDrinksMod.Cups
{
    public class Cup : ModItem<CupProvider>
    {
        public override string UniqueNameID => "Cup";
        public override GameObject Prefab => Prefabs.Find("Cup");
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
            },
            new Item.ItemProcess()
            {
                Process = Refs.DispenseRedSoda,
                Result = Refs.RedSoda,
                Duration = 1f
            },
            new Item.ItemProcess()
            {
                Process = Refs.DispenseGreenSoda,
                Result = Refs.GreenSoda,
                Duration = 1f
            },
            new Item.ItemProcess()
            {
                Process = Refs.DispenseBlueSoda,
                Result = Refs.BlueSoda,
                Duration = 1f
            }
        };

        protected override void Modify(Item item)
        {
            Prefab.ApplyMaterialToChild("Model/Cup", "CupBase");
        }
    }
}
