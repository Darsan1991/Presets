using DGames.Essentials;
using UnityEngine;
using UnityEngine.UI;

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

    public class TextLegacyFontable : IFontable
    {
        private readonly Text _text;
        
        public Font Font
        {
            get => _text.font;
            set => _text.font = value;
        }

        public TextLegacyFontable(Text text)
        {
            _text = text;
        }
    }
    
    public class TextMeshFontable : IFontable
    {
        private readonly TextMesh _text;
        
        public Font Font
        {
            get => _text.font;
            set => _text.font = value;
        }

        public TextMeshFontable(TextMesh text)
        {
            _text = text;
        }
    }

    public class TextMeshProFontable : IFontable
    {
        public Font Font { get; set; }

        public TextMeshProFontable()
        {
            
        }
    }
}