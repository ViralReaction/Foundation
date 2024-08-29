#region
//Foundation
//Copyright (C) 2023  ViralReaction
//
//This program is free software: you can redistribute it and/or modify
//it under the terms of the GNU General Public License as published by
//the Free Software Foundation, either version 3 of the License, or
//(at your option) any later version.
//
//This program is distributed in the hope that it will be useful,
//but WITHOUT ANY WARRANTY; without even the implied warranty of
//MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//GNU General Public License for more details.
//
//You should have received a copy of the GNU General Public License
//along with this program.  If not, see <http://www.gnu.org/licenses/>.
#endregion

using HarmonyLib;
using RimWorld.Planet;
using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Verse.AI;
using Verse;

namespace Foundation.HarmonyPatches
{
    [StaticConstructorOnStartup]
    internal static class FoundationHarmony
    {
        static FoundationHarmony()
        {
            Harmony harmony = new Harmony("rw.foundation");
            harmony.PatchAll(Assembly.GetExecutingAssembly());
            harmony.Patch((MethodBase)AccessTools.Method(typeof(Need_Food), "NeedInterval"), postfix: new HarmonyMethod(typeof(FoundationHarmony), "Anomalies_Full"));
            harmony.Patch((MethodBase)AccessTools.Method(typeof(Need_Food), "NeedInterval"), postfix: new HarmonyMethod(typeof(FoundationHarmony), "Anomalies_Starving"));
            harmony.Patch((MethodBase)AccessTools.Method(typeof(FoodUtility), "IsAcceptablePreyFor"), new HarmonyMethod(typeof(FoundationHarmony), "Anomalies_HumansOnlyAcceptablePrey"));
            harmony.Patch((MethodBase)AccessTools.Method(typeof(JobDriver_PredatorHunt), "CheckWarnPlayer"), new HarmonyMethod(typeof(FoundationHarmony), "OldMan_DontWarnPlayerHunted"));
            harmony.Patch((MethodBase)AccessTools.Method(typeof(JobDriver_PredatorHunt), "CheckWarnPlayer"), new HarmonyMethod(typeof(FoundationHarmony), "SCP939_DontWarnPlayerHunted"));
            harmony.Patch((MethodBase)AccessTools.Method(typeof(Pawn), "TicksPerMove"), postfix: new HarmonyMethod(typeof(FoundationHarmony), "ManyVoices_VoicesMovementSpeed"));
            harmony.Patch((MethodBase)AccessTools.Method(typeof(Pawn), "TickRare"), postfix: new HarmonyMethod(typeof(FoundationHarmony), "TickMindstateLeaveDaylight"));
            harmony.Patch((MethodBase)AccessTools.Method(typeof(WorldPawns), "GetSituation"), postfix: new HarmonyMethod(typeof(FoundationHarmony), "SituationSCPEvent"));
        }

        public static void Anomalies_Full(Need_Food __instance, Pawn ___pawn)
        {
            if (!(___pawn.def.defName == "Foundation_096_Shy_Guy"))
                return;
            __instance.CurLevel = 1f;
        }

        public static void Anomalies_Starving(Need_Food __instance, Pawn ___pawn)
        {
            if (!(___pawn.def.defName == "Foundation_106_Old_Man" || ___pawn.def.defName == "Foundation_ManyVoices"))
                return;
            __instance.CurLevel = 0.1f;
        }

        public static bool Anomalies_HumansOnlyAcceptablePrey(Pawn predator, Pawn prey, ref bool __result)
        {
            if (!(predator.def.defName == "Foundation_106_Old_Man") || predator.def.defName == "Foundation_ManyVoices")
                return true;
            __result = false;
            if (prey.RaceProps.Humanlike)
                __result = true;
            return false;
        }

        public static bool OldMan_DontWarnPlayerHunted(JobDriver_PredatorHunt __instance) => __instance.pawn.GetComp<Comp_OldMan>() == null;

        public static bool ManyVoices_DontWarnPlayerHunted(JobDriver_PredatorHunt __instance) => __instance.pawn.GetComp<CompVoices>() == null;

        public static void ManyVoices_VoicesMovementSpeed(bool diagonal, Pawn __instance, ref float __result)
        {
            if (__instance.TryGetComp<CompVoices>() == null)
                return;
            if (__instance.GetComp<CompVoices>().VoicesActive)
                __result *= 100;
            else if (__instance.GetComp<CompVoices>().TargetLured)
                __result *= 10000;
        }

        public static void TickMindstateLeaveDaylight(Pawn __instance)
        {
            IntVec3 result;
            if (__instance?.kindDef != PawnKindDefOf_SCP.Foundation_ManyVoices_Incident || !__instance.Spawned || GenLocalDate.HourOfDay(__instance.Map) < 5 || GenLocalDate.HourOfDay(__instance.Map) >= 19 || !CellFinder.TryFindRandomPawnExitCell(__instance, out result) || __instance.Map.GameConditionManager.ConditionIsActive(GameConditionDefOf.Eclipse) || __instance.Map.GameConditionManager.ConditionIsActive(GameConditionDefOf.VolcanicWinter) || __instance.Map.GameConditionManager.ConditionIsActive(GameConditionDefOf.UnnaturalDarkness))
                return;
            Job job = new Job(FoundationDefOf.LeaveMapDaylight, (LocalTargetInfo)result);
            __instance.jobs.TryTakeOrderedJob(job, JobTag.DraftedOrder);
        }

        public static void SituationSCPEvent(
          Pawn p,
          ref WorldPawnSituation __result,
          WorldPawns __instance)
        {
            if (__result != WorldPawnSituation.Free || p.kindDef != PawnKindDefOf_SCP.Foundation_ManyVoices_Incident)
                return;
            foreach (Map map in Find.Maps)
            {
                if (map.GameConditionManager.ActiveConditions.Any<GameCondition>((Predicate<GameCondition>)(x => x is GameCondition_ManyVoices && (x as GameCondition_ManyVoices).ActiveSCPInArea.Contains(p))))
                {
                    __result = WorldPawnSituation.InTravelingTransportPod;
                    break;
                }
            }
        }

        [HarmonyPatch(typeof(TradeUtility), "AllSellableColonyPawns")]
        class SellCapturedFoundation_Patch
        {
            public static IEnumerable<Pawn> Postfix(IEnumerable<Pawn> values, Map map, bool checkAcceptableTemperatureOfAnimals)
            {
                foreach (Pawn pawn in map.mapPawns.AllPawnsSpawned)
                {
                    if (pawn.IsOnHoldingPlatform && pawn.HostFaction == null && !pawn.InMentalState && !pawn.Downed && (!checkAcceptableTemperatureOfAnimals || map.mapTemperature.SeasonAndOutdoorTemperatureAcceptableFor(pawn.def)))
                        yield return pawn;
                }
            }
        }
    }
}