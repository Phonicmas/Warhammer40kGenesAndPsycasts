using RimWorld;
using System.Collections.Generic;
using System.Linq;
using Verse;


namespace BEWH
{
    public class MapComponent_DaemonPrince : MapComponent
    {
        public const int checkingInterval = 10000;

        public const int deathTiemr = 12000;

        public int tickCounter = 0;

        public List<Corpse> deadDaemonPrincePawns = new List<Corpse>();

        public MapComponent_DaemonPrince(Map map)
            : base(map)
        {
        }

        public override void MapComponentTick()
        {
            base.MapComponentUpdate();
            if (tickCounter >= checkingInterval)
            {
                tickCounter = 0;

                foreach (Thing thing in map.spawnedThings)
                {
                    if (thing is Corpse corpse)
                    {
                        if (corpse.InnerPawn != null && corpse.InnerPawn.genes.HasGene(BEWHDefOf.BEWH_DaemonMutation) && !deadDaemonPrincePawns.Contains(corpse))
                        {
                            deadDaemonPrincePawns.Add(corpse);
                        }
                    }
                }
                if (deadDaemonPrincePawns.Count > 0)
                {
                    List<Corpse> toRemove = new List<Corpse>();
                    foreach (Corpse corpse in deadDaemonPrincePawns)
                    {
                        if (Find.TickManager.TicksGame - corpse.timeOfDeath >= deathTiemr)
                        {
                            toRemove.Add(corpse);
                            if (corpse.InnerPawn != null)
                            {
                                ResurrectionUtility.Resurrect(corpse.InnerPawn);
                            }
                        }
                    }
                    for (int i = 0; i < toRemove.Count; i++)
                    {
                        deadDaemonPrincePawns.Remove(toRemove.First());
                    }
                    toRemove.Clear();
                }
                
            }
            tickCounter++;
        }
    }
}