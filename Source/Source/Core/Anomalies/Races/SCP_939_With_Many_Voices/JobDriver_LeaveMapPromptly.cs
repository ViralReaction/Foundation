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
using System;
using System.Collections.Generic;
using Verse.AI;

namespace Foundation
{
    public class JobDriver_LeaveMapPromptly : JobDriver
    {
        public override bool TryMakePreToilReservations(bool errorOnFailed) => true;

        protected override IEnumerable<Toil> MakeNewToils()
        {
            yield return Toils_Goto.GotoCell(TargetIndex.A, PathEndMode.OnCell);
            yield return new Toil()
            {
                initAction = (Action)(() => ((GameCondition_ManyVoices)this.pawn.Map.GameConditionManager.ActiveConditions.Find((Predicate<GameCondition>)(x => x is GameCondition_ManyVoices))).AddToRegistry(this.pawn))
            };
        }
    }
}