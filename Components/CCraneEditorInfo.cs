using Kitchen;
using KitchenData;
using KitchenMods;
using Unity.Entities;

namespace CraneCosmetics.Components
{
    public struct CCraneEditorInfo : IComponentData, IPlayerSpecificUI, IModComponent
    {
        Entity IPlayerSpecificUI.PlayerEntity => PlayerEntity;

        bool IPlayerSpecificUI.IsComplete => IsComplete;

        public InputIdentifier Player;

        public Entity PlayerEntity;

        public bool IsComplete;
    }
}