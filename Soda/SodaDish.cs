using KitchenData;
using KitchenDrinksMod.Customs;
using KitchenLib.Utils;
using System.Collections.Generic;

namespace KitchenDrinksMod.Soda
{
    public class SodaDish : ModDish
    {
        public override string UniqueNameID => "Soda Dish";
        public override DishType Type => DishType.Starter;
        public override DishCustomerChange CustomerMultiplier => DishCustomerChange.SmallDecrease;
        public override Unlock.RewardLevel ExpReward => Unlock.RewardLevel.Medium;
        public override HashSet<Item> MinimumIngredients => new()
        {
            Refs.RedSoda,
            Refs.Cup
        };
        public override HashSet<Process> RequiredProcesses => new()
        {
        };
        public override List<Dish.MenuItem> ResultingMenuItems => new()
        {
            new Dish.MenuItem()
            {
                Item = Refs.RedSoda,
                Phase = MenuPhase.Starter,
                Weight = 1
            },
            new Dish.MenuItem()
            {
                Item = Refs.GreenSoda,
                Phase = MenuPhase.Starter,
                Weight = 1
            },
            new Dish.MenuItem()
            {
                Item = Refs.BlueSoda,
                Phase = MenuPhase.Starter,
                Weight = 1
            }
        };
        public override Dictionary<Locale, string> Recipe => new Dictionary<Locale, string>
        {
            { Locale.English, "Dispense desired flavor of soda into a cup" }
        };
        public override IDictionary<Locale, UnlockInfo> LocalisedInfo => new Dictionary<Locale, UnlockInfo>
        {
            { Locale.English, LocalisationUtils.CreateUnlockInfo("Soda", "Adds fountain drinks as a starter", "Offers three flavours of soda") }
        };
    }
}
