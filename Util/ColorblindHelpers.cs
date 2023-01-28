﻿using KitchenData;
using KitchenLib.References;
using UnityEngine;

namespace KitchenDrinksMod.Util
{
    public static class ColorblindHelpers
    {
        private static GameObject _template;
        private static GameObject Template
        {
            get
            {
                if (_template == null)
                {
                    _template = GameData.Main.Get<Appliance>(ApplianceReferences.SourceIceCream).Prefab.GetChildFromPath("Colour Blind");
                }

                return _template;
            }
        }

        public static void AddApplianceColorblindLabel(this GameObject holder, string title)
        {
            var colorblindLabel = Object.Instantiate(Template);
            colorblindLabel.name = "Colour Blind";
            colorblindLabel.transform.SetParent(holder.transform);
            colorblindLabel.transform.localPosition = Vector3.zero;
            colorblindLabel.GetChild("Title").GetComponent<TMPro.TextMeshPro>().text = title;
        }
    }
}
