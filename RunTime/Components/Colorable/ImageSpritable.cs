using UnityEngine;
using UnityEngine.UI;

namespace DGames.Presets.Components
{
    public class ImageSpritable : ISpritable
    {
        private readonly Image _image;

        public Sprite Sprite
        {
            get => _image.sprite;
            set => _image.sprite = value;
        }

        public ImageSpritable(Image image)
        {
            _image = image;
        }
    }
}