using Verse;


namespace BEWH
{
    public class HediffCompPropertiesDaemonPrince : HediffCompProperties
    {
        public HediffCompPropertiesDaemonPrince()
        {
            compClass = typeof(HediffCompDaemonPrince);
        }

        public override void PostLoad()
        {
            base.PostLoad();
            HediffCompDaemonPrince instance = new HediffCompDaemonPrince();
            instance.PatchCorpse(BEWHMod.harmony);
        }

    }
}