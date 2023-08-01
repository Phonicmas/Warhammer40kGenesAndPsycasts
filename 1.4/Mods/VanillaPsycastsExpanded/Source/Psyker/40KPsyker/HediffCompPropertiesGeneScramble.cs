using Psyker;
using Verse;


namespace Psyker
{
    public class HediffCompPropertiesGeneScramble : HediffCompProperties
    {

        public int scrambleAmount;

        public HediffCompPropertiesGeneScramble()
        {
            compClass = typeof(HediffCompGeneScramble);
        }

    }
}