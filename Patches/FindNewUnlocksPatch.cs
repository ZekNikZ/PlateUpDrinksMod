using HarmonyLib;
using Kitchen;
using KitchenData;
using Unity.Entities;

namespace KitchenDrinksMod.Patches
{
    /// <summary>
    /// This patch is only used to ensure I get a specific card while testing in a real run.
    /// </summary>
    [HarmonyPatch(typeof(FindNewUnlocks), "AddOption")]
    public static class FindNewUnlocksPatch
    {
        public static bool IsFirst = true;
        public static UnlockOptions Options => new()
        {
            Unlock1 = Refs.MilkshakeDish,
            Unlock2 = Refs.MilkshakeDish
        };

        [HarmonyPrefix]
        public static bool Prefix(FindNewUnlocks __instance)
        {
            if (!Mod.DEBUG_MODE) return true;

            if (IsFirst)
            {
                Entity entity = __instance.EntityManager.CreateEntity(new ComponentType[]
                {
                    typeof(CProgressionOption)
                });
                __instance.EntityManager.SetComponentData(entity, new CProgressionOption
                {
                    ID = Options.Unlock1.ID,
                });

                IsFirst = false;
            }
            else
            {
                Entity entity = __instance.EntityManager.CreateEntity(new ComponentType[]
                {
                    typeof(CProgressionOption)
                });
                __instance.EntityManager.SetComponentData(entity, new CProgressionOption
                {
                    ID = Options.Unlock2.ID,
                });

                IsFirst = true;
            }

            return false;
        }
    }
}
