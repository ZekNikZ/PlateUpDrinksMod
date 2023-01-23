using KitchenDrinksMod.Customs;

namespace KitchenDrinksMod.Boba
{
    public class DispenseBlackTeaApplianceProcess : DispenseTeaApplianceProcess<DispenseBlackTea>
    {
        protected override string Name => "Black";
    }

    public class DispenseMatchaTeaApplianceProcess : DispenseTeaApplianceProcess<DispenseMatchaTea>
    {
        protected override string Name => "Matcha";
    }

    public class DispenseTaroTeaApplianceProcess : DispenseTeaApplianceProcess<DispenseTaroTea>
    {
        protected override string Name => "Taro";
    }

    public abstract class DispenseTeaApplianceProcess<T> : ModApplianceProcess<T> where T : DispenseTeaProcess
    {
        protected abstract string Name { get; }
        public override string UniqueName => $"Dispense ${Name} Boba Tea Appliance Process";
        public override float Speed => 0.75f;
        public override bool IsAutomatic => false;
    }
}
