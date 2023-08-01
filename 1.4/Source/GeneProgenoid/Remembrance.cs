﻿using RimWorld;
using Verse;


namespace BEWH
{
    public class RemembranceComp : CompAbilityEffect
    {
        private new RemembranceCompProp Props => (RemembranceCompProp)props;

        public override void Apply(LocalTargetInfo target, LocalTargetInfo dest)
        {
            Pawn pawn = parent.pawn;
            Corpse corpse = target.Thing as Corpse;

            foreach (SkillDef allDef in DefDatabase<SkillDef>.AllDefs)
            {
                SkillRecord pawnSkill = pawn.skills.GetSkill(allDef);
                SkillRecord corpseSkill = corpse.InnerPawn.skills.GetSkill(allDef);

                float xpToGive = (float) ((corpseSkill.XpTotalEarned) * 0.1);

                pawnSkill.Learn(xpToGive);
            }
            corpse.Strip();
            corpse.Destroy();
        }
        public override bool CanApplyOn(LocalTargetInfo target, LocalTargetInfo dest)
        {
            Corpse corpse = target.Thing as Corpse;
            if (!(corpse.InnerPawn.RaceProps.Humanlike))
            {
                return false;
            }
            return true;
        }

    }
}