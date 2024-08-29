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
    public class Comp_AnomalyHeal : ThingComp
    {
        public int tickCounter = 0;
        public CompProperties_AnomalyHeal Props => (CompProperties_AnomalyHeal)this.props;
        public override void CompTick()
        {
            base.CompTick();
            tickCounter++;
            if (tickCounter > Props.tickInterval)
            {
                Pawn pawn = this.parent as Pawn;
                if (pawn.health != null)
                {
                    List<Hediff_Injury> injuryList = new List<Hediff_Injury>();
                    List<Hediff> injuryCheck = pawn.health.hediffSet.hediffs;
                    for (int index = 0; index < injuryCheck.Count; index++)
                    {
                        Hediff_Injury injury = injuryCheck[index] as Hediff_Injury;
                        if (injury != null)
                        {
                            injuryList.Add(injury);
                        }
                    }
                    if (injuryList.Count > 0)
                    {
                        Hediff_Injury hurt = injuryList.RandomElement();
                        hurt.Severity = hurt.Severity - Props.healAmount;
                    }
                }
                tickCounter = 0;
            }
        }
    }
}
