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