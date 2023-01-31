using Kitchen;
using KitchenData;
using KitchenDrinksMod.Util;
using KitchenLib.Colorblind;
using KitchenLib.Customs;
using UnityEngine;

namespace KitchenDrinksMod.Customs
{
    public abstract class ModItemGroup<T> : CustomItemGroup<T>, IModGDO where T : ItemGroupView
    {
        public abstract override string UniqueNameID { get; }
        protected virtual Vector3 ColorblindLabelPosition { get; private set; } = Vector3.zero;
        protected virtual bool AddColorblindLabel { get; private set; } = true;

        public override ItemCategory ItemCategory => ItemCategory.Generic;
        public override ItemStorage ItemStorageFlags => ItemStorage.None;
        private bool GameDataBuilt = false;

        public override sealed void OnRegister(GameDataObject gdo)
        {
            gdo.name = $"DrinksMod - {UniqueNameID}";

            if (GameDataBuilt)
            {
                return;
            }

            Modify(gdo as ItemGroup);

            if (AddColorblindLabel && Prefab.TryGetComponent<ItemGroupView>(out var itemGroupView))
            {
                GameObject clonedColourBlind = ColorblindUtils.cloneColourBlindObjectAndAddToItem(GameDataObject as ItemGroup);
                ColorblindUtils.setColourBlindLabelObjectOnItemGroupView(itemGroupView, clonedColourBlind);
                clonedColourBlind.transform.localPosition = ColorblindLabelPosition;
            }

            GameDataBuilt = true;
        }

        protected virtual void Modify(ItemGroup itemGroup) { }
    }

    public abstract class ModItemGroup : ModItemGroup<DummyItemGroupView>
    {

    }
}
