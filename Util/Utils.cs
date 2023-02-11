using Kitchen;
using KitchenLib.Utils;

namespace KitchenDrinksMod.Util
{
    public static class Utils
    {
        public static CItemProvider GetUnlimitedCItemProviderWithDirectInsert(int itemId)
        {
            var provider = KitchenPropertiesUtils.GetUnlimitedCItemProvider(itemId);

            provider.DirectInsertionOnly = true;

            return provider;
        }
    }
}
