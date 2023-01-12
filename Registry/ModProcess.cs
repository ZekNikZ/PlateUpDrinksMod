using KitchenData;
using KitchenLib.Customs;
using System.Collections.Generic;

namespace KitchenDrinksMod.Processes
{
    public abstract class ModProcess : CustomProcess
    {
        public virtual IDictionary<Locale, ProcessInfo> LocalisedInfo { get; }

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
    }
}
