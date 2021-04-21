using System.Collections.Generic;
using System.Linq;

namespace EventsExpress.Core.Extensions
{
    public static class CollectionExtensions
    {
        /// <summary>Indicates whether the specified collection is null or has a length of zero.</summary>
        /// <param name="collection">The collection to check.</param>
        /// <returns>True if the collection parameter is null or has a length of zero.</returns>
        public static bool CollectionIsNullOrEmpty<T>(this IEnumerable<T> collection) =>
            collection == null || !collection.Any();
    }
}
