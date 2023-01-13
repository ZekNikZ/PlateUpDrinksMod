using HarmonyLib;
using KitchenData;
using KitchenLib.Utils;
using System.Collections.Generic;
using System.Linq;

namespace KitchenDrinksMod.Patches
{
    /// <summary>
    /// This class only exists until a mod is made to improve the likelihood of modded dishes/cards showing up.
    /// </summary>
    [HarmonyPatch(typeof(UnlockSorterPriority), "SortCards")]
    public static class UnlockSorterPriorityPatch
    {
        [HarmonyPrefix]
        public static bool Prefix(ref List<Unlock> candidates, HashSet<int> current_cards, UnlockRequest request, UnlockSorterPriority __instance)
        {
            var mIsPriority = ReflectionUtils.GetMethod<UnlockSorterPriority>("IsPriority");

            float randVal = UnityEngine.Random.value;
            bool is_priority = randVal < __instance.PriorityProbability;
            candidates = candidates.OrderByDescending((Unlock c) => ((is_priority && (bool) mIsPriority.Invoke(__instance, new object[] { c })) || randVal < c.SelectionBias) ? 1 : 0).ToList();

            return false;
        }
    }
}
