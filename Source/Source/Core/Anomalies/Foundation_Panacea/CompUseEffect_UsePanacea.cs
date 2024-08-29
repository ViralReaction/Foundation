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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace Foundation
{
    internal class CompUseEffect_UsePanacea : CompUseEffect
    {
        public override void DoEffect(Pawn pawn)
        {

            for (int index = pawn.health.hediffSet.hediffs.Count - 1; index >= 0; --index)
            {
                Hediff hediff = pawn.health.hediffSet.hediffs[index];
                if (hediff.def.IsAddiction || hediff.def.makesSickThought || hediff.def == HediffDefOf.FoodPoisoning)
                {
                    pawn.health.RemoveHediff(hediff);
                }
            }
        }
    }
}
