using KitchenData;
using KitchenLib.Customs;
using KitchenLib.Utils;
using System.Collections.Generic;

namespace KitchenDrinksMod.Soda
{
    public class RootBeerDish : CustomDish
    {
        public override string UniqueNameID => "RootBeer Dish";
        public override DishType Type => DishType.Starter;
        public override DishCustomerChange CustomerMultiplier => DishCustomerChange.None;
        public override Unlock.RewardLevel ExpReward => Unlock.RewardLevel.Medium;
        public override UnlockGroup UnlockGroup => UnlockGroup.Dish;
        public override bool IsUnlockable => true;
        public override bool RequiredNoDishItem => true;
        public override int Difficulty => 1;
        public override HashSet<Item> MinimumIngredients => new()
        {
            Refs.RootBeer,
            Refs.Cup
        };
        public override HashSet<Process> RequiredProcesses => new()
        {
        };
        public override List<Dish.MenuItem> ResultingMenuItems => new()
        {
            new Dish.MenuItem()
            {
                Item = Refs.RootBeer,
                Phase = MenuPhase.Starter,
                Weight = 1
            }
        };
        public override Dictionary<Locale, string> Recipe => new Dictionary<Locale, string>
        {
            { Locale.English, "Dispense desired flavor of soda into a cup" }
        };
        public override List<(Locale, UnlockInfo)> InfoList => new()
        {
            (Locale.English, LocalisationUtils.CreateUnlockInfo("Root Beer", "Adds root beer as a starter", "Offers a fourth flavour of soda"))
        };
    }
}
