using KitchenData;
using KitchenDrinksMod.ToMoveToLibraryModLater;
using KitchenDrinksMod.Utils;
using KitchenLib.Customs;
using KitchenLib.Utils;
using System.Collections.Generic;
using UnityEngine;

namespace KitchenDrinksMod.Boba.FinalProduct
{
    public class BlackTeaCombined : BaseServedTea
    {
        public override string UniqueNameID => "Black Tea - Serving";
        public override GameObject Prefab => Prefabs.BlackTeaCup;
        protected override string LiquidMaterial => "BlackTeaLiquid";
        protected override string LidMaterial => "BlackIndicator";
        protected override Item BaseTeaItem => Refs.BlackTea;
    }

    public class MatchaTeaCombined : BaseServedTea
    {
        public override string UniqueNameID => "Matcha Tea - Serving";
        public override GameObject Prefab => Prefabs.MatchaTeaCup;
        protected override string LiquidMaterial => "MatchaTeaLiquid";
        protected override string LidMaterial => "MatchaIndicator";
        protected override Item BaseTeaItem => Refs.MatchaTea;
    }

    public class TaroTeaCombined : BaseServedTea
    {
        public override string UniqueNameID => "Taro Tea - Serving";
        public override GameObject Prefab => Prefabs.TaroTeaCup;
        protected override string LiquidMaterial => "TaroTeaLiquid";
        protected override string LidMaterial => "TaroIndicator";
        protected override Item BaseTeaItem => Refs.TaroTea;
    }

    public abstract class BaseServedTea : CustomItemGroup
    {
        public abstract override string UniqueNameID { get; }
        public abstract override GameObject Prefab { get; }
        protected abstract string LiquidMaterial { get; }
        protected abstract string LidMaterial { get; }
        protected abstract Item BaseTeaItem { get; }

        public override ItemCategory ItemCategory => ItemCategory.Generic;
        public override ItemStorage ItemStorageFlags => ItemStorage.StackableFood;
        public override ItemValue ItemValue => ItemValue.Small;

        public override List<ItemGroup.ItemSet> Sets => new()
        {
            new()
            {
                Min = 2,
                Max = 2,
                Items = new()
                {
                    Refs.CookedBoba,
                    Refs.MilkIngredient
                }
            },
            new()
            {
                Min = 1,
                Max = 1,
                IsMandatory = true,
                Items = new()
                {
                    BaseTeaItem
                }
            }
        };

        private class View : CompletableItemGroupView
        {
            public override void Initialize(GameObject prefab)
            {
                ComponentGroups = new()
                {
                    new()
                    {
                        Objects = new() { GameObjectUtils.GetChildObject(prefab, "Liquid2") },
                        Item = Refs.MilkIngredient
                    },
                    new()
                    {
                        Objects = new() { GameObjectUtils.GetChildObject(prefab, "Boba") },
                        Item = Refs.CookedBoba
                    }
                };

                ComponentLabels = new()
                {
                    new()
                    {
                        Text = "Bl",
                        Item = Refs.BlackTea
                    },
                    new()
                    {
                        Text = "Ma",
                        Item = Refs.MatchaTea
                    },
                    new()
                    {
                        Text = "Ta",
                        Item = Refs.TaroTea
                    },
                    new()
                    {
                        Text = "Mi",
                        Item = Refs.MilkIngredient
                    },
                    new()
                    {
                        Text = "Bo",
                        Item = Refs.CookedBoba
                    }
                };

                CompletionObjects = new()
                {
                    GameObjectUtils.GetChildObject(prefab, "Lid"),
                    GameObjectUtils.GetChildObject(prefab, "Straw")
                };
            }

            protected override bool IsComplete(ItemList components)
            {
                return components.Count == 3;
            }
        }

        public override void OnRegister(GameDataObject gameDataObject)
        {
            MaterialUtils.ApplyMaterial<MeshRenderer>(Prefab, "Cup", MaterialHelpers.GetMaterialArray("BobaCup"));
            MaterialUtils.ApplyMaterial<MeshRenderer>(Prefab, "Liquid1", MaterialHelpers.GetMaterialArray(LiquidMaterial));
            MaterialUtils.ApplyMaterial<MeshRenderer>(Prefab, "Liquid2", MaterialHelpers.GetMaterialArray(LiquidMaterial));
            MaterialUtils.ApplyMaterial<MeshRenderer>(Prefab, "Lid", MaterialHelpers.GetMaterialArray(LidMaterial));
            MaterialUtils.ApplyMaterial<MeshRenderer>(Prefab, "Straw", MaterialHelpers.GetMaterialArray("Straw"));

            foreach (var mesh in Prefab.GetChildFromPath("Boba").GetComponentsInChildren<MeshRenderer>())
            {
                mesh.materials = MaterialHelpers.GetMaterialArray("CookedBoba");
            }

            if (!Prefab.HasComponent<View>())
            {
                var view = Prefab.AddComponent<View>();
                view.Initialize(Prefab);
            }
        }
    }
}
