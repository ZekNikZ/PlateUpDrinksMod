using HarmonyLib;
using Kitchen;
using KitchenDrinksMod.Smoothie;
using UnityEngine;

namespace KitchenDrinksMod.Customs.Patches
{
    [HarmonyPatch(typeof(ItemCollectionView), "UpdateData")]
    class ItemCollectionViewPatch
    {
        static void Postfix(ref ItemCollectionView __instance)
        {
            foreach (var drawnItem in __instance.DrawnItems)
            {
                if (drawnItem.Object.GetComponentInChildren<SmoothieItemGroupView>() != null || drawnItem.Object.GetComponentInChildren<BlenderCupItemGroupView>() != null)
                {
                    if (drawnItem.IsComplete)
                    {
                        drawnItem.Object.GetComponentInChildren<ColourBlindMode>().ShowInNonColourblindMode = false;
                        drawnItem.Object.GetComponentInChildren<ColourBlindMode>().ShowInColourblindMode = false;
                    }

                    drawnItem.Object.GetComponentInChildren<ColourBlindMode>().Element.transform.localPosition = new Vector3(0.05f, 0.8f, -1.1f);
                }
            }
        }
    }
}
