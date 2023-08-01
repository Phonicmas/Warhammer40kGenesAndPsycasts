using RimWorld;
using Verse;


namespace BEWH
{
    public class ThoughtWorkerSigmaPariah : ThoughtWorker
    {
        protected override ThoughtState CurrentSocialStateInternal(Pawn pawn, Pawn other)
        {
            if (!other.RaceProps.Humanlike || !RelationsUtility.PawnsKnowEachOther(pawn, other))
            {
                return false;
            }
            if (pawn.story.traits.HasTrait(TraitDefOf.Kind))
            {
                return false;
            }
            if (!other.genes.HasGene(BEWHDefOf.BEWH_SigmaPariah))
            {
                return false;
            }
            return true;
        }
    }
}