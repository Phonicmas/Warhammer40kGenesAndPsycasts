using RimWorld;
using System.Collections.Generic;
using Verse;
using Verse.AI;

namespace BEWH
{
    public class RitualWorkGiver_DoBill : WorkGiver_DoBill
    {

        public List<string> DPrecipes = new List<string>()
        {
            "BEWH_DPUndividedRitual",
            "BEWH_DPKhorneRitual",
            "BEWH_DPTzeentchRitual",
            "BEWH_DPNurgleRitual",
            "BEWH_DPSlaaneshRitual"
        };

        public List<string> Mrecipes = new List<string>()
        {
            "BEWH_UndividedRitual",
            "BEWH_KhorneRitual",
            "BEWH_TzeentchRitual",
            "BEWH_NurgleRitual",
            "BEWH_SlaaneshRitual"
        };

        public List<string> ASrecipes = new List<string>()
        {
            "BEWH_CustodesAscend",
            "BEWH_PrimarchAscend"
        };

        public override Job JobOnThing(Pawn pawn, Thing thing, bool forced = false)
        {
            //Check if pawn would have a job by vanilla standards
            if (base.JobOnThing(pawn, thing, forced) != null && thing is IBillGiver billGiver)
            {
                bool markedPawn = IsMarked(pawn);
                //Check is there is any recipe at all
                BillStack billStack = billGiver.BillStack;
                if (billStack.FirstShouldDoNow == null)
                {
                    return null;
                }
                string recipeDefName = billStack.FirstShouldDoNow.recipe.defName;
                if (recipeDefName != null)
                {
                    //Checks what kind of ritual is being made, and if they are not allowed to do said ritual
                    if (DPrecipes.Contains(recipeDefName) && (!markedPawn || pawn.genes.HasGene(BEWHDefOf.BEWH_DaemonMutation)))
                    {
                        return null;
                    }
                    else if (Mrecipes.Contains(recipeDefName) && markedPawn)
                    {
                        return null;
                    }
                    else if (ASrecipes.Contains(recipeDefName) && IsAscended(pawn))
                    {
                        return null;
                    }
                }
                return base.JobOnThing(pawn, thing, forced);
            }
            return null;
        }

        public override bool HasJobOnThing(Pawn pawn, Thing t, bool forced = false)
        {
            bool hasJob = base.HasJobOnThing(pawn, t, forced);
            if (t is IBillGiver billGiver)
            {
                BillStack billStack = billGiver.BillStack;
                if (billStack.FirstShouldDoNow == null)
                {
                    return false;
                }
                string recipeDefName = billStack.FirstShouldDoNow.recipe.defName;
                if (recipeDefName != null && hasJob)
                {
                    bool markedPawn = IsMarked(pawn);
                    //This make sure the float menu is telling a pawn trying to become daemon prince, which may not, is told the reason why, being either that it is not marked or it is already a prince
                    if (DPrecipes.Contains(recipeDefName))
                    {
                        if (!markedPawn)
                        {
                            JobFailReason.Is("Is not marked".Translate());
                        }
                        else if (pawn.genes.HasGene(BEWHDefOf.BEWH_DaemonMutation))
                        {
                            JobFailReason.Is("Is already a daemon prince".Translate());
                        }
                        else
                        {
                            switch (recipeDefName)
                            {
                                case "BEWH_DPUndividedRitual":
                                    if (!pawn.genes.HasGene(BEWHDefOf.BEWH_UndividedMark))
                                    {
                                        JobFailReason.Is("Not marked by the great Undivided".Translate());
                                    }
                                    break;
                                case "BEWH_DPKhorneRitual":
                                    if (!pawn.genes.HasGene(BEWHDefOf.BEWH_UndividedMark))
                                    {
                                        JobFailReason.Is("Not marked by Khorne".Translate());
                                    }
                                    break;
                                case "BEWH_DPTzeentchRitual":
                                    if (!pawn.genes.HasGene(BEWHDefOf.BEWH_UndividedMark))
                                    {
                                        JobFailReason.Is("Not marked by Tzeentch".Translate());
                                    }
                                    break;
                                case "BEWH_DPNurgleRitual":
                                    if (!pawn.genes.HasGene(BEWHDefOf.BEWH_UndividedMark))
                                    {
                                        JobFailReason.Is("Not marked by Nurgle".Translate());
                                    }
                                    break;
                                case "BEWH_DPSlaaneshRitual":
                                    if (!pawn.genes.HasGene(BEWHDefOf.BEWH_UndividedMark))
                                    {
                                        JobFailReason.Is("Not marked by Slaanesh".Translate());
                                    }
                                    break;
                                default:
                                    break;
                            }
                        }
                        return false;
                    }
                    //This make sure the float menu is telling a pawn trying to become marked, which may not, is told the reason why, being that it is already marked.
                    else if (Mrecipes.Contains(recipeDefName) && markedPawn)
                    {
                        JobFailReason.Is("Is already marked by a god".Translate());
                        return false;
                    }
                    else if (ASrecipes.Contains(recipeDefName) && IsAscended(pawn))
                    {
                        JobFailReason.Is("Is already ascended".Translate());
                        return false;
                    }
                    return true;
                }
                else if (recipeDefName == null)
                {
                    return false;
                }
            }
            return false;
        }

        private bool IsMarked(Pawn pawn)
        {
            if (pawn.genes.HasGene(BEWHDefOf.BEWH_KhorneMark) || pawn.genes.HasGene(BEWHDefOf.BEWH_NurgleMark) || pawn.genes.HasGene(BEWHDefOf.BEWH_TzeentchMark) || pawn.genes.HasGene(BEWHDefOf.BEWH_SlaaneshMark) || pawn.genes.HasGene(BEWHDefOf.BEWH_UndividedMark))
            {
                return true;
            }
            return false;
        }

        private bool IsAscended(Pawn pawn)
        {
            if (pawn.genes.HasGene(BEWHDefOf.BEWH_Custodes) || pawn.genes.HasGene(BEWHDefOf.BEWH_Primarch))
            {
                return true;
            }
            return false;
        }

    }

}   