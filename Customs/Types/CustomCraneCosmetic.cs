using System;
using CraneCosmetics.Enums;
using CraneCosmetics.Utility;
using Kitchen;
using Kitchen.Modules;
using KitchenData;
using KitchenLib.Customs;
using UnityEngine;

namespace CraneCosmetics.Customs.Types
{
    public class CraneCosmetic : GameDataObject, IGridItem
    {
        protected override void InitialiseDefaults()
        {
        }

        public CraneCosmeticType CosmeticType = CraneCosmeticType.Null;
        public GameObject Prefab;
        public bool HideOriginal = false;
        public bool HideBottomChain = false;
        public Texture2D Icon;
        public bool DisableInGame;
        public DateTime UnlockDate;

        public Texture2D GetSnapshot()
        {
            return CraneSnapshot.GetCraneSnapshot(this);
        }

        public int SnapshotKey => ID;
    }

    public abstract class CustomCraneCosmetic : CustomGameDataObject<CraneCosmetic>
    {
        public virtual CraneCosmeticType CosmeticType { get; protected set; } = CraneCosmeticType.Null;
        public virtual GameObject Prefab { get; protected set; }
        public virtual bool HideOriginal { get; protected set; } = false;
        public virtual bool HideBottomChain { get; protected set; } = false;
        public virtual Texture2D Icon { get; protected set; }
        public virtual bool DisableInGame { get; protected set; } = false;
        public virtual DateTime UnlockDate { get; protected set; }

        public override void Convert(GameData gameData, out GameDataObject gameDataObject)
        {
            CraneCosmetic result = ScriptableObject.CreateInstance<CraneCosmetic>();

            result.ID = ID;
            result.Prefab = Prefab;
            result.CosmeticType = CosmeticType;
            result.HideOriginal = HideOriginal;
            result.HideBottomChain = HideBottomChain;
            result.Icon = Icon;
            result.DisableInGame = DisableInGame;
            result.UnlockDate = UnlockDate;

            gameDataObject = result;
        }
    }
}