using Kitchen;
using KitchenLib.Event;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;

namespace KitchenDrinksMod.Customs
{
    internal class ModRegistry
    {
        private EntityQuery Upgrades;

        private static readonly List<ModAppliance> VariableAppliances = new();

        public static readonly Dictionary<int, List<ModAppliance.VariableApplianceProcess>> VariableApplianceProcesses = new();

        private static bool GameDataBuilt = false;

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

            // Variable processes
            foreach (var appliance in VariableAppliances)
            {
                VariableApplianceProcesses.Add(appliance.ID, appliance.VariableApplianceProcesses);
                Mod.LogInfo($"Registered variable processes for appliance \"{appliance.UniqueNameID}\"");
            }

            Mod.LogInfo("Done building additional game data.");

            GameDataBuilt = true;
        }
    }
}
