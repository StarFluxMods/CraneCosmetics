using CraneCosmetics.Components;
using Kitchen;
using KitchenLib.Utils;
using KitchenMods;
using Unity.Entities;

namespace CraneCosmetics.Systems
{
    public class ManageCraneEditorIndicators : PlayerSpecificUIIndicator<CCraneEditor, CCraneEditorInfo>, IModSystem
    {
        protected override ViewType ViewType => (ViewType)VariousUtils.GetID("CraneCosmeticsUI");

        protected override CCraneEditorInfo GetInfo(Entity source, CCraneEditor selector, CTriggerPlayerSpecificUI trigger, CPlayer player)
        {
            return new CCraneEditorInfo
            {
                Player = player,
                PlayerEntity = trigger.TriggerEntity
            };
        }
    }
}