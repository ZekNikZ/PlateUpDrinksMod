using KitchenData;
using KitchenDrinksMod.ToMoveToLibraryModLater.Registry;
using KitchenLib.Utils;
using System.Collections.Generic;

namespace KitchenDrinksMod.Boba.Processes
{
    public class DispenseTaroTea : ModProcess
    {
        public override string UniqueNameID => "DispenseTaroTea Process";
        public override GameDataObject BasicEnablingAppliance => Refs.TeaProvider;
        public override int EnablingApplianceCount => 1;
        public override string Icon => "<sprite name=\"fill_coffee\">";
        public override bool CanObfuscateProgress => true;

        public override IDictionary<Locale, ProcessInfo> LocalisedInfo => new Dictionary<Locale, ProcessInfo>() {
            { Locale.English, LocalisationUtils.CreateProcessInfo("Dispense Taro Tea", "$fill_coffee$") }
        };
    }
}
