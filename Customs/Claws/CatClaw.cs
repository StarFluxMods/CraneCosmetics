using System;
using CraneCosmetics.Customs.Types;
using CraneCosmetics.Enums;
using KitchenLib.Utils;
using UnityEngine;

namespace CraneCosmetics.Customs.Claws
{
    public class CatClaw : CustomCraneCosmetic
    {
        public override string UniqueNameID => "CatClaw";
        public override GameObject Prefab => Mod.Bundle.LoadAsset<GameObject>("CatClaw").AssignMaterialsByNames();
        public override CraneCosmeticType CosmeticType => CraneCosmeticType.Claw;
        public override bool HideOriginal => true;
        public override DateTime UnlockDate => new DateTime(2024, 10, 1);
    }
}