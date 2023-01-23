using KitchenData;
using KitchenLib.Customs;

namespace KitchenDrinksMod.Customs
{
    public abstract class ModItemGroup : CustomItemGroup, IModGDO
    {
        public abstract override string UniqueNameID { get; }
        public override ItemCategory ItemCategory => ItemCategory.Generic;
        public override ItemStorage ItemStorageFlags => ItemStorage.None;
        private bool GameDataBuilt = false;

        public override sealed void OnRegister(GameDataObject gdo)
        {
            if (GameDataBuilt)
            {
                return;
            }

            gdo.name = $"DrinksMod - {UniqueNameID}";

            Modify(gdo as ItemGroup);

            GameDataBuilt = true;
        }

        protected virtual void Modify(ItemGroup itemGroup) { }
    }
}
