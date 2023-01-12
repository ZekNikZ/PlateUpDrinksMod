using KitchenData;
using KitchenLib.Customs;

namespace KitchenDrinksMod.Processes
{
    public class ShakeApplianceProcess : CustomApplianceProccess
    {
        public override string UniqueName => "Shake";
        public override Process Process => Refs.Shake;
        public override float Speed => 1f;
    }
}
