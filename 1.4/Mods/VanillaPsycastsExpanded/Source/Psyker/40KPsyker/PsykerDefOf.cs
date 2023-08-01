using RimWorld;
using Verse;


namespace PsykerMod
{
    [DefOf]
    public static class PsykerDefOf
    {
        public static DamageDef BEWH_WarpFlame;

        public static HediffDef BEWH_EcstaticOblivion;

        static PsykerDefOf()
        {
            DefOfHelper.EnsureInitializedInCtor(typeof(PsykerDefOf));
        }
    }
}