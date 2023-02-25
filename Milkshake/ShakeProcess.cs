using ApplianceLib.Api;
using ApplianceLib.Customs.GDO;
using KitchenData;
using KitchenLib.Utils;
using System.Collections.Generic;

namespace KitchenDrinksMod.Milkshakes
{
    public class ShakeProcess : ModProcess
    {
        public override string UniqueNameID => "Shake Process";
        public override GameDataObject BasicEnablingAppliance => Refs.Counter;
        public override int EnablingApplianceCount => 1;
        public override bool CanObfuscateProgress => true;

        public override List<(Locale, ProcessInfo)> InfoList => new()
        {
            (Locale.English, LocalisationUtils.CreateProcessInfo("Shake", "<sprite name=\"knead\">"))
        };

        protected override void Modify(Process process)
        {
            ApplianceGroups.AddProcessToGroup(ApplianceGroup.AllCounters, new()
            {
                Process = process,
                Speed = 0.75f
            });

            ApplianceGroups.AddProcessToGroup(ApplianceGroup.Mixers, new()
            {
                Process = process,
                Speed = 2f,
                IsAutomatic = true
            });
        }
    }
}
