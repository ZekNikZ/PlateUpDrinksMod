using ApplianceLib.Api;
using ApplianceLib.Customs.GDO;
using KitchenData;
using KitchenDrinksMod.Soda;

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

    public abstract class BaseDummyFloat<T1, T2> : ModItem where T1 : BaseSoda where T2 : BaseFloat<T1>
    {
        protected override void Modify(Item item)
        {
            DummyItemConversions.AddItemConversion(item, Refs.Find<Item, T2>(), new ItemList(Refs.Find<Item, T1>().ID, Refs.IceCreamVanilla.ID));
        }
    }
}
