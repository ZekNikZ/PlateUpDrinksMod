using KitchenData;
using KitchenDrinksMod.Registry;
using KitchenLib.Customs;
using System.Collections.Generic;

namespace KitchenDrinksMod.Dishes
{
    public abstract class ModDish : CustomDish, ILocalisedRecipeHolder
    {
        public virtual IDictionary<Locale, string> LocalisedRecipe { get; }

        public virtual IDictionary<Locale, UnlockInfo> LocalisedInfo { get; }

        public override LocalisationObject<UnlockInfo> Info
        {
            get
            {
                var info = new LocalisationObject<UnlockInfo>();

                foreach (var entry in LocalisedInfo)
                {
                    info.Add(entry.Key, entry.Value);
                }

                return info;
            }
        }

        public override void OnRegister(GameDataObject gameDataObject)
        {
            Dish dish = gameDataObject as Dish;
            ModRegistry.AddLocalisedRecipe(this, dish);

            if (Type == DishType.Base)
            {
                ModRegistry.AddBaseDish(dish);
            }
        }
    }
}
