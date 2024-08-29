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
using Verse.AI;
using Verse;

namespace Foundation
{
    public class MentalState_Voices : MentalState
    {
        public IntVec3 voicesHeardFrom;

        public override void MentalStateTick()
        {
            base.MentalStateTick();
            if (!(this.pawn.Position == this.voicesHeardFrom))
                return;
            this.RecoverFromState();
        }

        public override RandomSocialMode SocialModeMax() => RandomSocialMode.Quiet;

        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Values.Look<IntVec3>(ref this.voicesHeardFrom, "voicesHeardFrom");
        }
    }
}