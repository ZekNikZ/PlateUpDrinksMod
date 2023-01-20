﻿using KitchenData;
using KitchenDrinksMod.ToMoveToLibraryModLater.Dishes;
using KitchenLib.Utils;
using System.Collections.Generic;
using UnityEngine;

namespace KitchenDrinksMod.Dishes
{
    public class MilkshakeDish : ModDish
    {
        public override string UniqueNameID => "Milkshake Dish";
        public override DishType Type => DishType.Dessert;
        public override CardType CardType => CardType.Default;
        public override UnlockGroup UnlockGroup => UnlockGroup.Dish;
        public override GameObject DisplayPrefab => Prefabs.MilkshakeChocolate;
        public override GameObject IconPrefab => Prefabs.MilkshakeChocolate;
        public override float SelectionBias => 1;

        public override List<string> StartingNameSet => new()
        {
        };

        public override DishCustomerChange CustomerMultiplier => DishCustomerChange.SmallDecrease;

        public override bool IsUnlockable => true;

        public override Unlock.RewardLevel ExpReward => Unlock.RewardLevel.Medium;

        public override HashSet<Item> MinimumIngredients => new()
        {
            Refs.IceCreamVanilla,
            Refs.Milk,
            Refs.Cup
        };

        public override HashSet<Process> RequiredProcesses => new()
        {
            Refs.Shake
        };

        public override List<Dish.MenuItem> ResultingMenuItems => new()
        {
            new Dish.MenuItem()
            {
                Item = Refs.MilkshakeVanilla,
                Phase = MenuPhase.Dessert
            },
            new Dish.MenuItem()
            {
                Item = Refs.MilkshakeChocolate,
                Phase = MenuPhase.Dessert
            },
            new Dish.MenuItem()
            {
                Item = Refs.MilkshakeStrawberry,
                Phase = MenuPhase.Dessert
            }
        };

        public override IDictionary<Locale, string> LocalisedRecipe => new Dictionary<Locale, string>
        {
            { Locale.English, "Add ice cream and milk to cup and shake" }
        };

        public override IDictionary<Locale, UnlockInfo> LocalisedInfo => new Dictionary<Locale, UnlockInfo>
        {
            { Locale.English, LocalisationUtils.CreateUnlockInfo("Milkshake", "Adds milkshakes as a dessert", "Offers three flavours of milkshake as a dessert") }
        };
    }
}
