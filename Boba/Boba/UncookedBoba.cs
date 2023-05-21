using KitchenData;
using KitchenLib.Customs;

namespace KitchenDrinksMod.Boba
{
    public class UncookedBoba : CustomItem
    {
        public override string UniqueNameID => "Boba - Uncooked";
        public override Appliance DedicatedProvider => Refs.BobaProvider;
    }
}
