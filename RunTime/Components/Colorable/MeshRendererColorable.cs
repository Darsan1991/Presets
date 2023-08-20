using DGames.Essentials;
using UnityEngine;

namespace DGames.Presets.Components
{
    public class MeshRendererColorable : IColorable
    {
        private readonly MeshRenderer _renderer;

        public Color Color
        {
            get => _renderer.material.color;
            set => _renderer.material.color = value;
        }

        public MeshRendererColorable(MeshRenderer renderer)
        {
            _renderer = renderer;
        }
    }
}