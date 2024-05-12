using UnityEngine;

namespace DGames.Presets.Components
{
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
}