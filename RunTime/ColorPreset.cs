using System;
using DGames.Essentials;
using DGames.Presets.Components;
using UnityEngine;

namespace DGames.Presets
{
    [ExecuteInEditMode]

    public class ColorPreset : Preset<Color, IColorable>
    {
        [SerializeField] private bool _ignoreAlpha;


        protected override IColorable GetTarget(Component component)
        {
            return ColorableComponentFactory.Create(component);
        }

        protected override void OnSetValue(Color value)
        {
            Target.Color = _ignoreAlpha ? new Color(value.r,value.g,value.b,Target.Color.a) : value;
        }

        private void Update()
        {
            // if (!Equals(Value, Target.Color))
            //     info.def = Target.Color;
        }

        protected override void OnValidate()
        {
            // if (string.IsNullOrEmpty(info.key)) info.def = Target.Color;
            base.OnValidate();
        }

        protected override void Reset()
        {
            base.Reset();
            info.def = Target.Color;
        }
    }
}
