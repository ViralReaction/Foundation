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

using System.Collections.Generic;
using Verse;

namespace Foundation.Comps
{
    public class CompProperties_GiveHediffSeverity : CompProperties
    {
        public HediffDef hediff;
        public int range = 10;
        public float severityTick = 1.0f;
        public int tickInterval = 300;
        public List<string> defNamesImmune = null;
        public bool psychicCheck = false;
        public bool scrantonCheck = false;

        public CompProperties_GiveHediffSeverity() => this.compClass = typeof(CompGiveHediff);
    }
}