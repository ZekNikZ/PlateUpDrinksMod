using ApplianceLib.Customs.GDO;
using KitchenData;
using KitchenLib.Utils;
using System.Collections.Generic;

namespace KitchenDrinksMod.Soda
{
    public class DispenseRedSoda : DispenseSodaProcess
    {
        protected override string Name => "Red";
    }

    public class DispenseGreenSoda : DispenseSodaProcess
    {
        protected override string Name => "Green";
    }

    public class DispenseBlueSoda : DispenseSodaProcess
    {
        protected override string Name => "Blue";
    }

    public abstract class DispenseSodaProcess : ModProcess
    {
        protected abstract string Name { get; }
        public override string UniqueNameID => $"Dispense {Name} Soda Process";
        public override GameDataObject BasicEnablingAppliance => Refs.SodaProvider;
        public override int EnablingApplianceCount => 1;
        public override bool CanObfuscateProgress => true;
        public override List<(Locale, ProcessInfo)> InfoList => new()
        {
            (Locale.English, LocalisationUtils.CreateProcessInfo($"Dispense {Name} Soda", "<sprite name=\"fill_soda\">"))
        };
    }
}
