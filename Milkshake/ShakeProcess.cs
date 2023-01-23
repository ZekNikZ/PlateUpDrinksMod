using KitchenData;
using KitchenDrinksMod.Customs;
using KitchenLib.Utils;
using System.Collections.Generic;

namespace KitchenDrinksMod.Milkshakes
{
    public class ShakeProcess : ModProcess
    {
        public override string UniqueNameID => "Shake Process";
        public override GameDataObject BasicEnablingAppliance => Refs.Counter;
        public override int EnablingApplianceCount => 1;
        public override string Icon => "<sprite name=\"knead\">";
        public override bool CanObfuscateProgress => true;

        public override IDictionary<Locale, ProcessInfo> LocalisedInfo => new Dictionary<Locale, ProcessInfo>() {
            { Locale.English, LocalisationUtils.CreateProcessInfo("Shake", "<sprite name=\"knead\">") }
        };
    }
}
