using CraneCosmetics.Customs;
using CraneCosmetics.Customs.Appliances;
using HarmonyLib;
using Kitchen;
using KitchenLib.Utils;
using Unity.Entities;
using UnityEngine;

namespace CraneCosmetics.Patches
{
    [HarmonyPatch(typeof(CreateLoadoutRoom), "CreateAdvancedBuildModeCrane")]
    public class CreateLoadoutRoomPatch
    {
        static bool Prefix(CreateLoadoutRoom __instance, Vector3 location)
        {
            EntityManager entityManager = __instance.EntityManager;
            Entity entity = entityManager.CreateEntity(new ComponentType[]
            {
                typeof(CCreateAppliance),
                typeof(CPosition)
            });
            entityManager.SetComponentData<CCreateAppliance>(entity, new CCreateAppliance
            {
                ID = GDOUtils.GetCustomGameDataObject<CraneCosmeticStation>().ID
            });
            entityManager.SetComponentData<CPosition>(entity, new CPosition(location));
            return false;
        }
    }
}