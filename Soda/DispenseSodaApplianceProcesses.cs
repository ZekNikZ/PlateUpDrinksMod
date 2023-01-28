using KitchenDrinksMod.Customs;

namespace KitchenDrinksMod.Soda
{
    public class DispenseRedSodaApplianceProcess : DispenseSodaApplianceProcess<DispenseRedSoda>
    {
        protected override string Name => "Red";
    }

    public class DispenseGreenSodaApplianceProcess : DispenseSodaApplianceProcess<DispenseGreenSoda>
    {
        protected override string Name => "Green";
    }

    public class DispenseBlueSodaApplianceProcess : DispenseSodaApplianceProcess<DispenseBlueSoda>
    {
        protected override string Name => "Blue";
    }

    public abstract class DispenseSodaApplianceProcess<T> : ModApplianceProcess<T> where T : DispenseSodaProcess
    {
        protected abstract string Name { get; }
        public override string UniqueName => $"Dispense ${Name} Soda Appliance Process";
        public override float Speed => 1.25f;
        public override bool IsAutomatic => false;
    }
}
