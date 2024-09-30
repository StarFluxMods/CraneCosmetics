using KitchenData;
using KitchenMods;
using Unity.Entities;
using UnityEngine;

namespace CraneCosmetics.Components
{
    public struct CCraneEditor : IApplianceProperty, IAttachableProperty, IComponentData, IPlayerSpecificUISource, IModComponent
    {
        Vector3 IPlayerSpecificUISource.DrawLocation => DrawLocation;
        public Vector3 DrawLocation;
    }
}