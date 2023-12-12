using FreeCammer.Scripts;
using HarmonyLib;
using UnityEngine;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeCammer.Patches
{
    internal class CanPerformScanPatch
    {
        [HarmonyPatch(typeof(HUDManager), nameof(HUDManager.CanPlayerScan))]
        private class HUDManager_CanPlayerScan
        {
            [HarmonyDebug]
            public static bool Prefix(ref bool __result)
            {
                if (!CammerScript.instance.inCammer)
                {
                    __result = false;
                    MyCammer.mls.LogInfo("HREHEOLAWOEOLAIKI");
                    return false;
                }
                MyCammer.mls.LogInfo("HREHEOLAWOEOLAIKI");
                return true;
            }
        }
    }
}
