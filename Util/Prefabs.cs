using KitchenData;
using KitchenLib.Utils;
using System.Collections.Generic;
using UnityEngine;

namespace KitchenDrinksMod
{
    internal class Prefabs
    {
        private static readonly Dictionary<string, GameObject> PrefabCache = new();

        public static GameObject Find(int id)
        {
            return (GDOUtils.GetExistingGDO(id) as IHasPrefab)?.Prefab;
        }

        public static GameObject Find(string name, string copyName = "")
        {
            if (!PrefabCache.ContainsKey(name + copyName))
            {
                var prefab = Mod.Bundle.LoadAsset<GameObject>(name);
                if (prefab == null)
                {
                    Mod.LogWarning($"Mod prefab with name \"{name}\" not found in asset bundle.");
                    return null;
                }

                if (copyName != "")
                {
                    var copy = Object.Instantiate(prefab);
                    copy.transform.localPosition = Vector3.positiveInfinity;
                    PrefabCache.Add(name + copyName, copy);
                }
                else
                {
                    PrefabCache.Add(name, prefab);
                }
            }

            return PrefabCache[name + copyName];
        }
    }
}
