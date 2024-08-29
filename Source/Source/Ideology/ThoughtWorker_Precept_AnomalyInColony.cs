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
using System.Collections.Generic;
using Verse;

namespace Foundation
{
    public class ThoughtWorker_Precept_AnomalyInColony : ThoughtWorker_Precept, IPreceptCompDescriptionArgs
    {
        private const int MinimumTicksBeforeFewAnimals = 900000;
        private string Density;
        protected override ThoughtState ShouldHaveThought(Pawn p)
        {
            if (ThoughtUtility.ThoughtNullified(p, this.def) || p.IsSlave)
            {
                return false;
            }
            float density = Utilities.PlayerAnomalyBodySizePerCapita(p);
            Density = density.ToString();
            int stageIndex = 0;
            for (int index = 0; index < density; index++)
            {
                ++stageIndex;
            }
            if (stageIndex < 2 && GenTicks.TicksAbs < MinimumTicksBeforeFewAnimals)
                return ThoughtState.Inactive;
            if (stageIndex > 6)
                stageIndex = 6;
            return ThoughtState.ActiveAtStage(stageIndex);
        }
        public override string PostProcessDescription(Pawn p, string description) => (string)(base.PostProcessDescription(p, description) + "\n\n" + "CurrentTotalAnomalyPerColonist".Translate() + ": " + Density + "\n" + "MinAnomalyPerColonist".Translate((NamedArgument)1f.ToString("F1")));
        public IEnumerable<NamedArgument> GetDescriptionArgs()
        {
            yield return ((string)("(" + "AnomalyPerColonist".Translate() + ": ") + (object)1f + ")").Named("STAGE1");
            yield return ((string)("(" + "AnomalyPerColonist".Translate() + ": ") + (object)2f + ")").Named("STAGE2");
            yield return ((string)("(" + "AnomalyPerColonist".Translate() + ": ") + (object)6f + ")").Named("STAGE4");
            yield return ((string)("(" + "AnomalyPerColonist".Translate() + ": ") + (object)8f + ")").Named("STAGE5");
        }
    }
}