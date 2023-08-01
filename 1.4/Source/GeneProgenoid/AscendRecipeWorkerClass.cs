using System.Collections.Generic;
using Verse;

namespace BEWH
{
    public class AscendRecipeWorkerClass : RecipeWorker
    {
        public override void Notify_IterationCompleted(Pawn billDoer, List<Thing> ingredients)
        {
            switch (recipe.defName)
            {
                case "BEWH_CustodesAscend":
                    billDoer.genes.AddGene(BEWHDefOf.BEWH_Custodes, true);
                    break;
                case "BEWH_PrimarchAscend":
                    billDoer.genes.AddGene(BEWHDefOf.BEWH_Primarch, true);
                    break;
                default:
                    break;
            }
        }

    }

}   