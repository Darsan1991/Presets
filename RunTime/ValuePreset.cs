using System;
using DGames.Essentials;
using DGames.Presets.Components;
using UnityEngine;
using UnityEngine.UI;

namespace DGames.Presets
{
    [ExecuteInEditMode]
    public class ValuePreset : Preset<float,ITextable>
    {
        [SerializeField] private string _format = "{0:F0}";
        
        protected override void OnSetValue(float value)
        {
            Target.Text = string.Format(_format, value);
        }

        protected override ITextable GetTarget(Component component) => TextableComponentFactory.Create(component);

    }
}