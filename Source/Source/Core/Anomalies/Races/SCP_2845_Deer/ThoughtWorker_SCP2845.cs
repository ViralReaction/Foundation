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
    internal class ThoughtWorker_SCP2845 : ThoughtWorker
    {
        protected override ThoughtState CurrentStateInternal(Pawn p)
        {
            Hediff firstHediffOfDef = p.health.hediffSet.GetFirstHediffOfDef(FoundationDefOf.Foundation_Deer_Transmute_Hediff);
            if (firstHediffOfDef == null || firstHediffOfDef.FullyImmune())
                return (ThoughtState)false;
            if ((double)firstHediffOfDef.Severity >= 0.8)
            {
                PawnKindDef kindDef = PawnKindDefOf_SCP.Foundation_Deer_Transmuted;
                IntVec3 result;
                CellFinder.TryFindRandomSpawnCellForPawnNear(p.Position, p.Map, out result);
                IntVec3 loc = CellFinder.RandomClosewalkCellNear(result, p.Map, 10);
                if (GenSpawn.Spawn((Thing)PawnGenerator.GeneratePawn(kindDef), loc, p.Map) is Pawn pawn)
                {
                    pawn.health.AddHediff(HediffDefOf.Scaria);
                    pawn.mindState.mentalStateHandler.TryStartMentalState(MentalStateDefOf.Manhunter);
                }
            }
            return (ThoughtState)true;
        }
    }
}