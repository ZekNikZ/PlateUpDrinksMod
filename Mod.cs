using Kitchen;
using KitchenLib;
using KitchenMods;
using System.Reflection;
using UnityEngine;
using System.Linq;
using TMPro;
using ApplianceLib.Customs;
using KitchenLib.Event;
using System.Collections.Generic;
using KitchenData;
using KitchenLib.Utils;
using KitchenDrinksMod.Smoothie;
using System;
using KitchenDrinksMod.Util;
using KitchenLib.Customs;
using KitchenLib.Preferences;
using KitchenDrinksMod.Customs.UI;

namespace KitchenDrinksMod
{
    public class Mod : BaseMod, IModSystem
    {
        public const string MOD_GUID = "io.zkz.plateup.drinks";
        public const string MOD_NAME = "DrinkUp!";
        public const string MOD_VERSION = "0.5.1";
        public const string MOD_AUTHOR = "ZekNikZ";
        public const string MOD_GAMEVERSION = ">=1.2.0";

#if DEBUG
        public const bool DEBUG_MODE = true;
#else
        public const bool DEBUG_MODE = false;
#endif

        public const bool SMOOTHIE_CARDS_ENABLED = false;

        public static AssetBundle Bundle;
        public static readonly PreferenceManager PreferenceManager = new(MOD_GUID);
        public static readonly PreferenceBool ColorblindUseTextPref = PreferenceManager.RegisterPreference(new PreferenceBool("colorblind_use_text"));
        public static bool ColorblindUseText => ColorblindUseTextPref.Get();

        public Mod() : base(MOD_GUID, MOD_NAME, MOD_AUTHOR, MOD_VERSION, MOD_GAMEVERSION, Assembly.GetExecutingAssembly()) { }

        protected override void OnInitialise()
        {
            LogWarning($"{MOD_GUID} v{MOD_VERSION} in use!");
        }

        private void AddGameData()
        {
            LogInfo("Attempting to register game data...");

            ModGDOs.RegisterModGDOs(this, Assembly.GetExecutingAssembly());

            // Smoothies
            BlendedSmoothieIngredients.Create();
            if (SMOOTHIE_CARDS_ENABLED)
            {
                SmoothieCards.Create();
            }

            LogInfo("Done loading game data.");
        }

        private void AddProcessIcons()
        {
            // No clue why this is needed, but it seems like I need to do this before loading any icon textures:
            Bundle.LoadAllAssets<Texture2D>();
            Bundle.LoadAllAssets<Sprite>();

            var spriteAsset = Bundle.LoadAsset<TMP_SpriteAsset>("Process Icons");
            TMP_Settings.defaultSpriteAsset.fallbackSpriteAssets.Add(spriteAsset);
            spriteAsset.material = UnityEngine.Object.Instantiate(TMP_Settings.defaultSpriteAsset.material);
            spriteAsset.material.mainTexture = Bundle.LoadAsset<Texture2D>("ProcessIcons");

            var spriteAsset2 = Bundle.LoadAsset<TMP_SpriteAsset>("smoothie_icons");
            TMP_Settings.defaultSpriteAsset.fallbackSpriteAssets.Add(spriteAsset2);
            spriteAsset2.material = UnityEngine.Object.Instantiate(TMP_Settings.defaultSpriteAsset.material);
            spriteAsset2.material.mainTexture = Bundle.LoadAsset<Texture2D>("smoothie_icons_tex");
        }

        private void SetupCustomMaterials()
        {
            AddMaterial(MaterialUtils.CreateFlat("drinkup_orange_straw", 0xCF8702));
            AddMaterial(MaterialUtils.CreateFlat("drinkup_root_beer_liquid", 0x321F1F));
        }

        /// <summary>
        /// This entity query and modifications are only used to test my starting dish easier.
        /// </summary>
        private bool colorblindSetup = false;
        protected override void OnUpdate()
        {
            if (!colorblindSetup)
            {
                foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
                {
                    foreach (var type in assembly.GetTypes())
                    {
                        if (typeof(IColorblindLabelPositionOverride).IsAssignableFrom(type))
                        {
                            if (CustomGDO.GDOsByType.TryGetValue(type, out var gdo) && gdo.GameDataObject is IHasPrefab hasPrefab)
                            {
                                var colorblind = hasPrefab.Prefab.GetChild("Colour Blind");
                                if (colorblind != null)
                                {
                                    colorblind.transform.localPosition = ((IColorblindLabelPositionOverride)gdo).ColorblindLabelPosition;
                                }
                            }
                        }
                        if (typeof(IColorblindLabelVisibilityOverride).IsAssignableFrom(type))
                        {
                            if (CustomGDO.GDOsByType.TryGetValue(type, out var gdo) && gdo.GameDataObject is IHasPrefab hasPrefab)
                            {
                                var colorblind = hasPrefab.Prefab.GetChild("Colour Blind");
                                var comp = colorblind?.GetComponent<ColourBlindMode>();
                                if (comp != null)
                                {
                                    comp.ShowInColourblindMode = ((IColorblindLabelVisibilityOverride)gdo).ColorblindLabelVisibleWhenColorblindEnabled;
                                    comp.ShowInNonColourblindMode = ((IColorblindLabelVisibilityOverride)gdo).ColorblindLabelVisibleWhenColorblindDisabled;
                                }
                            }
                        }
                        if (typeof(IColorblindLabelSizeOverride).IsAssignableFrom(type))
                        {
                            if (CustomGDO.GDOsByType.TryGetValue(type, out var gdo) && gdo.GameDataObject is IHasPrefab hasPrefab)
                            {
                                var colorblind = hasPrefab.Prefab.GetChild("Colour Blind/Title");
                                var transform = colorblind?.GetComponent<RectTransform>();
                                if (transform != null)
                                {
                                    transform.offsetMin += ((IColorblindLabelSizeOverride)gdo).ColorblindLabelOffsetMinAdjust;
                                    transform.offsetMax += ((IColorblindLabelSizeOverride)gdo).ColorblindLabelOffsetMaxAdjust;
                                }
                            }
                        }
                    }
                }

                //Refs.BlackTea.Prefab.GetChild("Colour Blind").transform.localPosition = new Vector3(0, 0.1f, 0);
                //Refs.MatchaTea.Prefab.GetChild("Colour Blind").transform.localPosition = new Vector3(0, 0.1f, 0);
                //Refs.TaroTea.Prefab.GetChild("Colour Blind").transform.localPosition = new Vector3(0, 0.1f, 0);

                //Refs.VanillaMilkshake.Prefab.GetChild("Colour Blind").transform.localPosition = new Vector3(0, 0.45f, 0);
                //Refs.ChocolateMilkshake.Prefab.GetChild("Colour Blind").transform.localPosition = new Vector3(0, 0.45f, 0);
                //Refs.StrawberryMilkshake.Prefab.GetChild("Colour Blind").transform.localPosition = new Vector3(0, 0.45f, 0);
                //Refs.ServedVanillaMilkshake.Prefab.GetChild("Colour Blind").transform.localPosition = new Vector3(0, 0.45f, 0);
                //Refs.ServedChocolateMilkshake.Prefab.GetChild("Colour Blind").transform.localPosition = new Vector3(0, 0.45f, 0);
                //Refs.ServedStrawberryMilkshake.Prefab.GetChild("Colour Blind").transform.localPosition = new Vector3(0, 0.45f, 0);
                //Refs.RedFloat.Prefab.GetChild("Colour Blind").transform.localPosition = new Vector3(0, 0.45f, 0);
                //Refs.BlueFloat.Prefab.GetChild("Colour Blind").transform.localPosition = new Vector3(0, 0.45f, 0);
                //Refs.GreenFloat.Prefab.GetChild("Colour Blind").transform.localPosition = new Vector3(0, 0.45f, 0);
                //Refs.RootBeerFloat.Prefab.GetChild("Colour Blind").transform.localPosition = new Vector3(0, 0.45f, 0);

                //Refs.RedSoda.Prefab.GetChild("Colour Blind").transform.localPosition = new Vector3(0, 0.2f, 0);
                //Refs.GreenSoda.Prefab.GetChild("Colour Blind").transform.localPosition = new Vector3(0, 0.2f, 0);
                //Refs.BlueSoda.Prefab.GetChild("Colour Blind").transform.localPosition = new Vector3(0, 0.2f, 0);
                //Refs.RootBeer.Prefab.GetChild("Colour Blind").transform.localPosition = new Vector3(0, 0.2f, 0);

                //Refs.MilkInCup.Prefab.GetChild("Colour Blind").transform.localPosition = new Vector3(0, 0.7f, 0);

                //Refs.TeaProvider.Prefab.GetChild("TeaDispenser1/ColorblindLabelParent").AddApplianceColorblindLabel("Bl");
                //Refs.TeaProvider.Prefab.GetChild("TeaDispenser2/ColorblindLabelParent").AddApplianceColorblindLabel("Ma");
                //Refs.TeaProvider.Prefab.GetChild("TeaDispenser3/ColorblindLabelParent").AddApplianceColorblindLabel("T");

                //Refs.SodaProvider.Prefab.GetChild("ColorblindLabelParent1").AddApplianceColorblindLabel("R");
                //Refs.SodaProvider.Prefab.GetChild("ColorblindLabelParent2").AddApplianceColorblindLabel("G");
                //Refs.SodaProvider.Prefab.GetChild("ColorblindLabelParent3").AddApplianceColorblindLabel("B");

                colorblindSetup = true;
            }
        }

        protected override void OnPostActivate(KitchenMods.Mod mod)
        {
            // Register preferences
            PreferenceManager.Load();

            // Create materials
            SetupCustomMaterials();

            // Register menus
            ModsPreferencesMenu<MenuAction>.RegisterMenu(MOD_NAME, typeof(PreferencesMenu<MenuAction>), typeof(MenuAction));
            Events.PlayerPauseView_SetupMenusEvent += (s, args) =>
            {
                args.addMenu.Invoke(args.instance, new object[] { typeof(PreferencesMenu<MenuAction>), new PreferencesMenu<MenuAction>(args.instance.ButtonContainer, args.module_list) });
            };

            LogInfo("Attempting to load asset bundle...");
            Bundle = mod.GetPacks<AssetBundleModPack>().SelectMany(e => e.AssetBundles).First();
            LogInfo("Done loading asset bundle.");

            SetupCustomMaterials();

            AddGameData();

            AddProcessIcons();

            Events.BuildGameDataEvent += (s, args) =>
            {
                // Add dispense processes to cup
                Refs.Cup.DerivedProcesses.AddRange(new List<Item.ItemProcess>()
                {
                    new Item.ItemProcess()
                    {
                        Process = Refs.DispenseBlackTea,
                        Result = Refs.BlackTea,
                        Duration = 1f
                    },
                    new Item.ItemProcess()
                    {
                        Process = Refs.DispenseMatchaTea,
                        Result = Refs.MatchaTea,
                        Duration = 1f
                    },
                    new Item.ItemProcess()
                    {
                        Process = Refs.DispenseTaroTea,
                        Result = Refs.TaroTea,
                        Duration = 1f
                    },
                    new Item.ItemProcess()
                    {
                        Process = Refs.DispenseRedSoda,
                        Result = Refs.RedSoda,
                        Duration = 1f
                    },
                    new Item.ItemProcess()
                    {
                        Process = Refs.DispenseGreenSoda,
                        Result = Refs.GreenSoda,
                        Duration = 1f
                    },
                    new Item.ItemProcess()
                    {
                        Process = Refs.DispenseBlueSoda,
                        Result = Refs.BlueSoda,
                        Duration = 1f
                    },
                    new Item.ItemProcess()
                    {
                        Process = Refs.DispenseRootBeer,
                        Result = Refs.RootBeer,
                        Duration = 1f
                    }
                }.ToList());

                // Add blend processes to smoothie ingredients
                SmoothieIngredients.AllIngredients.ForEach(ingredient =>
                {
                    ingredient.Item.DerivedProcesses.Add(new Item.ItemProcess
                    {
                        Process = Refs.Blend,
                        Result = ingredient.BlendedEquivalent,
                        RequiresWrapper = true
                    });
                });

                // Fix modded kitchen
                var mkMilk = Refs.Find<ItemGroup>("The Modded Kitchen", "Milk Glass");
                if (mkMilk != null)
                {
                    mkMilk.Sets = new List<ItemGroup.ItemSet>
                    {
                        new ItemGroup.ItemSet()
                        {
                            Max = 2,
                            Min = 2,
                            IsMandatory = true,
                            Items = new List<Item>()
                            {
                                Refs.Cup,
                                Refs.DummyShimItem
                            }
                        }
                    };
                }

                var mkMilkDish = Refs.Find<Dish>("The Modded Kitchen", "Milk Glass Dish");
                if (mkMilkDish != null)
                {
                    mkMilkDish.ResultingMenuItems = new List<Dish.MenuItem>
                    {
                        new Dish.MenuItem
                        {
                            Item = Refs.MilkInCup,
                            Phase = MenuPhase.Side,
                            Weight = 1
                        }
                    };
                }

                var mkExtraMilkDish = Refs.Find<Dish>("The Modded Kitchen", "Extra Milk");
                if (mkExtraMilkDish != null)
                {
                    mkExtraMilkDish.ExtraOrderUnlocks = new HashSet<Dish.IngredientUnlock>()
                    {
                        new Dish.IngredientUnlock
                        {
                            MenuItem = Refs.MilkInCup,
                            Ingredient = Refs.MilkInCup
                        }
                    };
                }
            };
        }

        #region Logging
        public static void LogInfo(string _log) { Debug.Log($"[{MOD_NAME}] " + _log); }
        public static void LogWarning(string _log) { Debug.LogWarning($"[{MOD_NAME}] " + _log); }
        public static void LogError(string _log) { Debug.LogError($"[{MOD_NAME}] " + _log); }
        public static void LogInfo(object _log) { LogInfo(_log.ToString()); }
        public static void LogWarning(object _log) { LogWarning(_log.ToString()); }
        public static void LogError(object _log) { LogError(_log.ToString()); }
        #endregion
    }
}
