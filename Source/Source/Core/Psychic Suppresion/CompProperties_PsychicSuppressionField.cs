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
using Verse;

namespace Foundation.SRA
{
    public class CompProperties_PsychicSuppressionField : CompProperties
    {

        public float MinEffect = -2f;
        public float MaxEffect = 0f;
        public float EffectStep = 0.1f;
        public float EffectChangeSpeedPerSecond = 0.01f;

        public float MinRadius = 1f;
        public float MaxRadius = 5f;
        public float RadiusChangeSpeedPerSecond = 0.02f;

        public float BasePowerConsumption = 50f;
        public float PowerPerCellEffect = 2f;

        public CompProperties_PsychicSuppressionField()
        {
            compClass = typeof(CompPsychicSuppressionField);
        }

    }
}