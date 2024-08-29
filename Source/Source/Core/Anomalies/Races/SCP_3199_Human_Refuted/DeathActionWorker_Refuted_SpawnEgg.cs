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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;
using Foundation;
using Verse.AI.Group;

namespace Foundation
{
    internal class DeathActionWorker_Refuted_SpawnEgg : DeathActionWorker
    {
        public override void PawnDied(Corpse corpse, Lord prevLord)
        {
            if (corpse.Map == null)
                return;
            GenPlace.TryPlaceThing(ThingMaker.MakeThing(FoundationDefOf.Foundation_Refuted_Egg), corpse.Position, corpse.Map, ThingPlaceMode.Near);
        }
    }
}