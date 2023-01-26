using Kitchen;
using KitchenData;
using KitchenDrinksMod.Util;
using KitchenLib.Utils;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using TMPro;
using UnityEngine;

namespace KitchenDrinksMod.Customs
{
    /// <summary>
    /// Note to onlookers: I would have just made my own version which inherits from MonoBehavior, but custom classes are not
    /// serializable by Unity when loaded from external assemblies (i.e., MODS lol). So we either need to use basic serializable
    /// types (any Unity object, C# primitives, and lists/arrays of these) or serializable types from a game DLL (from the game
    /// itself or from something included with the game (such as TMP, for example)).
    /// </summary>
    public class CompletableItemGroupView : ItemGroupView, IItemSpecificView
    {
        private Dictionary<int, ComponentGroup> DrawComponents;

        private FieldInfo fColourblindLabel = ReflectionUtils.GetField<ItemGroupView>("ColourblindLabel");
        private TextMeshPro ColourblindLabel => (TextMeshPro)fColourblindLabel.GetValue(this);

        private FieldInfo fSubviewPrefab = ReflectionUtils.GetField<ItemGroupView>("SubviewPrefab");
        private GameObject SubviewPrefab => (GameObject)fSubviewPrefab.GetValue(this);

        private FieldInfo fSubviewContainer = ReflectionUtils.GetField<ItemGroupView>("SubviewContainer");
        private GameObject SubviewContainer => (GameObject)fSubviewContainer.GetValue(this);

        [SerializeField]
        protected List<GameObject> CompletionObjects;

        [SerializeField]
        protected string CompletionLabel;

        [SerializeField]
        private CompletableItemGroupView Subview;

        private void Start()
        {
            Setup();
        }

        private void Setup()
        {
            DrawComponents = ComponentGroups.ToDictionary(e => e.Item.ID, e => e);
        }

        protected virtual bool IsComplete(ItemList components) => false;

        public new void PerformUpdate(int item_id, ItemList components)
        {
            if (SubviewPrefab != null)
            {
                if (Subview == null)
                {
                    Subview = Instantiate(SubviewPrefab).GetComponent<CompletableItemGroupView>();
                    Transform transform = Subview.transform;
                    transform.parent = SubviewContainer.transform;
                    transform.localScale = Vector3.one;
                    transform.localPosition = Vector3.zero;
                }
                Subview.PerformUpdate(item_id, components);
            }

            if (DrawComponents == null)
            {
                Setup();
            }

            foreach (var keyValuePair in DrawComponents)
            {
                if (!keyValuePair.Value.Objects.IsNullOrEmpty())
                {
                    foreach (GameObject gameObject in keyValuePair.Value.Objects)
                    {
                        gameObject.SetActive(false);
                    }
                }
                else
                {
                    keyValuePair.Value.GameObject.SetActive(false);
                }
            }

            AddComponent(item_id);
            foreach (int num in components)
            {
                AddComponent(num);
            }

            if (CompletionObjects != null)
            {
                var isComplete = IsComplete(components);
                foreach (var gameObject in CompletionObjects)
                {
                    gameObject.SetActive(isComplete);
                }
            }

            if (ColourblindLabel != null && ComponentLabels != null)
            {
                if (CompletionLabel != null && IsComplete(components))
                {
                    ColourblindLabel.text = CompletionLabel;
                }
                else
                {
                    StringBuilder stringBuilder = new();
                    foreach (ColourBlindLabel colourBlindLabel in ComponentLabels)
                    {
                        foreach (int num2 in components)
                        {
                            if (colourBlindLabel.Item.ID == num2)
                            {
                                stringBuilder.Append(colourBlindLabel.Text);
                            }
                        }
                    }
                    ColourblindLabel.text = stringBuilder.ToString();
                }
            }
        }

        private void AddComponent(int id)
        {
            if (DrawComponents.TryGetValue(id, out ComponentGroup componentGroup))
            {
                if (!componentGroup.Objects.IsNullOrEmpty())
                {
                    foreach (GameObject gameObject in componentGroup.Objects)
                    {
                        if (!gameObject.activeSelf)
                        {
                            gameObject.SetActive(true);
                            if (!componentGroup.DrawAll)
                            {
                                break;
                            }
                        }
                    }
                }
                else
                {
                    componentGroup.GameObject.SetActive(true);
                }
            }
        }
    }
}
