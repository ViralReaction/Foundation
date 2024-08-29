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
    public class IncidentWorker_Refuted : IncidentWorker_AggressiveAnimals
    {
        protected override bool TryExecuteWorker(IncidentParms parms)
        {
            Map target = (Map)parms.target;
            PawnKindDef animalKind = PawnKindDefOf_SCP.Foundation_Refuted;
            if (animalKind == null && !AggressiveAnimalIncidentUtility.TryFindAggressiveAnimalKind(parms.points, target, out animalKind) || AggressiveAnimalIncidentUtility.GetAnimalsCount(animalKind, parms.points) == 0)
                return false;
            IntVec3 result = parms.spawnCenter;
            if (!result.IsValid && !RCellFinder.TryFindRandomPawnEntryCell(out result, target, CellFinder.EdgeRoadChance_Animal))
                return false;
            List<Pawn> animals = AggressiveAnimalIncidentUtility.GenerateAnimals(animalKind, target.Tile, parms.points * 1f, parms.pawnCount);
            Rot4 rot = Rot4.FromAngleFlat((target.Center - result).AngleFlat);
            for (int index = 0; index < animals.Count; ++index)
            {
                Pawn newThing = animals[index];
                IntVec3 loc = CellFinder.RandomClosewalkCellNear(result, target, 10);
                QuestUtility.AddQuestTag((object)GenSpawn.Spawn((Thing)newThing, loc, target, rot), parms.questTag);
                if (!ModsConfig.AnomalyActive || this.def != IncidentDefOf.FrenziedAnimals)
                    newThing.health.AddHediff(HediffDefOf.Scaria);
                newThing.mindState.mentalStateHandler.TryStartMentalState(MentalStateDefOf.ManhunterPermanent);
                newThing.mindState.exitMapAfterTick = Find.TickManager.TicksGame + Rand.Range(60000, 120000);
            }
            if (ModsConfig.AnomalyActive)
            {
                if (this.def == IncidentDefOf.FrenziedAnimals)
                    this.SendStandardLetter("FrenziedAnimalsLabel".Translate(), "FrenziedAnimalsText".Translate((NamedArgument)animalKind.GetLabelPlural()), LetterDefOf.ThreatBig, parms, (LookTargets)(Thing)animals[0]);
                else
                    this.SendStandardLetter("LetterLabelSCP3199Attack".Translate((NamedArgument)animalKind.label.CapitalizeFirst()), "LetterSCP3199Attack".Translate((NamedArgument)animalKind.label), LetterDefOf.ThreatBig, parms, (LookTargets)(Thing)animals[0]);
            }
            else
                this.SendStandardLetter("LetterLabelSCP3199Attack".Translate((NamedArgument)animalKind.label.CapitalizeFirst()), "LetterSCP3199Attack".Translate((NamedArgument)animalKind.label), LetterDefOf.ThreatBig, parms, (LookTargets)(Thing)animals[0]);
            Find.TickManager.slower.SignalForceNormalSpeedShort();
            return true;
        }
    }
}
