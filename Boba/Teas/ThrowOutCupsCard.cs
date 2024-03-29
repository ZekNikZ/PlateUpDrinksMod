﻿using Kitchen;
using KitchenData;
using KitchenLib.Customs;
using KitchenLib.Utils;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;

namespace KitchenDrinksMod.Boba
{
    public class ThrowOutCupsCard : CustomUnlockCard
    {
        public const RestaurantStatus RestaurantStatus = (RestaurantStatus)(-628800);

        public override UnlockGroup UnlockGroup => UnlockGroup.Dish;
        public override string UniqueNameID => "ThrowOutCupsCard";
        public override Unlock.RewardLevel ExpReward => Unlock.RewardLevel.Medium;
        public override DishCustomerChange CustomerMultiplier => DishCustomerChange.SmallDecrease;
        public override bool IsUnlockable => true;

        public override List<UnlockEffect> Effects => new()
        {
            new StatusEffect
            {
                Status = RestaurantStatus
            }
        };

        public override List<Unlock> HardcodedRequirements => new()
        {
            Refs.BobaDish
        };

        public override List<(Locale, UnlockInfo)> InfoList => new()
        {
            (Locale.English, LocalisationUtils.CreateUnlockInfo("Dirty Cups", "Customers leave behind their dirty boba cups which must be thrown out", "Hopefully you have a bin!"))
        };
    }

    [UpdateAfter(typeof(GroupReceiveItem))]
    public class ThrowOutCupsSystem : GameSystemBase
    {
        private EntityQuery AllOrderedItems;

        protected override void Initialise()
        {
            AllOrderedItems = GetEntityQuery(new QueryHelper()
                .All(typeof(CWaitingForItem.Marker))
            );
        }

        protected override void OnUpdate()
        {
            if (!HasStatus(ThrowOutCupsCard.RestaurantStatus))
            {
                return;
            }

            using var orderedItems = AllOrderedItems.ToEntityArray(Allocator.Temp);
            foreach (var entity in orderedItems)
            {
                var buffer = EntityManager.GetBuffer<CWaitingForItem>(entity);
                for (int i = 0; i < buffer.Length; i++)
                {
                    var orderedItem = buffer[i];

                    if (orderedItem.ItemID == Refs.ServedBlackTea.ID || orderedItem.ItemID == Refs.ServedMatchaTea.ID || orderedItem.ItemID == Refs.ServedTaroTea.ID)
                    {
                        orderedItem.DirtItem = Refs.DirtyBobaCup.ID;
                    }

                    buffer[i] = orderedItem;
                }
            }
        }
    }
}
