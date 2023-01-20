using KitchenData;
using KitchenLib.Customs;

namespace KitchenDrinksMod.Boba.Processes
{
    public class DispenseMatchaTeaApplianceProcess : CustomApplianceProccess
    {
        public override string UniqueName => "DispenseMatchaTea";
        public override Process Process => Refs.DispenseMatchaTea;
        public override float Speed => 0.75f;
        public override bool IsAutomatic => false;
    }
}
