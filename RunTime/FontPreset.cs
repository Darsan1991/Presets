using DGames.Presets.Components;
using UnityEngine;

namespace DGames.Presets
{
    [ExecuteInEditMode]
    public class FontPreset : Preset<Font,IFontable>
    {

        protected override void OnSetValue(Font value)
        {
            Target.Font = value;
        }

        protected override IFontable GetTarget(Component component)
        {
            return FontableComponentFactory.Create(component);
        }

        protected override void Reset()
        {
            base.Reset();
            info.def = Target.Font;
        }
    }
}