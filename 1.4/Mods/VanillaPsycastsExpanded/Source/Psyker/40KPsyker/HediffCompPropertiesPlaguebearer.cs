using Verse;


namespace Psyker
{
    public class HediffCompPropertiesPlaguebearer : HediffCompProperties
    {
        public HediffCompPropertiesPlaguebearer()
        {
            compClass = typeof(HediffCompPlaguebearer);
        }

        public override void PostLoad()
        {
            base.PostLoad();
            HediffCompPlaguebearer instance = new HediffCompPlaguebearer();
            instance.PatchCorpse(PsykerMod.harmony);
        }

    }
}