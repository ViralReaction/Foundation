using RimWorld;
using Verse;

namespace Foundation
{
    public class ThoughtWorker_Precept_Anomaly : ThoughtWorker_Precept
    {
        protected override ThoughtState ShouldHaveThought(Pawn p) => (ThoughtState)p.IsMutant;
    }
}