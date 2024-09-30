using System.Collections.Generic;
using System.IO;
using CraneCosmetics.Customs.Types;
using Kitchen;
using KitchenData;
using KitchenLib.Utils;
using Newtonsoft.Json;
using UnityEngine;

namespace CraneCosmetics.Utility
{
    public static class SavedCosmeticManager
    {
        private static string SAVE_DIRECTORY = Path.Combine(Application.persistentDataPath, "ModData", "CraneCosmetics");
        private static string DATA_FILE = Path.Combine(SAVE_DIRECTORY, "playerdata.json");

        private static Dictionary<string, SavedPlayerData> _savedPlayerData = new Dictionary<string, SavedPlayerData>();

        private static PlayerProfile GetOrCreateProfile(ProfileIdentifier name)
        {
            PlayerProfile playerProfile;
            if (ProfileStore.Main.TryGetProfile(name, out playerProfile))
            {
                return playerProfile;
            }

            PlayerProfile @default = PlayerProfile.Default;
            @default.Name = name;
            Set(name, @default);
            return @default;
        }

        private static void Set(ProfileIdentifier identifier, PlayerProfile profile)
        {
            ProfileStore.Main.SetProfile(identifier, profile);
        }

        private static void Load()
        {
            EnsureDirectory();
            if (!File.Exists(DATA_FILE)) return;
            if (_savedPlayerData.IsNullOrEmpty())
            {
                string json = File.ReadAllText(DATA_FILE);
                _savedPlayerData = JsonConvert.DeserializeObject<Dictionary<string, SavedPlayerData>>(json);
            }
        }

        private static void Save()
        {
            EnsureDirectory();
            string json = JsonConvert.SerializeObject(_savedPlayerData, Formatting.Indented);
            File.WriteAllText(DATA_FILE, json);
        }

        private static void EnsureDirectory()
        {
            if (Directory.Exists(SAVE_DIRECTORY)) return;
            Directory.CreateDirectory(SAVE_DIRECTORY);
        }

        private static bool TryGetSavedPlayerData(string playerName, out SavedPlayerData savedPlayerData)
        {
            Load();
            return _savedPlayerData.TryGetValue(playerName, out savedPlayerData);
        }

        private static SavedPlayerData GetSavedPlayerData(string playerName)
        {
            Load();
            return _savedPlayerData.ContainsKey(playerName) ? _savedPlayerData[playerName] : null;
        }

        public static void SetCosmetic(CraneCosmetic cosmetic, int player_id)
        {
            ProfileIdentifier profileIdentifier;
            if (Players.Main.TryGetActiveProfile(player_id, out profileIdentifier))
            {
                SetCosmetic(cosmetic, profileIdentifier);
            }
        }

        public static DataObjectList GetCosmetics(int player_id)
        {
            ProfileIdentifier profileIdentifier;
            if (Players.Main.TryGetActiveProfile(player_id, out profileIdentifier))
            {
                return GetCosmetics(profileIdentifier);
            }

            return new DataObjectList();
        }

        public static DataObjectList GetCosmetics(ProfileIdentifier profileIdentifier)
        {
            PlayerProfile playerProfile = GetOrCreateProfile(profileIdentifier);
            if (TryGetSavedPlayerData(playerProfile.Name, out SavedPlayerData savedPlayerData))
            {
                DataObjectList cosmetics = new DataObjectList();
                foreach (var cosmeticId in savedPlayerData.Cosmetics)
                {
                    if (GameData.Main.TryGet(cosmeticId, out CraneCosmetic cosmetic))
                    {
                        cosmetics.Add(cosmetic.ID);
                    }
                }

                return cosmetics;
            }

            return new DataObjectList();
        }

        public static void SetCosmetic(CraneCosmetic cosmetic, ProfileIdentifier profileIdentifier)
        {
            PlayerProfile playerProfile = GetOrCreateProfile(profileIdentifier);
            if (TryGetSavedPlayerData(playerProfile.Name, out SavedPlayerData savedPlayerData))
            {
                foreach (int cosmeticID in savedPlayerData.Cosmetics)
                {
                    if (GameData.Main.TryGet(cosmeticID, out CraneCosmetic craneCosmetic))
                    {
                        if (craneCosmetic.CosmeticType == cosmetic.CosmeticType && craneCosmetic.ID != cosmetic.ID)
                        {
                            savedPlayerData.Cosmetics.Remove(cosmeticID);
                            break;
                        }
                    }
                }

                if (savedPlayerData.Cosmetics.Contains(cosmetic.ID))
                {
                    savedPlayerData.Cosmetics.Remove(cosmetic.ID);
                }
                else
                {
                    savedPlayerData.Cosmetics.Add(cosmetic.ID);
                }

                SetPlayerData(playerProfile.Name, savedPlayerData);
            }
            else
            {
                SavedPlayerData newSavedPlayerData = new SavedPlayerData
                {
                    PlayerName = playerProfile.Name,
                    Cosmetics = new List<int> { cosmetic.ID }
                };
                SetPlayerData(playerProfile.Name, newSavedPlayerData);
            }
        }

        public static void SetPlayerData(string playerName, SavedPlayerData savedPlayerData)
        {
            Load();
            _savedPlayerData[playerName] = savedPlayerData;
            Save();
        }
    }

    public class SavedPlayerData
    {
        public string PlayerName;
        public List<int> Cosmetics = new List<int>();
    }
}