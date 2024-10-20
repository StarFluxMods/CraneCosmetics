using System.Collections.Generic;
using CraneCosmetics.Customs.Types;
using CraneCosmetics.Enums;
using CraneCosmetics.Menus.Grids;
using Kitchen.Modules;
using KitchenData;
using UnityEngine;

namespace CraneCosmetics.Utility
{
    public class MenuBuilder
    {
        public static GridMenuNavigationConfig BuildMenu()
        {
            GridMenuNavigationConfig rootMenu = ScriptableObject.CreateInstance<GridMenuNavigationConfig>();
            CraneCosmeticGridConfig clawMenu = ScriptableObject.CreateInstance<CraneCosmeticGridConfig>();
            CraneCosmeticGridConfig chainMenu = ScriptableObject.CreateInstance<CraneCosmeticGridConfig>();
            CraneCosmeticGridConfig headMenu = ScriptableObject.CreateInstance<CraneCosmeticGridConfig>();
            
            List<CraneCosmetic> Heads = new List<CraneCosmetic>();
            List<CraneCosmetic> Chains = new List<CraneCosmetic>();
            List<CraneCosmetic> Claws = new List<CraneCosmetic>();

            foreach (CraneCosmetic cosmetic in GameData.Main.Get<CraneCosmetic>())
            {
                if (cosmetic.CosmeticType == CraneCosmeticType.Head && !cosmetic.DisableInGame)
                    Heads.Add(cosmetic);
                
                if (cosmetic.CosmeticType == CraneCosmeticType.Chains && !cosmetic.DisableInGame)
                    Chains.Add(cosmetic);
                
                if (cosmetic.CosmeticType == CraneCosmeticType.Claw && !cosmetic.DisableInGame)
                    Claws.Add(cosmetic);
            }

            clawMenu.Cosmetics = Heads;
            clawMenu.Icon = Mod.Bundle.LoadAsset<Texture2D>("Head");
                    
            chainMenu.Cosmetics = Chains;
            chainMenu.Icon = Mod.Bundle.LoadAsset<Texture2D>("Chains");
                
            headMenu.Cosmetics = Claws;
            headMenu.Icon = Mod.Bundle.LoadAsset<Texture2D>("Claw");
            
            rootMenu.Links = new List<GridMenuConfig>
            {
                headMenu,
                chainMenu,
                clawMenu,
            };
            
            rootMenu.Icon = Mod.Bundle.LoadAsset<Texture2D>("Crane");

            return rootMenu;
        }
    }
}