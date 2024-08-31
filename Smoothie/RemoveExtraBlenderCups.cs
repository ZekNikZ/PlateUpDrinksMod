using Kitchen;
using Unity.Collections;
using Unity.Entities;

namespace KitchenDrinksMod.Smoothie
{
    internal class RemoveExtraBlenderCups : GenericSystemBase
    {
        private EntityQuery Items;

        protected override void Initialise()
        {
            Items = GetEntityQuery(typeof(CItem));
        }

        protected override void OnUpdate()
        {
            using var entities = Items.ToEntityArray(Allocator.Temp);
            using var items = Items.ToComponentDataArray<CItem>(Allocator.Temp);

            for (int i = 0; i < entities.Length; i++)
            {
                var entity = entities[i];
                var item = items[i];

                if (item.ID == Refs.SmoothieRaw.ID)
                {
                    var success = false;
                    for (int j = 0; j < item.Items.Count; j++)
                    {
                        if (item.Items[j] == Refs.BlenderCup.ID)
                        {
                            item.Items[j] = Refs.DirtyBlenderCup.ID;
                            success = true;
                            break;
                        }
                    }
                    if (success)
                    {
                        Set(entity, item);
                    }
                }
                else if (item.ID == Refs.SmoothieBlended.ID)
                {
                    var success = false;
                    for (int j = 0; j < item.Items.Count; j++)
                    {
                        if (item.Items[j] == Refs.BlenderCup.ID)
                        {
                            item.Items[j] = Refs.DirtyBlenderCup.ID;
                            success = true;
                            break;
                        }
                    }
                    if (success)
                    {
                        Set(entity, item);
                    }
                }
            }
        }
    }
}
