using System;
using System.Collections.Generic;
using System.Reflection;
using CraneCosmetics.Customs.Types;
using CraneCosmetics.Enums;
using CraneCosmetics.Menus.Grids;
using CraneCosmetics.Views;
using HarmonyLib;
using Kitchen;
using Kitchen.Modules;
using KitchenData;
using KitchenLib.Utils;
using UnityEngine;

namespace CraneCosmetics.Patches
{
    [HarmonyPatch(typeof(LocalViewRouter), "GetPrefab")]
    public class LocalViewRouter_MenuPatch
    {
        private static GameObject NewUIPrefab = null;
        private static GameObject container;

        private static FieldInfo _AssetDirectory = ReflectionUtils.GetField<LocalViewRouter>("AssetDirectory");
        private static FieldInfo _Container = ReflectionUtils.GetField<CostumeChangeIndicator>("Container");

        static bool Prefix(LocalViewRouter __instance, ViewType view_type, ref GameObject __result)
        {
            DateTime now = DateTime.Now;
            if (view_type != (ViewType)VariousUtils.GetID("CraneCosmeticsUI"))
            {
                return true;
            }

            if (container == null)
            {
                container = new GameObject("temp");
                container.SetActive(false);
            }

            if (NewUIPrefab != null)
            {
                __result = NewUIPrefab;
                return false;
            }

            List<CraneCosmetic> Heads = new List<CraneCosmetic>();
            List<CraneCosmetic> Chains = new List<CraneCosmetic>();
            List<CraneCosmetic> Claws = new List<CraneCosmetic>();

            foreach (CraneCosmetic cosmetic in GameData.Main.Get<CraneCosmetic>())
            {
                if (cosmetic.CosmeticType == CraneCosmeticType.Head && !cosmetic.DisableInGame && (cosmetic.UnlockDate - now <= TimeSpan.Zero || Mod.UnlockAllCosmetics))
                    Heads.Add(cosmetic);
                
                if (cosmetic.CosmeticType == CraneCosmeticType.Chains && !cosmetic.DisableInGame && (cosmetic.UnlockDate - now <= TimeSpan.Zero || Mod.UnlockAllCosmetics))
                    Chains.Add(cosmetic);
                
                if (cosmetic.CosmeticType == CraneCosmeticType.Claw && !cosmetic.DisableInGame && (cosmetic.UnlockDate - now <= TimeSpan.Zero || Mod.UnlockAllCosmetics))
                    Claws.Add(cosmetic);
            }

            AssetDirectory AssetDirectory = (AssetDirectory)_AssetDirectory.GetValue(__instance);
            NewUIPrefab = GameObject.Instantiate(AssetDirectory.ViewPrefabs[ViewType.CostumeChangeInfo], container.transform);
            CostumeChangeIndicator costumeChangeIndicator = NewUIPrefab.GetComponent<CostumeChangeIndicator>();
            if (costumeChangeIndicator != null)
            {
                CraneCosmeticIndicator upgradeIndicator = NewUIPrefab.AddComponent<CraneCosmeticIndicator>();
                upgradeIndicator.Container = (Transform)_Container?.GetValue(costumeChangeIndicator);

                GridMenuNavigationConfig rootMenu = ScriptableObject.CreateInstance<GridMenuNavigationConfig>();
                CraneCosmeticGridConfig clawMenu = ScriptableObject.CreateInstance<CraneCosmeticGridConfig>();
                CraneCosmeticGridConfig chainMenu = ScriptableObject.CreateInstance<CraneCosmeticGridConfig>();
                CraneCosmeticGridConfig headMenu = ScriptableObject.CreateInstance<CraneCosmeticGridConfig>();

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

                upgradeIndicator.RootMenuConfig = rootMenu;
                Component.DestroyImmediate(costumeChangeIndicator);
            }

            __result = NewUIPrefab;
            return false;
        }
    }
}