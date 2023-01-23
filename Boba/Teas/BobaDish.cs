using KitchenData;
using KitchenDrinksMod.Customs;
using KitchenLib.Utils;
using System.Collections.Generic;
using UnityEngine;

namespace KitchenDrinksMod.Boba
{
    public class BobaDish : ModDish
    {
        public override string UniqueNameID => "Boba Dish";
        public override DishType Type => DishType.Base;
        public override GameObject DisplayPrefab => Prefabs.BobaIcon;
        public override GameObject IconPrefab => Prefabs.BobaIcon;
        public override DishCustomerChange CustomerMultiplier => DishCustomerChange.SmallDecrease;
        public override Unlock.RewardLevel ExpReward => Unlock.RewardLevel.Medium;
        public override List<string> StartingNameSet => new()
        {
            "Boba Tea"
        };
        public override HashSet<Item> MinimumIngredients => new()
        {
            Refs.BlackTea,
            Refs.Pot,
            Refs.UncookedBoba,
            Refs.Cup,
            Refs.Water,
            Refs.Milk
        };
        public override HashSet<Process> RequiredProcesses => new()
        {
            Refs.Cook
        };
        public override List<Dish.MenuItem> ResultingMenuItems => new()
        {
            new Dish.MenuItem()
            {
                Item = Refs.BlackTeaCombined,
                Phase = MenuPhase.Main
            },
            new Dish.MenuItem()
            {
                Item = Refs.MatchaTeaCombined,
                Phase = MenuPhase.Main
            },
            new Dish.MenuItem()
            {
                Item = Refs.TaroTeaCombined,
                Phase = MenuPhase.Main
            }
        };
        public override IDictionary<Locale, string> LocalisedRecipe => new Dictionary<Locale, string>
        {
            { Locale.English, "Add boba pearls to water in pot and cook. Combine cooked boba with appropriate tea, then add milk and serve" }
        };
        public override IDictionary<Locale, UnlockInfo> LocalisedInfo => new Dictionary<Locale, UnlockInfo>
        {
            { Locale.English, LocalisationUtils.CreateUnlockInfo("Boba Tea", "Adds boba teas as a main", "Offers three types of boba teas to enjoy") }
        };
    }
}
