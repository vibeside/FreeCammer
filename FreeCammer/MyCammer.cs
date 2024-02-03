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
        public const string modVersion = "0.1.2.2";
        public static ConfigEntry<string> enterCam;
        public static ConfigEntry<bool> fpsCam;
        public static ConfigEntry<int> mouseButton;
        public static ConfigEntry<bool> fullbrightMethod;
        public static ConfigEntry<string> camControls;
        public static ConfigEntry<string> intensityControls;
        public static ConfigEntry<string> showCursorBind;
        public static ConfigEntry<bool> tapOrHold;
        public static ConfigEntry<KeyCode> dummy;
        private static GameObject cammerHolder;
        
        //logger.loginfo(""); to log
        private readonly Harmony harmony = new Harmony(modGUID);
        public static ManualLogSource mls;
        private static MyCammer instance;
        public void Awake()
        {
            KeyCode temp;
            mouseButton = Config.Bind("General","Mouse button", 1, "Button used to turn camera if not using FPS cam for freecam.");
            fullbrightMethod = Config.Bind("General", "Full bright method", true, "True for directional light, false for a flashlight-sort of method.");
            enterCam = Config.Bind("Keybinds","Camera Activation", "Z", "The key used to enter the camera(google a list of keycodes if you want a specific key)");
            fpsCam = Config.Bind("General", "First person Freecammer", true, "Whether or not rotating requires you to hold right click");
            tapOrHold = Config.Bind("General", "Hold to increase brightness", false, "Set to true for holding the key down in order to increase brightness");
            showCursorBind = Config.Bind("Keybinds", "Key to show cursor", "X", "Pressing this key while in freecam will show your cursor");
            camControls = Config.Bind("Keybinds", "WASD controls for camera","U H J K","Format the 4 keys you wish to use as the default, and it should work. If not, double check the spelling. Check the bottom of the config for options!");
            intensityControls = Config.Bind("Keybinds", "Keys to increase light brightness", "Comma Period", "The keys used to increase and decrease light brightness!");
            dummy = Config.Bind("List of Keys", "", KeyCode.None,"List of keycodes you can use. Triple check when typing it to ensure no bugs.");
            if(!Enum.TryParse(enterCam.Value,out temp)) enterCam.Value = "Z";
            if (instance == null)
            {
                instance = this;
            }
            mls = base.Logger;
            mls.LogInfo("Cammer loaded.");
            
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
            harmony.PatchAll();
        }
    }
}
