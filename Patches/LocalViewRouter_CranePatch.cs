using System.Collections.Generic;
using System.Reflection;
using CraneCosmetics.Views;
using HarmonyLib;
using Kitchen;
using KitchenLib.Utils;
using UnityEngine;

namespace CraneCosmetics.Patches
{
    [HarmonyPatch(typeof(LocalViewRouter), "GetPrefab")]
    public class LocalViewRouter_CranePatch
    {
        private static GameObject NewCranePrefab = null;

        static bool Prefix(LocalViewRouter __instance, ViewType view_type, ref GameObject __result)
        {
            if (view_type != ViewType.PlayerCrane)
            {
                return true;
            }

            if (NewCranePrefab != null)
            {
                __result = NewCranePrefab;
                return false;
            }

            FieldInfo _AssetDirectory = ReflectionUtils.GetField<LocalViewRouter>("AssetDirectory");
            FieldInfo _PlayerColourRenderers = ReflectionUtils.GetField<PlayerIdentificationComponent>("PlayerColourRenderers");
            AssetDirectory assetDirectory = (AssetDirectory)_AssetDirectory.GetValue(__instance);
            if (assetDirectory.ViewPrefabs.TryGetValue(ViewType.PlayerCrane, out GameObject cranePrefab))
            {
                NewCranePrefab = cranePrefab;
                PlayerIdentificationComponent playerIdentificationComponent = NewCranePrefab.GetComponentInChildren<PlayerIdentificationComponent>();
                List<Renderer> PlayerColourRenderers = (List<Renderer>)_PlayerColourRenderers.GetValue(playerIdentificationComponent);

                NewCranePrefab.AddComponent<ManageActiveCosmeticsSubview>();
                CraneCosmeticSubview subview = NewCranePrefab.AddComponent<CraneCosmeticSubview>();

                subview.Chains.Add(NewCranePrefab.GetChild("Chain/PlayerCraneChain"));
                subview.Chains.Add(NewCranePrefab.GetChild("Chain (1)/PlayerCraneChain"));
                subview.Chains.Add(NewCranePrefab.GetChild("Chain (2)/PlayerCraneChain"));
                subview.Chains.Add(NewCranePrefab.GetChild("Chain (3)/PlayerCraneChain"));
                subview.Chains.Add(NewCranePrefab.GetChild("Chain (4)/PlayerCraneChain"));
                subview.BottomChain = NewCranePrefab.GetChild("Chain (4)/PlayerCraneChain");
                subview.Claw = NewCranePrefab.GetChild("Chain (4)/PlayerCraneMagnet");
                subview.Head = NewCranePrefab.GetChild("Crane Top/PlayerCrane");


                _PlayerColourRenderers.SetValue(playerIdentificationComponent, PlayerColourRenderers);
            }

            __result = NewCranePrefab;
            return false;
        }
    }
}