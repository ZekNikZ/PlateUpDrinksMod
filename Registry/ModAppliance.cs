using KitchenData;
using KitchenLib.Customs;
using System.Collections.Generic;

namespace KitchenDrinksMod.Registry
{
    public abstract class ModAppliance : CustomAppliance
    {
        public virtual IDictionary<Locale, ApplianceInfo> LocalisedInfo { get; }

        public override LocalisationObject<ApplianceInfo> Info
        {
            get
            {
                var info = new LocalisationObject<ApplianceInfo>();

                foreach (var entry in LocalisedInfo)
                {
                    info.Add(entry.Key, entry.Value);
                }

                return info;
            }
        }

        public override void OnRegister(GameDataObject gameDataObject)
        {
            Appliance appliance = gameDataObject as Appliance;
        }
    }
}
