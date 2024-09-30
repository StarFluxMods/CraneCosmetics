using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class CraneCosmeticSubview : MonoBehaviour
{
    private List<AttachedCraneCosmetic> AttachedCraneCosmetics = new List<AttachedCraneCosmetic>();
    private List<CraneAttachmentPoint> CraneAttachmentPoints = new List<CraneAttachmentPoint>();

    public GameObject Head = null;
    public GameObject Claw = null;
    public GameObject BottomChain = null;
    public List<GameObject> Chains = new List<GameObject>();

    public GameObject CustomHead;
    public GameObject CustomChain;
    public GameObject CustomClaw;

    public bool HideHead;
    public bool HideChain;
    public bool HideClaw;
    
    public bool HideBottomChain;
    
    private struct AttachedCraneCosmetic
    {
        public GameObject GameObject;
    }
    private struct CraneAttachmentPoint
    {
        public CraneCosmeticType Type;

        public Transform Transform;
    }

    private void Awake()
    {
        Setup();
    }

    private GameObject Cache_CustomHead;
    private GameObject Cache_CustomChain;
    private GameObject Cache_CustomClaw;

    private bool Cache_HideHead;
    private bool Cache_HideChain;
    private bool Cache_HideClaw;
    
    private bool Cache_HideBottomChain;
    
    private void Update()
    {
        if (Cache_CustomHead != CustomHead ||
            Cache_CustomChain != CustomChain ||
            Cache_CustomClaw != CustomClaw ||
            Cache_HideHead != HideHead ||
            Cache_HideChain != HideChain ||
            Cache_HideClaw != HideClaw ||
            Cache_HideBottomChain != HideBottomChain)
        {
            Cache_CustomHead = CustomHead;
            Cache_CustomChain = CustomChain;
            Cache_CustomClaw = CustomClaw;
            Cache_HideHead = HideHead;
            Cache_HideChain = HideChain;
            Cache_HideClaw = HideClaw;
            Cache_HideBottomChain = HideBottomChain;
        }
        SetupCosmetics();
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
                    Type = CraneCosmeticType.Claw
                });
            }
        }
    
    private void SetupCosmetics()
    {
        RemoveAttachments();
        ShowOriginalModels();
        HideRequiredModels();
        AttachRequiredCosmetics();
    }
    
    private void RemoveAttachments()
    {
        foreach (AttachedCraneCosmetic attachedCraneCosmetic in AttachedCraneCosmetics)
        {
            Destroy(attachedCraneCosmetic.GameObject);
        }

        AttachedCraneCosmetics.Clear();
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
    
    private void HideRequiredModels()
    {
        Head.SetActive(!HideHead);
        Claw.SetActive(!HideClaw);
        foreach (GameObject chain in Chains)
        {
            chain.SetActive(!HideChain);
        }
    }
    
    private void AttachRequiredCosmetics()
    {
        if (CustomHead != null)
        {
            Attach(CustomHead, CraneCosmeticType.Head);
        }
        
        if (CustomChain != null)
        {
            Attach(CustomChain, CraneCosmeticType.Chains);
        }
        
        if (CustomClaw != null)
        {
            Attach(CustomClaw, CraneCosmeticType.Claw);
        }

        if (HideBottomChain)
        {
            BottomChain.SetActive(false);
        }
    }

    private void Attach(GameObject prefab, CraneCosmeticType type)
    {
        foreach (CraneAttachmentPoint attachmentPoint in CraneAttachmentPoints)
        {
            if (attachmentPoint.Type != type) continue;
            GameObject cosmeticObject = Instantiate(prefab, attachmentPoint.Transform);

            cosmeticObject.transform.localPosition = Vector3.zero;
            cosmeticObject.transform.localRotation = Quaternion.identity;
                    
            AttachedCraneCosmetics.Add(new AttachedCraneCosmetic
            {
                GameObject = cosmeticObject
            });
        }
    }
}

public enum CraneCosmeticType
{
    Null,
    Head,
    Chains,
    Claw
}