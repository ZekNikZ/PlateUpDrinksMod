using KitchenData;
using KitchenDrinksMod.Customs;
using KitchenLib.Utils;
using System.Collections.Generic;

namespace KitchenDrinksMod.Milkshakes
{
    public class MilkshakeDish : ModDish
    {
        public override string UniqueNameID => "Milkshake Dish";
        public override DishType Type => DishType.Dessert;
        public override DishCustomerChange CustomerMultiplier => DishCustomerChange.SmallDecrease;
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
                Item = Refs.ServedVanillaMilkshake,
                Phase = MenuPhase.Dessert,
                Weight = 1
            },
            new Dish.MenuItem()
            {
                Item = Refs.ServedChocolateMilkshake,
                Phase = MenuPhase.Dessert,
                Weight = 1
            },
            new Dish.MenuItem()
            {
                Item = Refs.ServedStrawberryMilkshake,
                Phase = MenuPhase.Dessert,
                Weight = 1
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
