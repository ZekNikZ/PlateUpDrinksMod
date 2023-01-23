using Kitchen;
using KitchenData;
using KitchenLib.Event;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;

namespace KitchenDrinksMod.Customs
{
    [UpdateBefore(typeof(GrantUpgrades))]
    internal class ModRegistry : GenericSystemBase
    {
        private EntityQuery Upgrades;

        private static readonly List<ModDish> RecipeHolders = new();
        private static readonly List<Dish> BaseDishes = new();
        private static readonly List<ModAppliance> VariableAppliances = new();

        public static readonly Dictionary<int, List<ModAppliance.VariableApplianceProcess>> VariableApplianceProcesses = new();

        private static bool GameDataBuilt = false;

        public static void AddLocalisedRecipe(ModDish dish)
        {
            RecipeHolders.Add(dish);
        }

        public static void AddBaseDish(Dish dish)
        {
            BaseDishes.Add(dish);
        }

        public static void AddVariableApplianceProcesses(ModAppliance appliance)
        {
            VariableAppliances.Add(appliance);
        }

        public static void HandleBuildGameDataEvent(BuildGameDataEventArgs args)
        {
            if (GameDataBuilt)
            {
                return;
            }

            // Recipe holders
            foreach (var holder in RecipeHolders)
            {
                foreach (var entry in holder.LocalisedRecipe)
                {
                    args.gamedata.GlobalLocalisation.Recipes.Info.Get(entry.Key).Text.Add(holder.GameDataObject as Dish, entry.Value);
                    Mod.LogInfo($"Registered recipe \"{entry.Key}\" localization entry for dish {(holder.GameDataObject as Dish).Name} ({holder.GameDataObject.ID}): \"{entry.Value}\"");
                }
            }

            // Variable processes
            foreach (var appliance in VariableAppliances)
            {
                VariableApplianceProcesses.Add(appliance.ID, appliance.VariableApplianceProcesses);
                Mod.LogInfo($"Registered variable processes for appliance \"{appliance.UniqueNameID}\"");
            }

            Mod.LogInfo("Done building additional game data.");

            GameDataBuilt = true;
        }

        protected override void Initialise()
        {
            Upgrades = GetEntityQuery(typeof(CUpgrade));
        }

        protected override void OnUpdate()
        {
            // Base dishes
            using NativeArray<CUpgrade> existing = Upgrades.ToComponentDataArray<CUpgrade>(Allocator.Temp);
            foreach (var dish in BaseDishes)
            {
                foreach (CUpgrade item in existing)
                {
                    if (item.ID == dish.ID)
                    {
                        goto next_dish;
                    }
                }

                var entity = EntityManager.CreateEntity(typeof(CUpgrade), typeof(CPersistThroughSceneChanges));
                EntityManager.AddComponentData(entity, new CUpgrade
                {
                    ID = dish.ID
                });
                Mod.LogInfo($"Registered base dish {dish.Name} ({dish.ID})");

            next_dish: { }
            }
        }
    }
}
