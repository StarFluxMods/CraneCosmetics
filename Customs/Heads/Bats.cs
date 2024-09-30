using System;
using CraneCosmetics.Customs.Types;
using CraneCosmetics.Enums;
using KitchenLib.Utils;
using UnityEngine;

namespace CraneCosmetics.Customs.Heads
{
    public class Bats : CustomCraneCosmetic
    {
        public override string UniqueNameID => "Bats";
        public override GameObject Prefab => Mod.Bundle.LoadAsset<GameObject>("Bats").AssignMaterialsByNames().AssignVFXByNames();
        public override CraneCosmeticType CosmeticType => CraneCosmeticType.Head;
        public override bool HideOriginal => false;
        public override Texture2D Icon => Mod.Bundle.LoadAsset<Texture2D>("BatsIcon");
        public override DateTime UnlockDate => new DateTime(2024, 10, 1);
    }
}