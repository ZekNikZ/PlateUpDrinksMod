using KitchenData;
using KitchenLib.Customs;
using System.Collections.Generic;

namespace KitchenDrinksMod.Customs
{
    public abstract class ModUnlockCard : CustomUnlockCard, IModGDO
    {
        public abstract override string UniqueNameID { get; }
        public override CardType CardType => CardType.Default;
        public override UnlockGroup UnlockGroup => UnlockGroup.Generic;
        public override bool IsUnlockable => true;
        private bool GameDataBuilt = false;

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

            UnlockCard card = gdo as UnlockCard;

            gdo.name = $"DrinksMod - {UniqueNameID}";

            Modify(card);

            GameDataBuilt = true;
        }

        protected virtual void Modify(UnlockCard dish) { }
    }
}
