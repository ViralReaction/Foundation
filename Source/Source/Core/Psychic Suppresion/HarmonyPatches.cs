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
using System.Reflection;
using Verse;

namespace Foundation.SRA
{
    public static class HarmonyPatch_PsychicSuppression
    {
        private static readonly System.Type patchType = typeof(HarmonyPatch_PsychicSuppression);
        static HarmonyPatch_PsychicSuppression() => new Harmony("SCP.psychic.suppress").PatchAll(Assembly.GetExecutingAssembly());

        [HarmonyPatch(typeof(StatWorker), "GetValueUnfinalized")]
        public static class Foundation_StatValuePatch
        {

            public static float Postfix(float __result, StatRequest req, StatDef ___stat)
            {
                if (!ScrantonCachingUtility.EverAffectsStat(___stat) || !req.HasThing) return __result;

                if (req.Thing is Pawn pawn)
                {
                    if (___stat == StatDefOf.PsychicSensitivity)
                    {
                        var field = SuppressionFieldAccessUtility.GetSuppressionFieldManager(pawn.Map)
                            ?.GetEffectOnCell(pawn.Position) ?? 0f;
                        if (field != 0f)
                        {
                            __result += field;
                        }

                        return __result;
                    }
                } return __result;
            }
        }
    }
}
