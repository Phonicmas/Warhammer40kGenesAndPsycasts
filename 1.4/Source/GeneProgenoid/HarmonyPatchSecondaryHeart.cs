﻿using HarmonyLib;
using RimWorld;
using Verse;


namespace BEWH
{
    [HarmonyPatch(typeof(PawnCapacityWorker_BloodPumping), "CalculateCapacityLevel")]
    public class SecondaryHeartBloodSourcePatch
    {
        public static float Postfix(float originalResult, HediffSet __0) 
        {
            HediffSet hediffSet = __0;

            Pawn pawn = hediffSet.pawn;
            if (pawn.DestroyedOrNull() || pawn.genes == null || pawn.Dead)
            {
                return originalResult;
            }
            if (pawn.genes.HasGene(BEWHDefOf.BEWH_SecondaryHeart))
            {
                float result = originalResult + 0.5f;
                return result;
            }
            return originalResult;
        }
    }
}