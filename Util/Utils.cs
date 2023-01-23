using Kitchen;
using KitchenLib.Utils;
using System.Collections.Generic;
using System.Linq;

namespace KitchenDrinksMod.Util
{
    public static class Utils
    {
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> list)
        {
            return list == null || list.Count() == 0;
        }

        public static CItemProvider GetUnlimitedCItemProviderWithDirectInsert(int itemId)
        {
            var provider = KitchenPropertiesUtils.GetUnlimitedCItemProvider(itemId);

            provider.DirectInsertionOnly = true;

            return provider;
        }
    }
}
