using CraneCosmetics.Customs.Types;
using CraneCosmetics.Enums;
using KitchenData;
using KitchenMods;
using Unity.Entities;

namespace CraneCosmetics.Components
{
    public struct CCraneCosmetics : IComponentData, IModComponent
    {
        public CraneCosmetic Get(CraneCosmeticType type)
        {
            foreach (int num in CraneCosmetics)
            {
                CraneCosmetic craneCosmetic;
                if (GameData.Main.TryGet(num, out craneCosmetic, false) && craneCosmetic.CosmeticType == type)
                {
                    return craneCosmetic;
                }
            }
            return null;
        }
        
        public void Set(CraneCosmeticType type, int id)
        {
            CraneCosmetics = Set(CraneCosmetics, type, id);
        }

        public static DataObjectList Set(DataObjectList cosmetics, CraneCosmeticType type, int id)
        {
            bool flag = false;
            for (int i = 0; i < cosmetics.Count; i++)
            {
                int num = cosmetics[i];
                CraneCosmetic craneCosmetic;
                if (GameData.Main.TryGet(num, out craneCosmetic) && craneCosmetic.CosmeticType == type)
                {
                    cosmetics[i] = id;
                    flag = true;
                }
            }
            if (!flag)
            {
                cosmetics.Add(id);
            }
            for (int j = cosmetics.Count - 1; j >= 0; j--)
            {
                if (cosmetics[j] == 0 || !GameData.Main.TryGet(cosmetics[j], out CraneCosmetic _, false))
                {
                    cosmetics.RemoveAt(j);
                }
            }
            return cosmetics;
        }
        
        public DataObjectList CraneCosmetics;
    }
}