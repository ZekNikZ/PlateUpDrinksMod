﻿using Kitchen;
using KitchenData;
using KitchenDrinksMod.Customs;
using KitchenDrinksMod.Util;
using System.Collections.Generic;
using UnityEngine;

namespace KitchenDrinksMod.Cups
{
    public class MilkInCup : ModItemGroup
    {
        public override string UniqueNameID => "MilkInCup";
        public override string ColourBlindTag => "Mi";
        public override ItemStorage ItemStorageFlags => ItemStorage.StackableFood;
        public override GameObject Prefab => Prefabs.Find("SodaCupPrefab", "Milk");
        protected override Vector3 ColorblindLabelPosition => new(0, 0.7f, 0);
        protected override bool AddColorblindLabel => false;

        public override List<ItemGroup.ItemSet> Sets => new()
        {
            new ()
            {
                Max = 2,
                Min = 2,
                Items = new()
                {
                    Refs.Cup,
                    Refs.MilkIngredient
                }
            }
        };

        public override List<Item.ItemProcess> Processes => new()
        {
            new Item.ItemProcess()
            {
                Process = Refs.DispenseBlackTea,
                Result = Refs.BlackTeaWithMilk,
                Duration = 1f
            },
            new Item.ItemProcess()
            {
                Process = Refs.DispenseMatchaTea,
                Result = Refs.MatchaTeaWithMilk,
                Duration = 1f
            },
            new Item.ItemProcess()
            {
                Process = Refs.DispenseTaroTea,
                Result = Refs.TaroTeaWithMilk,
                Duration = 1f
            }
        };

        protected override void Modify(ItemGroup item)
        {
            Prefab.SetupMaterialsLikeSodaCup("Milk");
            Prefab.GetChild("Model/Straw").SetActive(false);
        }
    }
}