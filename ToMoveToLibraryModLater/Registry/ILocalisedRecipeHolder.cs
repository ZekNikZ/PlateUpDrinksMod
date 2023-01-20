using KitchenData;
using System.Collections.Generic;

namespace KitchenDrinksMod.ToMoveToLibraryModLater.Registry
{
    public interface ILocalisedRecipeHolder
    {
        IDictionary<Locale, string> LocalisedRecipe { get; }
    }
}
