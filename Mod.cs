using System;
using KitchenLib;
using KitchenLib.Logging.Exceptions;
using KitchenMods;
using System.Linq;
using System.Reflection;
using CraneCosmetics.Utility;
using KitchenLib.Interfaces;
using KitchenLib.Utils;
using UnityEngine;
using KitchenLogger = KitchenLib.Logging.KitchenLogger;

namespace CraneCosmetics
{
    public class Mod : BaseMod, IModSystem, IAutoRegisterAll
    {
        public const string MOD_GUID = "com.starfluxgames.cranecosmetics";
        public const string MOD_NAME = "Crane Cosmetics";
        public const string MOD_VERSION = "0.1.3";
        public const string MOD_AUTHOR = "StarFluxGames";
        public const string MOD_GAMEVERSION = ">=1.2.0";

        internal static AssetBundle Bundle;
        internal static KitchenLogger Logger;
        
        internal static bool UnlockAllCosmetics = false;
        internal static MethodInfo Register;

        public Mod() : base(MOD_GUID, MOD_NAME, MOD_AUTHOR, MOD_VERSION, MOD_GAMEVERSION, Assembly.GetExecutingAssembly())
        {
        }

        protected override void OnInitialise()
        {
            Logger.LogWarning($"{MOD_GUID} v{MOD_VERSION} in use!");
            if (Register != null)
            {
                Register.Invoke(null, new object[]{ MenuBuilder.BuildMenu() });
            }
        }

        protected override void OnUpdate()
        { 
        }

        protected override void OnPostActivate(KitchenMods.Mod mod)
        {
            Bundle = mod.GetPacks<AssetBundleModPack>().SelectMany(e => e.AssetBundles).FirstOrDefault() ?? throw new MissingAssetBundleException(MOD_GUID);
            Logger = InitLogger();
            CheckForOptionalMod();
        }

        private void CheckForOptionalMod()
        {
            foreach (KitchenMods.Mod loadedMod in ModPreload.Mods)
            {
                foreach (AssemblyModPack pack in loadedMod.GetPacks<AssemblyModPack>())
                {
                    foreach (Type type in pack.Asm.GetTypes())
                    {
                        if (type.FullName != "FrontDoorAppliances.Utils.FrontDoorMenus") continue;
                        
                        Register = ReflectionUtils.GetMethod(type, "Register");
                        return;
                    }
                }
            }
        }
    }
}