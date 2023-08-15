using RimWorld;
using Verse;


namespace PsykerMod
{
    [DefOf]
    public static class PsykerDefOf
    {
        public static DamageDef BEWH_WarpFlame;

        public static HediffDef BEWH_EcstaticOblivion;

        public static PawnKindDef BEWH_SummonedPinkHorror;

        public static PawnKindDef BEWH_SummonedPlaguebearer;
        
        public static PawnKindDef BEWH_SummonedDaemonette;

        static PsykerDefOf()
        {
            DefOfHelper.EnsureInitializedInCtor(typeof(PsykerDefOf));
        }
    }
}