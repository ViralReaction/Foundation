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
    public class ThoughtWorker_Precept_Social_Anomaly : ThoughtWorker_Precept_Social
    {
        protected override ThoughtState ShouldHaveThought(Pawn p, Pawn otherPawn)
        {
            if (otherPawn.IsMutant || otherPawn.IsGhoul)
            {
                return true;
            }
            return false;
        }
        //protected override ThoughtState CurrentSocialStateInternal(Pawn p, Pawn otherPawn)
        //{
        //    Ideo ideo = p.Ideo;
        //    if (ideo == null)
        //    {
        //        Log.Message("here?" + " " + otherPawn);
        //        return ThoughtState.Inactive;
        //    }
        //    if (!ideo.cachedPossibleSituationalThoughts.Contains(this.def))
        //    {
        //        Log.Message("heh?" + " " + otherPawn);
        //        return ThoughtState.Inactive;
        //    }
        //    if (this.def.gender != Gender.None && otherPawn.gender != this.def.gender)
        //    {
        //        Log.Message("???" + " " + otherPawn);
        //        return ThoughtState.Inactive;
        //    }
        //    Log.Message("here?" + " " + otherPawn);
        //    return this.ShouldHaveThought(p, otherPawn);
        //}
    }
}