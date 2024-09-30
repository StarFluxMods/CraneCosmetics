using CraneCosmetics.Customs.Types;
using CraneCosmetics.Enums;
using KitchenLib.Utils;
using UnityEngine;

namespace CraneCosmetics.Customs.Claws
{
    public class MagnetHook : CustomCraneCosmetic
    {
        public override string UniqueNameID => "MagnetHook";
        public override GameObject Prefab => Mod.Bundle.LoadAsset<GameObject>("MagnetHook").AssignMaterialsByNames();
        public override CraneCosmeticType CosmeticType => CraneCosmeticType.Claw;
        public override bool HideOriginal => true;
    }
}