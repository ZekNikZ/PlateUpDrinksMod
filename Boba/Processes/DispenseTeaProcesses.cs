using KitchenData;
using KitchenDrinksMod.Customs;
using KitchenLib.Utils;
using System.Collections.Generic;

namespace KitchenDrinksMod.Boba
{
    public class DispenseBlackTea : DispenseTeaProcess
    {
        protected override string Name => "Black";
    }

    public class DispenseMatchaTea : DispenseTeaProcess
    {
        protected override string Name => "Matcha";
    }

    public class DispenseTaroTea : DispenseTeaProcess
    {
        protected override string Name => "Taro";
    }

    public abstract class DispenseTeaProcess : ModProcess
    {
        protected abstract string Name { get; }
        public override string UniqueNameID => $"Dispense {Name} Boba Tea Process";
        public override GameDataObject BasicEnablingAppliance => Refs.TeaProvider;
        public override int EnablingApplianceCount => 1;
        public override string Icon => "<sprite name=\"fill_boba\">";
        public override bool CanObfuscateProgress => true;
        public override IDictionary<Locale, ProcessInfo> LocalisedInfo => new Dictionary<Locale, ProcessInfo>() {
            { Locale.English, LocalisationUtils.CreateProcessInfo($"Dispense {Name} Tea", "<sprite name=\"fill_boba\">") }
        };
    }
}
