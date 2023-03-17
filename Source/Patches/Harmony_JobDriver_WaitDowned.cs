﻿using HarmonyLib;
using Verse;
using Verse.AI;

namespace MedPod
{
    // Remove induced coma hediff and wake patient if they are accidentally kicked off a MedPod
    // This handles an edge case where the player prioritizes Patient B to use a MedPod while it is already treating Patient A
    [HarmonyPatch(typeof(JobDriver_WaitDowned), nameof(JobDriver_WaitDowned.DecorateWaitToil))]
    public static class JobDriver_WaitDowned_DecorateWaitToil_WakePatientIfKickedOffMedPod
    {
        public static void Prefix(Pawn ___pawn)
        {
            if (___pawn.health.hediffSet.hediffs.Any((Hediff x) => x.def == MedPodDef.MedPod_InducedComa))
            {
                Building_BedMedPod.WakePatient(___pawn, false);
            }
        }
    }
}
