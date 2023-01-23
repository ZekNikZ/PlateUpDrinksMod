using KitchenData;
using KitchenLib.Customs;
using System.Collections.Generic;

namespace KitchenDrinksMod.Customs
{
    public abstract class ModProcess : CustomProcess
    {
        public abstract override string UniqueNameID { get; }
        public virtual IDictionary<Locale, ProcessInfo> LocalisedInfo { get; }
        private bool GameDataBuilt = false;

        public override LocalisationObject<ProcessInfo> Info
        {
            get
            {
                var info = new LocalisationObject<ProcessInfo>();

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

            Modify(gdo as Process);

            GameDataBuilt = true;
        }

        protected virtual void Modify(Process process) { }
    }
}
