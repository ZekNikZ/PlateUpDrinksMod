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
        protected override string Name => "Black";
        protected override string LiquidMaterial => "BlackTeaLiquid";
        protected override string LidMaterial => "BlackIndicator";
    }

    public class ServedMatchaBobaTea : BaseServedBobaTea<MatchaBobaTea>
    {
        protected override string Name => "Matcha";
        protected override string LiquidMaterial => "MatchaTeaLiquid";
        protected override string LidMaterial => "MatchaIndicator";
    }

    public class ServedTaroBobaTea : BaseServedBobaTea<TaroBobaTea>
    {
        protected override string Name => "Taro";
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

    public abstract class BaseServedBobaTea<T> : ModItemGroup where T : BobaTea
    {
        protected abstract string Name { get; }
        protected abstract string LiquidMaterial { get; }
        protected abstract string LidMaterial { get; }

        public override string UniqueNameID => $"Boba Tea - {Name} - Serving";
        public override GameObject Prefab => Prefabs.Find("BobaCupPrefab", $"{Name}Served");
        public override ItemCategory ItemCategory => ItemCategory.Generic;
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
            Prefab.SetupMaterialsLikeBobaCup(LiquidMaterial, LidMaterial);

            if (!Prefab.HasComponent<ServedBobaView>())
            {
                var view = Prefab.AddComponent<ServedBobaView>();
                view.Initialize(Prefab);
            }
        }
    }
}
