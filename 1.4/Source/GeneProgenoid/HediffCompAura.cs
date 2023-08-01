using Verse;


namespace BEWH
{
    public class HediffCompAura : HediffComp
    {
        public int tickCounter = 0;

        public HediffCompPropertiesAura Props => (HediffCompPropertiesAura)props;

        public override void CompPostTick(ref float severityAdjustment)
        {
            base.CompPostTick(ref severityAdjustment);
            tickCounter++;
            if (tickCounter < Props.tickInterval)
            {
                return;
            }
            Pawn pawn = parent.pawn;
            if (pawn != null && pawn.Map != null && !pawn.Dead)
            {
                foreach (Thing item in GenRadial.RadialDistinctThingsAround(pawn.Position, pawn.Map, Props.radius, useCenter: true))
                {
                    if (item is Pawn otherPawn && !pawn.Dead && otherPawn != pawn)
                    {
                        otherPawn.health.AddHediff(Props.hediff);
                    }
                }

            }
            tickCounter = 0;
        }
    }
}