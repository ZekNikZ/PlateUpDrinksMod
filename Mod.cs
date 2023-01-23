using Kitchen;
using KitchenLib;
using KitchenLib.Event;
using KitchenMods;
using System.Reflection;
using Unity.Entities;
using UnityEngine;
using System.Linq;
using KitchenDrinksMod.Items;
using KitchenDrinksMod.Dishes;
using KitchenDrinksMod.Appliances;
using KitchenDrinksMod.Utils;
using Unity.Collections;
using KitchenDrinksMod.Processes;
using TMPro;
using System.Collections.Generic;
using KitchenLib.References;
using KitchenData;
using KitchenDrinksMod.Boba;
using KitchenLib.Customs;
using KitchenDrinksMod.Boba.Teas;
using KitchenDrinksMod.Boba.Processes;
using KitchenDrinksMod.ToMoveToLibraryModLater.Registry;
using KitchenDrinksMod.Boba.FinalProduct;
using KitchenLib.Utils;

// Namespace should have "Kitchen" in the beginning
namespace KitchenDrinksMod
{
    public class Mod : BaseMod, IModSystem
    {
        // guid must be unique and is recommended to be in reverse domain name notation
        // mod name that is displayed to the player and listed in the mods menu
        // mod version must follow semver e.g. "1.2.3"
        public const string MOD_GUID = "io.zkz.plateup.drinks";
        public const string MOD_NAME = "Drinks";
        public const string MOD_VERSION = "0.1.0";
        public const string MOD_AUTHOR = "ZekNikZ";
        public const string MOD_GAMEVERSION = ">=1.1.3";
        // Game version this mod is designed for in semver
        // e.g. ">=1.1.1" current and all future
        // e.g. ">=1.1.1 <=1.2.3" for all from/until

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

            // For log file output so the official plateup support staff can identify if/which a mod is being used
            LogWarning($"{MOD_GUID} v{MOD_VERSION} in use!");

            MenuItemQuery = GetEntityQuery(new QueryHelper()
                .All(typeof(CDishChoice)));
        }

        private void AddGameData()
        {
            LogInfo("Attempting to register game data...");

            // Cups
            AddGameDataObject<Cup>();
            AddGameDataObject<CupProvider>();

            // Milkshakes
            AddGameDataObject<ShakeProcess>();
            AddSubProcess<ShakeApplianceProcess>();
            AddSubProcess<ShakeApplianceProcessFast>();
            AddGameDataObject<MilkshakeVanilla>();
            AddGameDataObject<MilkshakeChocolate>();
            AddGameDataObject<MilkshakeStrawberry>();
            AddGameDataObject<MilkshakeVanillaRaw>();
            AddGameDataObject<MilkshakeChocolateRaw>();
            AddGameDataObject<MilkshakeStrawberryRaw>();
            AddGameDataObject<MilkshakeDish>();

            // Boba
            AddGameDataObject<DispenseBlackTea>();
            AddGameDataObject<DispenseMatchaTea>();
            AddGameDataObject<DispenseTaroTea>();
            AddSubProcess<DispenseBlackTeaApplianceProcess>();
            AddSubProcess<DispenseMatchaTeaApplianceProcess>();
            AddSubProcess<DispenseTaroTeaApplianceProcess>();
            AddGameDataObject<TeaProvider>();
            AddGameDataObject<BobaProvider>();
            AddGameDataObject<UncookedBoba>();
            AddGameDataObject<CookedBoba>();
            AddGameDataObject<UncookedBobaPot>();
            AddGameDataObject<CookedBobaPot>();
            AddGameDataObject<BobaDish>();
            AddGameDataObject<BlackTea>();
            AddGameDataObject<MatchaTea>();
            AddGameDataObject<TaroTea>();
            AddGameDataObject<BlackTeaCombined>();
            AddGameDataObject<MatchaTeaCombined>();
            AddGameDataObject<TaroTeaCombined>();

            LogInfo("Done loading game data.");
        }

        private void AddMaterials()
        {
            LogInfo("Attempting to create materials...");

            AddMaterial(MaterialHelpers.CreateFlat("Cup Base", 0xE0E0E0));
            AddMaterial(MaterialHelpers.CreateFlat("Straw", 0x0099DB));

            AddMaterial(MaterialHelpers.CreateFlat("Milk", 0xB0B0B0));

            AddMaterial(MaterialHelpers.CreateFlat("Vanilla", 0xFFF16D));
            AddMaterial(MaterialHelpers.CreateFlat("Chocolate", 0x622300));
            AddMaterial(MaterialHelpers.CreateFlat("Strawberry", 0xFF044E));

            foreach (var asset in Bundle.LoadAllAssets<TextAsset>())
            {
                if (asset.name.StartsWith("mat"))
                {
                    LogInfo($"Loading material from {asset.name}.json");
                    AddMaterial(CustomMaterials.LoadMaterialFromJson(asset.text));
                }
            }

            LogInfo("Done creating materials.");
        }

        private void AddProcessIcons()
        {
            // No clue why this is needed, but it seems like I need to do this before loading any textures:
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
            if (!DEBUG_MODE || done) return;

            done = true;

            var menuChoices = MenuItemQuery.ToEntityArray(Allocator.TempJob);
            foreach (var menuChoice in menuChoices)
            {
                CDishChoice cDishChoice = EntityManager.GetComponentData<CDishChoice>(menuChoice);
                cDishChoice.Dish = Refs.BobaDish.ID;
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

            AddMaterials();

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

                // Prepare the materials for the boba icon prefab
                var bobaIconPrefab = Prefabs.BobaIcon;
                MaterialUtils.ApplyMaterial<MeshRenderer>(bobaIconPrefab, "Cup", MaterialHelpers.GetMaterialArray("BobaCup"));
                MaterialUtils.ApplyMaterial<MeshRenderer>(bobaIconPrefab, "Liquid1", MaterialHelpers.GetMaterialArray("BlackTeaLiquid"));
                MaterialUtils.ApplyMaterial<MeshRenderer>(bobaIconPrefab, "Liquid2", MaterialHelpers.GetMaterialArray("BlackTeaLiquid"));
                MaterialUtils.ApplyMaterial<MeshRenderer>(bobaIconPrefab, "Lid", MaterialHelpers.GetMaterialArray("BlackIndicator"));
                MaterialUtils.ApplyMaterial<MeshRenderer>(bobaIconPrefab, "Straw", MaterialHelpers.GetMaterialArray("Straw"));
                foreach (var mesh in bobaIconPrefab.GetChildFromPath("Boba").GetComponentsInChildren<MeshRenderer>())
                {
                    mesh.materials = MaterialHelpers.GetMaterialArray("CookedBoba");
                }
            };
        }
#region Logging
        // You can remove this, I just prefer a more standardized logging
        public static void LogInfo(string _log) { Debug.Log($"[{MOD_NAME}] " + _log); }
        public static void LogWarning(string _log) { Debug.LogWarning($"[{MOD_NAME}] " + _log); }
        public static void LogError(string _log) { Debug.LogError($"[{MOD_NAME}] " + _log); }
        public static void LogInfo(object _log) { LogInfo(_log.ToString()); }
        public static void LogWarning(object _log) { LogWarning(_log.ToString()); }
        public static void LogError(object _log) { LogError(_log.ToString()); }
#endregion
    }
}
