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

using RimWorld;
using Verse;

namespace Foundation
{
    internal class ThoughtWorker_ManyVoices : ThoughtWorker
    {
        protected override ThoughtState CurrentStateInternal(Pawn p)
        {
            Hediff firstHediffOfDef = p.health.hediffSet.GetFirstHediffOfDef(FoundationDefOf.Foundation_ManyVoices_Breath_Hediff);
            if (firstHediffOfDef == null || firstHediffOfDef.FullyImmune())
                return (ThoughtState)false;
            if ((double)firstHediffOfDef.Severity >= 0.35)
                p.mindState.mentalStateHandler.TryStartMentalState(MentalStateDefOf.Wander_Psychotic, (string)("MBReason".Translate() + "SCP-939"));
            return (ThoughtState)true;
        }
    }
}