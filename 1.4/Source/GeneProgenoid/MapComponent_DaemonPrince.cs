using HarmonyLib;
using RimWorld;
using System.Collections.Generic;
using Verse;


namespace BEWH
{
    public class MapComponent_DaemonPrince : MapComponent
    {
        public const int checkingInterval = 1000;

        public const int timeSinceDeath = 120000;

        public int tickCounter = 0;

        public List<Pawn> aliveDaemonPrincePawns = new List<Pawn>();

        public List<Pawn> deadDaemonPrincePawns = new List<Pawn>();

        public MapComponent_DaemonPrince(Map map)
            : base(map)
        {
        }

        public override void MapComponentTick()
        {

            base.MapComponentUpdate();
        }

        public override void MapComponentUpdate()
        {

        }
    }
}