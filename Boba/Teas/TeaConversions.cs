using ApplianceLib.Api;
using ApplianceLib.Customs.GDO;
using KitchenData;

namespace KitchenDrinksMod.Boba
{
    public class BlackTeaWithMilk : BaseDummyTea<BlackBobaTea, ServedBlackBobaTea>
    {
        public override string UniqueNameID => "Boba Tea - Black - Milk";
    }

    public class MatchaTeaWithMilk : BaseDummyTea<MatchaBobaTea, ServedMatchaBobaTea>
    {
        public override string UniqueNameID => "Boba Tea - Matcha - Milk";
    }

    public class TaroTeaWithMilk : BaseDummyTea<TaroBobaTea, ServedTaroBobaTea>
    {
        public override string UniqueNameID => "Boba Tea - Taro - Milk";
    }

    public abstract class BaseDummyTea<T1, T2> : ModItem where T1 : BobaTea where T2 : BaseServedBobaTea<T1>
    {
        protected override void Modify(Item item)
        {
            DummyItemConversions.AddItemConversion(item, Refs.Find<Item, T2>(), new ItemList(Refs.Find<Item, T1>().ID, Refs.MilkIngredient.ID));
        }
    }
}
