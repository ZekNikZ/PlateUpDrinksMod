using Kitchen;
using Kitchen.Modules;
using KitchenLib;
using System.Collections.Generic;
using UnityEngine;

namespace KitchenDrinksMod.Customs.UI
{
    public class PreferencesMenu<T> : KLMenu<T>
    {
        public PreferencesMenu(Transform container, ModuleList moduleList) : base(container, moduleList)
        {
        }

        public override void Setup(int player_id)
        {
            AddLabel("Smoothie Labels");
            Add(new Option<bool>(
                new List<bool>
                {
                    false, true
                },
                Mod.ColorblindUseTextPref.Get(),
                new List<string>
                {
                    "Icons", "Text"
                }
            )).OnChanged += delegate (object _, bool newVal)
            {
                Mod.ColorblindUseTextPref.Set(newVal);
                Mod.PreferenceManager.Save();
            };
            AddInfo("Whether to use icons or text labels for smoothies.");

            AddButton(Localisation["MENU_BACK_SETTINGS"], delegate
            {
                RequestPreviousMenu();
            });
        }
    }
}
