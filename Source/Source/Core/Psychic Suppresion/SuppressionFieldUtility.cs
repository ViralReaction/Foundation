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
