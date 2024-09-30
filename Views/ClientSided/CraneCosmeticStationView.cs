using System;
using UnityEngine;

namespace CraneCosmetics.Views.ClientSided
{
    public class CraneCosmeticStationView : MonoBehaviour
    {
        public GameObject HalloweenMode;
        public GameObject ChristmasMode;
        public GameObject ValentinesMode;
        public GameObject EasterMode;

        private void Awake()
        {
            string month = DateTime.Now.ToString("MM");
            switch (month)
            {
                case "10" :
                    SetMode(HalloweenMode);
                    break;
                case "12" :
                    SetMode(ChristmasMode);
                    break;
                case "04" :
                    SetMode(EasterMode);
                    break;
                case "02" :
                    SetMode(ValentinesMode);
                    break;
                default:
                    SetMode(null);
                    break;
            }
        }

        private void SetMode(GameObject mode)
        {
            HalloweenMode.SetActive(false);
            ChristmasMode.SetActive(false);
            ValentinesMode.SetActive(false);
            EasterMode.SetActive(false);
            
            if (mode != null) 
                mode.SetActive(true);
        }
    }
}