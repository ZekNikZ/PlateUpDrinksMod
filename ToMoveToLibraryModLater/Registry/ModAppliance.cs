using KitchenData;
using KitchenLib.Customs;
using System.Collections.Generic;

namespace KitchenDrinksMod.ToMoveToLibraryModLater.Registry
{
    public abstract class ModAppliance : CustomAppliance
    {
        public virtual IDictionary<Locale, ApplianceInfo> LocalisedInfo { get; private set; }

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

        public virtual List<VariableApplianceProcess> VariableApplianceProcesses { get; private set; }

        public override void OnRegister(GameDataObject gameDataObject)
        {
            if (VariableApplianceProcesses != null)
            {
                ModRegistry.AddVariableApplianceProcesses(this);
            }
        }
    }
}
