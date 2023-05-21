using KitchenData;
using KitchenLib.Customs;
using KitchenLib.Utils;
using System.Collections.Generic;

namespace KitchenDrinksMod.Milkshakes
{
    public class MilkshakeDish : CustomDish
    {
        public override string UniqueNameID => "Milkshake Dish";
        public override DishType Type => DishType.Dessert;
        public override DishCustomerChange CustomerMultiplier => DishCustomerChange.SmallDecrease;
        public override Unlock.RewardLevel ExpReward => Unlock.RewardLevel.Medium;
        public override UnlockGroup UnlockGroup => UnlockGroup.Dish;
        public override bool IsUnlockable => true;
        public override bool RequiredNoDishItem => true;
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
        public override Dictionary<Locale, string> Recipe => new()
        {
            { Locale.English, "Add ice cream and milk to cup and shake" }
        };
        public override List<(Locale, UnlockInfo)> InfoList => new()
        {
            (Locale.English, LocalisationUtils.CreateUnlockInfo("Milkshakes", "Adds milkshakes as a dessert", "Offers three flavours of milkshake as a dessert"))
        };
    }
}
