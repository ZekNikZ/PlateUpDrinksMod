using ApplianceLib.Api.References;
using KitchenData;
using KitchenDrinksMod.Boba;
using KitchenDrinksMod.Cups;
using KitchenDrinksMod.Floats;
using KitchenDrinksMod.Milkshakes;
using KitchenDrinksMod.Soda;
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
        public static Appliance Portioner => Find<Appliance>(ApplianceReferences.Portioner);
        public static Item Water => Find<Item>(ItemReferences.Water);
        public static Item Pot => Find<Item>(ItemReferences.Pot);
        public static Process Cook => Find<Process>(ProcessReferences.Cook);
        public static Process Clean => Find<Process>(ProcessReferences.Clean);
        #endregion

        #region IngredientLib References
        public static Item Milk => Find<Item>("IngredientLib", "milk");
        public static Item MilkIngredient => Find<Item>("IngredientLib", "milk ingredient");
        #endregion

        #region ApplianceLib References
        public static Item Cup => ApplianceLibGDOs.Refs.Cup;
        public static Appliance CupProvider => ApplianceLibGDOs.Refs.CupProvider;
        #endregion

        #region Modded References
        // Cups
        public static Item MilkInCup => Find<Item, MilkInCup>();
        public static Item VanillaIceCreamInCup => Find<Item, VanillaIceCreamInCup>();

        // Milkshakes
        public static Item ServedVanillaMilkshake => Find<Item, ServedVanillaMilkshake>();
        public static Item ServedChocolateMilkshake => Find<Item, ServedChocolateMilkshake>();
        public static Item ServedStrawberryMilkshake => Find<Item, ServedStrawberryMilkshake>();
        public static ItemGroup VanillaMilkshake => Find<ItemGroup, VanillaMilkshake>();
        public static ItemGroup ChocolateMilkshake => Find<ItemGroup, ChocolateMilkshake>();
        public static ItemGroup StrawberryMilkshake => Find<ItemGroup, StrawberryMilkshake>();
        public static Dish MilkshakeDish => Find<Dish, MilkshakeDish>();
        public static Process Shake => Find<Process, ShakeProcess>();

        // Boba
        public static Appliance TeaProvider => Find<Appliance, BobaTeaProvider>();
        public static Item BlackTea => Find<Item, BlackBobaTea>();
        public static Item MatchaTea => Find<Item, MatchaBobaTea>();
        public static Item TaroTea => Find<Item, TaroBobaTea>();
        public static Item BlackTeaWithMilk => Find<Item, BlackTeaWithMilk>();
        public static Item MatchaTeaWithMilk => Find<Item, MatchaTeaWithMilk>();
        public static Item TaroTeaWithMilk => Find<Item, TaroTeaWithMilk>();
        public static Item ServedBlackTea => Find<Item, ServedBlackBobaTea>();
        public static Item ServedMatchaTea => Find<Item, ServedMatchaBobaTea>();
        public static Item ServedTaroTea => Find<Item, ServedTaroBobaTea>();
        public static Process DispenseBlackTea => Find<Process, DispenseBlackTea>();
        public static Process DispenseMatchaTea => Find<Process, DispenseMatchaTea>();
        public static Process DispenseTaroTea => Find<Process, DispenseTaroTea>();
        public static Item UncookedBoba => Find<Item, UncookedBoba>();
        public static Item CookedBoba => Find<Item, CookedBoba>();
        public static Item BobaBag => Find<Item, BobaBag>();
        public static Appliance BobaProvider => Find<Appliance, BobaProvider>();
        public static Item CookedBobaPot => Find<Item, CookedBobaPot>();
        public static Dish BobaDish => Find<Dish, BobaDish>();
        public static Item DirtyBobaCup => Find<Item, DirtyBobaCup>();

        // Soda
        public static Item RedSoda => Find<Item, RedSoda>();
        public static Item GreenSoda => Find<Item, GreenSoda>();
        public static Item BlueSoda => Find<Item, BlueSoda>();
        public static Appliance SodaProvider => Find<Appliance, SodaProvider>();
        public static Process DispenseRedSoda => Find<Process, DispenseRedSoda>();
        public static Process DispenseGreenSoda => Find<Process, DispenseGreenSoda>();
        public static Process DispenseBlueSoda => Find<Process, DispenseBlueSoda>();
        public static Dish SodaDish => Find<Dish, SodaDish>();

        // Floats
        public static Item RedFloat => Find<Item, RedFloat>();
        public static Item GreenFloat => Find<Item, GreenFloat>();
        public static Item BlueFloat => Find<Item, BlueFloat>();
        public static Item RedSodaWithIceCream => Find<Item, RedSodaWithIceCream>();
        public static Item GreenSodaWithIceCream => Find<Item, GreenSodaWithIceCream>();
        public static Item BlueSodaWithIceCream => Find<Item, BlueSodaWithIceCream>();
        public static Dish FloatsDish => Find<Dish, FloatsDish>();
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
