using KitchenData;
using KitchenDrinksMod.Utils;
using KitchenLib.Customs;
using KitchenLib.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace KitchenDrinksMod.Boba.Teas
{
    public class BlackTea : BaseTea
    {
        public override string UniqueNameID => "Black Tea - Base";
        public override GameObject Prefab => Prefabs.BlackTeaBase;
        protected override string LiquidMaterial => "BlackTeaLiquid";
    }

    public class MatchaTea : BaseTea
    {
        public override string UniqueNameID => "Matcha Tea - Base";
        public override GameObject Prefab => Prefabs.MatchaTeaBase;
        protected override string LiquidMaterial => "MatchaTeaLiquid";
    }

    public class TaroTea : BaseTea
    {
        public override string UniqueNameID => "Taro Tea - Base";
        public override GameObject Prefab => Prefabs.TaroTeaBase;
        protected override string LiquidMaterial => "TaroTeaLiquid";
    }

    public abstract class BaseTea: CustomItem
    {
        public abstract override string UniqueNameID { get; }
        public abstract override GameObject Prefab { get; }
        protected abstract string LiquidMaterial { get; }

        public override ItemCategory ItemCategory => ItemCategory.Generic;
        public override ItemStorage ItemStorageFlags => ItemStorage.None;
        public override ItemValue ItemValue => ItemValue.Small;
        public override Appliance DedicatedProvider => Refs.TeaProvider;

        public override void OnRegister(GameDataObject gameDataObject)
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
            foreach(var childPath in disabledChildObjects)
            {
                Prefab.GetChildFromPath(childPath).SetActive(false);
            }
        }
    }
}
