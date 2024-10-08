﻿#region
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
using System.Linq;
using UnityEngine;
using Verse;

namespace Foundation
{
    public class IncidentWorker_ManyVoices : IncidentWorker_MakeGameCondition
    {
        protected override bool CanFireNowSub(IncidentParms parms)
        {
            if (!base.CanFireNowSub(parms))
                return false;
            Map target = (Map)parms.target;
            return target.IsPlayerHome && !target.GameConditionManager.ActiveConditions.Any<GameCondition>((Predicate<GameCondition>)(x => x is GameCondition_ManyVoices));
        }

        private void ResolveArrivalPoints(IncidentParms parms)
        {
            if ((double)parms.points > 0.0)
                return;
            Log.Error("RaidEnemy is resolving raid points. They should always be set before initiating the incident.");
            parms.points = StorytellerUtility.DefaultThreatPointsNow(parms.target);
        }

        protected override bool TryExecuteWorker(IncidentParms parms)
        {
            this.ResolveArrivalPoints(parms);
            Map target = (Map)parms.target;
            //PawnKindDef scp939PawnkindDef = PawnKindDefOf_SCP.Foundation_939_Incident;
            int num = Mathf.Clamp(GenMath.RoundRandom(parms.points / PawnKindDefOf_SCP.Foundation_ManyVoices_Incident.combatPower), 1, Rand.RangeInclusive(2, 20));
            GameCondition_ManyVoices cond = (GameCondition_ManyVoices)GameConditionMaker.MakeCondition(this.def.gameCondition, Mathf.RoundToInt(this.def.durationDays.RandomInRange * 60000f));
            cond.scp939Count = num;
            parms.target.GameConditionManager.RegisterCondition((GameCondition)cond);
            Find.LetterStack.ReceiveLetter((string)"LetterSCP939Enters".Translate().CapitalizeFirst(), (string)"LetterSCP939EntersText".Translate(), LetterDefOf.ThreatBig, (string)null);
            return true;
        }

        private int HoursTillDawn(Map map)
        {
            int num = GenLocalDate.HourOfDay(map);
            return num <= 6 ? 6 - num : 24 - num + 6;
        }
    }
}