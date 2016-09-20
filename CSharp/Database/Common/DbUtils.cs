using System;
using System.Collections.Generic;
using System.Linq;

namespace Kevins.Examples.Database.Common
{
    class DbUtils
    {
        public const int SQL_WHERE_IN_MAX = 1000;

        public static void InBatches<T>(IEnumerable<T> ids, Action<IEnumerable<T>> action)
        {
            InBatches(ids, SQL_WHERE_IN_MAX, action);
        }

        public static void InBatches<T>(IEnumerable<T> ids, int batchSize, Action<IEnumerable<T>> action)
        {
            if (ids != null)
            {
                var idList = ids.ToList<T>();
                for (var i = 0; i < idList.Count(); i += batchSize)
                {
                    var count = Math.Min(batchSize, idList.Count() - i);
                    var batchIds = idList.GetRange(i, count);
                    action.Invoke(batchIds);
                }
            }
        }
    }
}
