using Kitchen;
using KitchenData;
using KitchenDrinksMod.Customs;
using KitchenDrinksMod.Util;
using KitchenLib.Utils;
using System.Collections.Generic;
using UnityEngine;

namespace KitchenDrinksMod.Boba
{
    public class UncookedBobaPot : ModItemGroup
    {
        public override string UniqueNameID => "Boba - Pot - Uncooked";
        public override GameObject Prefab => Prefabs.Find("BobaPot");
        public override Item DisposesTo => Refs.Pot;

        public override List<ItemGroup.ItemSet> Sets => new()
        {
            new()
            {
                Min = 2,
                Max = 2,
                Items = new List<Item>
                {
                    Refs.UncookedBoba,
                    Refs.Water
                }
            },
            new()
            {
                Min = 1,
                Max = 1,
                IsMandatory = true,
                Items = new List<Item>
                {
                    Refs.Pot
                }
            }
        };

        public override List<Item.ItemProcess> Processes => new()
        {
            new()
            {
                Process = Refs.Cook,
                Result = Refs.CookedBobaPot,
                Duration = 1
            }
        };

        private class UncookedBobaPotView : ItemGroupView
        {
            internal void Setup(GameObject prefab)
            {
                ComponentGroups = new()
                {
                    new ComponentGroup()
                    {
                        GameObject = GameObjectUtils.GetChildObject(prefab, "BobaBalls"),
                        Item = Refs.UncookedBoba
                    },
                    new ComponentGroup()
                    {
                        GameObject = GameObjectUtils.GetChildObject(prefab, "Water"),
                        Item = Refs.Water
                    }
                };
            }
        }

        protected override void Modify(ItemGroup itemGroup)
        {
            Prefab.SetupMaterialsLikePot();
            Prefab.GetChildFromPath("BobaBalls").ApplyMaterialToChildren("UncookedBoba");

            var view = Prefab.AddComponent<UncookedBobaPotView>();
            view.Setup(Prefab);
        }
    }
}
