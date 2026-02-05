using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using System;

namespace FreeCammer
{
    [BepInPlugin(modGUID,modName,modVersion)]
    public class Plugin : BaseUnityPlugin
    {
        public const string modGUID = "grug.lethalcompany.freecammer";
        public const string modName = "Free Cammer";
        public const string modVersion = "0.2.0.0";


        public static ConfigEntry<bool> dragRotation;

        public static ConfigEntry<bool> debugLogs;


        internal static ManualLogSource mls;
        void Awake()
        {
            mls = base.Logger;
            mls.LogInfo("FreeCammer Started");
        }


            
        public static void LogDebug(object msg)
        {
            if (debugLogs.Value)
                Plugin.mls.LogDebug(msg);
        }
    }
}
