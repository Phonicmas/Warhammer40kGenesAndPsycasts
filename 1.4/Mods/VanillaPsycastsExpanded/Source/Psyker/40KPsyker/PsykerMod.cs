using HarmonyLib;
using RimWorld;
using Verse;


namespace Psyker
{
    public class PsykerMod : Mod
    {
        public static Harmony harmony;
        public PsykerMod(ModContentPack content) : base(content)
        {
            harmony = new Harmony("Psyker.Mod");
            harmony.PatchAll();
        }
    }
}