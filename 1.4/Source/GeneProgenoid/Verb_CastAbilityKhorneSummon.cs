using RimWorld;
using UnityEngine;
using Verse;

namespace BEWH
{
    public class Verb_CastAbilityKhorneSummon : Verb_CastAbility
    {

        protected override bool TryCastShot()
        {
            Pawn pawn = Caster as Pawn;
            int melee = 0;

            foreach (var skill in pawn.skills.skills)
            {
                if (skill.def.defName == "Melee")
                {
                    melee = skill.levelInt;
                }
            }
            
            int summonAmount = Mathf.FloorToInt(melee/2);

            if (base.TryCastShot())
            {
                for (int i = 0; i < summonAmount; i++)
                {
                    GenSpawn.Spawn(PawnGenerator.GeneratePawn(BEWHDefOf.BEWH_SummonedBloodletter, pawn.Faction), currentTarget.Cell, pawn.Map);
                }
            }
            return false;
        }
    }
}