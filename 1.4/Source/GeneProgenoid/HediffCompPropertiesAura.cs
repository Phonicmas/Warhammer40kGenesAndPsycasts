using Verse;


namespace BEWH
{
    public class HediffCompPropertiesAura : HediffCompProperties
    {

        public int radius = 1;

        public int tickInterval = 100;

        public HediffDef hediff;

        public float severity = 1f;

        public HediffCompPropertiesAura()
        {
            compClass = typeof(HediffCompAura);
        }

    }
}