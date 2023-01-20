using KitchenData;
using KitchenDrinksMod.Appliances;
using KitchenDrinksMod.Boba;
using KitchenDrinksMod.Boba.Processes;
using KitchenDrinksMod.Boba.Teas;
using KitchenDrinksMod.Dishes;
using KitchenDrinksMod.Items;
using KitchenDrinksMod.Processes;
using KitchenLib.Customs;
using KitchenLib.References;
using KitchenLib.Utils;

namespace KitchenDrinksMod
{
    internal class Refs
    {
        #region Vanilla References
        public static Item IceCreamVanilla => Find<Item>(ItemReferences.IceCreamVanilla);
        public static Item IceCreamChocolate => Find<Item>(ItemReferences.IceCreamChocolate);
        public static Item IceCreamStrawberry => Find<Item>(ItemReferences.IceCreamStrawberry);
        public static Item IceCreamServing => Find<Item>(ItemReferences.IceCreamServing);
        public static Appliance Counter => Find<Appliance>(ApplianceReferences.Countertop);
        public static Item Water => Find<Item>(ItemReferences.Water);
        public static Item Pot => Find<Item>(ItemReferences.Pot);
        public static Process Cook => Find<Process>(ProcessReferences.Cook);
        #endregion

        #region IngredientLib References
        public static Item Milk => Find<Item>(IngredientLib.IngredientReferences.Milk);
        public static Item MilkIngredient => Find<Item>(IngredientLib.SplitIngredientReferences.Milk);
        #endregion

        #region Modded References
        public static Item Cup => Find<Item, Cup>();
        public static Appliance CupProvider => Find<Appliance, CupProvider>();
        public static Item MilkshakeVanilla => Find<Item, MilkshakeVanilla>();
        public static ItemGroup MilkshakeVanillaRaw => Find<ItemGroup, MilkshakeVanillaRaw>();
        public static Item MilkshakeChocolate => Find<Item, MilkshakeChocolate>();
        public static ItemGroup MilkshakeChocolateRaw => Find<ItemGroup, MilkshakeChocolateRaw>();
        public static Item MilkshakeStrawberry => Find<Item, MilkshakeStrawberry>();
        public static ItemGroup MilkshakeStrawberryRaw => Find<ItemGroup, MilkshakeStrawberryRaw>();
        public static Dish MilkshakeDish => Find<Dish, MilkshakeDish>();
        public static Process Shake => Find<Process, ShakeProcess>();
        public static Appliance.ApplianceProcesses ShakeApplianceProcess => FindApplianceProcess<ShakeApplianceProcess>();
        public static Appliance.ApplianceProcesses ShakeApplianceProcessFast => FindApplianceProcess<ShakeApplianceProcessFast>();
        public static Appliance TeaProvider => Find<Appliance, TeaProvider>();
        public static Item BlackTea => Find<Item, BlackTea>();
        public static Item MatchaTea => Find<Item, MatchaTea>();
        public static Item TaroTea => Find<Item, TaroTea>();
        public static Process DispenseBlackTea => Find<Process, DispenseBlackTea>();
        public static Process DispenseMatchaTea => Find<Process, DispenseMatchaTea>();
        public static Process DispenseTaroTea => Find<Process, DispenseTaroTea>();
        public static Appliance.ApplianceProcesses DispenseBlackTeaApplianceProcess => FindApplianceProcess<DispenseBlackTeaApplianceProcess>();
        public static Appliance.ApplianceProcesses DispenseMatchaTeaApplianceProcess => FindApplianceProcess<DispenseMatchaTeaApplianceProcess>();
        public static Appliance.ApplianceProcesses DispenseTaroTeaApplianceProcess => FindApplianceProcess<DispenseTaroTeaApplianceProcess>();
        public static Item UncookedBoba => Find<Item, UncookedBoba>();
        public static Item CookedBoba => Find<Item, CookedBoba>();
        public static Appliance BobaProvider => Find<Appliance, BobaProvider>();
        public static Item CookedBobaPot => Find<Item, CookedBobaPot>();
        #endregion

        internal static T Find<T>(int id) where T : GameDataObject
        {
            return (T)GDOUtils.GetExistingGDO(id) ?? (T)GDOUtils.GetCustomGameDataObject(id)?.GameDataObject;
        }

        internal static T Find<T, C>() where T : GameDataObject where C : CustomGameDataObject
        {
            return GDOUtils.GetCastedGDO<T, C>();
        }

        private static Appliance.ApplianceProcesses FindApplianceProcess<C>() where C : CustomSubProcess
        {
            ((CustomApplianceProccess)CustomSubProcess.GetSubProcess<C>()).Convert(out var process);
            return process;
        }
    }
}
