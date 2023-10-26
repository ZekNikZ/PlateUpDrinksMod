using KitchenData;
using KitchenDrinksMod.Util;
using KitchenLib.Customs;
using KitchenLib.Utils;
using System.Collections.Generic;
using UnityEngine;

namespace KitchenDrinksMod.Boba
{
    public class BobaDish : CustomDish
    {
        public override string UniqueNameID => "Boba Dish";
        public override DishType Type => DishType.Base;
        public override GameObject DisplayPrefab => Prefabs.Find("BobaCupIconPrefab");
        public override GameObject IconPrefab => Prefabs.Find("BobaCupIconPrefab");
        public override DishCustomerChange CustomerMultiplier => DishCustomerChange.LargeIncrease;
        public override Unlock.RewardLevel ExpReward => Unlock.RewardLevel.Medium;
        public override UnlockGroup UnlockGroup => UnlockGroup.Dish;
        public override bool IsUnlockable => true;
        public override bool RequiredNoDishItem => true;
        public override bool IsAvailableAsLobbyOption => true;
        public override int Difficulty => 2;
        public override List<string> StartingNameSet => new()
        {
            "Quali-Tea Drinks",
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
        public override List<(Locale, UnlockInfo)> InfoList => new()
        {
            (Locale.English, LocalisationUtils.CreateUnlockInfo("Boba Tea", "Adds boba teas as a main", "Offers three types of boba teas to enjoy"))
        };

        public override void SetupDisplayPrefab(GameObject prefab)
        {
            prefab.GetChild("BobaCupPrefab").SetupMaterialsLikeBobaCup("BlackTeaLiquid", "BlackIndicator");
        }
    }
}
