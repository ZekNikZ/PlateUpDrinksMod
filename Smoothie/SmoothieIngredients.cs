using KitchenData;
using KitchenLib.References;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace KitchenDrinksMod.Smoothie
{
    internal class SmoothieIngredients
    {
        public struct SmoothieIngredient
        {
            public string Name;
            public Func<Item> BaseItem;
            public string Instructions;
            public int CardNumber;
            public List<Func<Item>> StartingIngredients;
            public List<Func<Process>> RequiredProcesses;
            public Color Color;
            public string ColorblindLabel;
            public string IconString;
            public string UnblendedModelChild;
            public Color UnblendedModelColor;

            public Item Item => BaseItem.Invoke();
            public Item BlendedEquivalent => Refs.Find<Item>(Mod.MOD_GUID, $"{Name} - blended");
            public string InstructionsBlurb => Instructions ?? Name;
            public List<Func<Item>> MinimumIngredients => StartingIngredients ?? new() { BaseItem };
        }

        public static List<SmoothieIngredient> BaseIngredients = new()
        {
            new SmoothieIngredient
            {
                Name = "ice",
                BaseItem = () => Refs.Find<Item>(IngredientLib.References.GetIngredient("ice")),
                CardNumber = 0,
                StartingIngredients = new()
                {
                    () => Refs.Find<Item>(IngredientLib.References.GetIngredient("ice")),
                    () => Refs.Cup,
                },
                Color = MakeColor(141, 212, 212, 63),
                ColorblindLabel = "Ic",
                IconString = MakeIcon("ice"),
                UnblendedModelChild = "Ingredients/Ice",
            },
        };

        public static List<SmoothieIngredient> LiquidIngredients = new()
        {
            new SmoothieIngredient
            {
                Name = "milk",
                BaseItem = () => Refs.Find<Item>(IngredientLib.References.GetSplitIngredient("milk")),
                CardNumber = 0,
                StartingIngredients = new() { () => Refs.Find<Item>(IngredientLib.References.GetIngredient("milk")) },
                Color = MakeColor(255, 255, 255, 255),
                ColorblindLabel = "Mi",
                IconString = MakeIcon("milk"),
                UnblendedModelColor = MakeColor(255, 255, 255, 255),
            },
        };

        public static List<SmoothieIngredient> FillerIngredients = new()
        {
            new SmoothieIngredient
            {
                Name = "apples",
                BaseItem = () => Refs.Find<Item>(ItemReferences.AppleSlices),
                Instructions = "apple slices",
                CardNumber = 0,
                StartingIngredients = new() { () => Refs.Find<Item>(ItemReferences.Apple) },
                Color = MakeColor(187, 34, 34, 255),
                ColorblindLabel = "Ap",
                IconString = MakeIcon("apple"),
                UnblendedModelChild = "Ingredients/Apples",
            },
            new SmoothieIngredient
            {
                Name = "spinach",
                BaseItem = () => Refs.Find<Item>(IngredientLib.References.GetIngredient("chopped spinach")),
                Instructions = "chopped spinach",
                CardNumber = 0,
                StartingIngredients = new() { () => Refs.Find<Item>(IngredientLib.References.GetIngredient("spinach")) },
                Color = MakeColor(77, 126, 51, 255),
                ColorblindLabel = "Sp",
                IconString = MakeIcon("spinach"),
                UnblendedModelChild = "Ingredients/Spinach",
            },
            new SmoothieIngredient
            {
                Name = "bananas",
                BaseItem = () => Refs.Find<Item>(IngredientLib.References.GetIngredient("chopped banana")),
                Instructions = "peeled bananas",
                CardNumber = 0,
                StartingIngredients = new() { () => Refs.Find<Item>(IngredientLib.References.GetIngredient("banana")) },
                Color = MakeColor(255, 251, 201, 255),
                ColorblindLabel = "Ba",
                IconString = MakeIcon("banana"),
                UnblendedModelChild = "Ingredients/Banana",
            },
            new SmoothieIngredient
            {
                Name = "oats",
                BaseItem = () => Refs.Find<Item>(IngredientLib.References.GetIngredient("oats")),
                CardNumber = 0,
                Color = MakeColor(216, 194, 157, 255),
                ColorblindLabel = "Oa",
                IconString = MakeIcon("oat"),
                UnblendedModelChild = "Ingredients/Oats",
            },

            new SmoothieIngredient
            {
                Name = "nuts",
                BaseItem = () => Refs.Find<Item>(ItemReferences.NutsChopped),
                Instructions = "chopped nuts",
                CardNumber = 1,
                StartingIngredients = new() { () => Refs.Find<Item>(ItemReferences.NutsIngredient) },
                Color = MakeColor(178, 160, 127, 255),
                ColorblindLabel = "Nu",
                IconString = MakeIcon("peanut"),
                UnblendedModelChild = "Ingredients/Nuts",
            },
            new SmoothieIngredient
            {
                Name = "mandarins",
                BaseItem = () => Refs.Find<Item>(ItemReferences.MandarinSlice),
                Instructions = "one mandarin slice",
                CardNumber = 1,
                StartingIngredients = new() { () => Refs.Find<Item>(ItemReferences.MandarinRaw) },
                Color = MakeColor(240, 129, 19, 255),
                ColorblindLabel = "Ma",
                IconString = MakeIcon("orange"),
                UnblendedModelChild = "Ingredients/Mandarins",
            },
            new SmoothieIngredient
            {
                Name = "lemons",
                BaseItem = () => Refs.Find<Item>(IngredientLib.References.GetIngredient("chopped lemon")),
                Instructions = "chopped lemons",
                CardNumber = 1,
                StartingIngredients = new() { () => Refs.Find<Item>(IngredientLib.References.GetIngredient("lemon")) },
                Color = MakeColor(204, 187, 52, 255),
                ColorblindLabel = "Le",
                IconString = MakeIcon("lemon"),
                UnblendedModelChild = "Ingredients/Lemon",
            },
            new SmoothieIngredient
            {
                Name = "limes",
                BaseItem = () => Refs.Find<Item>(IngredientLib.References.GetIngredient("chopped lime")),
                Instructions = "chopped limes",
                CardNumber = 1,
                StartingIngredients = new() { () => Refs.Find<Item>(IngredientLib.References.GetIngredient("lime")) },
                Color = MakeColor(83, 146, 49, 255),
                ColorblindLabel = "Li",
                IconString = MakeIcon("lime"),
                UnblendedModelChild = "Ingredients/Lime",
            },

            new SmoothieIngredient
            {
                Name = "cherries",
                BaseItem = () => Refs.Find<Item>(ItemReferences.Cherry),
                CardNumber = 2,
                Color = MakeColor(144, 0, 49, 255),
                ColorblindLabel = "Ch",
                IconString = MakeIcon("cherry"),
                UnblendedModelChild = "Ingredients/Cherries",
            },
            new SmoothieIngredient
            {
                Name = "caramel",
                BaseItem = () => Refs.Find<Item>(IngredientLib.References.GetIngredient("caramel")),
                CardNumber = 2,
                StartingIngredients = new() { () => Refs.Find<Item>(ItemReferences.Sugar) },
                RequiredProcesses = new() { () => Refs.Cook },
                Color = MakeColor(198, 142, 23, 255),
                ColorblindLabel = "Cr",
                IconString = MakeIcon("caramel"),
                UnblendedModelColor = MakeColor(198, 142, 23, 255),
            },
            new SmoothieIngredient
            {
                Name = "cranberries",
                BaseItem = () => Refs.Find<Item>(ItemReferences.CranberriesChopped),
                Instructions = "chopped cranberries",
                CardNumber = 2,
                StartingIngredients = new() { () => Refs.Find<Item>(ItemReferences.Cranberries) },
                Color = MakeColor(238, 104, 16, 255),
                ColorblindLabel = "Cr",
                IconString = MakeIcon("cranberry"),
                UnblendedModelChild = "Ingredients/Cranberries",
            },
            new SmoothieIngredient
            {
                Name = "cinnamon",
                BaseItem = () => Refs.Find<Item>(IngredientLib.References.GetIngredient("cinnamon")),
                CardNumber = 2,
                Color = MakeColor(197, 140, 102, 255),
                ColorblindLabel = "Ci",
                IconString = MakeIcon("cinnamon"),
                UnblendedModelChild = "Ingredients/Cinnamon",
            },

            new SmoothieIngredient
            {
                Name = "carrots",
                Instructions = "chopped carrots",
                BaseItem = () => Refs.Find<Item>(ItemReferences.CarrotChopped),
                CardNumber = 3,
                StartingIngredients = new() { () => Refs.Find<Item>(ItemReferences.Carrot) },
                Color = MakeColor(238, 104, 16, 255),
                ColorblindLabel = "Ca",
                IconString = MakeIcon("carrot"),
                UnblendedModelChild = "Ingredients/Carrot",
            },
            new SmoothieIngredient
            {
                Name = "pumpkins",
                BaseItem = () => Refs.Find<Item>(ItemReferences.PumpkinPieces),
                Instructions = "pumpkin chunks",
                CardNumber = 3,
                StartingIngredients = new() { () => Refs.Find<Item>(ItemReferences.Pumpkin) },
                Color = MakeColor(247, 187, 148, 255),
                ColorblindLabel = "Pu",
                IconString = MakeIcon("pumpkin"),
                UnblendedModelChild = "Ingredients/Pumpkins",
            },
            new SmoothieIngredient
            {
                Name = "blueberries",
                BaseItem = () => Refs.Find<Item>(IngredientLib.References.GetIngredient("blueberries")),
                CardNumber = 3,
                Color = MakeColor(16, 90, 244, 255),
                ColorblindLabel = "Bb",
                IconString = MakeIcon("blueberry"),
                UnblendedModelChild = "Ingredients/Blueberries",
            },
            new SmoothieIngredient
            {
                Name = "broccoli",
                BaseItem = () => Refs.Find<Item>(ItemReferences.BroccoliChopped),
                Instructions = "chopped broccoli",
                CardNumber = 3,
                StartingIngredients = new() { () => Refs.Find<Item>(ItemReferences.BroccoliRaw) },
                Color = MakeColor(60, 128, 5, 255),
                ColorblindLabel = "Br",
                IconString = MakeIcon("broccoli"),
                UnblendedModelChild = "Ingredients/Broccoli",
            },
        };

        public static List<SmoothieIngredient> AllIngredients => BaseIngredients.Concat(LiquidIngredients).Concat(FillerIngredients).ToList();

        private static Color MakeColor(int r, int g, int b, int a)
        {
            return new Color(r / 255f, g / 255f, b / 255f, a / 255f);
        }

        private static string MakeIcon(string key)
        {
            return $"<sprite name=\"sm_{key}\">";
        }
    }
}
