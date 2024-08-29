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
using RimWorld;
using System.Collections.Generic;
using Verse;

namespace Foundation
{
    [HarmonyPatch(typeof(ThoughtHandler), "GetSocialThoughts", new System.Type[] { typeof(Pawn), typeof(List<ISocialThought>) })]
    public static class Foundation_Tools_Precept_Patch
    {
        public static bool Prefix(Pawn otherPawn, List<ISocialThought> outThoughts, ThoughtHandler __instance)
        {
            if (!__instance.pawn.IsMutant)
            {
                Ideo ideo = __instance.pawn.Ideo;
                if ((ideo != null ? (ideo.HasPrecept(FoundationDefOf.Foundation_Tools) ? 1 : 0) : 0) != 0 && otherPawn.IsMutant)
                    return false;
            }
            return true;
        }
    }
    [HarmonyPatch(typeof(ThoughtHandler), "GetSocialThoughts", new System.Type[] { typeof(Pawn), typeof(List<ISocialThought>) })]
    public static class Foundation_Tools_Bonded_Precept_Patch
    {
        public static bool Prefix(Pawn otherPawn, List<ISocialThought> outThoughts, ThoughtHandler __instance)
        {
            if (!__instance.pawn.IsMutant)
            {
                Ideo ideo = __instance.pawn.Ideo;
                if ((ideo != null ? (ideo.HasPrecept(FoundationDefOf.Foundation_Tools) ? 1 : 0) : 0) != 0 && otherPawn.IsMutant)
                    return false;
            }
            return true;
        }
    }

    [HarmonyPatch(typeof(TaleUtility), "Notify_PawnDied")]
    public static class Foundation_Died_Precept_Patch
    {
        public static void Postfix(Pawn victim, DamageInfo? dinfo)
        {
            if (victim.IsEntity || victim.IsMutant)
            {
                if (dinfo?.Instigator is Pawn instigator)
                    Find.HistoryEventsManager.RecordEvent(new HistoryEvent(FoundationDefOf.Foundation_Died, new SignalArgs(instigator.Named(HistoryEventArgsNames.Doer))));
                else
                    Find.HistoryEventsManager.RecordEvent(new HistoryEvent(FoundationDefOf.Foundation_Died));
            }
        }
    }
}