using UnityEngine;
using UnityEngine.UI;

namespace DGames.Presets.Components
{
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
}