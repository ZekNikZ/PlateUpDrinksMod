using Kitchen;

namespace KitchenDrinksMod.Util
{
    public class DummyItemGroupView : ItemGroupView { 
        public void Awake()
        {
            ComponentGroups = new();
        }
    }
}
