using System;
using System.Collections.Generic;
using System.Text;

namespace FreeCammer
{
    internal class CompatibilityClass
    {
        public static bool Enabled =>
        BepInEx.Bootstrap.Chainloader.PluginInfos.ContainsKey("com.rune580.LethalCompanyInputUtils");
    }
}
