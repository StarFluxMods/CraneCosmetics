using CraneCosmetics.Customs.Types;
using CraneCosmetics.Enums;
using KitchenLib.Utils;
using UnityEngine;

namespace CraneCosmetics.Customs.Heads
{
    public class GhostHead : CustomCraneCosmetic
    {
        public override string UniqueNameID => "GhostHead";
        public override GameObject Prefab => Mod.Bundle.LoadAsset<GameObject>("GhostHead").AssignMaterialsByNames();
        public override CraneCosmeticType CosmeticType => CraneCosmeticType.Head;
        public override bool HideOriginal => true;
        public override Texture2D Icon => Mod.Bundle.LoadAsset<Texture2D>("Ghost");
    }
}