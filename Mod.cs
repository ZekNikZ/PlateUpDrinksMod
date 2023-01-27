﻿using Kitchen;
using KitchenLib;
using KitchenLib.Event;
using KitchenMods;
using System.Reflection;
using Unity.Entities;
using UnityEngine;
using System.Linq;
using KitchenDrinksMod.Util;
using Unity.Collections;
using TMPro;
using System.Collections.Generic;
using KitchenLib.References;
using KitchenData;
using KitchenDrinksMod.Customs;

namespace KitchenDrinksMod
{
    public class Mod : BaseMod, IModSystem
    {
        public const string MOD_GUID = "io.zkz.plateup.drinks";
        public const string MOD_NAME = "Drinks";
        public const string MOD_VERSION = "0.2.1";
        public const string MOD_AUTHOR = "ZekNikZ";
        public const string MOD_GAMEVERSION = ">=1.1.3";

#if DEBUG
        public const bool DEBUG_MODE = true;
#else
        public const bool DEBUG_MODE = false;
#endif

        private EntityQuery MenuItemQuery;

        public static AssetBundle Bundle;

        public Mod() : base(MOD_GUID, MOD_NAME, MOD_AUTHOR, MOD_VERSION, MOD_GAMEVERSION, Assembly.GetExecutingAssembly()) { }

        protected override void Initialise()
        {
            base.Initialise();

            LogWarning($"{MOD_GUID} v{MOD_VERSION} in use!");

            MenuItemQuery = GetEntityQuery(new QueryHelper()
                .All(typeof(CDishChoice)));
        }

        private void AddGameData()
        {
            LogInfo("Attempting to register game data...");

            // Note to onlookers: this is a way for me to automatically register my custom
            // GDOs without individually referencing each one here. Copy at your own risk.
            MethodInfo mAddGameDataObject = typeof(BaseMod).GetMethod(nameof(BaseMod.AddGameDataObject));
            MethodInfo mAddSubProcess = typeof(BaseMod).GetMethod(nameof(BaseMod.AddSubProcess));
            foreach (var type in Assembly.GetExecutingAssembly().GetTypes())
            {
                if (type.IsAbstract)
                {
                    continue;
                }

                if (typeof(IModGDO).IsAssignableFrom(type))
                {
                    LogInfo($"Found custom GDO of type {type.Name}");
                    MethodInfo generic = mAddGameDataObject.MakeGenericMethod(type);
                    generic.Invoke(this, null);
                }

                if (typeof(IModProcess).IsAssignableFrom(type))
                {
                    LogInfo($"Found sub process of type {type.Name}");
                    MethodInfo generic = mAddSubProcess.MakeGenericMethod(type);
                    generic.Invoke(this, null);
                }
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
            spriteAsset.material = Object.Instantiate(TMP_Settings.defaultSpriteAsset.material);
            spriteAsset.material.mainTexture = Bundle.LoadAsset<Texture2D>("ProcessIcons");
        }

        /// <summary>
        /// This entity query and modifications are only used to test my starting dish easier.
        /// </summary>
        private bool done = false;
        protected override void OnUpdate()
        {
            // Because of the silly way colorblind support is done in KitchenLib, I have to do this here
            Refs.BlackTea.Prefab.GetChildFromPath("Colour Blind").transform.localPosition = new Vector3(0, 0.2f, 0);
            Refs.MatchaTea.Prefab.GetChildFromPath("Colour Blind").transform.localPosition = new Vector3(0, 0.2f, 0);
            Refs.TaroTea.Prefab.GetChildFromPath("Colour Blind").transform.localPosition = new Vector3(0, 0.2f, 0);
            Refs.ServedVanillaMilkshake.Prefab.GetChildFromPath("Colour Blind").transform.localPosition = new Vector3(0, 0.45f, 0);
            Refs.ServedChocolateMilkshake.Prefab.GetChildFromPath("Colour Blind").transform.localPosition = new Vector3(0, 0.45f, 0);
            Refs.ServedStrawberryMilkshake.Prefab.GetChildFromPath("Colour Blind").transform.localPosition = new Vector3(0, 0.45f, 0);

            if (!DEBUG_MODE || done) return;

            var menuChoices = MenuItemQuery.ToEntityArray(Allocator.TempJob);
            foreach (var menuChoice in menuChoices)
            {
                CDishChoice cDishChoice = EntityManager.GetComponentData<CDishChoice>(menuChoice);
                cDishChoice.Dish = Refs.MilkshakeDish.ID;
                EntityManager.SetComponentData(menuChoice, cDishChoice);
            }
            menuChoices.Dispose();
        }

        protected override void OnPostActivate(KitchenMods.Mod mod)
        {
            LogInfo("Attempting to load asset bundle...");
            Bundle = mod.GetPacks<AssetBundleModPack>().SelectMany(e => e.AssetBundles).First();
            LogInfo("Done loading asset bundle.");

            AddGameData();

            AddProcessIcons();

            Events.BuildGameDataEvent += delegate (object s, BuildGameDataEventArgs args)
            {
                ModRegistry.HandleBuildGameDataEvent(args);

                // Add the milkshake shake process to the relevant processes
                List<int> slowAppliances = new()
                {
                    ApplianceReferences.Countertop,
                    ApplianceReferences.CoffeeTable,
                    ApplianceReferences.TableLarge,
                    ApplianceReferences.TableBasicCloth,
                    ApplianceReferences.TableFancyCloth,
                    ApplianceReferences.TableCheapMetal,
                    ApplianceReferences.SourceOil,
                    ApplianceReferences.Workstation
                };
                foreach (var appliance in slowAppliances)
                {
                    Refs.Find<Appliance>(appliance).Processes.Add(Refs.ShakeApplianceProcess);
                }
                List<int> fastAppliances = new()
                {
                    ApplianceReferences.Mixer,
                    ApplianceReferences.MixerHeated,
                    ApplianceReferences.MixerRapid,
                    ApplianceReferences.MixerPusher,
                };
                foreach (var appliance in fastAppliances)
                {
                    Refs.Find<Appliance>(appliance).Processes.Add(Refs.ShakeApplianceProcessFast);
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
