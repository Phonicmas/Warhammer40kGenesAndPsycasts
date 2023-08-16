using HarmonyLib;
using Verse;


namespace BEWH
{
    public class BEWHMod : Mod
    {
        public static Harmony harmony;
        public BEWHMod(ModContentPack content) : base(content)
        {
            harmony = new Harmony("BEWH.Mod");
            harmony.PatchAll();
        }
    }
}