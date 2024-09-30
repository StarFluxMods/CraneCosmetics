using System;
using CraneCosmetics.Customs.Types;
using CraneCosmetics.Enums;
using KitchenLib.Utils;
using UnityEngine;

namespace CraneCosmetics.Customs.Chains
{
    public class PumpkinChains : CustomCraneCosmetic
    {
        public override string UniqueNameID => "PumpkinChains";
        public override GameObject Prefab => Mod.Bundle.LoadAsset<GameObject>("PumpkinChains").AssignMaterialsByNames();
        public override CraneCosmeticType CosmeticType => CraneCosmeticType.Chains;
        public override bool HideOriginal => true;
        public override DateTime UnlockDate => new DateTime(2024, 10, 1);
    }
}