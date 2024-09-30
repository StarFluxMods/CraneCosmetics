using System.Collections.Generic;
using CraneCosmetics.Customs.Types;
using Kitchen.Modules;
using UnityEngine;

namespace CraneCosmetics.Menus.Grids
{
    public class CraneCosmeticGridConfig : GridMenuConfig
    {
        public override GridMenu Instantiate(Transform container, int player, bool has_back)
        {
            return new CraneCosmeticGridMenu(Cosmetics, container, player, has_back);
        }

        public List<CraneCosmetic> Cosmetics = new List<CraneCosmetic>();
    }
}