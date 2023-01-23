using KitchenData;
using KitchenLib.Customs;
using System.Collections.Generic;

namespace KitchenDrinksMod.Customs
{
    public abstract class ModDish : CustomDish
    {
        public abstract override string UniqueNameID { get; }
        public abstract override DishType Type { get; }
        public override CardType CardType => CardType.Default;
        public override UnlockGroup UnlockGroup => UnlockGroup.Dish;
        public override float SelectionBias => 1;
        public override bool IsUnlockable => true;
        private bool GameDataBuilt = false;

        public virtual IDictionary<Locale, string> LocalisedRecipe { get; }
        public virtual IDictionary<Locale, UnlockInfo> LocalisedInfo { get; }

        public override LocalisationObject<UnlockInfo> Info
        {
            get
            {
                var info = new LocalisationObject<UnlockInfo>();

                foreach (var entry in LocalisedInfo)
                {
                    info.Add(entry.Key, entry.Value);
                }

                return info;
            }
        }

        public override sealed void OnRegister(GameDataObject gdo)
        {
            if (GameDataBuilt)
            {
                return;
            }

            Dish dish = gdo as Dish;
            ModRegistry.AddLocalisedRecipe(this);

            if (Type == DishType.Base)
            {
                ModRegistry.AddBaseDish(dish);
            }

            gdo.name = $"DrinksMod - {UniqueNameID}";

            Modify(dish);

            GameDataBuilt = true;
        }

        protected virtual void Modify(Dish dish) { }
    }
}
