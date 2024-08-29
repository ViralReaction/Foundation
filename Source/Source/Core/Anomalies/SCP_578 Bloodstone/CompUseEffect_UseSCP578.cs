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
    public class IngestionOutcomeDoer_IngestBloodOpal : IngestionOutcomeDoer
    {
        protected override void DoIngestionOutcomeSpecial(Pawn pawn, Thing ingested, int ingestedCount)
        {
            IntVec3 pos = pawn.Position;
            ThingDef filth = pawn.RaceProps.BloodDef;
            Map map = pawn.MapHeld;
            Thing thing = GenSpawn.Spawn(FoundationDefOf.Foundation_BloodOpal, pos, map);
            thing.stackCount = 100;
            GenPlace.TryPlaceThing(thing, pos, map, ThingPlaceMode.Direct);
            pawn.inventory.DropAllNearPawn(pos, true);
            pawn.equipment.DropAllEquipment(pawn.Position, true);
            pawn.apparel.DropAll(pos, true);
            for (int i = 0; i < 20; i++) 
            {
                IntVec3 c;
                if (!CellFinder.TryFindRandomReachableNearbyCell(pos, map, 3 , TraverseParms.For(TraverseMode.NoPassClosedDoors, Danger.Deadly, false, false, false), (IntVec3 x) => x.Standable(pawn.MapHeld), (Region x) => true, out c, 999999))
                {
                    Log.Message("Failing");
                    return;
                }
                Log.Message("Are we getting here");
                FilthMaker.TryMakeFilth(c, map, filth, 1, FilthSourceFlags.None, true);
            }
            
            pawn.Destroy(DestroyMode.KillFinalize);

        }
       
    }
}