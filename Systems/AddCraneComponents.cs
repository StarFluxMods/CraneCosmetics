using CraneCosmetics.Components;
using Kitchen;
using KitchenMods;
using Unity.Collections;
using Unity.Entities;

namespace CraneCosmetics.Systems
{
    public class AddCraneComponents : GameSystemBase, IModSystem
    {
        private EntityQuery _players;
        protected override void Initialise()
        {
            base.Initialise();
            _players = GetEntityQuery(typeof(CPlayer));
        }

        protected override void OnUpdate()
        {
            using (NativeArray<Entity> players = _players.ToEntityArray(Allocator.Temp))
            {
                foreach (Entity player in players)
                {
                    if (!Has<CCraneCosmetics>(player))
                        EntityManager.AddComponentData(player, new CCraneCosmetics());
                }
            }
        }
    }
}