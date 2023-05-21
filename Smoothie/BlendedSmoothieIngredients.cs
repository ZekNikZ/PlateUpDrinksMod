using ApplianceLib.Customs.GDO;
using KitchenData;
using KitchenDrinksMod.Util;
using KitchenLib.Customs;
using System;
using System.Linq;
using System.Reflection;

namespace KitchenDrinksMod.Smoothie
{
    internal class BlendedSmoothieIngredients
    {
        public static void Create()
        {
            var items = SmoothieIngredients.AllIngredients
                .OrderBy(ing => ing.Name)
                .Select((ingredient, index) => MakeGDO(ingredient, DummyClasses.Types[index]));

            foreach (var item in items)
            {
                item.ModID = Mod.MOD_GUID;
                item.ModName = Mod.MOD_NAME;
                CustomGDO.RegisterGameDataObject(item);
                Mod.LogInfo($"Registered dynamically-generated blended smoothie item \"{item.UniqueNameID}\"");
            }
        }

        private static CustomItem MakeGDO(SmoothieIngredients.SmoothieIngredient ingredient, Type type)
        {
            var method = typeof(BlendedSmoothieIngredients).GetMethod(nameof(BlendedSmoothieIngredients.MakeGDOGeneric), BindingFlags.NonPublic | BindingFlags.Static);
            var generic = method.MakeGenericMethod(type);
            return (CustomItem)generic.Invoke(null, new object[] { ingredient });
        }

        private static CustomItem MakeGDOGeneric<T>(SmoothieIngredients.SmoothieIngredient ingredient)
        {
            return new BlendedSmoothieIngredient<T>(ingredient);
        }

        public class BlendedSmoothieIngredient<T> : CustomItem, IPreventRegistration
        {
            private readonly SmoothieIngredients.SmoothieIngredient _ingredient;

            public BlendedSmoothieIngredient(SmoothieIngredients.SmoothieIngredient ingredient)
            {
                _ingredient = ingredient;
            }

            public override string UniqueNameID => $"{_ingredient.Name} - blended";
            public override ItemCategory ItemCategory => ItemCategory.Generic;
            public override ItemStorage ItemStorageFlags => ItemStorage.StackableFood;
        }
    }
}
