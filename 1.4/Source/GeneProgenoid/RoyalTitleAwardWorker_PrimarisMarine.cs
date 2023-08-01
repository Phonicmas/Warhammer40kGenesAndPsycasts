using RimWorld;
using System.Collections.Generic;
using System.Linq;
using Verse;


namespace BEWH
{
    public class RoyalTitleAwardWorker_PrimarisMarine : RoyalTitleAwardWorker
    {
        public override void DoAward(Pawn pawn, Faction faction, RoyalTitleDef currentTitle, RoyalTitleDef newTitle)
        {
            List<GeneDef> primarisPack = PrimarisPack();
            for (int i = 0; i < primarisPack.Count(); i++)
            {
                pawn.genes.AddGene(primarisPack[i], true);
            }

            base.DoAward(pawn, faction, currentTitle, newTitle);
        }

        private List<GeneDef> PrimarisPack()
        {
            List<GeneDef> genedef = new List<GeneDef>
            {
                BEWHDefOf.BEWH_SinewCoil,
                BEWHDefOf.BEWH_Magnificat,
                BEWHDefOf.BEWH_BelisarianFurnace
            };
            return genedef;
        }
    }

}