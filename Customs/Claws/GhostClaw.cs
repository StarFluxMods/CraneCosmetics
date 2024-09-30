using CraneCosmetics.Customs.Types;
using CraneCosmetics.Enums;
using KitchenLib.Utils;
using UnityEngine;

namespace CraneCosmetics.Customs.Claws
{
    public class GhostClaw : CustomCraneCosmetic
    {
        public override string UniqueNameID => "GhostClaw";
        public override GameObject Prefab => Mod.Bundle.LoadAsset<GameObject>("GhostClaw").AssignMaterialsByNames();
        public override CraneCosmeticType CosmeticType => CraneCosmeticType.Claw;
        public override bool HideOriginal => true;
        public override Texture2D Icon => Mod.Bundle.LoadAsset<Texture2D>("Ghost");
    }
}