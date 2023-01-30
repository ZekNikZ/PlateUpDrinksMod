using Kitchen;
using KitchenMods;
using Unity.Collections;
using Unity.Entities;

namespace KitchenDrinksMod.Customs
{
    public struct CAutomatedDrinkInteractor : IModComponent
    {
    }

    public struct CAutomatedDrinkInteractorProvider : IModComponent
    {
    }

    public struct CAutomatedDrinkInteractorReciever : IModComponent
    {
    }

    public class AutoDrinkDispensingSystem : StartOfDaySystem
    {
        private EntityQuery Appliances;

        protected override void Initialise()
        {
            Appliances = GetEntityQuery(new QueryHelper()
                .All(typeof(CAppliance), typeof(CPosition))
                .None(typeof(CAutomatedDrinkInteractorProvider))
            );
        }

        protected override void OnUpdate()
        {
            using var appliances = Appliances.ToEntityArray(Allocator.Temp);
            foreach (var appliance in appliances)
            {
                // Ensure this is an appliance
                if (!Require(appliance, out CAppliance cAppliance) || !Require(appliance, out CPosition position))
                {
                    continue;
                }

                // Ensure this is a portioner
                if (cAppliance.ID != Refs.Portioner.ID)
                {
                    continue;
                }

                // Check if it is facing a tea dispenser
                Entity facingAppliance = GetOccupant(position.ForwardPosition);
                if (!CanReach(position, position.ForwardPosition) || facingAppliance == default || HasComponent<CAutomatedDrinkInteractorReciever>(facingAppliance) || !Require(facingAppliance, out CAppliance cFacingAppliance) || (cFacingAppliance.ID != Refs.TeaProvider.ID && cFacingAppliance.ID != Refs.SodaProvider.ID))
                {
                    continue;
                }

                // Create automated interactor
                var interactor = EntityManager.CreateEntity(typeof(CAutomatedInteractor), typeof(CAutomatedDrinkInteractor), typeof(CPosition));
                EntityManager.SetComponentData(interactor, new CAutomatedInteractor
                {
                    Type = InteractionType.Act,
                    DoNotReceive = true,
                    IsHeld = true
                });
                EntityManager.SetComponentData(interactor, position);

                // Mark the portioner so that we don't make multiple interactors
                EntityManager.AddComponent<CAutomatedDrinkInteractorProvider>(appliance);

                // Mark the drink provider so that we don't allow speeding up the process with multiple portioners
                EntityManager.AddComponent<CAutomatedDrinkInteractorReciever>(facingAppliance);
            }
        }
    }

    public class AutoDrinkCleanupSystem : StartOfNightSystem
    {
        private EntityQuery Interactors;
        private EntityQuery InteractorProviders;

        protected override void Initialise()
        {
            Interactors = GetEntityQuery(new QueryHelper().All(typeof(CAutomatedDrinkInteractor)));
            InteractorProviders = GetEntityQuery(new QueryHelper().Any(typeof(CAutomatedDrinkInteractorProvider), typeof(CAutomatedDrinkInteractorReciever)));
        }

        protected override void OnUpdate()
        {
            // Destroy interactors
            using var interactors = Interactors.ToEntityArray(Allocator.Temp);
            EntityManager.DestroyEntity(interactors);

            // Remove markers
            using var appliances = InteractorProviders.ToEntityArray(Allocator.Temp);
            EntityManager.RemoveComponent<CAutomatedDrinkInteractorProvider>(appliances);
            EntityManager.RemoveComponent<CAutomatedDrinkInteractorReciever>(appliances);
        }
    }
}
