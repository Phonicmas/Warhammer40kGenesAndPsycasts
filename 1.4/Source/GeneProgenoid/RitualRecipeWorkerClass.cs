using System.Collections.Generic;
using Verse;

namespace BEWH
{
    public class RitualRecipeWorkerClass : RecipeWorker
    {
        public override void Notify_IterationCompleted(Pawn billDoer, List<Thing> ingredients)
        {
            switch (recipe.defName)
            {
                case "BEWH_UndividedRitual":
                    billDoer.genes.AddGene(BEWHDefOf.BEWH_UndividedMark, true);
                    break;
                case "BEWH_KhorneRitual":
                    billDoer.genes.AddGene(BEWHDefOf.BEWH_KhorneMark, true);
                    break;
                case "BEWH_TzeentchRitual":
                    billDoer.genes.AddGene(BEWHDefOf.BEWH_TzeentchMark, true);
                    break;
                case "BEWH_NurgleRitual":
                    billDoer.genes.AddGene(BEWHDefOf.BEWH_NurgleMark, true);
                    break;
                case "BEWH_SlaaneshRitual":
                    billDoer.genes.AddGene(BEWHDefOf.BEWH_SlaaneshMark, true);
                    break;
                case "BEWH_DPUndividedRitual":
                    billDoer.genes.AddGene(BEWHDefOf.BEWH_DaemonMutation, true);
                    billDoer.genes.AddGene(BEWHDefOf.BEWH_DaemonHide, true);
                    billDoer.genes.AddGene(BEWHDefOf.BEWH_DaemonWings, true);
                    billDoer.genes.AddGene(BEWHDefOf.BEWH_DaemonTail, true);
                    billDoer.genes.AddGene(BEWHDefOf.BEWH_DaemonHorns, true);
                    billDoer.genes.AddGene(BEWHDefOf.BEWH_UndividedColor, true);
                    break;
                case "BEWH_DPKhorneRitual":
                    billDoer.genes.AddGene(BEWHDefOf.BEWH_DaemonMutation, true);
                    billDoer.genes.AddGene(BEWHDefOf.BEWH_DaemonHide, true);
                    billDoer.genes.AddGene(BEWHDefOf.BEWH_DaemonWings, true);
                    billDoer.genes.AddGene(BEWHDefOf.BEWH_DaemonTail, true);
                    billDoer.genes.AddGene(BEWHDefOf.BEWH_DaemonHorns, true);
                    billDoer.genes.AddGene(BEWHDefOf.BEWH_KhorneColor, true);
                    break;
                case "BEWH_DPTzeentchRitual":
                    billDoer.genes.AddGene(BEWHDefOf.BEWH_DaemonMutation, true);
                    billDoer.genes.AddGene(BEWHDefOf.BEWH_DaemonHide, true);
                    billDoer.genes.AddGene(BEWHDefOf.BEWH_DaemonWings, true);
                    billDoer.genes.AddGene(BEWHDefOf.BEWH_DaemonTail, true);
                    billDoer.genes.AddGene(BEWHDefOf.BEWH_DaemonHorns, true);
                    billDoer.genes.AddGene(BEWHDefOf.BEWH_TzeencthColor, true);
                    break;
                case "BEWH_DPNurgleRitual":
                    billDoer.genes.AddGene(BEWHDefOf.BEWH_DaemonMutation, true);
                    billDoer.genes.AddGene(BEWHDefOf.BEWH_DaemonHide, true);
                    billDoer.genes.AddGene(BEWHDefOf.BEWH_DaemonWings, true);
                    billDoer.genes.AddGene(BEWHDefOf.BEWH_DaemonTail, true);
                    billDoer.genes.AddGene(BEWHDefOf.BEWH_DaemonHorns, true);
                    billDoer.genes.AddGene(BEWHDefOf.BEWH_NurgleColor, true);
                    break;
                case "BEWH_DPSlaaneshRitual":
                    billDoer.genes.AddGene(BEWHDefOf.BEWH_DaemonMutation, true);
                    billDoer.genes.AddGene(BEWHDefOf.BEWH_DaemonHide, true);
                    billDoer.genes.AddGene(BEWHDefOf.BEWH_DaemonWings, true);
                    billDoer.genes.AddGene(BEWHDefOf.BEWH_DaemonTail, true);
                    billDoer.genes.AddGene(BEWHDefOf.BEWH_DaemonHorns, true);
                    billDoer.genes.AddGene(BEWHDefOf.BEWH_SlaaneshColor, true);
                    break;
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