using KitchenData;
using KitchenDrinksMod.Customs;
using KitchenDrinksMod.Util;
using KitchenLib.Utils;
using System.Collections.Generic;
using UnityEngine;

namespace KitchenDrinksMod.Boba
{
    public class BobaDish : ModDish
    {
        public override string UniqueNameID => "Boba Dish";
        public override DishType Type => DishType.Base;
        public override GameObject DisplayPrefab => Prefabs.Find("BobaCupIconPrefab");
        public override GameObject IconPrefab => Prefabs.Find("BobaCupIconPrefab");
        public override DishCustomerChange CustomerMultiplier => DishCustomerChange.SmallIncrease;
        public override Unlock.RewardLevel ExpReward => Unlock.RewardLevel.Medium;
        public override List<string> StartingNameSet => new()
        {
            "Quali-Tea",
            "The Perfect Matcha",
            "Taro-ific Teas",
            "The Tea Room",
            "Bubble Bubble"
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
                Item = Refs.ServedBlackTea,
                Phase = MenuPhase.Main,
                Weight = 1
            },
            new Dish.MenuItem()
            {
                Item = Refs.ServedMatchaTea,
                Phase = MenuPhase.Main,
                Weight = 1
            },
            new Dish.MenuItem()
            {
                Item = Refs.ServedTaroTea,
                Phase = MenuPhase.Main,
                Weight = 1
            }
        };
        public override Dictionary<Locale, string> Recipe => new Dictionary<Locale, string>
        {
            { Locale.English, "Add boba pearls to water in pot and cook. Combine cooked boba with appropriate tea, then add milk and serve" }
        };
        public override IDictionary<Locale, UnlockInfo> LocalisedInfo => new Dictionary<Locale, UnlockInfo>
        {
            { Locale.English, LocalisationUtils.CreateUnlockInfo("Boba Tea", "Adds boba teas as a main", "Offers three types of boba teas to enjoy") }
        };

        protected override void Modify(Dish dish)
        {
            DisplayPrefab.GetChildFromPath("BobaCupPrefab").SetupMaterialsLikeBobaCup("BlackTeaLiquid", "BlackIndicator");
        }
    }
}
