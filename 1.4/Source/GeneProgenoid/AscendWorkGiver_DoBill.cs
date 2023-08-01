using RimWorld;
using Verse;
using Verse.AI;

namespace BEWH
{
    public class AscendWorkGiver_DoBill : WorkGiver_DoBill
    {

        public override Job JobOnThing(Pawn pawn, Thing thing, bool forced = false)
        {
            if (base.JobOnThing(pawn, thing, forced) != null)
            {
                if (IsAscended(pawn)) 
                    return null;
                return base.JobOnThing(pawn, thing, forced);
            }
            return null;
        }

        public override bool HasJobOnThing(Pawn pawn, Thing t, bool forced = false)
        {
            bool flag = (base.HasJobOnThing(pawn, t, forced));

            if (!flag && IsAscended(pawn))
            {
                JobFailReason.Is("Is already ascended".Translate());
            }

            return flag && !IsAscended(pawn);
        }

        private bool IsAscended(Pawn pawn)
        {
            if (pawn.genes.HasGene(BEWHDefOf.BEWH_Custodes) || pawn.genes.HasGene(BEWHDefOf.BEWH_Primarch))
            {
                return true;
            }
            return false;
        }

    }

}   