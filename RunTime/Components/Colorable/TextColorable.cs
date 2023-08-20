using DGames.Essentials;
using UnityEngine;
using UnityEngine.UI;

namespace DGames.Presets.Components
{
    public class TextColorable : IColorable
    {
        private readonly Text _text;

        public Color Color
        {
            get => _text.color;
            set => _text.color = value;
        }

        public TextColorable(Text text)
        {
            _text = text;
        }
    }
}