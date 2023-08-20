using DGames.Essentials;
using DGames.Presets.Components;
using UnityEngine;

namespace DGames.Presets
{
    [ExecuteInEditMode]
    public class IntPreset : Preset<int,ITextable>
    {
        [SerializeField] private string _format = "{0:F0}";
        
        protected override void OnSetValue(int value)
        {
            Target.Text = string.Format(_format, value);
        }

        protected override ITextable GetTarget(Component component) => TextableComponentFactory.Create(component);
    }
}