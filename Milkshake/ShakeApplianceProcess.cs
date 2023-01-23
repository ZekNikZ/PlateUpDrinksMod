using KitchenData;
using KitchenLib.Customs;

namespace KitchenDrinksMod.Milkshakes
{
    public class ShakeApplianceProcess : CustomApplianceProccess
    {
        public override string UniqueName => "Shake Appliance Process";
        public override Process Process => Refs.Shake;
        public override float Speed => 0.75f;
    }
}
