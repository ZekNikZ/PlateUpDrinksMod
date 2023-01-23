using KitchenData;
using KitchenDrinksMod.Customs;
using KitchenDrinksMod.Util;
using KitchenLib.Utils;
using System.Collections.Generic;
using UnityEngine;

namespace KitchenDrinksMod.Boba
{
    public class BlackBobaTea : BobaTea
    {
        public override string UniqueNameID => "Boba Tea - Black";
        public override GameObject Prefab => Prefabs.Find("BobaCupPrefab", "BlackBase");
        protected override string LiquidMaterial => "BlackTeaLiquid";
    }

    public class MatchaBobaTea : BobaTea
    {
        public override string UniqueNameID => "Boba Tea - Matcha";
        public override GameObject Prefab => Prefabs.Find("BobaCupPrefab", "MatchaBase");
        protected override string LiquidMaterial => "MatchaTeaLiquid";
    }

    public class TaroBobaTea : BobaTea
    {
        public override string UniqueNameID => "Boba Tea - Taro";
        public override GameObject Prefab => Prefabs.Find("BobaCupPrefab", "TaroBase");
        protected override string LiquidMaterial => "TaroTeaLiquid";
    }

    public abstract class BobaTea : ModItem
    {
        public abstract override string UniqueNameID { get; }
        public abstract override GameObject Prefab { get; }
        protected abstract string LiquidMaterial { get; }

        public override ItemCategory ItemCategory => ItemCategory.Generic;
        public override ItemStorage ItemStorageFlags => ItemStorage.None;
        public override ItemValue ItemValue => ItemValue.Small;
        public override Appliance DedicatedProvider => Refs.TeaProvider;

        protected override void Modify(Item item)
        {
            MaterialUtils.ApplyMaterial<MeshRenderer>(Prefab, "Cup", MaterialHelpers.GetMaterialArray("BobaCup"));
            MaterialUtils.ApplyMaterial<MeshRenderer>(Prefab, "Liquid1", MaterialHelpers.GetMaterialArray(LiquidMaterial));

            var disabledChildObjects = new List<string>()
            {
                "Boba",
                "Liquid2",
                "Lid",
                "Straw"
            };
            foreach (var childPath in disabledChildObjects)
            {
                Prefab.GetChildFromPath(childPath).SetActive(false);
            }
        }
    }
}
