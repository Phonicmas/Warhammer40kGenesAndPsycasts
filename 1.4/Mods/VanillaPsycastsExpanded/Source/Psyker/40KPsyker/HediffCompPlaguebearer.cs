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
            Pawn pawn = __instance.InnerPawn;
            Hediff hediff = pawn.health.hediffSet.hediffs.Find((Hediff x) => x.def == PsykerDefOf.BEWH_NurglesRot && x.Visible);
            if (hediff != null)
            {
                pawn.Strip();
                Pawn pb = PawnGenerator.GeneratePawn(PsykerDefOf.BEWH_SummonedPlaguebearer);
                GenSpawn.Spawn(pb, __instance.Position, __instance.Map);
                Log.Message("" + pawn.Faction);
                Log.Message("" + Faction.OfPlayer);
                pb.HostileTo(Faction.OfPlayer);
                __instance.Destroy();
            }
        }
    }
}