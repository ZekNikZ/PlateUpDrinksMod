using KitchenData;
using KitchenLib.Customs;
using KitchenLib.Utils;
using System.Collections.Generic;

namespace KitchenDrinksMod.Floats
{
    public class RootBeerFloatDish : CustomDish
    {
        public override string UniqueNameID => "Root Beer Float Dish";
        public override DishType Type => DishType.Dessert;
        public override DishCustomerChange CustomerMultiplier => DishCustomerChange.None;
        public override Unlock.RewardLevel ExpReward => Unlock.RewardLevel.Medium;
        public override UnlockGroup UnlockGroup => UnlockGroup.Dish;
        public override bool IsUnlockable => true;
        public override bool RequiredNoDishItem => true;
        public override HashSet<Item> MinimumIngredients => new()
        {
            Refs.Cup,
            Refs.IceCreamVanilla
        };
        public override HashSet<Process> RequiredProcesses => new()
        {
        };
        public override List<Dish.MenuItem> ResultingMenuItems => new()
        {
            new Dish.MenuItem()
            {
                Item = Refs.RootBeerFloat,
                Phase = MenuPhase.Dessert,
                Weight = 1
            }
        };
        public override Dictionary<Locale, string> Recipe => new Dictionary<Locale, string>
        {
            { Locale.English, "Dispense desired flavor of soda into a cup and add vanilla ice cream" }
        };
        public override List<(Locale, UnlockInfo)> InfoList => new()
        {
            (Locale.English, LocalisationUtils.CreateUnlockInfo("Root Beer Floats", "Adds root beer floats as a dessert", "Offers an additional flavor of vanilla floats"))
        };
        public override List<Unlock> HardcodedRequirements => new()
        {
            Refs.RootBeerDish,
            Refs.NormalFloatsDish
        };
        public override List<Unlock> HardcodedBlockers => new()
        {
            Refs.AllFloatsDish
        };
    }
}
