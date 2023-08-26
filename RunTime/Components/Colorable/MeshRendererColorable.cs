using DGames.Essentials;
using UnityEngine;

namespace DGames.Presets.Components
{
    public class MeshRendererColorable : IColorable
    {
        private readonly MeshRenderer _renderer;

        public Color Color
        {
            get => (Application.isPlaying ?  _renderer.material : _renderer.sharedMaterial).color;
            set
            {
                var material = Application.isPlaying ?  _renderer.material : _renderer.sharedMaterial;
                material.color = value;
            }
        }

        public MeshRendererColorable(MeshRenderer renderer)
        {
            _renderer = renderer;
        }
    }
}