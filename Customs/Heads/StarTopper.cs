using System;
using CraneCosmetics.Customs.Types;
using CraneCosmetics.Enums;
using KitchenLib.Utils;
using UnityEngine;

namespace CraneCosmetics.Customs.Heads
{
    public class StarTopper : CustomCraneCosmetic
    {
        public override string UniqueNameID => "StarTopper";
        public override GameObject Prefab => Mod.Bundle.LoadAsset<GameObject>("StarTopper").AssignMaterialsByNames();
        public override CraneCosmeticType CosmeticType => CraneCosmeticType.Head;
        public override bool HideOriginal => true;
        public override DateTime UnlockDate => new DateTime(2024, 12, 1);
    }
}