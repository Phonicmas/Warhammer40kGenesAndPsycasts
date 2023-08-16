using RimWorld;
using Verse;


namespace PsykerMod
{
    [DefOf]
    public static class PsykerDefOf
    {
        

        public static HediffDef BEWH_EcstaticOblivion;
        public static HediffDef BEWH_NurglesRot;

        public static PawnKindDef BEWH_SummonedPinkHorror;
        public static PawnKindDef BEWH_SummonedPlaguebearer;
        public static PawnKindDef BEWH_SummonedDaemonette;

        public static FactionDef BEWH_ChaosFactionHidden;

        static PsykerDefOf()
        {
            DefOfHelper.EnsureInitializedInCtor(typeof(PsykerDefOf));
        }
    }
}