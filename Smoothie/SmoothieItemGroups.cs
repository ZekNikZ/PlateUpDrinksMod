using ApplianceLib.Api;
using ApplianceLib.Api.Prefab;
using ApplianceLib.Api.References;
using Kitchen;
using KitchenData;
using KitchenDrinksMod.Util;
using KitchenLib.Customs;
using KitchenLib.Utils;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace KitchenDrinksMod.Smoothie
{
    public abstract class BaseSmoothie : CustomItemGroup<BlenderCupItemGroupView>, IColorblindLabelPositionOverride, IColorblindLabelVisibilityOverride, IColorblindLabelSizeOverride
    {
        public Vector3 ColorblindLabelPosition => new(0, 0.7f, 0);
        public bool ColorblindLabelVisibleWhenColorblindEnabled => true;
        public bool ColorblindLabelVisibleWhenColorblindDisabled => true;
        public Vector2 ColorblindLabelOffsetMinAdjust => new(-0.3f, 0);
        public Vector2 ColorblindLabelOffsetMaxAdjust => new(0.3f, 0);
        public override Item DisposesTo => Refs.BlenderCup;

        public override void SetupPrefab(GameObject prefab)
        {
            // Blender cup
            //PrefabBuilder.AttachBlenderCup(prefab);
            prefab.ApplyMaterialToChild("Cup", MaterialUtils.GetMaterialArray("Door Glass", "Door Glass", "Door Glass"));

            // Liquid
            prefab.ApplyMaterialToChild("Liquid", "drinkup:smoothie_liquid", "drinkup:smoothie_liquid", "drinkup:smoothie_liquid", "drinkup:smoothie_liquid");
            prefab.ApplyMaterialToChild("Liquid2", "drinkup:smoothie_liquid", "drinkup:smoothie_liquid", "drinkup:smoothie_liquid", "drinkup:smoothie_liquid");

            // Setup the ItemGroupView
            prefab.GetComponent<BlenderCupItemGroupView>()?.Setup(prefab);

            // Setup the ingredients
            prefab.ApplyMaterialToChild("Ingredients/Ice/Model", "Ice");

            prefab.ApplyMaterialToChild("Ingredients/Apples/Apple1/Model", "AppleRed", "Wood", "Apple Flesh", "Wood 3", "Wood 3");
            prefab.ApplyMaterialToChild("Ingredients/Apples/Apple2/Model", "AppleRed", "Wood", "Apple Flesh", "Wood 3", "Wood 3");

            prefab.ApplyMaterialToChild("Ingredients/Spinach/Model", IL("Spinach"), IL("Spinach Stem"));

            prefab.ApplyMaterialToChild("Ingredients/Banana", IL("Banana"), IL("Banana Inner"));

            prefab.ApplyMaterialToChild("Ingredients/Oats/Oats1", IL("Oat Grain"));
            prefab.ApplyMaterialToChild("Ingredients/Oats/Oats2", IL("Oat Grain"));
            prefab.ApplyMaterialToChild("Ingredients/Oats/Oats3", IL("Oat Grain"));

            prefab.ApplyMaterialToChild("Ingredients/Nuts/Model", "Cashew");

            prefab.ApplyMaterialToChild("Ingredients/Mandarins/Mandarin1/Model", "Mandarin Skin");
            prefab.ApplyMaterialToChild("Ingredients/Mandarins/Mandarin2/Model", "Mandarin Skin");

            prefab.ApplyMaterialToChild("Ingredients/Lemon", IL("Lemon"), IL("Lemon Inner"), IL("White Fruit"));

            prefab.ApplyMaterialToChild("Ingredients/Lime", IL("Lime"), IL("Lime Inner"), IL("White Fruit"));

            prefab.ApplyMaterialToChild("Ingredients/Cherries/Cherry1", "Cherry", "Wood - Dark");
            prefab.ApplyMaterialToChild("Ingredients/Cherries/Cherry2", "Cherry", "Wood - Dark");

            prefab.ApplyMaterialToChild("Ingredients/Cranberries/Model", "Cherry");

            prefab.ApplyMaterialToChild("Ingredients/Cinnamon/Cinnamon1", IL("Cinnamon"));
            prefab.ApplyMaterialToChild("Ingredients/Cinnamon/Cinnamon2", IL("Cinnamon"));

            prefab.ApplyMaterialToChild("Ingredients/Carrot/Model", "Carrot");

            prefab.ApplyMaterialToChild("Ingredients/Pumpkins/Pumpkin1", "Pumpkin - Flesh", "Pumpkin");
            prefab.ApplyMaterialToChild("Ingredients/Pumpkins/Pumpkin2", "Pumpkin - Flesh", "Pumpkin");

            prefab.ApplyMaterialToChild("Ingredients/Blueberries/Blueberry1", IL("Blueberry"), IL("Blueberry 2"));
            prefab.ApplyMaterialToChild("Ingredients/Blueberries/Blueberry2", IL("Blueberry"), IL("Blueberry 2"));
            prefab.ApplyMaterialToChild("Ingredients/Blueberries/Blueberry3", IL("Blueberry"), IL("Blueberry 2"));
            prefab.ApplyMaterialToChild("Ingredients/Blueberries/Blueberry4", IL("Blueberry"), IL("Blueberry 2"));
            prefab.ApplyMaterialToChild("Ingredients/Blueberries/Blueberry5", IL("Blueberry"), IL("Blueberry 2"));
            prefab.ApplyMaterialToChild("Ingredients/Blueberries/Blueberry6", IL("Blueberry"), IL("Blueberry 2"));

            prefab.ApplyMaterialToChild("Ingredients/Broccoli/Broccoli1/Model", "Lettuce", "Lettuce");
            prefab.ApplyMaterialToChild("Ingredients/Broccoli/Broccoli2/Model", "Lettuce", "Lettuce");
            prefab.ApplyMaterialToChild("Ingredients/Broccoli/Broccoli3/Model", "Lettuce", "Lettuce");
        }

        private static string IL(string matName) => $"IngredientLib - \"{matName}\"";
    }

    public class SmoothieRaw : BaseSmoothie
    {
        public override string UniqueNameID => "Smoothie - Raw";
        public override List<ItemGroup.ItemSet> Sets => SmoothieIngredients.AllIngredients
            .Select(ingredient => new ItemGroup.ItemSet
            {
                Items = new()
                {
                    ingredient.Item,
                    ingredient.BlendedEquivalent
                },
                Min = 0,
                Max = 1,
            })
            .Concat(new List<ItemGroup.ItemSet>()
            {
                new()
                {
                    Items = new()
                    {
                        Refs.BlenderCup,
                        Refs.DummyBlenderCup
                    },
                    Min = 1,
                    Max = 1,
                    IsMandatory = true,
                }
            })
            .ToList();
        public override List<Item.ItemProcess> Processes => new()
        {
            new Item.ItemProcess
            {
                Process = Refs.Blend,
                Duration = 3,
                Result = Refs.SmoothieBlended
            }
        };
        public override List<IItemProperty> Properties => new()
        {
            new CPreventItemMerge
            {
                Condition = MergeCondition.MaintainWrapper
            }
        };
        public override GameObject Prefab => Prefabs.Find("SmoothieInBlenderCup", "Raw");
    }

    public class SmoothieBlended : BaseSmoothie
    {
        public override string UniqueNameID => "Smoothie - Blended";
        public override bool AllowSplitMerging => true;
        public override bool SplitByComponents => true;
        public override Item SplitByComponentsHolder => Refs.DummyBlenderCup;
        public override bool PreventExplicitSplit => true;
        public override bool ApplyProcessesToComponents => true;
        public override int SplitCount => 1;
        public override GameObject Prefab => Prefabs.Find("SmoothieInBlenderCup", "Blended");

        public override void OnRegister(ItemGroup item)
        {
            RestrictedItemTransfers.AllowItem(RestrictedTransferKeys.Blender, item);
        }
    }

    public class BlenderCupItemGroupView : ItemGroupView, IItemSpecificView
    {
        public struct LiquidColor
        {
            public Item Item;
            public Color Color;
        }

        public struct ColourBlindLabel
        {
            public Item Item;
            public string Text;
            public string Icon;

            public string Label => Mod.ColorblindUseText ? Text : Icon;
        }

        public List<LiquidColor> ColorModifiers;
        public List<LiquidColor> SmallColorModifiers;
        public List<ColourBlindLabel> Group1Labels;
        public List<ColourBlindLabel> Group2Labels;
        public GameObject Liquid;
        public GameObject Liquid2;
        public Dictionary<int, Color> ColorModifierMap = new();
        public Dictionary<int, Color> SmallColorModifierMap = new();

        public void Setup(GameObject prefab)
        {
            ComponentGroups = SmoothieIngredients.AllIngredients
                .SelectMany(ingredient => ingredient.UnblendedModelChild == null ? new List<ComponentGroup>() : new List<ComponentGroup>()
                {
                    new ComponentGroup
                    {
                        Item = ingredient.Item,
                        GameObject = prefab.GetChild(ingredient.UnblendedModelChild),
                    }
                })
                .ToList();

            Group1Labels = SmoothieIngredients.AllIngredients
                .Select(ingredient => new ColourBlindLabel
                {
                    Item = ingredient.BlendedEquivalent,
                    Text = ingredient.ColorblindLabel,
                    Icon = ingredient.IconString
                })
                .ToList();

            Group2Labels = SmoothieIngredients.AllIngredients
                .Select(ingredient => new ColourBlindLabel
                {
                    Item = ingredient.Item,
                    Text = ingredient.ColorblindLabel,
                    Icon = ingredient.IconString
                })
                .ToList();

            ColorModifiers = SmoothieIngredients.AllIngredients
                .SelectMany(ingredient => new List<LiquidColor>() {
                    new LiquidColor {
                        Item = ingredient.BlendedEquivalent,
                        Color = ingredient.Color
                    }
                })
                .ToList();

            ColorModifierMap = ColorModifiers.ToDictionary(el => el.Item.ID, el => el.Color);

            SmallColorModifiers = SmoothieIngredients.AllIngredients
                .SelectMany(ingredient => ingredient.UnblendedModelColor != default ? new List<LiquidColor>() {
                    new LiquidColor {
                        Item = ingredient.Item,
                        Color = ingredient.UnblendedModelColor,
                    }
                } : new())
                .ToList();

            SmallColorModifierMap = SmallColorModifiers.ToDictionary(el => el.Item.ID, el => el.Color);

            Liquid = prefab.GetChild("Liquid");
            Liquid2 = prefab.GetChild("Liquid2");
        }

        public new void PerformUpdate(int item_id, ItemList components)
        {
            if (SubviewPrefab != null)
            {
                if (Subview == null)
                {
                    Subview = Instantiate(SubviewPrefab).GetComponent<ItemGroupView>();
                    Transform transform = Subview.transform;
                    transform.parent = SubviewContainer.transform;
                    transform.localScale = Vector3.one;
                    transform.localPosition = Vector3.zero;
                }
                Subview.PerformUpdate(item_id, components);
            }
            if (DrawComponents == null)
            {
                Setup();
            }
            foreach (KeyValuePair<int, ComponentGroup> keyValuePair in DrawComponents)
            {
                if (keyValuePair.Value.Objects != null && keyValuePair.Value.Objects.Count > 0)
                {
                    foreach (GameObject gameObject in keyValuePair.Value.Objects)
                    {
                        gameObject.SetActive(false);
                    }
                }
                else
                {
                    keyValuePair.Value.GameObject.SetActive(false);
                }
            }
            AddComponent(item_id);
            foreach (int num in components)
            {
                AddComponent(num);
            }
            if (ColourblindLabel != null && ComponentLabels != null)
            {
                // Group 1
                var group1Builder = new StringBuilder();
                foreach (ColourBlindLabel colourBlindLabel in Group1Labels)
                {
                    foreach (int num2 in components)
                    {
                        if (colourBlindLabel.Item.ID == num2)
                        {
                            group1Builder.Append(colourBlindLabel.Label);
                        }
                    }
                }
                var group1 = group1Builder.ToString();

                // Group 2
                var group2Builder = new StringBuilder();
                foreach (ColourBlindLabel colourBlindLabel in Group2Labels)
                {
                    foreach (int num2 in components)
                    {
                        if (colourBlindLabel.Item.ID == num2)
                        {
                            group2Builder.Append(colourBlindLabel.Label);
                        }
                    }
                }
                var group2 = group2Builder.ToString();

                if (group1.IsNullOrEmpty())
                {
                    ColourblindLabel.text = group2;
                }
                else if (group2.IsNullOrEmpty())
                {
                    ColourblindLabel.text = group1;
                }
                else
                {
                    ColourblindLabel.text = group1 + "+" + group2;
                }
            }

            List<Color> colors = new();
            foreach (var num in components)
            {
                if (ColorModifierMap.TryGetValue(num, out var color))
                {
                    colors.Add(color);
                }
            }
            var liquidRenderer = Liquid.GetComponent<MeshRenderer>();
            if (colors.Count == 0)
            {
                Liquid.SetActive(false);
            }
            else
            {
                var res = new Color(0, 0, 0, 0);
                foreach (var color in colors)
                {
                    res += color;
                }
                var finalColor = res / colors.Count;

                Material mat = new(Shader.Find("Simple Flat"));
                mat.SetColor("_Color0", finalColor);

                Liquid.SetActive(true);

                liquidRenderer.materials = new Material[] { mat, mat, mat, mat };
            }

            List<Color> smallLiquidColors = new();
            foreach (var num in components)
            {
                if (SmallColorModifierMap.TryGetValue(num, out var color))
                {
                    smallLiquidColors.Add(color);
                }
            }
            var smallLiquidRenderer = Liquid2.GetComponent<MeshRenderer>();
            if (smallLiquidColors.Count == 0)
            {
                Liquid2.SetActive(false);
            }
            else
            {
                var res = new Color(0, 0, 0, 0);
                foreach (var color in smallLiquidColors)
                {
                    res += color;
                }
                var finalColor = res / smallLiquidColors.Count;

                Material mat = new(Shader.Find("Simple Flat"));
                mat.SetColor("_Color0", finalColor);

                Liquid2.SetActive(true);

                smallLiquidRenderer.materials = new Material[] { mat, mat, mat, mat };
            }
        }
    }
}
