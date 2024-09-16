using DGames.Presets.Components;
using UnityEngine;

namespace DGames.Presets
{
    [ExecuteInEditMode]

    public class SpritePreset : Preset<Sprite, ISpritable>
    {
        protected override ISpritable GetTarget(Component component)
        {
            return SpritableComponentFactory.Create(component);
        }

        protected override void OnSetValue(Sprite value)
        {
            Target.Sprite = value;
        }
        
        protected override void Reset()
        {
            base.Reset();
            info.def = Target.Sprite;
        }

    }
}