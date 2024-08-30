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
    [DefOf]
    public static class FoundationDefOf
    {
        public static ThingDef Foundation_Refuted_Egg;
        public static ThingDef Foundation_BloodOpal;
        public static ThingDef Filth_OldMan;

        public static DamageDef Foundation_OldMan_Scratch;

        public static EffecterDef HumeShield_Break;

        public static FleckDef ExplosionHumeFlash;

        public static HediffDef Foundation_Plastic_Poison; // Plastic Organ Poisoning
        public static HediffDef Foundation_ManyVoices_Breath_Hediff; // With Many Voices Amneisa Breath
        public static HediffDef Foundation_Deer_Transmute_Hediff; // The Deer Transmute Aura
        public static HediffDef GutWorms; // Pope Soap Cleanining

        [MayRequireIdeology]
        public static HistoryEventDef Foundation_Died;

        public static JobDef LeaveMapDaylight;

        public static MentalStateDef Foundation_FollowTheVoices;

        [MayRequireIdeology]
        public static PreceptDef Foundation_Tools;

        public static ThoughtDef Foundation_GenderStone_BadThought;
        public static ThoughtDef Foundation_Infected_ManyVoices_Breath;

        public static FactionDef Foundation_Foundation;
        public static PawnKindDef Foundation_D_Class;

        static FoundationDefOf() => DefOfHelper.EnsureInitializedInCtor(typeof(FoundationDefOf));
    }
}