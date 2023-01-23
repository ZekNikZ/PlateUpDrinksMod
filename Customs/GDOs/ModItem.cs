using KitchenData;
using KitchenLib.Customs;

namespace KitchenDrinksMod.Customs
{
    public abstract class ModItem : CustomItem
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
