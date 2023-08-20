using DGames.Essentials;
using UnityEngine.UI;

namespace DGames.Presets.Components
{
    public class TextLegacyTextable : ITextable
    {
        private readonly Text _text;

        public string Text
        {
            get => _text.text;
            set => _text.text = value;
        }

        public TextLegacyTextable(Text text)
        {
            _text = text;
        }
    }
}