using UnityEngine;
using UnityEngine.UI;

namespace DGames.Presets.Components
{
    public class ImageColorable : IColorable
    {
        private readonly Image _image;

        public Color Color
        {
            get => _image.color;
            set => _image.color = value;
        }

        public ImageColorable(Image image)
        {
            _image = image;
        }
    }
}