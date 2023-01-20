using KitchenData;
using KitchenLib.Customs;
using System.Collections.Generic;
using UnityEngine;

namespace KitchenDrinksMod.Boba
{
    public class BobaCombined : CustomItemGroup
    {
        public override string UniqueNameID => "Boba - Combined";
        public override GameObject Prefab => Prefabs.BobaCombined;
        public override ItemCategory ItemCategory => ItemCategory.Generic;
        public override ItemStorage ItemStorageFlags => ItemStorage.StackableFood;
        public override ItemValue ItemValue => ItemValue.Small;
        public override Appliance DedicatedProvider => Refs.TeaProvider;

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
                    Refs.BlackTea,
                    Refs.MatchaTea,
                    Refs.TaroTea
                }
            }
        };
    }
}
