using RimWorld;
using System.Collections.Generic;
using Verse;

namespace Foundation
{
    public class ThoughtWorker_Precept_AnomalyPresent : ThoughtWorker_Precept
    {
        protected override ThoughtState ShouldHaveThought(Pawn p)
        {
            if (p.Faction == null || !p.IsColonist)
            {
                return false;
            }
            List<Pawn> aliveOfPlayerFaction = PawnsFinder.AllMapsCaravansAndTravelingTransportPods_Alive_OfPlayerFaction;
            for (int index = 0; index < aliveOfPlayerFaction.Count; index++)
            {
                Pawn pawn = aliveOfPlayerFaction[index];
                if (pawn.IsColonyMutant)
                    return ThoughtState.ActiveDefault;
            }
            return ThoughtState.Inactive;
        }
    }
}