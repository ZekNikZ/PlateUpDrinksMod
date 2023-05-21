namespace KitchenDrinksMod.Util
{
    internal interface IColorblindLabelVisibilityOverride
    {
        public bool ColorblindLabelVisibleWhenColorblindEnabled { get; }
        public bool ColorblindLabelVisibleWhenColorblindDisabled { get; }
    }
}
