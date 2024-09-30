using System;
using CraneCosmetics.Customs.Types;
using CraneCosmetics.Enums;
using CraneCosmetics.Utility;
using KitchenLib.Utils;
using UnityEngine;

namespace CraneCosmetics.Customs.Heads
{
    public class GooglyEyes : CustomCraneCosmetic
    {
        public override string UniqueNameID => "GooglyEyes";
        public override GameObject Prefab => Mod.Bundle.LoadAsset<GameObject>("GooglyEyes").AssignMaterialsByNames();
        public override CraneCosmeticType CosmeticType => CraneCosmeticType.Head;
        public override bool HideOriginal => false;

        public override void OnRegister(CraneCosmetic gameDataObject)
        {
            base.OnRegister(gameDataObject);

            foreach (Transform child in gameDataObject.Prefab.transform)
            {
                GooglyEye GooglyEye = child.gameObject.AddComponent<GooglyEye>();
                GooglyEye.Eye = child.gameObject.GetChild("eye").transform;
                GooglyEye.Speed = 1;
                GooglyEye.GravityMultiplier = 1;
                GooglyEye.Bounciness = 0.4f;
            }
        }
    }
}