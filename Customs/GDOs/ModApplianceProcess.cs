using KitchenData;
using KitchenLib.Customs;

namespace KitchenDrinksMod.Customs
{
    public abstract class ModApplianceProcess<T> : CustomApplianceProccess, IModProcess where T : CustomProcess
    {
        public abstract override string UniqueName { get; }
        public override Process Process => Refs.Find<Process, T>();
    }
}
