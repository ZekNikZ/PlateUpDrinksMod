using ApplianceLib.Customs.GDO;
using KitchenData;
using KitchenDrinksMod.Util;
using KitchenLib.Customs;
using KitchenLib.References;
using KitchenLib.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace KitchenDrinksMod.Smoothie
{
    internal class SmoothieCards
    {
        private static readonly string[] CardNames = new string[] {
            "Smoothies",
            "Smoothies: Citrus Burst",
            "Smoothies: Sweets & Spices",
            "Smoothies: Healthy Choices"
        };

        public static void Create()
        {
            var dishes = SmoothieIngredients.AllIngredients
                .OrderBy(ing => ing.Name)
                .GroupBy(ing => ing.CardNumber)
                .OrderBy(group => group.Key)
                .Select(group => MakeGDO(group.Key, group.ToList(), DummyClasses.Types[group.Key]));

            foreach (var dish in dishes)
            {
                dish.ModID = Mod.MOD_GUID;
                dish.ModName = Mod.MOD_NAME;
                CustomGDO.RegisterGameDataObject(dish);
                Mod.LogInfo($"Registered dynamically-generated smoothie card \"{dish.UniqueNameID}\"");
            }
        }

        private static CustomDish MakeGDO(int key, List<SmoothieIngredients.SmoothieIngredient> ingredients, Type type)
        {
            var method = typeof(SmoothieCards).GetMethod(nameof(SmoothieCards.MakeGDOGeneric), BindingFlags.NonPublic | BindingFlags.Static);
            var generic = method.MakeGenericMethod(type);
            return (CustomDish)generic.Invoke(null, new object[] { key, ingredients });
        }

        private static CustomDish MakeGDOGeneric<T>(int key, List<SmoothieIngredients.SmoothieIngredient> ingredients)
        {
            return new SmoothieDish<T>(key, ingredients);
        }

        public class SmoothieDish<T> : CustomDish, IPreventRegistration
        {
            private readonly int _key;
            private readonly List<SmoothieIngredients.SmoothieIngredient> _ingredients;

            public SmoothieDish(int key, List<SmoothieIngredients.SmoothieIngredient> ingredients)
            {
                _key = key;
                _ingredients = ingredients;
            }

            public override string UniqueNameID => $"smoothie dish {_key}";
            public override bool IsAvailableAsLobbyOption => _key == 0;
            public override bool RequiredNoDishItem => true;
            public override GameObject DisplayPrefab => Refs.Find<Dish>(DishReferences.BurgerBase).DisplayPrefab;
            public override GameObject IconPrefab => Refs.Find<Dish>(DishReferences.BurgerBase).IconPrefab;
            public override List<Unlock> HardcodedRequirements => _key == 0 ? new() : new() { Refs.Find<Dish>(Mod.MOD_GUID, "smoothie dish 0") };
            public override HashSet<Dish.IngredientUnlock> IngredientsUnlocks => _ingredients
                .Select(ingredient => new Dish.IngredientUnlock
                {
                    Ingredient = ingredient.BlendedEquivalent,
                    MenuItem = Refs.ServedSmoothie
                })
                .ToHashSet();
            public override List<Dish.MenuItem> ResultingMenuItems => _key != 0 ? new() : new()
            {
                new Dish.MenuItem
                {
                    Item = Refs.ServedSmoothie,
                    Phase = MenuPhase.Main,
                    Weight = 1
                }
            };
            private string _instructions
            {
                get
                {
                    var instructions = _ingredients.Select(ing => ing.InstructionsBlurb).ToList();
                    instructions[instructions.Count - 1] = "or " + instructions[instructions.Count - 1];
                    return string.Join(", ", instructions);
                }
            }
            public override Dictionary<Locale, string> Recipe
            {
                get
                {


                    return new()
                    {
                        {
                            Locale.English,
                            $"Add {_instructions} to a blender and blend to create a smoothie mixture."
                        }
                    };
                }
            }
            private string _ingredientsList
            {
                get
                {
                    var instructions = _ingredients.Select(ing => ing.Name).ToList();
                    instructions[instructions.Count - 1] = "and " + instructions[instructions.Count - 1];
                    return string.Join(", ", instructions);
                }
            }
            public override List<(Locale, UnlockInfo)> InfoList => new()
            {
                (Locale.English, LocalisationUtils.CreateUnlockInfo(CardNames[_key], _key == 0 ? "Adds smoothies as a main" : $"Adds {_ingredientsList} as smoothie ingredients.", "Healthy drinks made to order!"))
            };
            public override HashSet<Item> MinimumIngredients => _ingredients
                .SelectMany(ing => ing.MinimumIngredients)
                .Select(getter => getter.Invoke())
                .ToHashSet();
            public override HashSet<Process> RequiredProcesses => _ingredients
                .SelectMany(ing => ing.RequiredProcesses ?? new() { })
                .Select(getter => getter.Invoke())
                .Concat(new List<Process>()
                {
                    Refs.Blend
                }).ToHashSet();
            public override List<string> StartingNameSet => new()
            {
                "Smoothies"
            };
            public override DishType Type => _key == 0 ? DishType.Base : DishType.Extra;
            public override DishCustomerChange CustomerMultiplier => DishCustomerChange.SmallDecrease;
            public override Unlock.RewardLevel ExpReward => Unlock.RewardLevel.Medium;
            public override UnlockGroup UnlockGroup => UnlockGroup.Dish;
            public override bool IsUnlockable => true;
            public override int Difficulty => 3;
        }
    }
}
