using CraneCosmetics.Customs.Types;
using CraneCosmetics.Enums;
using KitchenLib.Utils;
using UnityEngine;

namespace CraneCosmetics.Customs.Heads
{
    public class HolographicHead : CustomCraneCosmetic
    {
        public override string UniqueNameID => "HolographicHead";
        public override GameObject Prefab => Mod.Bundle.LoadAsset<GameObject>("HolographicHead").AssignMaterialsByNames();
        public override CraneCosmeticType CosmeticType => CraneCosmeticType.Head;
        public override bool HideOriginal => true;
    }
    public class HolographicChain : CustomCraneCosmetic
    {
        public override string UniqueNameID => "HolographicChain";
        public override GameObject Prefab => Mod.Bundle.LoadAsset<GameObject>("HolographicChain").AssignMaterialsByNames();
        public override CraneCosmeticType CosmeticType => CraneCosmeticType.Chains;
        public override bool HideOriginal => true;
    }
    public class HolographicClaw : CustomCraneCosmetic
    {
        public override string UniqueNameID => "HolographicClaw";
        public override GameObject Prefab => Mod.Bundle.LoadAsset<GameObject>("HolographicClaw").AssignMaterialsByNames();
        public override CraneCosmeticType CosmeticType => CraneCosmeticType.Claw;
        public override bool HideOriginal => true;
    }
}