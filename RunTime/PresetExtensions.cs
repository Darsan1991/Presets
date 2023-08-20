using UnityEngine;

namespace DGames.Presets
{
    public static class PresetExtensions
    {
        public static void DisablePresetIfExist<T>(this Component component)
        {
            component.SetPresetStateIfExist<T>(false);
        }

        public static void EnablePresetIfExist<T>(this Component component)
        {
            component.SetPresetStateIfExist<T>(true);
        }

        public static void SetPresetStateIfExist<T>(this Component component, bool state)
        {
            var colorPreset = component.GetComponent<Preset<T>>();
            if (!colorPreset) return;
            colorPreset.Active = state;
        }
    }
}