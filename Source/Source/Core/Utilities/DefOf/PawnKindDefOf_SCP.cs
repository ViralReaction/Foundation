using RimWorld;
using Verse;

namespace Foundation
{
    [DefOf]
    public static class PawnKindDefOf_SCP
    {
        public static PawnKindDef Foundation_ShyGuy;
        public static PawnKindDef Foundation_ImpossibleReptile;
        public static PawnKindDef Foundation_ManyVoices_Normal;
        public static PawnKindDef Foundation_ManyVoices_Incident;
        public static PawnKindDef Foundation_Deer;
        public static PawnKindDef Foundation_Deer_Transmuted;
        public static PawnKindDef Foundation_Refuted;

        static PawnKindDefOf_SCP() => DefOfHelper.EnsureInitializedInCtor(typeof(PawnKindDefOf_SCP));
    }
}