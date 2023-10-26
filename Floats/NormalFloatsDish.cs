using KitchenData;
using KitchenLib.Customs;
using KitchenLib.Utils;
using System.Collections.Generic;

namespace KitchenDrinksMod.Floats
{
    public class NormalFloatsDish : CustomDish
    {
        public override string UniqueNameID => "Floats Dish";
        public override DishType Type => DishType.Dessert;
        public override DishCustomerChange CustomerMultiplier => DishCustomerChange.SmallDecrease;
        public override Unlock.RewardLevel ExpReward => Unlock.RewardLevel.Medium;
        public override UnlockGroup UnlockGroup => UnlockGroup.Dish;
        public override bool IsUnlockable => true;
        public override bool RequiredNoDishItem => true;
        public override int Difficulty => 2;
        public override HashSet<Item> MinimumIngredients => new()
        {
            Refs.RedSoda,
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
                Item = Refs.RedFloat,
                Phase = MenuPhase.Dessert,
                Weight = 1
            },
            new Dish.MenuItem()
            {
                Item = Refs.GreenFloat,
                Phase = MenuPhase.Dessert,
                Weight = 1
            },
            new Dish.MenuItem()
            {
                Item = Refs.BlueFloat,
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
            (Locale.English, LocalisationUtils.CreateUnlockInfo("Floats", "Adds soda floats as a dessert", "Offers three flavours of vanilla floats"))
        };
        public override List<Unlock> HardcodedBlockers => new()
        {
            Refs.AllFloatsDish,
            Refs.RootBeerDish
        };
    }
}
