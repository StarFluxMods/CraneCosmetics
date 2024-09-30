using System.Collections.Generic;
using CraneCosmetics.Customs.Types;
using CraneCosmetics.Utility;
using Kitchen.Modules;
using KitchenLib.UI.PlateUp.Grids;
using UnityEngine;

namespace CraneCosmetics.Menus.Grids
{
    public class CraneCosmeticGridMenu : KLPageGridMenu<CraneCosmetic>
    {
        public CraneCosmeticGridMenu(List<CraneCosmetic> items, Transform container, int player, bool has_back) : base(items, container, player, has_back)
        {
        }

        protected override void SetupElement(CraneCosmetic item, GridMenuElement element, int playerID = 0)
        {
            element.Set(item);
        }

        protected override void OnSelect(CraneCosmetic item)
        {
            if (Player != 0 && item != null)
            {
                SavedCosmeticManager.SetCosmetic(item, Player);
            }
        }
        
        protected override int RowLength => 4;
        protected override int ColumnLength => 3;
    }
}