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

        public List<string> PPrecipes = new List<string>()
        {
            "BEWH_PsykerRitual",
            "BEWH_PariahAscend"
        };

        public override Job JobOnThing(Pawn pawn, Thing thing, bool forced = false)
        {
            //Check if pawn would have a job by vanilla standards
            if (base.JobOnThing(pawn, thing, forced) != null && thing is IBillGiver billGiver)
            {
                //Check is there is any recipe at all
                BillStack billStack = billGiver.BillStack;
                if (billStack.FirstShouldDoNow == null)
                {
                    return null;
                }
                string recipeDefName = billStack.FirstShouldDoNow.recipe.defName;
                if (recipeDefName != null)
                {
                    bool markedPawn = IsMarked(pawn);
                    bool psykerPawn = IsPsyker(pawn);
                    bool pariahPawn = IsPariah(pawn);
                    //Checks what kind of ritual is being made, and if they are not allowed to do said ritual
                    if (DPrecipes.Contains(recipeDefName) && (!markedPawn || pawn.genes.HasGene(BEWHDefOf.BEWH_DaemonMutation) || pariahPawn))
                    {
                        return null;
                    }
                    else if (Mrecipes.Contains(recipeDefName))
                    {
                        if (recipeDefName == "BEWH_KhorneRitual" && psykerPawn)
                        {
                            return null;
                        }
                        else if (markedPawn)
                        {
                            return null;
                        }
                        else if (pariahPawn)
                        {
                            return null;
                        }
                    }
                    else if (ASrecipes.Contains(recipeDefName) && IsAscended(pawn))
                    {
                        return null;
                    }
                    else if (PPrecipes.Contains(recipeDefName) && (psykerPawn || pariahPawn))
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
                if (recipeDefName != null && !hasJob)
                {
                    bool markedPawn = IsMarked(pawn);
                    bool pariahPawn = IsPariah(pawn);
                    bool psykerPawn = IsPsyker(pawn);
                    //This make sure the float menu is telling a pawn trying to become daemon prince, which may not, is told the reason why, being either that it is not marked or it is already a prince
                    if (DPrecipes.Contains(recipeDefName))
                    {
                        if (!markedPawn)
                        {
                            JobFailReason.Is("Is not marked".Translate());
                        }
                        else if (pariahPawn)
                        {
                            JobFailReason.Is("Is a pariahs".Translate());
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
                                    if (!pawn.genes.HasGene(BEWHDefOf.BEWH_KhorneMark))
                                    {
                                        JobFailReason.Is("Not marked by Khorne".Translate());
                                    }
                                    else if (psykerPawn)
                                    {
                                        JobFailReason.Is("Khorne does not allow psykers".Translate());
                                    }
                                    break;
                                case "BEWH_DPTzeentchRitual":
                                    if (!pawn.genes.HasGene(BEWHDefOf.BEWH_TzeentchMark))
                                    {
                                        JobFailReason.Is("Not marked by Tzeentch".Translate());
                                    }
                                    break;
                                case "BEWH_DPNurgleRitual":
                                    if (!pawn.genes.HasGene(BEWHDefOf.BEWH_NurgleMark))
                                    {
                                        JobFailReason.Is("Not marked by Nurgle".Translate());
                                    }
                                    break;
                                case "BEWH_DPSlaaneshRitual":
                                    if (!pawn.genes.HasGene(BEWHDefOf.BEWH_SlaaneshMark))
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
                    else if (Mrecipes.Contains(recipeDefName))
                    {
                        if (recipeDefName == "BEWH_KhorneRitual" && psykerPawn)
                        {
                            JobFailReason.Is("Khorne does not allow psykers".Translate());
                        }
                        else if (markedPawn)
                        {
                            JobFailReason.Is("Is already marked by a god".Translate());
                        }
                        else if (pariahPawn)
                        {
                            JobFailReason.Is("Is a pariahs".Translate());
                        }
                        return false;
                    }
                    else if (ASrecipes.Contains(recipeDefName) && IsAscended(pawn))
                    {
                        JobFailReason.Is("Is already ascended".Translate());
                        return false;
                    }
                    else if (recipeDefName == "BEWH_PsykerRitual")
                    {
                        if (psykerPawn)
                        {
                            JobFailReason.Is("Is already a psyker".Translate());
                        }
                        else if (pariahPawn)
                        {
                            JobFailReason.Is("Pariahs cannot be psykers".Translate());
                        }
                        return false;
                    }
                    else if (recipeDefName == "BEWH_PariahAscend")
                    {
                        if (psykerPawn)
                        {
                            JobFailReason.Is("Psykers cannot be pariahs".Translate());
                        }
                        else if (pariahPawn)
                        {
                            JobFailReason.Is("Is already a pariah".Translate());
                        }
                        return false;
                    }
                }
                else if (recipeDefName == null)
                {
                    return false;
                }
                return true;
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

        private bool IsPariah(Pawn pawn)
        {
            if (pawn.genes.HasGene(BEWHDefOf.BEWH_SigmaPariah) || pawn.genes.HasGene(BEWHDefOf.BEWH_UpsilonPariah) || pawn.genes.HasGene(BEWHDefOf.BEWH_OmegaPariah))
            {
                return true;
            }
            return false;
        }

        private bool IsPsyker(Pawn pawn)
        {
            if (pawn.genes.HasGene(BEWHDefOf.BEWH_IotaPsyker) || pawn.genes.HasGene(BEWHDefOf.BEWH_Psyker) || pawn.genes.HasGene(BEWHDefOf.BEWH_DeltaPsyker) || pawn.genes.HasGene(BEWHDefOf.BEWH_BetaPsyker))
            {
                return true;
            }
            return false;
        }

    }

}   