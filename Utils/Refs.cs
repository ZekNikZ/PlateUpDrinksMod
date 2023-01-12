using KitchenData;
using KitchenDrinksMod.Appliances;
using KitchenDrinksMod.Dishes;
using KitchenDrinksMod.Items;
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
        public static Process Shake => Find<Process>(ProcessReferences.Chop);
        #endregion

        #region IngredientLib References
        public static Item Milk => Find<Item>(IngredientLib.References.IngredientReferences.Milk);
        public static Item MilkIngredient => Find<Item>(IngredientLib.References.SplitIngredientReferences.Milk);
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
        #endregion

        private static T Find<T>(int id) where T : GameDataObject
        {
            return (T)GDOUtils.GetExistingGDO(id) ?? (T)GDOUtils.GetCustomGameDataObject(id)?.GameDataObject;
        }

        private static T Find<T, C>() where T : GameDataObject where C : CustomGameDataObject
        {
            return (T)GDOUtils.GetCustomGameDataObject<C>()?.GameDataObject;
        }
    }
}
