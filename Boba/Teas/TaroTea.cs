using KitchenData;
using KitchenLib.Customs;

namespace KitchenDrinksMod.Boba.Teas
{
    public class TaroTea : CustomItemGroup
    {
        public override string UniqueNameID => "Taro Boba Tea";
        public override ItemCategory ItemCategory => ItemCategory.Generic;
        public override ItemStorage ItemStorageFlags => ItemStorage.None;
        public override ItemValue ItemValue => ItemValue.Small;
        public override Appliance DedicatedProvider => Refs.TeaProvider;
    }
}
