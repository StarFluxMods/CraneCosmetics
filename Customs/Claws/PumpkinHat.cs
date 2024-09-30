using System;
using CraneCosmetics.Customs.Types;
using CraneCosmetics.Enums;
using KitchenLib.Utils;
using UnityEngine;

namespace CraneCosmetics.Customs.Claws
{
    public class PumpkinHat : CustomCraneCosmetic
    {
        public override string UniqueNameID => "PumpkinHat";
        public override GameObject Prefab => Mod.Bundle.LoadAsset<GameObject>("PumpkinHat").AssignMaterialsByNames();
        public override CraneCosmeticType CosmeticType => CraneCosmeticType.Claw;
        public override bool HideOriginal => false;
        public override DateTime UnlockDate => new DateTime(2024, 10, 1);
    }
}