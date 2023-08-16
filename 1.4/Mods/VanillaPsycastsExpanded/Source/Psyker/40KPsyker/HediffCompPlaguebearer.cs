using HarmonyLib;
using Psyker;
using PsykerMod;
using RimWorld;
using Verse;


namespace Psyker
{
    public class HediffCompPlaguebearer : HediffComp
    {
        public HediffCompPropertiesPlaguebearer Props => (HediffCompPropertiesPlaguebearer)props;

        public void PatchCorpse(Harmony harmony)
        {
            harmony.Patch(AccessTools.Method(typeof(Corpse), "TickRare"), null, new HarmonyMethod(GetType(), "CorpseTick"));
        }

        public static void CorpseTick(Corpse __instance)
        {
            if (__instance.InnerPawn.health.hediffSet.hediffs.Find((Hediff x) => x.def == PsykerDefOf.BEWH_NurglesRot && x.Visible) != null)
            {
                __instance.InnerPawn.Strip();
                Pawn pb = PawnGenerator.GeneratePawn(PsykerDefOf.BEWH_Plaguebearer);
                GenSpawn.Spawn(pb, __instance.Position, __instance.Map);
                pb.HostileTo(Faction.OfPlayer);
                __instance.DeSpawn();
            }
        }
    }
}