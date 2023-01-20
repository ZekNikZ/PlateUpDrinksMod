using KitchenData;
using KitchenLib.Customs;

namespace KitchenDrinksMod.Boba.Processes
{
    public class DispenseBlackTeaApplianceProcess : CustomApplianceProccess
    {
        public override string UniqueName => "DispenseBlackTea";
        public override Process Process => Refs.DispenseBlackTea;
        public override float Speed => 0.75f;
        public override bool IsAutomatic => false;
    }
}
