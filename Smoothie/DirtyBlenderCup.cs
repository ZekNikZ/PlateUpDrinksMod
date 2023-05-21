using KitchenData;
using KitchenLib.Customs;
using KitchenLib.References;
using System.Collections.Generic;
using UnityEngine;

namespace KitchenDrinksMod.Smoothie
{
    public class DirtyBlenderCup : CustomItem
    {
        public override string UniqueNameID => "dirty smoothie cup";
        public override GameObject Prefab => Refs.Find<Item>(ItemReferences.Apple).Prefab;
        public override List<Item.ItemProcess> Processes => new()
        {
            new()
            {
                Duration = 0.75f,
                Process = Refs.Clean,
                Result = Refs.BlenderCup
            }
        };

        public override void SetupPrefab(GameObject prefab)
        {
            //PrefabBuilder.AttachBlenderCup(prefab);
        }
    }
}
