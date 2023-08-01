using RimWorld;
using System.Collections.Generic;
using System.Linq;
using Verse;

namespace BEWH
{
    public class GeneProgenoidRemovalWorkerClass : Recipe_Surgery
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

            if (IsPrimaris(pawn))
            {
                genepack.Initialize(PrimarisPack());
            }
            else
            {
                genepack.Initialize(AstartesPack());
            }
            
            List<Genepack> genepacks = new List<Genepack>();

            genepacks.Add(genepack);

            Xenogerm xenogerm = (Xenogerm)ThingMaker.MakeThing(ThingDefOf.Xenogerm);
            if (IsPrimaris(pawn))
            {
                xenogerm.Initialize(genepacks, "Primaris Space Marine", BEWHDefOf.BEWH_Primaris);
            }
            else
            {
                xenogerm.Initialize(genepacks, "Space Marine", BEWHDefOf.BEWH_Astartes);
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

        private List<GeneDef> PrimarisPack()
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
                BEWHDefOf.BEWH_BlackCarapace,
                BEWHDefOf.BEWH_SinewCoil,
                BEWHDefOf.BEWH_Magnificat,
                BEWHDefOf.BEWH_BelisarianFurnace
            };
            return genedef;
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