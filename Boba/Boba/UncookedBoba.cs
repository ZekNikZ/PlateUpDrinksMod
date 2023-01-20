using KitchenData;
using KitchenLib.Customs;

namespace KitchenDrinksMod.Boba
{
    public class CookedBoba: CustomItem
    {
        public override string UniqueNameID => "Boba - Cooked";
        public override ItemCategory ItemCategory => ItemCategory.Generic;
        public override ItemStorage ItemStorageFlags => ItemStorage.None;
        public override ItemValue ItemValue => ItemValue.Small;
    }
}
