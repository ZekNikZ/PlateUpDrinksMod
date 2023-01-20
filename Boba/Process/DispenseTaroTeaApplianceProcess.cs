using KitchenData;
using KitchenLib.Customs;

namespace KitchenDrinksMod.Boba.Processes
{
    public class DispenseTaroTeaApplianceProcess : CustomApplianceProccess
    {
        public override string UniqueName => "DispenseTaroTea";
        public override Process Process => Refs.DispenseTaroTea;
        public override float Speed => 0.75f;
        public override bool IsAutomatic => false;
    }
}
