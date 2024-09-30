using CraneCosmetics.Customs.Appliances;
using Kitchen;
using KitchenLib.Utils;
using KitchenMods;
using Unity.Collections;
using Unity.Entities;
using UnityEngine;

namespace CraneCosmetics.Systems
{
    public class CreateCraneCosmeticStation : NightSystem, IModSystem
    {
        private EntityQuery _practiceModeTrigger;
        
        protected override void Initialise()
        {
            base.Initialise();
            _practiceModeTrigger = GetEntityQuery(typeof(CAppliance), typeof(CTriggerPracticeMode));
        }

        protected override void OnUpdate()
        {
            if (HasSingleton<SCreateCraneCosmeticStation>())
            {
                return;
            }
            
            using (NativeArray<Entity> practiceModeTriggers = _practiceModeTrigger.ToEntityArray(Allocator.Temp))
            {
                foreach (Entity practiceModeTrigger in practiceModeTriggers)
                {
                    if (Require(practiceModeTrigger, out CPosition position))
                    {
                        Entity entity = EntityManager.CreateEntity(typeof(CCreateAppliance), typeof(CPosition), typeof(SCreateCraneCosmeticStation), typeof(CDoNotPersist));
                        EntityManager.SetComponentData(entity, new CCreateAppliance
                        {
                            ID = GDOUtils.GetCustomGameDataObject<CraneCosmeticStation>().ID
                        });
                        int num = ((position.Position.x > 0f) ? (-1) : 1);
                        EntityManager.SetComponentData(entity, new CPosition(position.Position + new Vector3(num * 1, 0f, 0)));
                    }
                }
            }
        }

        private struct SCreateCraneCosmeticStation : IComponentData, IModComponent
        {
        }
    }
}
