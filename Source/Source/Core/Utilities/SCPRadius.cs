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

using System.Collections.Generic;
using Verse;

namespace Foundation
{
    public class SCPRadius : Def
    {
        public static List<Pawn> GetPawnsAround(IntVec3 center, int radius, Map map)
        {
            List<Pawn> pawnList = new List<Pawn>();

            int numCells = GenRadial.NumCellsInRadius(radius);
            var thingGrid = map.thingGrid;

            for (int i = 0; i < numCells; i++)
            {
                var c = center + GenRadial.RadialPattern[i];
                if (c.InBounds(map) == false) continue;

                var things = thingGrid.ThingsListAtFast(c);

                for (int j = 0; j < things.Count; j++)
                {
                    if (things[j].def.category == ThingCategory.Pawn)
                    {
                        pawnList.Add(things[j] as Pawn);
                    }
                }
            }
            return pawnList;
        }
    }
}
