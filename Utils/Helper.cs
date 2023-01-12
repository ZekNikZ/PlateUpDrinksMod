using Kitchen;
using Kitchen.Components;
using KitchenData;
using KitchenLib.Customs;
using KitchenLib.References;
using KitchenLib.Utils;
using System.Collections.Generic;
using UnityEngine;

namespace KitchenDrinksMod.Utils
{
    public static class Helper
    {
        // Color
        public static Color ColorFromHex(int hex)
        {
            return new Color(((hex & 0xFF0000) >> 16) / 255.0f, ((hex & 0xFF00) >> 8) / 255.0f, (hex & 0xFF) / 255.0f);
        }

        // Prefab / GameObject
        public static GameObject GetPrefab(string name)
        {
            return Mod.Bundle.LoadAsset<GameObject>(name);
        }
        public static bool HasComponent<T>(this GameObject gameObject) where T : Component
        {
            return gameObject.GetComponent<T>() != null;
        }
        public static T TryAddComponent<T>(this GameObject gameObject) where T : Component
        {
            T comp = gameObject.GetComponent<T>();
            if (comp == null)
                return gameObject.AddComponent<T>();
            return comp;
        }
        public static GameObject GetChild(this GameObject gameObject, string childName)
        {
            return gameObject.transform.Find(childName).gameObject;
        }
        public static GameObject GetChild(this GameObject gameObject, int childIndex)
        {
            return gameObject.transform.GetChild(childIndex).gameObject;
        }
        public static GameObject GetChildFromPath(this GameObject gameObject, string childPath)
        {
            return GameObjectUtils.GetChildObject(gameObject, childPath);
        }
        public static int GetChildCount(this GameObject gameObject)
        {
            return gameObject.transform.childCount;
        }

        // GDO
        public static T GetGDO<T>(int id) where T : GameDataObject
        {
            return (T)GDOUtils.GetExistingGDO(id);
        }
        public static T1 GetModdedGDO<T1, T2>() where T1 : GameDataObject where T2 : CustomGameDataObject
        {
            return (T1)GDOUtils.GetCustomGameDataObject<T2>().GameDataObject;
        }

        // Provider Util
        internal static List<Appliance.ApplianceProcesses> CreateCounterProcesses()
        {
            return new List<Appliance.ApplianceProcesses>()
            {
                new Appliance.ApplianceProcesses()
                {
                    Process = GetGDO<Process>(ProcessReferences.Chop),
                    Speed = 0.75f,
                    IsAutomatic = false,
                    Validity = ProcessValidity.Generic
                },
                new Appliance.ApplianceProcesses()
                {
                    Process = GetGDO<Process>(ProcessReferences.Knead),
                    Speed = 0.75f,
                    IsAutomatic = false,
                    Validity = ProcessValidity.Generic
                },
            };
        }
        internal static void SetupCounter(GameObject prefab, string itemName)
        {
            SetupCounter(prefab, itemName, true);
        }
        internal static void SetupCounter(GameObject prefab, string itemName, bool addComponents)
        {
            Transform holdTransform = GameObjectUtils.GetChildObject(prefab, "Block/HoldPoint").transform;

            if (addComponents)
            {
                prefab.TryAddComponent<HoldPointContainer>().HoldPoint = holdTransform;

                var sourceView = prefab.TryAddComponent<LimitedItemSourceView>();
                sourceView.HeldItemPosition = holdTransform;
                ReflectionUtils.GetField<LimitedItemSourceView>("Items").SetValue(sourceView, new List<GameObject>()
                {
                    GameObjectUtils.GetChildObject(prefab, $"Block/HoldPoint/{itemName}")
                });
            }

            GameObject parent = prefab.GetChildFromPath("Block/Counter2");
            var paintedWood = MaterialHelpers.GetMaterialArray("Wood 4 - Painted");
            var defaultWood = MaterialHelpers.GetMaterialArray("Wood - Default");
            parent.ApplyMaterialToChild("Counter", paintedWood);
            parent.ApplyMaterialToChild("Counter Doors", paintedWood);
            parent.ApplyMaterialToChild("Counter Surface", defaultWood);
            parent.ApplyMaterialToChild("Counter Top", defaultWood);
            parent.ApplyMaterialToChild("Handles", "Knob");
        }
        internal static void SetupGenericCrates(GameObject prefab)
        {
            prefab.GetChild("GenericStorage").ApplyMaterialToChildren("Cube", "Wood - Default");
        }
        internal static void SetupFridge(GameObject prefab)
        {
            GameObject fridge = prefab.GetChild("Fridge");
            GameObject fridge2 = fridge.GetChild("Fridge2");

            prefab.TryAddComponent<ItemHolderView>();
            fridge.TryAddComponent<ItemHolderView>();

            var sourceView = fridge.TryAddComponent<ItemSourceView>();
            var quad = fridge.GetChild("Quad").GetComponent<MeshRenderer>();
            quad.materials = MaterialHelpers.GetMaterialArray("Flat Image");
            ReflectionUtils.GetField<ItemSourceView>("Renderer").SetValue(sourceView, quad);
            ReflectionUtils.GetField<ItemSourceView>("Animator").SetValue(sourceView, fridge2.GetComponent<Animator>());

            var soundSource = fridge2.TryAddComponent<AnimationSoundSource>();
            soundSource.SoundList = new List<AudioClip>() { Mod.Bundle.LoadAsset<AudioClip>("Fridge_mixdown") };
            soundSource.Category = SoundCategory.Effects;
            soundSource.ShouldLoop = false;

            // Fridge Materials
            fridge2.ApplyMaterialToChild("Body", "Metal- Shiny", "Metal- Shiny", "Metal- Shiny");
            fridge2.ApplyMaterialToChild("Door", "Metal- Shiny", "Metal Dark", "Door Glass");
            fridge2.ApplyMaterialToChild("Divider", "Plastic - Dark Grey");
            fridge2.ApplyMaterialToChild("Wire", "Plastic - Blue");
        }
        internal static void SetupLocker(GameObject prefab)
        {
            // Components
            var lockerPrefab = prefab.GetChild("Locker");
            var lockerModel = lockerPrefab.GetChild("Locker");

            prefab.TryAddComponent<ItemHolderView>();
            lockerPrefab.TryAddComponent<ItemHolderView>();

            var sourceView = lockerPrefab.TryAddComponent<ItemSourceView>();
            var quad = lockerPrefab.GetChild("Quad").GetComponent<MeshRenderer>();
            quad.materials = MaterialHelpers.GetMaterialArray("Flat Image");
            ReflectionUtils.GetField<ItemSourceView>("Renderer").SetValue(sourceView, quad);
            ReflectionUtils.GetField<ItemSourceView>("Animator").SetValue(sourceView, lockerModel.GetComponent<Animator>());

            var soundSource = lockerModel.TryAddComponent<AnimationSoundSource>();
            soundSource.SoundList = new List<AudioClip>() { Mod.Bundle.LoadAsset<AudioClip>("Fridge_mixdown") };
            soundSource.Category = SoundCategory.Effects;
            soundSource.ShouldLoop = false;

            // Models
            lockerModel.ApplyMaterialToChild("Body", "Metal- Shiny", "Metal- Shiny", "Metal- Shiny", "Plastic - Red", "Plastic - Blue");
            lockerModel.ApplyMaterialToChild("Door", "Metal- Shiny", "Door Glass", "Metal Dark");
        }

    }
}