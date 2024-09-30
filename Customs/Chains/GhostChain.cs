using CraneCosmetics.Customs.Types;
using CraneCosmetics.Enums;
using KitchenLib.Utils;
using UnityEngine;

namespace CraneCosmetics.Customs.Chains
{
    public class GhostChain : CustomCraneCosmetic
    {
        public override string UniqueNameID => "GhostChain";
        public override GameObject Prefab => Mod.Bundle.LoadAsset<GameObject>("GhostChain").AssignMaterialsByNames();
        public override CraneCosmeticType CosmeticType => CraneCosmeticType.Chains;
        public override bool HideOriginal => true;
        public override Texture2D Icon => Mod.Bundle.LoadAsset<Texture2D>("Ghost");
    }
}