using KitchenData;
using KitchenDrinksMod.Boba;
using KitchenDrinksMod.Cups;
using KitchenDrinksMod.Milkshake;
using KitchenDrinksMod.Milkshakes;
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
        public static Item Milk => Find<Item>("IngredientLib", "milk ");
        //public static Item Milk => Find<Item>(IngredientLib.References.GetIngredient("milk"));
        public static Item MilkIngredient => Find<Item>("IngredientLib", "milk ingredient");
        //public static Item MilkIngredient => Find<Item>(IngredientLib.References.GetSplitIngredient("milk"));
        #endregion

        #region Modded References
        // Cups
        public static Item Cup => Find<Item, Cup>();
        public static Appliance CupProvider => Find<Appliance, CupProvider>();

        // Milkshakes
        public static Item ServedVanillaMilkshake => Find<Item, ServedVanillaMilkshake>();
        public static Item ServedChocolateMilkshake => Find<Item, ServedChocolateMilkshake>();
        public static Item ServedStrawberryMilkshake => Find<Item, ServedStrawberryMilkshake>();
        public static ItemGroup VanillaMilkshake => Find<ItemGroup, VanillaMilkshake>();
        public static ItemGroup ChocolateMilkshake => Find<ItemGroup, ChocolateMilkshake>();
        public static ItemGroup StrawberryMilkshake => Find<ItemGroup, StrawberryMilkshake>();
        public static Dish MilkshakeDish => Find<Dish, MilkshakeDish>();
        public static Process Shake => Find<Process, ShakeProcess>();
        public static Appliance.ApplianceProcesses ShakeApplianceProcess => FindApplianceProcess<ShakeApplianceProcess>();
        public static Appliance.ApplianceProcesses ShakeApplianceProcessFast => FindApplianceProcess<ShakeApplianceProcessFast>();

        // Boba
        public static Appliance TeaProvider => Find<Appliance, BobaTeaProvider>();
        public static Item BlackTea => Find<Item, BlackBobaTea>();
        public static Item MatchaTea => Find<Item, MatchaBobaTea>();
        public static Item TaroTea => Find<Item, TaroBobaTea>();
        public static Item ServedBlackTea => Find<Item, ServedBlackBobaTea>();
        public static Item ServedMatchaTea => Find<Item, ServedMatchaBobaTea>();
        public static Item ServedTaroTea => Find<Item, ServedTaroBobaTea>();
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
        public static Dish BobaDish => Find<Dish, BobaDish>();
        #endregion

        internal static T Find<T>(int id) where T : GameDataObject
        {
            return (T)GDOUtils.GetExistingGDO(id) ?? (T)GDOUtils.GetCustomGameDataObject(id)?.GameDataObject;
        }

        internal static T Find<T, C>() where T : GameDataObject where C : CustomGameDataObject
        {
            return GDOUtils.GetCastedGDO<T, C>();
        }

        internal static T Find<T>(string modName, string name) where T : GameDataObject
        {
            return GDOUtils.GetCastedGDO<T>(modName, name);
        }

        private static Appliance.ApplianceProcesses FindApplianceProcess<C>() where C : CustomSubProcess
        {
            ((CustomApplianceProccess)CustomSubProcess.GetSubProcess<C>()).Convert(out var process);
            return process;
        }
    }
}
