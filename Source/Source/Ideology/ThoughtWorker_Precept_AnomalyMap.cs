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
using System.Collections.Generic;
using Verse;

namespace Foundation
{
    public class ThoughtWorker_Precept_AnomalyMap : ThoughtWorker_Precept
    {
        protected override ThoughtState ShouldHaveThought(Pawn p)
        {
            if (p.Faction == null || !p.IsColonist)
            {
                return false;
            }
            IReadOnlyList<Pawn> spawnedAnomaly = p.MapHeld.mapPawns.AllPawnsSpawned;
            for (int i = 0; i < spawnedAnomaly.Count; i++)
            {
                Pawn pawn = spawnedAnomaly[i];
                if (pawn.IsMutant || pawn.IsEntity)
                    return ThoughtState.ActiveDefault;
            }
            if (p.MapHeld.IsPlayerHome)
            {
                IReadOnlyList<Pawn> containedAnomaly = p.MapHeld.mapPawns.AllPawnsUnspawned;
                for (int i = 0; i < containedAnomaly.Count; i++)
                {
                    Pawn pawn = containedAnomaly[i];
                    if (pawn.IsOnHoldingPlatform)
                        return ThoughtState.ActiveDefault;
                }
            }
            return ThoughtState.Inactive;

        }
    }
}