using KitchenLib.Utils;
using UnityEngine;

namespace KitchenDrinksMod.Util
{
    internal static class MaterialHelpers
    {
        public static GameObject SetupMaterialsLikeCounter(this GameObject gameObject)
        {
            gameObject.ApplyMaterialToChild("Block/Counter2/Counter", "Wood 4 - Painted");
            gameObject.ApplyMaterialToChild("Block/Counter2/Counter Doors", "Wood 4 - Painted");
            gameObject.ApplyMaterialToChild("Block/Counter2/Counter Surface", "Wood - Default");
            gameObject.ApplyMaterialToChild("Block/Counter2/Counter Top", "Wood - Default");
            gameObject.ApplyMaterialToChild("Block/Counter2/Handles", "Knob");

            return gameObject;
        }

        public static GameObject SetupMaterialsLikePot(this GameObject gameObject)
        {
            gameObject.ApplyMaterialToChild("Pot/Pot/Cylinder", "Metal");
            gameObject.ApplyMaterialToChild("Pot/Pot/Cylinder.003", "Metal Dark");
            gameObject.ApplyMaterialToChild("Water", "Water");

            return gameObject;
        }

        public static GameObject SetupMaterialsLikeBobaCup(this GameObject gameObject, string liquidMaterial, string lidMaterial = "BlackIndicator")
        {

            gameObject.ApplyMaterialToChild("Cup", "BobaCup");
            gameObject.ApplyMaterialToChild("Liquid1", liquidMaterial);
            gameObject.ApplyMaterialToChild("Liquid2", liquidMaterial);
            gameObject.ApplyMaterialToChild("Lid", lidMaterial);
            gameObject.ApplyMaterialToChild("Straw", ApplianceLib.Api.References.MaterialReferences.CupStraw); ;

            gameObject.GetChild("Boba").ApplyMaterialToChildren("Boba", "CookedBoba");

            return gameObject;
        }

        public static GameObject SetupMaterialsLikeSodaCup(this GameObject gameObject, string liquidMaterial)
        {

            gameObject.ApplyMaterialToChild("Model/Cup", "CupBase");
            gameObject.ApplyMaterialToChild("Model/Liquid", liquidMaterial);
            gameObject.ApplyMaterialToChild("Model/Straw", ApplianceLib.Api.References.MaterialReferences.CupStraw);

            return gameObject;
        }

        public static GameObject SetupMaterialsLikeMilkshake(this GameObject gameObject, string liquidMaterial, string iceCreamMaterial = "Vanilla")
        {
            gameObject.ApplyMaterialToChild("MilkshakeCup/Cup", "BobaCup");
            gameObject.ApplyMaterialToChild("MilkshakeCup/LiquidFull", liquidMaterial);
            gameObject.ApplyMaterialToChild("MilkshakeCup/LiquidHalf", liquidMaterial);
            gameObject.ApplyMaterialToChild("MilkshakeCup/Straw", ApplianceLib.Api.References.MaterialReferences.CupStraw);
            gameObject.ApplyMaterialToChild("MilkshakeCup/IceCream1", iceCreamMaterial);
            gameObject.ApplyMaterialToChild("MilkshakeCup/IceCream2", iceCreamMaterial);
            gameObject.ApplyMaterialToChild("MilkshakeCup/IceCream3", iceCreamMaterial);

            return gameObject;
        }
    }
}