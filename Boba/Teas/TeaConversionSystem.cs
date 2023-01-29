using Kitchen;
using KitchenMods;
using Unity.Collections;
using Unity.Entities;

namespace KitchenDrinksMod.Boba.Teas
{
    public class TeaConversionSystem : GenericSystemBase, IModSystem
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
            //using var entities = AllItems.ToEntityArray(Allocator.Temp);
            //using var itemComponents = AllItems.ToComponentDataArray<CItem>(Allocator.Temp);

            //for (var i = 0; i < entities.Length; ++i)
            //{
            //    var entity = entities[i];
            //    var itemComponent = itemComponents[i];

            //    if (itemComponent.ID == Refs.Cup.ID)
            //    {
            //        EntityManager.SetComponentData(entity, new CItem
            //        {
            //            ID = Refs.ServedBlackTea.ID,
            //            IsPartial = true,
            //            IsTransient = itemComponent.IsTransient,
            //            IsGroup = true,
            //            Items = new KitchenData.ItemList(Refs.BlackTea.ID, Refs.MilkIngredient.ID),
            //            Category = Refs.ServedBlackTea.ItemCategory
            //        });
            //    }
            //}
        }
    }
}
