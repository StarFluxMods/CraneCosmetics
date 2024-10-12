using System.Collections.Generic;
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

        private Dictionary<int, CCraneCosmetics> CCraneCosmeticsCache = new Dictionary<int, CCraneCosmetics>();

        public override void BeforeSaving(SaveSystemType system_type)
        {
            base.AfterSaving(system_type);
            CCraneCosmeticsCache.Clear();
            using (NativeArray<Entity> players = _players.ToEntityArray(Allocator.Temp))
            {
                foreach (Entity player in players)
                {
                    if (!Require(player, out CCraneCosmetics cCraneCosmetics) || !Require(player, out CPlayer cPlayer)) continue;
                    if (CCraneCosmeticsCache.ContainsKey(cPlayer.ID)) continue;
                    CCraneCosmeticsCache.Add(cPlayer.ID, cCraneCosmetics);
                    EntityManager.RemoveComponent<CCraneCosmetics>(player);
                }
            }
        }

        public override void AfterSaving(SaveSystemType system_type)
        {
            base.AfterSaving(system_type);
            using (NativeArray<Entity> players = _players.ToEntityArray(Allocator.Temp))
            {
                foreach (Entity player in players)
                {
                    if (Require(player, out CPlayer cPlayer) && !Require(player, out CCraneCosmetics cCraneCosmetics))
                    {
                        if (CCraneCosmeticsCache.ContainsKey(cPlayer.ID))
                        {
                            EntityManager.AddComponentData(player, CCraneCosmeticsCache[cPlayer.ID]);
                        }
                    }
                }
            }
            CCraneCosmeticsCache.Clear();
        }
    }
}