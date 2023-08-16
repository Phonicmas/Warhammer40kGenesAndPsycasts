﻿using PsykerMod;
using RimWorld.Planet;
using UnityEngine;
using Verse;

namespace Psyker
{
    public class Ability_SpawnDaemon : VFECore.Abilities.Ability
    {
        public override void Cast(params GlobalTargetInfo[] targets)
        {
            base.Cast(targets);
            foreach (GlobalTargetInfo globalTargetInfo in targets)
            {
                IntVec3 position = globalTargetInfo.Cell;
                PawnKindDef summon;

                if (def.defName == null)
                {
                    return;
                }

                switch (def.defName)
                {
                    case "BEWH_SummonDaemonTzeentch":
                        summon = PsykerDefOf.BEWH_SummonedPinkHorror;
                        break;
                    case "BEWH_SummonDaemonNurgle":
                        summon = PsykerDefOf.BEWH_SummonedPlaguebearer;
                        break;
                    case "BEWH_SummonDaemonSlaanesh":
                        summon = PsykerDefOf.BEWH_SummonedDaemonette;
                        break;
                    default:
                        Log.Error("Failed to find defName for psyker power (summon)");
                        return;
                }

                int summonAmount = Mathf.FloorToInt(GetPowerForPawn());
                if (summonAmount == 0)
                {
                    GenSpawn.Spawn(PawnGenerator.GeneratePawn(summon, pawn.Faction), position, pawn.Map);
                }
                else
                {
                    for (int i = 0; i < summonAmount; i++)
                    {
                        GenSpawn.Spawn(PawnGenerator.GeneratePawn(summon, pawn.Faction), position, pawn.Map);
                    }
                }
            }
        }
    }

}