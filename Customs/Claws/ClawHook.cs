using CraneCosmetics.Customs.Types;
using CraneCosmetics.Enums;
using KitchenLib.Utils;
using UnityEngine;

namespace CraneCosmetics.Customs.Claws
{
    public class ClawHook : CustomCraneCosmetic
    {
        public override string UniqueNameID => "ClawHook";
        public override GameObject Prefab => Mod.Bundle.LoadAsset<GameObject>("ClawHook").AssignMaterialsByNames();
        public override CraneCosmeticType CosmeticType => CraneCosmeticType.Claw;
        public override bool HideOriginal => true;
    }
}