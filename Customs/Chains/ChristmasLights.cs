using System;
using CraneCosmetics.Customs.Types;
using CraneCosmetics.Enums;
using KitchenLib.Utils;
using UnityEngine;

namespace CraneCosmetics.Customs.Chains
{
    public class ChristmasLights : CustomCraneCosmetic
    {
        public override string UniqueNameID => "ChristmasLights";
        public override GameObject Prefab => Mod.Bundle.LoadAsset<GameObject>("ChristmasLights").AssignMaterialsByNames();
        public override CraneCosmeticType CosmeticType => CraneCosmeticType.Chains;
        public override bool HideOriginal => false;
        public override DateTime UnlockDate => new DateTime(2024, 12, 1);
    }
}