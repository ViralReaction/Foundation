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
using RimWorld;
using Verse;

namespace Foundation.SRA
{
    [StaticConstructorOnStartup]
    public static class ScrantonCachingUtility
    {

        public static int _totalStatsInGame;
        private static bool[] _cachedAffectedStats;
        public static readonly List<ThingDef> CachedCryptosleepDefs = new List<ThingDef>();

        static ScrantonCachingUtility()
        {

            // Initialize cache array
            _totalStatsInGame = DefDatabase<StatDef>.DefCount;
            _cachedAffectedStats = new bool[_totalStatsInGame];

            // Cache psychic sensitivity for the suppression field just in case
            _cachedAffectedStats[StatDefOf.PsychicSensitivity.index] = true;

        }

        public static bool EverAffectsStat(StatDef stat)
        {
            return stat.index < _totalStatsInGame && _cachedAffectedStats[stat.index];
        }
    }
}