using Kitchen;
using KitchenLib;
using KitchenMods;
using System.Reflection;
using UnityEngine;
using System.Linq;
using KitchenDrinksMod.Util;
using TMPro;
using ApplianceLib.Customs;
using KitchenLib.Event;
using System.Collections.Generic;
using KitchenData;
using ApplianceLib.Api;

namespace KitchenDrinksMod
{
    public class Mod : BaseMod, IModSystem
    {
        public const string MOD_GUID = "io.zkz.plateup.drinks";
        public const string MOD_NAME = "Drinks";
        public const string MOD_VERSION = "0.4.3";
        public const string MOD_AUTHOR = "ZekNikZ";
        public const string MOD_GAMEVERSION = ">=1.1.3";

#if DEBUG
        public const bool DEBUG_MODE = true;
#else
        public const bool DEBUG_MODE = false;
#endif

        public static AssetBundle Bundle;

        public Mod() : base(MOD_GUID, MOD_NAME, MOD_AUTHOR, MOD_VERSION, MOD_GAMEVERSION, Assembly.GetExecutingAssembly()) { }

        protected override void OnInitialise()
        {
            LogWarning($"{MOD_GUID} v{MOD_VERSION} in use!");
        }

        private void AddGameData()
        {
            LogInfo("Attempting to register game data...");

            ModGDOs.RegisterModGDOs(this, Assembly.GetExecutingAssembly());

            LogInfo("Done loading game data.");
        }

        private void AddProcessIcons()
        {
            // No clue why this is needed, but it seems like I need to do this before loading any icon textures:
            Bundle.LoadAllAssets<Texture2D>();
            Bundle.LoadAllAssets<Sprite>();

            var spriteAsset = Bundle.LoadAsset<TMP_SpriteAsset>("Process Icons");
            TMP_Settings.defaultSpriteAsset.fallbackSpriteAssets.Add(spriteAsset);
            spriteAsset.material = Object.Instantiate(TMP_Settings.defaultSpriteAsset.material);
            spriteAsset.material.mainTexture = Bundle.LoadAsset<Texture2D>("ProcessIcons");
        }

        /// <summary>
        /// This entity query and modifications are only used to test my starting dish easier.
        /// </summary>
        private bool colorblindSetup = false;
        protected override void OnUpdate()
        {
            if (!colorblindSetup)
            {
                Refs.BlackTea.Prefab.GetChildFromPath("Colour Blind").transform.localPosition = new Vector3(0, 0.7f, 0);
                Refs.MatchaTea.Prefab.GetChildFromPath("Colour Blind").transform.localPosition = new Vector3(0, 0.7f, 0);
                Refs.TaroTea.Prefab.GetChildFromPath("Colour Blind").transform.localPosition = new Vector3(0, 0.7f, 0);

                Refs.ServedVanillaMilkshake.Prefab.GetChildFromPath("Colour Blind").transform.localPosition = new Vector3(0, 0.45f, 0);
                Refs.ServedChocolateMilkshake.Prefab.GetChildFromPath("Colour Blind").transform.localPosition = new Vector3(0, 0.45f, 0);
                Refs.ServedStrawberryMilkshake.Prefab.GetChildFromPath("Colour Blind").transform.localPosition = new Vector3(0, 0.45f, 0);

                Refs.RedSoda.Prefab.GetChildFromPath("Colour Blind").transform.localPosition = new Vector3(0, 0.7f, 0);
                Refs.GreenSoda.Prefab.GetChildFromPath("Colour Blind").transform.localPosition = new Vector3(0, 0.7f, 0);
                Refs.BlueSoda.Prefab.GetChildFromPath("Colour Blind").transform.localPosition = new Vector3(0, 0.7f, 0);

                Refs.MilkInCup.Prefab.GetChildFromPath("Colour Blind").transform.localPosition = new Vector3(0, 0.7f, 0);

                Refs.TeaProvider.Prefab.GetChildFromPath("TeaDispenser1/ColorblindLabelParent").AddApplianceColorblindLabel("Bl");
                Refs.TeaProvider.Prefab.GetChildFromPath("TeaDispenser2/ColorblindLabelParent").AddApplianceColorblindLabel("Ma");
                Refs.TeaProvider.Prefab.GetChildFromPath("TeaDispenser3/ColorblindLabelParent").AddApplianceColorblindLabel("T");

                Refs.SodaProvider.Prefab.GetChildFromPath("ColorblindLabelParent1").AddApplianceColorblindLabel("R");
                Refs.SodaProvider.Prefab.GetChildFromPath("ColorblindLabelParent2").AddApplianceColorblindLabel("G");
                Refs.SodaProvider.Prefab.GetChildFromPath("ColorblindLabelParent3").AddApplianceColorblindLabel("B");

                colorblindSetup = true;
            }
        }

        protected override void OnPostActivate(KitchenMods.Mod mod)
        {
            LogInfo("Attempting to load asset bundle...");
            Bundle = mod.GetPacks<AssetBundleModPack>().SelectMany(e => e.AssetBundles).First();
            LogInfo("Done loading asset bundle.");

            AddGameData();

            AddProcessIcons();

            Events.BuildGameDataEvent += (s, args) =>
            {
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
                    }
                }.ToList());
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
