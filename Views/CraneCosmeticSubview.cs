using System.Collections.Generic;
using CraneCosmetics.Components;
using CraneCosmetics.Customs.Types;
using CraneCosmetics.Enums;
using Kitchen;
using KitchenData;
using KitchenMods;
using MessagePack;
using Unity.Collections;
using Unity.Entities;
using UnityEngine;
using UnityEngine.VFX;

namespace CraneCosmetics.Views
{
    public class CraneCosmeticSubview : UpdatableObjectView<CraneCosmeticSubview.ViewData>
    {
        public class UpdateView : IncrementalViewSystemBase<ViewData>, IModSystem
        {
            private EntityQuery playerCranes;

            protected override void Initialise()
            {
                base.Initialise();
                playerCranes = GetEntityQuery(typeof(CIsCraneMode), typeof(CLinkedView), typeof(CCraneCosmetics));
            }

            protected override void OnUpdate()
            {
                using (NativeArray<Entity> playerCranes = this.playerCranes.ToEntityArray(Allocator.Temp))
                {
                    foreach (Entity playerCrane in playerCranes)
                    {
                        if (!Require(playerCrane, out CLinkedView cLinkedView)) continue;
                        if (!Require(playerCrane, out CCraneCosmetics cCraneCosmetics)) continue;
                        if (!Require(playerCrane, out CPlayer cPlayer)) continue;

                        SendUpdate(cLinkedView.Identifier, new ViewData
                        {
                            Cosmetics = cCraneCosmetics.CraneCosmetics,
                            Player = cPlayer.ID
                        });
                    }
                }
            }
        }

        [MessagePackObject(false)]
        public struct ViewData : ISpecificViewData, IViewData.ICheckForChanges<ViewData>
        {
            public bool IsChangedFrom(ViewData check)
            {
                return !Cosmetics.IsEquivalent(check.Cosmetics) || Player != check.Player;
            }

            public IUpdatableObject GetRelevantSubview(IObjectView view)
            {
                return view.GetSubView<CraneCosmeticSubview>();
            }

            [Key(0)] public DataObjectList Cosmetics;
            [Key(1)] public int Player;
        }

        protected override void UpdateData(ViewData data)
        {
            SetupCosmetics(data);
            Player = data.Player;
        }

        private void Update()
        {
            SetDynamicColors();
        }

        private void SetDynamicColors()
        {
            if (Player == 0) return;

            PlayerInfo playerInfo = Players.Main.Get(Player);
            foreach (Material material in dynamicMaterials)
            {
                material.SetColor(Color0, playerInfo.Profile.Colour);
            }
        }

        private void SetupCosmetics(ViewData data)
        {
            RemoveAttachments();
            ShowOriginalModels();
            AttachRequiredCosmetics(data);
            HideRequiredModels(data);
        }

        private void RemoveAttachments()
        {
            foreach (AttachedCraneCosmetic attachedCraneCosmetic in AttachedCraneCosmetics)
            {
                Destroy(attachedCraneCosmetic.GameObject);
            }

            AttachedCraneCosmetics.Clear();
        }

        private void AttachRequiredCosmetics(ViewData data)
        {
            dynamicMaterials.Clear();
            foreach (int cosmeticID in data.Cosmetics)
            {
                if (!GameData.Main.TryGet(cosmeticID, out CraneCosmetic cosmetic)) continue;
                foreach (CraneAttachmentPoint attachmentPoint in CraneAttachmentPoints)
                {
                    if (attachmentPoint.Type != cosmetic.CosmeticType) continue;
                    GameObject cosmeticObject = Instantiate(cosmetic.Prefab, attachmentPoint.Transform);

                    cosmeticObject.transform.localPosition = Vector3.zero;
                    cosmeticObject.transform.localRotation = Quaternion.identity;
                    
                    AttachedCraneCosmetics.Add(new AttachedCraneCosmetic
                    {
                        Cosmetic = cosmetic,
                        GameObject = cosmeticObject
                    });
                }
            }
            
            foreach (AttachedCraneCosmetic attachedCraneCosmetic in AttachedCraneCosmetics)
            {
                foreach (Renderer renderer in attachedCraneCosmetic.GameObject.GetComponentsInChildren<Renderer>())
                {
                    foreach (Material material in renderer.materials)
                    {
                        if (material.name == "Player (Instance)" || material.name.Contains("Dynamic_Player"))
                        {
                            dynamicMaterials.Add(material);
                        }
                    }
                }
                
                foreach (VisualEffect effect in attachedCraneCosmetic.GameObject.GetComponentsInChildren<VisualEffect>())
                {
                    effect.enabled = false;
                    effect.enabled = true;
                }
            }

            foreach (AttachedCraneCosmetic attachedCraneCosmetic in AttachedCraneCosmetics)
            {
                if (BottomChain != null)
                    BottomChain.SetActive(!attachedCraneCosmetic.Cosmetic.HideBottomChain);

                foreach (CraneAttachmentPoint attachmentPoint in CraneAttachmentPoints)
                    if (attachmentPoint.Original == BottomChain)
                        attachmentPoint.Transform.gameObject.SetActive(!attachedCraneCosmetic.Cosmetic.HideBottomChain);
                
                if (attachedCraneCosmetic.Cosmetic.HideBottomChain)
                    break;
            }

        }

        private void HideRequiredModels(ViewData data)
        {
            foreach (int cosmeticID in data.Cosmetics)
            {
                if (GameData.Main.TryGet(cosmeticID, out CraneCosmetic cosmetic) && cosmetic.HideOriginal)
                {
                    switch (cosmetic.CosmeticType)
                    {
                        case CraneCosmeticType.Head:
                            Head.SetActive(false);
                            break;
                        case CraneCosmeticType.Claw:
                            Claw.SetActive(false);
                            break;
                        case CraneCosmeticType.Chains:
                            foreach (GameObject chain in Chains)
                            {
                                chain.SetActive(false);
                            }

                            break;
                    }
                }
            }
        }

        private void ShowOriginalModels()
        {
            if (Head != null) Head.SetActive(true);
            if (Claw != null) Claw.SetActive(true);
            foreach (GameObject chain in Chains)
            {
                chain.SetActive(true);
            }
        }

        private List<AttachedCraneCosmetic> AttachedCraneCosmetics = new List<AttachedCraneCosmetic>();
        private List<CraneAttachmentPoint> CraneAttachmentPoints = new List<CraneAttachmentPoint>();

        public GameObject Head = null;
        public GameObject Claw = null;
        public GameObject BottomChain = null;
        public List<GameObject> Chains = new List<GameObject>();
        private List<Material> dynamicMaterials = new List<Material>();
        private static readonly int Color0 = Shader.PropertyToID("_Color0");
        private int Player = 0;

        private void Awake()
        {
            Setup();
        }

        public void Setup()
        {
            foreach (GameObject chain in Chains)
            {
                GameObject chainLocation = new GameObject(chain.name + " AttachmentPoint");
                chainLocation.transform.SetParent(chain.transform.parent);
                chainLocation.transform.localPosition = chain.transform.localPosition;
                chainLocation.transform.localRotation = chain.transform.localRotation;
                chainLocation.transform.localScale = chain.transform.localScale;
                CraneAttachmentPoints.Add(new CraneAttachmentPoint
                {
                    Transform = chainLocation.transform,
                    Original = chain,
                    Type = CraneCosmeticType.Chains
                });
            }

            if (Head != null)
            {
                GameObject headLocation = new GameObject(Head.name + " AttachmentPoint");
                headLocation.transform.SetParent(Head.transform.parent);
                headLocation.transform.localPosition = Head.transform.localPosition;
                headLocation.transform.localRotation = Head.transform.localRotation;
                headLocation.transform.localScale = Head.transform.localScale;
                CraneAttachmentPoints.Add(new CraneAttachmentPoint
                {
                    Transform = headLocation.transform,
                    Original = Head,
                    Type = CraneCosmeticType.Head
                });
            }

            if (Claw != null)
            {
                GameObject clawLocation = new GameObject(Claw.name + " AttachmentPoint");
                clawLocation.transform.SetParent(Claw.transform.parent);
                clawLocation.transform.localPosition = Claw.transform.localPosition;
                clawLocation.transform.localRotation = Claw.transform.localRotation;
                clawLocation.transform.localScale = Claw.transform.localScale;
                CraneAttachmentPoints.Add(new CraneAttachmentPoint
                {
                    Transform = clawLocation.transform,
                    Original = Claw,
                    Type = CraneCosmeticType.Claw
                });
            }
        }

        private struct AttachedCraneCosmetic
        {
            public CraneCosmetic Cosmetic;

            public GameObject GameObject;
        }

        private struct CraneAttachmentPoint
        {
            public CraneCosmeticType Type;

            public GameObject Original;

            public Transform Transform;
        }
    }
}