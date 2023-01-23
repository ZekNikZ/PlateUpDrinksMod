using KitchenData;
using KitchenDrinksMod.Customs;
using KitchenDrinksMod.Util;
using KitchenLib.Utils;
using System.Collections.Generic;
using UnityEngine;

namespace KitchenDrinksMod.Boba
{
    public class ServedBlackBobaTea : BaseServedBobaTea<BlackBobaTea>
    {
        public override string UniqueNameID => "Boba Tea - Black - Serving";
        public override GameObject Prefab => Prefabs.BlackTeaCup;
        protected override string LiquidMaterial => "BlackTeaLiquid";
        protected override string LidMaterial => "BlackIndicator";
    }

    public class ServedMatchaBobaTea : BaseServedBobaTea<MatchaBobaTea>
    {
        public override string UniqueNameID => "Boba Tea - Matcha - Serving";
        public override GameObject Prefab => Prefabs.MatchaTeaCup;
        protected override string LiquidMaterial => "MatchaTeaLiquid";
        protected override string LidMaterial => "MatchaIndicator";
    }

    public class ServedTaroBobaTea : BaseServedBobaTea<TaroBobaTea>
    {
        public override string UniqueNameID => "Boba Tea - Taro - Serving";
        public override GameObject Prefab => Prefabs.TaroTeaCup;
        protected override string LiquidMaterial => "TaroTeaLiquid";
        protected override string LidMaterial => "TaroIndicator";
    }
    internal class ServedBobaView : CompletableItemGroupView
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

    public abstract class BaseServedBobaTea<T> : ModItemGroup where T: BobaTea
    {
        public abstract override string UniqueNameID { get; }
        public abstract override GameObject Prefab { get; }
        protected abstract string LiquidMaterial { get; }
        protected abstract string LidMaterial { get; }

        public override ItemStorage ItemStorageFlags => ItemStorage.StackableFood;
        public override ItemValue ItemValue => ItemValue.Medium;

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
                    Refs.Find<Item, T>()
                }
            }
        };

        protected override void Modify(ItemGroup itemGroup)
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

            if (!Prefab.HasComponent<ServedBobaView>())
            {
                var view = Prefab.AddComponent<ServedBobaView>();
                view.Initialize(Prefab);
            }
        }
    }
}
