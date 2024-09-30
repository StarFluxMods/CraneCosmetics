using System;
using CraneCosmetics.Customs.Types;
using CraneCosmetics.Enums;
using KitchenLib.Utils;
using UnityEngine;

namespace CraneCosmetics.Customs.Heads
{
    public class WitchHat : CustomCraneCosmetic
    {
        public override string UniqueNameID => "WitchHat";
        public override GameObject Prefab => Mod.Bundle.LoadAsset<GameObject>("WitchHat").AssignMaterialsByNames();
        public override CraneCosmeticType CosmeticType => CraneCosmeticType.Head;
        public override bool HideOriginal => false;
        public override DateTime UnlockDate => new DateTime(2024, 10, 1);
    }
}