using System.Collections.Generic;
using CraneCosmetics.Customs.Types;
using CraneCosmetics.Enums;
using CraneCosmetics.Views;
using Kitchen;
using KitchenData;
using KitchenLib.Utils;
using UnityEngine;

namespace CraneCosmetics.Utility
{
    public static class CraneSnapshot
    {
        private static Dictionary<int, Texture2D> _CachedImages = new Dictionary<int, Texture2D>();
        private static readonly int Image = Shader.PropertyToID("_Image");
        private static int CacheMaxSize = 20;
        public static Texture2D GetCraneSnapshot(CraneCosmetic cosmetic)
        {
            if (_CachedImages.TryGetValue(cosmetic.ID, out Texture2D texture2D) && texture2D != null)
            {
                return texture2D;
            }
            
            GameObject prefab = null;
            switch (cosmetic.CosmeticType)
            {
                case CraneCosmeticType.Chains:
                    prefab = GameObject.Instantiate(Mod.Bundle.LoadAsset<GameObject>("ChainShapshot").AssignMaterialsByNames());
                    break;
                case CraneCosmeticType.Head:
                    prefab = GameObject.Instantiate(Mod.Bundle.LoadAsset<GameObject>("HeadSnapshot").AssignMaterialsByNames());
                    break;
                case CraneCosmeticType.Claw:
                    prefab = GameObject.Instantiate(Mod.Bundle.LoadAsset<GameObject>("ClawSnapshot").AssignMaterialsByNames());
                    break;
            }

            if (prefab == null) return null;
            
            CraneCosmeticSubview subview = prefab.AddComponent<CraneCosmeticSubview>();
            
            switch (cosmetic.CosmeticType)
            {
                case CraneCosmeticType.Chains:
                    subview.Chains.Add(prefab.GetChild("PlayerCraneChain1"));
                    subview.Chains.Add(prefab.GetChild("PlayerCraneChain2"));
                    subview.Chains.Add(prefab.GetChild("PlayerCraneChain3"));
                    subview.Chains.Add(prefab.GetChild("PlayerCraneChain4"));
                    subview.Chains.Add(prefab.GetChild("PlayerCraneChain5"));
                    break;
                case CraneCosmeticType.Head:
                    subview.Head = prefab.GetChild("PlayerCrane");
                    break;
                case CraneCosmeticType.Claw:
                    subview.Claw = prefab.GetChild("PlayerCraneMagnet");
                    break;
            }
            
            subview.Setup();

            GameObject Icon = prefab.GetChild("Icon");

            if (cosmetic.Icon == null)
            {
                Icon.SetActive(false);
            }
            else
            {
                Material material = Icon.GetComponent<MeshRenderer>().material;
                material.SetTexture(Image, cosmetic.Icon);
            }

            subview.UpdateData(new CraneCosmeticSubview.ViewData
            {
                Cosmetics = new DataObjectList(cosmetic.ID)
            });
            
            SnapshotTexture result = Snapshot.RenderToTexture(512, 512, prefab, 1f, 1f, -10f, 10f, prefab.transform.localPosition);

            prefab.SetActive(false);
            GameObject.Destroy(prefab);
            
            if (_CachedImages.Count > CacheMaxSize)
            {
                _CachedImages.Clear();
            }
            _CachedImages[cosmetic.ID] = result.Snapshot;
            
            return result.Snapshot;
        }
    }
}