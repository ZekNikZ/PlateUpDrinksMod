using KitchenData;
using System.Collections.Generic;

namespace KitchenDrinksMod.Registry
{
    public interface ILocalisedRecipeHolder
    {
        IDictionary<Locale, string> LocalisedRecipe { get; }
    }
}
