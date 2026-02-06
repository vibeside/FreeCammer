using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using HarmonyLib;
using System;
using System.Reflection;
using UnityEngine;

namespace FreeCammer
{
    [BepInPlugin(modGUID,modName,modVersion)]
    [BepInDependency("com.rune580.LethalCompanyInputUtils", BepInDependency.DependencyFlags.SoftDependency)]
    public class Plugin : BaseUnityPlugin
    {
        public const string modGUID = "grug.lethalcompany.freecammer";
        public const string modName = "Free Cammer";
        public const string modVersion = "0.2.0.0";

        public static ConfigEntry<bool> dragRotation;

        public static ConfigEntry<bool> doDebugLogs;

        Freecam freecam;

        internal static ManualLogSource mls;
        void Awake()
        {
            mls = base.Logger;
            mls.LogInfo("FreeCammer Started");

            if(freecam == null) 
            freecam = new GameObject("comeracomeracomeracomeleon").AddComponent<Freecam>();


            // i dont use harmony, this is just for the network patch.
            Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly(),modGUID);
        }

        public static void LogDebug(object msg)
        {
            if (doDebugLogs.Value)
                mls.LogDebug(msg);
        }
    }
}
