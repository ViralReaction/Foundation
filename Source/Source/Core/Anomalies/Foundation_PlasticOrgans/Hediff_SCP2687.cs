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
    // Adds plastic poisoning for SCP2687 on adding Hediff with this class.
    public class Hediff_PlasticOrgans : Hediff_High
    {
        public override void PostAdd(DamageInfo? dinfo)
        {
            if (!this.pawn.health.hediffSet.HasHediff(FoundationDefOf.Foundation_Plastic_Poison))
            {
                Hediff hediff = HediffMaker.MakeHediff(FoundationDefOf.Foundation_Plastic_Poison, this.pawn);
                hediff.Severity = 0.05f;
                this.pawn.health.AddHediff(hediff);
            }
            else
                this.pawn.health.hediffSet.GetFirstHediffOfDef(FoundationDefOf.Foundation_Plastic_Poison).Severity += 0.05f;
        }
    }
}