using System;
using DGames.Essentials;
using DGames.Presets.Components;
using UnityEngine;
using Object = UnityEngine.Object;

namespace DGames.Presets
{
    
    [ExecuteInEditMode]
    public class TextPreset : Preset<string,ITextable>
    {
        [SerializeField] private string _format = "{0}";

       



        protected override void OnSetValue(string value)
        {
            Target.Text = string.Format(_format, value);
        }

        protected override ITextable GetTarget(Component component)
        {
            return TextableComponentFactory.Create(component);
        }
    }
}


