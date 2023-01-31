using Kitchen;
using KitchenDrinksMod.Customs;
using KitchenMods;
using Unity.Collections;
using Unity.Entities;

namespace KitchenDrinksMod.Floats
{
    public class RedSodaWithIceCream : ModItem
    {
        public override string UniqueNameID => "Cup - Ice Cream - Red";
    }
    public class GreenSodaWithIceCream : ModItem
    {
        public override string UniqueNameID => "Cup - Ice Cream - Green";
    }
    public class BlueSodaWithIceCream : ModItem
    {
        public override string UniqueNameID => "Cup - Ice Cream - Blue";
    }

    public class FloatConversionSystem : GenericSystemBase, IModSystem
    {
        private EntityQuery AllItems;

        protected override void Initialise()
        {
            AllItems = GetEntityQuery(new QueryHelper()
                .All(typeof(CItem))
            );
        }

        protected override void OnUpdate()
        {
            using var entities = AllItems.ToEntityArray(Allocator.Temp);
            using var itemComponents = AllItems.ToComponentDataArray<CItem>(Allocator.Temp);

            for (var i = 0; i < entities.Length; ++i)
            {
                var entity = entities[i];
                var itemComponent = itemComponents[i];

                if (itemComponent.ID == Refs.RedSodaWithIceCream.ID)
                {
                    EntityManager.SetComponentData(entity, new CItem
                    {
                        ID = Refs.RedFloat.ID,
                        IsPartial = true,
                        IsTransient = itemComponent.IsTransient,
                        IsGroup = true,
                        Items = new KitchenData.ItemList(Refs.RedSoda.ID, Refs.IceCreamVanilla.ID),
                        Category = Refs.RedFloat.ItemCategory
                    });
                }
                else if (itemComponent.ID == Refs.GreenSodaWithIceCream.ID)
                {
                    EntityManager.SetComponentData(entity, new CItem
                    {
                        ID = Refs.GreenFloat.ID,
                        IsPartial = true,
                        IsTransient = itemComponent.IsTransient,
                        IsGroup = true,
                        Items = new KitchenData.ItemList(Refs.GreenSoda.ID, Refs.IceCreamVanilla.ID),
                        Category = Refs.GreenFloat.ItemCategory
                    });
                }
                else if (itemComponent.ID == Refs.BlueSodaWithIceCream.ID)
                {
                    EntityManager.SetComponentData(entity, new CItem
                    {
                        ID = Refs.BlueFloat.ID,
                        IsPartial = true,
                        IsTransient = itemComponent.IsTransient,
                        IsGroup = true,
                        Items = new KitchenData.ItemList(Refs.BlueSoda.ID, Refs.IceCreamVanilla.ID),
                        Category = Refs.BlueFloat.ItemCategory
                    });
                }
            }
        }
    }
}
