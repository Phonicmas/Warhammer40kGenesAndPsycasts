﻿using RimWorld;
using System.Collections.Generic;
using System.Linq;
using Verse;

namespace BEWH
{
    public class GeneProgenoidRemovalAllGenesWorkerClass : Recipe_Surgery
    {
        public override bool AvailableOnNow(Thing thing, BodyPartRecord part = null)
        {
            if (!base.AvailableOnNow(thing, part) || !(thing is Pawn pawn))
                return false;
            if (!pawn.genes.HasGene(BEWHDefOf.BEWH_ProgenoidGlands))
                return false;
            if (recipe.defName == "BEWH_PrimarisPack" && !IsPrimaris(pawn))
                return false;
            List<Hediff> hediffs = pawn.health.hediffSet.hediffs;
            for (int index = 0; index < hediffs.Count; ++index)
            {
                if ((!recipe.targetsBodyPart || hediffs[index].Part != null) && hediffs[index].def == recipe.removesHediff && hediffs[index].Visible)
                {
                    if (hediffs[index].Severity >= 1f)
                        return true;
                }
            }
            return false;
        }

        public override void ApplyOnPawn(Pawn pawn, BodyPartRecord part, Pawn billDoer, List<Thing> ingredients, Bill bill)
        {
            if (billDoer != null)
            {
                if (CheckSurgeryFail(billDoer, pawn, ingredients, part, bill))
                {
                    return;
                }
                TaleRecorder.RecordTale(TaleDefOf.DidSurgery, billDoer, pawn);
                if (PawnUtility.ShouldSendNotificationAbout(pawn) || PawnUtility.ShouldSendNotificationAbout(billDoer))
                {
                    string text = (recipe.successfullyRemovedHediffMessage.NullOrEmpty() ? ((string)"MessageSuccessfullyRemovedHediff".Translate(billDoer.LabelShort, pawn.LabelShort, recipe.removesHediff.label.Named("HEDIFF"), billDoer.Named("SURGEON"), pawn.Named("PATIENT"))) : ((string)recipe.successfullyRemovedHediffMessage.Formatted(billDoer.LabelShort, pawn.LabelShort)));
                    Messages.Message(text, pawn, MessageTypeDefOf.PositiveEvent);
                }
            }
            Hediff hediff = pawn.health.hediffSet.hediffs.Find((Hediff x) => x.def == recipe.removesHediff && x.Part == part && x.Visible);
            if (hediff != null)
            {
                pawn.health.RemoveHediff(hediff);
                OnSurgerySuccess(pawn, part, billDoer, ingredients, bill);
            }
        }

        protected override void OnSurgerySuccess(Pawn pawn, BodyPartRecord part, Pawn billDoer, List<Thing> ingredients, Bill bill)
        {
            Genepack genepack = (Genepack)ThingMaker.MakeThing(ThingDefOf.Genepack);
            List<GeneDef> genedef = new List<GeneDef>();
            for (int i = 0; i < pawn.genes.Xenogenes.Count(); i++)
            {
                genedef.Add(pawn.genes.Xenogenes[i].def);
            }
            genepack.Initialize(genedef);

            List<Genepack> genepacks = new List<Genepack>();

            genepacks.Add(genepack);

            Xenogerm xenogerm = (Xenogerm)ThingMaker.MakeThing(ThingDefOf.Xenogerm);
            if (IsPrimaris(pawn))
            {
                xenogerm.Initialize(genepacks, "Primaris Space Marine", BEWHDefOf.BEWH_Astartes);
            }
            else
            {
                xenogerm.Initialize(genepacks, "Space Marine", BEWHDefOf.BEWH_Primaris);
            }

            ClearQueue(pawn);
            if (GenPlace.TryPlaceThing(((Thing)xenogerm), pawn.PositionHeld, pawn.MapHeld, ThingPlaceMode.Near))
                return;
            Log.Error("Could not drop item near " + (object)pawn.PositionHeld);
        }

        private void ClearQueue(Pawn pawn)
        {
            BillStack bills = pawn.health.surgeryBills;
            for (int i = 1; i < bills.Count; i++)
            {
                if (bills[i].recipe.defName == "BEWH_AstartesPack")
                {
                    bills.Delete(bills[i]);
                    i--;
                }
                if (bills[i].recipe.defName == "BEWH_PrimarisPack")
                {
                    bills.Delete(bills[i]);
                    i--;
                }
            }
        }

        private bool IsPrimaris(Pawn pawn)
        {
            if (pawn.genes.HasGene(BEWHDefOf.BEWH_SinewCoil) && pawn.genes.HasGene(BEWHDefOf.BEWH_Magnificat) && pawn.genes.HasGene(BEWHDefOf.BEWH_BelisarianFurnace))
            {
                return true;
            }
            return false;
        }
    }

}