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
using JetBrains.Annotations;
using System.Collections.Generic;
using Verse;

namespace Foundation.SRA
{
    public class SuppressionFieldAccessUtility
    {

        private static readonly Dictionary<int, SuppressionFieldManager> CachedManagers =
            new Dictionary<int, SuppressionFieldManager>();

        [CanBeNull]
        public static SuppressionFieldManager GetSuppressionFieldManager(Map map)
        {
            if (map == null) return null;

            if (CachedManagers.TryGetValue(map.Index, out var existing)) return existing;

            var manager = map.GetComponent<SuppressionFieldManager>();
            CachedManagers.Add(map.Index, manager);
            return manager;
        }

    }
}
