using KitchenData;
using KitchenLib.Customs;
using UnityEngine;

namespace KitchenDrinksMod.Boba
{
    public class UncookedBoba: CustomItem
    {
        public override string UniqueNameID => "Boba - Uncooked";
        public override ItemCategory ItemCategory => ItemCategory.Generic;
        public override ItemStorage ItemStorageFlags => ItemStorage.StackableFood;
        public override ItemValue ItemValue => ItemValue.Small;
        public override Appliance DedicatedProvider => Refs.BobaProvider;
    }
}
