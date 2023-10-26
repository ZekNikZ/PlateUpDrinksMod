using ApplianceLib.Api.Prefab;
using KitchenData;
using KitchenDrinksMod.Util;
using KitchenLib.Customs;
using KitchenLib.Utils;
using System.Collections.Generic;
using UnityEngine;

namespace KitchenDrinksMod.Cups
{
    public class MilkInCup : CustomItemGroup, IColorblindLabelPositionOverride
    {
        public override string UniqueNameID => "MilkInCup";
        public override string ColourBlindTag => "Mi";
        public override ItemStorage ItemStorageFlags => ItemStorage.StackableFood;
        public override GameObject Prefab => Prefabs.Create("MilkCup");
        public Vector3 ColorblindLabelPosition => new(0, 0.7f, 0);

        public override List<ItemGroup.ItemSet> Sets => new()
        {
            new ()
            {
                Max = 2,
                Min = 2,
                Items = new()
                {
                    Refs.Cup,
                    Refs.Milk
                }
            }
        };

        public override List<Item.ItemProcess> Processes => new()
        {
            new Item.ItemProcess()
            {
                Process = Refs.DispenseBlackTea,
                Result = Refs.BlackTeaWithMilk,
                Duration = 1f
            },
            new Item.ItemProcess()
            {
                Process = Refs.DispenseMatchaTea,
                Result = Refs.MatchaTeaWithMilk,
                Duration = 1f
            },
            new Item.ItemProcess()
            {
                Process = Refs.DispenseTaroTea,
                Result = Refs.TaroTeaWithMilk,
                Duration = 1f
            }
        };

        public override void SetupPrefab(GameObject prefab)
        {
            prefab.AttachCup(MaterialUtils.GetMaterialArray("Milk")[0]);
        }
    }
}
