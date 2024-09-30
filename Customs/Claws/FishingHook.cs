using CraneCosmetics.Customs.Types;
using CraneCosmetics.Enums;
using KitchenLib.Utils;
using UnityEngine;

namespace CraneCosmetics.Customs.Claws
{
    public class FishingHook : CustomCraneCosmetic
    {
        public override string UniqueNameID => "FishingHook";
        public override GameObject Prefab => Mod.Bundle.LoadAsset<GameObject>("FishingHook").AssignMaterialsByNames();
        public override CraneCosmeticType CosmeticType => CraneCosmeticType.Claw;
        public override bool HideOriginal => true;
        public override bool HideBottomChain => true;
    }
}