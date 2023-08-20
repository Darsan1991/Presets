using DGames.Essentials;
using UnityEngine;

namespace DGames.Presets.Components
{
    public class SpriteColorable : IColorable
    {
        private readonly SpriteRenderer _renderer;

        public Color Color
        {
            get => _renderer.color;
            set => _renderer.color = value;
        }

        public SpriteColorable(SpriteRenderer renderer)
        {
            _renderer = renderer;
        }
    }
}