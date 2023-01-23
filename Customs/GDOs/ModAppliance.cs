using KitchenData;
using KitchenLib.Customs;
using System.Collections.Generic;

namespace KitchenDrinksMod.Customs
{
    public abstract class ModAppliance : CustomAppliance
    {
        public struct VariableApplianceProcess
        {
            public int Item;
            public List<Appliance.ApplianceProcesses> Processes;
        }

        public abstract override string UniqueNameID { get; }
        public virtual IDictionary<Locale, ApplianceInfo> LocalisedInfo { get; internal set; }
        public virtual List<VariableApplianceProcess> VariableApplianceProcesses { get; internal set; }
        private bool GameDataBuilt = false;

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

        public override sealed void OnRegister(GameDataObject gdo)
        {
            if (GameDataBuilt)
            {
                return;
            }

            if (VariableApplianceProcesses != null)
            {
                ModRegistry.AddVariableApplianceProcesses(this);
            }

            gdo.name = $"DrinksMod - {UniqueNameID}";

            Modify(gdo as Appliance);

            GameDataBuilt = true;
        }

        protected virtual void Modify(Appliance appliance) { }
    }
}
