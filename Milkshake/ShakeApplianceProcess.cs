using KitchenDrinksMod.Customs;

namespace KitchenDrinksMod.Milkshakes
{
    public class ShakeApplianceProcess : ModApplianceProcess<ShakeProcess>
    {
        public override string UniqueName => "Shake Appliance Process";
        public override float Speed => 0.75f;
    }
}
