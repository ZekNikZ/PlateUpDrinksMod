using Kitchen;
using KitchenLib.Utils;

namespace KitchenDrinksMod.Utils
{
    public class PropertyUtils
    {
        public static CItemProvider GetUnlimitedCItemProviderWithDirectInsert(int itemId)
        {
            var provider = KitchenPropertiesUtils.GetUnlimitedCItemProvider(itemId);

            provider.DirectInsertionOnly = true;

            return provider;
        }
    }
}
