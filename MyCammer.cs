using BepInEx;
using BepInEx.Logging;
using BepInEx.Configuration;
using FreeCammer.Scripts;
using GameNetcodeStuff;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using FreeCammer.Patches;
using System.Reflection;
using HarmonyLib.Tools;

namespace FreeCammer
{
    [BepInPlugin(modGUID, modName, modVersion)]
    public class MyCammer : BaseUnityPlugin
    {
        public const string modGUID = "grug.lethalcompany.freecammer";
        public const string modName = "Free Cammer";
        public const string modVersion = "0.1.0.1";
        public static ConfigEntry<KeyCode> enterCam;
        public static ConfigEntry<bool> fpsCam;
        private static GameObject cammerHolder;
        
        //logger.loginfo(""); to log
        private readonly Harmony harmony = new Harmony(modGUID);
        public static ManualLogSource mls;
        private static MyCammer instance;
        public void Awake()
        {
            enterCam = Config.Bind("General","Camera Activation", KeyCode.LeftBracket, "The key used to enter the camera");
            fpsCam = Config.Bind("General", "First person Freecammer", true, "Whether or not rotating requires you to hold right click");
            if (instance == null)
            {
                instance = this;
            }
            mls = base.Logger;
            mls.LogInfo("Cammer Loaded!!!");
            if (cammerHolder == null)
            {
                cammerHolder = new GameObject("cammerHolder");
                DontDestroyOnLoad(cammerHolder);
                cammerHolder.hideFlags = HideFlags.HideAndDontSave;
                cammerHolder.AddComponent<CammerScript>();
            }
            if (cammerHolder != null)
            {
                mls.LogInfo("Cammer not null");
                if(cammerHolder.GetComponent<CammerScript>() != null)
                {
                    mls.LogInfo("CammerScript not null");
             
                }
            }
            harmony.PatchAll(typeof(CanPerformScanPatch));
        }
    }
}
