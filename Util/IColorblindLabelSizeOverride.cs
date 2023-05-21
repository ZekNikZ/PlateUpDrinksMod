using UnityEngine;

namespace KitchenDrinksMod.Util
{
    internal interface IColorblindLabelSizeOverride
    {
        public Vector2 ColorblindLabelOffsetMinAdjust { get; }
        public Vector2 ColorblindLabelOffsetMaxAdjust { get; }
    }
}
