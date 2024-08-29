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
    public class Utilities
    {
        public static float PlayerAnomalyBodySizePerCapita(Pawn colonist)
        {
            float anomalyNum = 0f;
            int colonistNum = 0;
            List<Pawn> allMapsCaravansAndTravelingTransportPods_Alive_OfPlayerFaction = PawnsFinder.AllMapsCaravansAndTravelingTransportPods_Alive_OfPlayerFaction;
            for (int i = 0; i < allMapsCaravansAndTravelingTransportPods_Alive_OfPlayerFaction.Count; i++)
            {
                Pawn pawn = allMapsCaravansAndTravelingTransportPods_Alive_OfPlayerFaction[i];
                if (pawn.IsFreeColonist && !pawn.IsQuestLodger() && !pawn.IsGhoul)
                {
                    colonistNum++;
                }
                if (pawn.IsGhoul || IsMutantAnimal(pawn))
                {
                    anomalyNum += pawn.BodySize;
                }
            }
            IReadOnlyList<Pawn> anomalyList = colonist.MapHeld.mapPawns.AllPawnsUnspawned;
            for (int i = 0; i < anomalyList.Count; i++)
            {
                Pawn pawn = anomalyList[i];
                if (pawn.IsOnHoldingPlatform && pawn.IsEntity)
                {
                    anomalyNum += pawn.BodySize;
                }
            }
            if (colonistNum <= 0)
            {
                return 0f;
            }
            return anomalyNum / (float)colonistNum;
        }

        public static bool IsMutantAnimal(Pawn pawn)
        {
            return pawn.RaceProps.Animal && pawn.IsMutant;
        }

    }
}
