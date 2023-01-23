using KitchenData;
using KitchenLib.Customs;

namespace KitchenDrinksMod.Milkshakes
{
    public class ShakeApplianceProcessFast : CustomApplianceProccess
    {
        public override string UniqueName => "Shake Appliance Process - Fast";
        public override Process Process => Refs.Shake;
        public override float Speed => 2f;
        public override bool IsAutomatic => true;
    }
}
