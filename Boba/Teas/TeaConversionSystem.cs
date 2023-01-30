using Kitchen;
using KitchenDrinksMod.Customs;
using KitchenMods;
using Unity.Collections;
using Unity.Entities;

namespace KitchenDrinksMod.Boba
{
    public class BlackTeaWithMilk : ModItem
    {
        public override string UniqueNameID => "Boba Tea - Black - Milk";
    }
    public class MatchaTeaWithMilk : ModItem
    {
        public override string UniqueNameID => "Boba Tea - Matcha - Milk";
    }
    public class TaroTeaWithMilk : ModItem
    {
        public override string UniqueNameID => "Boba Tea - Taro - Milk";
    }

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
            using var entities = AllItems.ToEntityArray(Allocator.Temp);
            using var itemComponents = AllItems.ToComponentDataArray<CItem>(Allocator.Temp);

            for (var i = 0; i < entities.Length; ++i)
            {
                var entity = entities[i];
                var itemComponent = itemComponents[i];

                if (itemComponent.ID == Refs.BlackTeaWithMilk.ID)
                {
                    EntityManager.SetComponentData(entity, new CItem
                    {
                        ID = Refs.ServedBlackTea.ID,
                        IsPartial = true,
                        IsTransient = itemComponent.IsTransient,
                        IsGroup = true,
                        Items = new KitchenData.ItemList(Refs.BlackTea.ID, Refs.MilkIngredient.ID),
                        Category = Refs.ServedBlackTea.ItemCategory
                    });
                }
                else if (itemComponent.ID == Refs.MatchaTeaWithMilk.ID)
                {
                    EntityManager.SetComponentData(entity, new CItem
                    {
                        ID = Refs.ServedMatchaTea.ID,
                        IsPartial = true,
                        IsTransient = itemComponent.IsTransient,
                        IsGroup = true,
                        Items = new KitchenData.ItemList(Refs.MatchaTea.ID, Refs.MilkIngredient.ID),
                        Category = Refs.ServedMatchaTea.ItemCategory
                    });
                }
                else if (itemComponent.ID == Refs.TaroTeaWithMilk.ID)
                {
                    EntityManager.SetComponentData(entity, new CItem
                    {
                        ID = Refs.ServedTaroTea.ID,
                        IsPartial = true,
                        IsTransient = itemComponent.IsTransient,
                        IsGroup = true,
                        Items = new KitchenData.ItemList(Refs.TaroTea.ID, Refs.MilkIngredient.ID),
                        Category = Refs.ServedTaroTea.ItemCategory
                    });
                }
            }
        }
    }
}
