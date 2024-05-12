using DGames.Essentials;
using UnityEngine;

namespace DGames.Presets.Components
{
    public class CameraColorable : IColorable
    {
        private readonly Camera _camera;

        public Color Color
        {
            get => _camera.backgroundColor;
            set => _camera.backgroundColor = value;
        }

        public CameraColorable(Camera camera)
        {
            _camera = camera;
        }
    }
}