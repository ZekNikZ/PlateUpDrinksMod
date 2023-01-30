using KitchenData;
using KitchenLib.Customs;

namespace KitchenDrinksMod.Customs
{
    public abstract class ModItem : CustomItem, IModGDO
    {
        public abstract override string UniqueNameID { get; }
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

            Modify(gdo as Item);

            GameDataBuilt = true;
        }

        protected virtual void Modify(Item item) { }
    }

    public abstract class ModItem<T> : ModItem where T : CustomAppliance
    {
        public override Appliance DedicatedProvider => Refs.Find<Appliance, T>();
    }
}
