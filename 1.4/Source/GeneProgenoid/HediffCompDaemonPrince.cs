using HarmonyLib;
using RimWorld;
using Verse;


namespace BEWH
{
    public class HediffCompDaemonPrince : HediffComp
    {
        public const int TicksToResurrect = 120;

        public HediffCompPropertiesDaemonPrince Props => (HediffCompPropertiesDaemonPrince)props;

        public void PatchCorpse(Harmony harmony)
        {
            harmony.Patch(AccessTools.Method(typeof(Corpse), "TickRare"), null, new HarmonyMethod(GetType(), "CorpseTick"));
        }

        public static void CorpseTick(Corpse __instance)
        {
            Pawn pawn = __instance.InnerPawn;
            Hediff hediff = pawn.health.hediffSet.hediffs.Find((Hediff x) => x.def == BEWHDefOf.BEWH_DaemonPrince && x.Visible);
            if (hediff != null)
            {
                if (Find.TickManager.TicksGame - __instance.timeOfDeath >= 120000)
                {
                    ResurrectionUtility.Resurrect(pawn);
                }
            }
        }
    }
}