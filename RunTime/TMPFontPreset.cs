#if TMP
using DGames.Presets.Components;
using TMPro;
using UnityEngine;

namespace DGames.Presets
{
    [ExecuteInEditMode]
    public class TMPFontPreset : Preset<TMP_FontAsset,ITMPFontable>
    {

        protected override void OnSetValue(TMP_FontAsset value)
        {
            Target.Font = value;
        }

        protected override ITMPFontable GetTarget(Component component)
        {
            return TMPFontableComponentFactory.Create(component);
        }
    }
}
#endif