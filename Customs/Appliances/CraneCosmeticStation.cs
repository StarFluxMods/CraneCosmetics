using System.Collections.Generic;
using CraneCosmetics.Components;
using CraneCosmetics.Views.ClientSided;
using Kitchen;
using KitchenData;
using KitchenLib.Customs;
using KitchenLib.Utils;
using UnityEngine;

namespace CraneCosmetics.Customs.Appliances
{
    public class CraneCosmeticStation : CustomAppliance
    {
        public override string UniqueNameID => "CraneCosmeticStation";
        public override GameObject Prefab => Mod.Bundle.LoadAsset<GameObject>("CraneCosmeticStation").AssignMaterialsByNames().AssignVFXByNames();

        public override List<IApplianceProperty> Properties => new List<IApplianceProperty>
        {
            new CImmovable(),
            new CFixedRotation(),
            new CDestroyApplianceAtDay
            {
                HideBin = true
            },
            new CTriggerPlayerSpecificUI(),
            new CCraneEditor()
        };

        public override void OnRegister(Appliance gameDataObject)
        {
            base.OnRegister(gameDataObject);
            CraneCosmeticStationView view = gameDataObject.Prefab.AddComponent<CraneCosmeticStationView>();

            view.HalloweenMode = gameDataObject.Prefab.GetChild("HalloweenMode");
            view.ChristmasMode = gameDataObject.Prefab.GetChild("ChristmasMode");
            view.ValentinesMode = gameDataObject.Prefab.GetChild("ValentinesMode");
            view.EasterMode = gameDataObject.Prefab.GetChild("EasterMode");
        }
    }
}