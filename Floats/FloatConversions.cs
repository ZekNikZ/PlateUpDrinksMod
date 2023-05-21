using ApplianceLib.Api;
using KitchenData;
using KitchenDrinksMod.Soda;
using KitchenLib.Customs;

namespace KitchenDrinksMod.Floats
{
    public class RedSodaWithIceCream : BaseDummyFloat<RedSoda, RedFloat>
    {
        public override string UniqueNameID => "Cup - Ice Cream - Red";
    }

    public class GreenSodaWithIceCream : BaseDummyFloat<GreenSoda, GreenFloat>
    {
        public override string UniqueNameID => "Cup - Ice Cream - Green";
    }

    public class BlueSodaWithIceCream : BaseDummyFloat<BlueSoda, BlueFloat>
    {
        public override string UniqueNameID => "Cup - Ice Cream - Blue";
    }

    public class RootBeerWithIceCream : BaseDummyFloat<RootBeer, RootBeerFloat>
    {
        public override string UniqueNameID => "Cup - Ice Cream - RootBeer";
    }

    public abstract class BaseDummyFloat<T1, T2> : CustomItem where T1 : BaseSoda where T2 : BaseFloat<T1>
    {
        public override void OnRegister(Item item)
        {
            DummyItemConversions.AddItemConversion(item, Refs.Find<Item, T2>(), new ItemList(Refs.Find<Item, T1>().ID, Refs.IceCreamVanilla.ID));
        }
    }
}
