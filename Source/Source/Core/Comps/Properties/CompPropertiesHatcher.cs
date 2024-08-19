using Verse;

namespace Foundation
{
    internal class CompProperties_Hatcher : CompProperties
    {
        public float hatcherDaystoHatch = 1f;
        public PawnKindDef hatcherPawn;
        public ThingDef ruinedEgg;

        public CompProperties_Hatcher() => this.compClass = typeof(CompHatcher);
    }
}