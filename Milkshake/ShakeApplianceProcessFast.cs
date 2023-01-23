using KitchenDrinksMod.Customs;

namespace KitchenDrinksMod.Milkshakes
{
    public class ShakeApplianceProcessFast : ModApplianceProcess<ShakeProcess>
    {
        public override string UniqueName => "Shake Appliance Process - Fast";
        public override float Speed => 2f;
        public override bool IsAutomatic => true;
    }
}
