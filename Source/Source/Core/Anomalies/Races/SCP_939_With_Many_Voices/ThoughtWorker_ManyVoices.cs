using RimWorld;
using Verse;

namespace Foundation
{
    internal class ThoughtWorker_ManyVoices : ThoughtWorker
    {
        protected override ThoughtState CurrentStateInternal(Pawn p)
        {
            Hediff firstHediffOfDef = p.health.hediffSet.GetFirstHediffOfDef(FoundationDefOf.Foundation_ManyVoices_Breath_Hediff);
            if (firstHediffOfDef == null || firstHediffOfDef.FullyImmune())
                return (ThoughtState)false;
            if ((double)firstHediffOfDef.Severity >= 0.35)
                p.mindState.mentalStateHandler.TryStartMentalState(MentalStateDefOf.Wander_Psychotic, (string)("MBReason".Translate() + "SCP-939"));
            return (ThoughtState)true;
        }
    }
}