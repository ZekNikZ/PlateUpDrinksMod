using HarmonyLib;
using Kitchen;
using KitchenData;
using System.Collections.Generic;
using System.Linq;
using Unity.Entities;

namespace KitchenDrinksMod.Customs
{
    [HarmonyPatch(typeof(ApplyItemProcesses), "Run")]
    public class ApplyItemProcessesRunPatch
    {
        internal static Entity ApplianceEntity;
        internal static EntityManager EntityManager;

        [HarmonyPrefix]
        static bool Prefix(Entity e, CItem item, ApplyItemProcesses __instance)
        {
            EntityManager = __instance.EntityManager;

            if (EntityManager.RequireComponent(e, out CHeldBy cHeldBy) && cHeldBy.Holder != default)
            {
                ApplianceEntity = cHeldBy;
            }
            else if (EntityManager.RequireComponent(e, out CStoredBy cStoredBy))
            {
                ApplianceEntity = cStoredBy;
            }
            else
            {
                ApplianceEntity = default;
            }

            return true;
        }
    }

    [HarmonyPatch(typeof(ProcessesView), "GetRelevantProcess")]
    public class ProcessesViewGetRelevantProcessPatch
    {
        [HarmonyPrefix]
        static bool Prefix(int item, int appliance, out ApplianceProcessPair process, ref bool __result)
        {
            if (ApplyItemProcessesRunPatch.EntityManager.RequireComponent(ApplyItemProcessesRunPatch.ApplianceEntity, out CVariableProcessContainer variableProcessContainer))
            {
                if (ModRegistry.VariableApplianceProcesses.TryGetValue(appliance, out List<ModAppliance.VariableApplianceProcess> variableProcesses))
                {
                    foreach (var variableProcess in variableProcesses)
                    {
                        if (variableProcess.Item == item)
                        {
                            var itemGDO = GameData.Main.Get<Item>(item);
                            var applianceProcess = variableProcess.Processes[variableProcessContainer.Current];
                            var itemProcess = itemGDO.DerivedProcesses.First(p => p.Process.ID == applianceProcess.Process.ID);
                            process = new ApplianceProcessPair(applianceProcess.Process.ID, applianceProcess.IsAutomatic, applianceProcess.Speed / itemProcess.Duration, itemProcess.IsBad);
                            __result = true;
                            return false;
                        }
                    }
                }
            }

            process = default;
            return true;
        }
    }
}
