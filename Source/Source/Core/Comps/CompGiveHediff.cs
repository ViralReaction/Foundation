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
using Foundation.SRA;
using RimWorld;
using System.Collections.Generic;
using Verse;

namespace Foundation
{
    public class CompGiveHediff : ThingComp
    {
        public int tickCounter = 0;
        public Pawn thisPawn;

        private CompProperties_GiveHediffSeverity Props => (CompProperties_GiveHediffSeverity)this.props;

        public override void CompTick()
        {
            base.CompTick();
            ++tickCounter;
            if (tickCounter > Props.tickInterval)
            {
                thisPawn = this.parent as Pawn;
                if (Props.scrantonCheck && ScrantonCheck(thisPawn))
                {
                        tickCounter = 0;
                        return;
                }
                if (thisPawn != null && thisPawn.Map != null && !thisPawn.Dead && !thisPawn.Downed)
                {
                    List<Pawn> pawnsAround = SCPRadius.GetPawnsAround(thisPawn.Position, Props.range, thisPawn.Map);
                    for (int index = 0; index < pawnsAround.Count; index++)
                    {
                        Pawn pawn = pawnsAround[index];
                        if (pawn != null && !Props.defNamesImmune.Contains(pawn.def.defName))
                        {
                            Hediff firstHediffOfDef = pawn.health.hediffSet.GetFirstHediffOfDef(Props.hediff);
                            if (!pawn.Dead)
                            {
                                if (Props.psychicCheck == true && pawn.GetStatValue(StatDefOf.PsychicSensitivity, true) > 0f)
                                {
                                    if (firstHediffOfDef != null)
                                        firstHediffOfDef.Severity += Props.severityTick;
                                    else
                                        pawn.health.AddHediff(Props.hediff);
                                }
                                else
                                {
                                    if (firstHediffOfDef != null)
                                        firstHediffOfDef.Severity += Props.severityTick;
                                    else
                                        pawn.health.AddHediff(Props.hediff);
                                }
                            }
                        }
                    }
                }
                tickCounter = 0;
            }
        }
        private bool ScrantonCheck(Pawn pawn)
        {
            var field = SuppressionFieldAccessUtility.GetSuppressionFieldManager(pawn.Map)
                            ?.GetEffectOnCell(pawn.Position) ?? 0f;
            if (field != 0f)
                return true;
            return false;
        }
    }
}