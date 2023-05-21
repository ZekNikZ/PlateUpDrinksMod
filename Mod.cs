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
using ApplianceLib.Api;
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
        public const string MOD_VERSION = "0.5.0";
        public const string MOD_AUTHOR = "ZekNikZ";
        public const string MOD_GAMEVERSION = ">=1.1.5";

#if DEBUG
        public const bool DEBUG_MODE = true;
#else
        public const bool DEBUG_MODE = false;
#endif

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
            SmoothieCards.Create();

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
            MaterialUtils.CreateFlat("drinkup:smoothie_liquid", new Color(0.5f, 0.5f, 0.5f, 1f));
            MaterialUtils.CreateFlat("drinkup:orange_straw", new Color(1f, 0.5f, 0f, 1f));
            MaterialUtils.CreateFlat("drinkup:root_beer_liquid", new Color(0.196f, 0.123f, 0.121f, 1f));
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
                                var colorblind = hasPrefab.Prefab.GetChild("Colour Blind(Clone)");
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
                                var colorblind = hasPrefab.Prefab.GetChild("Colour Blind(Clone)");
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
                                var colorblind = hasPrefab.Prefab.GetChild("Colour Blind(Clone)/Title");
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

                //Refs.BlackTea.Prefab.GetChild("Colour Blind").transform.localPosition = new Vector3(0, 0.7f, 0);
                //Refs.MatchaTea.Prefab.GetChild("Colour Blind").transform.localPosition = new Vector3(0, 0.7f, 0);
                //Refs.TaroTea.Prefab.GetChild("Colour Blind").transform.localPosition = new Vector3(0, 0.7f, 0);

                //Refs.ServedVanillaMilkshake.Prefab.GetChild("Colour Blind").transform.localPosition = new Vector3(0, 0.45f, 0);
                //Refs.ServedChocolateMilkshake.Prefab.GetChild("Colour Blind").transform.localPosition = new Vector3(0, 0.45f, 0);
                //Refs.ServedStrawberryMilkshake.Prefab.GetChild("Colour Blind").transform.localPosition = new Vector3(0, 0.45f, 0);

                //Refs.RedSoda.Prefab.GetChild("Colour Blind").transform.localPosition = new Vector3(0, 0.7f, 0);
                //Refs.GreenSoda.Prefab.GetChild("Colour Blind").transform.localPosition = new Vector3(0, 0.7f, 0);
                //Refs.BlueSoda.Prefab.GetChild("Colour Blind").transform.localPosition = new Vector3(0, 0.7f, 0);

                //Refs.MilkInCup.Prefab.GetChild("Colour Blind").transform.localPosition = new Vector3(0, 0.7f, 0);

                Refs.TeaProvider.Prefab.GetChild("TeaDispenser1/ColorblindLabelParent").AddApplianceColorblindLabel("Bl");
                Refs.TeaProvider.Prefab.GetChild("TeaDispenser2/ColorblindLabelParent").AddApplianceColorblindLabel("Ma");
                Refs.TeaProvider.Prefab.GetChild("TeaDispenser3/ColorblindLabelParent").AddApplianceColorblindLabel("T");

                Refs.SodaProvider.Prefab.GetChild("ColorblindLabelParent1").AddApplianceColorblindLabel("R");
                Refs.SodaProvider.Prefab.GetChild("ColorblindLabelParent2").AddApplianceColorblindLabel("G");
                Refs.SodaProvider.Prefab.GetChild("ColorblindLabelParent3").AddApplianceColorblindLabel("B");

                colorblindSetup = true;
            }
        }

        protected override void OnPostActivate(KitchenMods.Mod mod)
        {
            // Register preferences
            PreferenceManager.Load();

            // Register menus
            ModsPreferencesMenu<MainMenuAction>.RegisterMenu(MOD_NAME, typeof(PreferencesMenu<MainMenuAction>), typeof(MainMenuAction));
            ModsPreferencesMenu<PauseMenuAction>.RegisterMenu(MOD_NAME, typeof(PreferencesMenu<PauseMenuAction>), typeof(PauseMenuAction));
            Events.PreferenceMenu_MainMenu_CreateSubmenusEvent += (s, args) =>
            {
                args.Menus.Add(typeof(PreferencesMenu<MainMenuAction>), new PreferencesMenu<MainMenuAction>(args.Container, args.Module_list));
            };
            Events.PreferenceMenu_PauseMenu_CreateSubmenusEvent += (s, args) =>
            {
                args.Menus.Add(typeof(PreferencesMenu<PauseMenuAction>), new PreferencesMenu<PauseMenuAction>(args.Container, args.Module_list));
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

                // Add dispense processes to Modded Kitchen milk glass
                Refs.Find<Item>("The Modded Kitchen", "Milk Glass")?.DerivedProcesses?.AddRange(Refs.MilkInCup.DerivedProcesses);

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
