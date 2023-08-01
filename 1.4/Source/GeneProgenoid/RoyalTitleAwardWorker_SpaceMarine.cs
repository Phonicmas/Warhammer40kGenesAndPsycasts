using RimWorld;
using System.Collections.Generic;
using System.Linq;
using Verse;


namespace BEWH
{
    public class RoyalTitleAwardWorker_SpaceMarine : RoyalTitleAwardWorker
    {
        public override void DoAward(Pawn pawn, Faction faction, RoyalTitleDef currentTitle, RoyalTitleDef newTitle)
        {
            /*Genepack genepack = (Genepack)ThingMaker.MakeThing(ThingDefOf.Genepack);
            genepack.Initialize(AstartesPack());

            List<Genepack> genepacks = new List<Genepack>
            {
                genepack
            };

            Xenogerm xenogerm = (Xenogerm)ThingMaker.MakeThing(ThingDefOf.Xenogerm);
            xenogerm.Initialize(genepacks, "Space Marine", BEWHDefOf.BEWH_Astartes);

            if (GenPlace.TryPlaceThing(((Thing)xenogerm), pawn.PositionHeld, pawn.MapHeld, ThingPlaceMode.Near))
                return;
            Log.Error("Could not drop item near " + (object)pawn.PositionHeld);*/

            List<GeneDef> astartesPack = AstartesPack();
            for (int i = 0; i < astartesPack.Count(); i++)
            {
                pawn.genes.AddGene(astartesPack[i], true);
            }

            base.DoAward(pawn, faction, currentTitle, newTitle);
        }

        private List<GeneDef> AstartesPack()
        {
            List<GeneDef> genedef = new List<GeneDef>
            {
                BEWHDefOf.BEWH_SecondaryHeart,
                BEWHDefOf.BEWH_Ossmodula,
                BEWHDefOf.BEWH_Biscopea,
                BEWHDefOf.BEWH_Haemastamen,
                BEWHDefOf.BEWH_LarramansOrgan,
                BEWHDefOf.BEWH_CatalepseanNode,
                BEWHDefOf.BEWH_Preomnor,
                BEWHDefOf.BEWH_Omophagea,
                BEWHDefOf.BEWH_MultiLung,
                BEWHDefOf.BEWH_Occulobe,
                BEWHDefOf.BEWH_LymansEar,
                BEWHDefOf.BEWH_SusAnMembrane,
                BEWHDefOf.BEWH_Melanochrome,
                BEWHDefOf.BEWH_OoliticKidney,
                BEWHDefOf.BEWH_Neuroglottis,
                BEWHDefOf.BEWH_Mucranoid,
                BEWHDefOf.BEWH_BetchersGland,
                BEWHDefOf.BEWH_ProgenoidGlands,
                BEWHDefOf.BEWH_BlackCarapace
            };
            return genedef;
        }
    }

}