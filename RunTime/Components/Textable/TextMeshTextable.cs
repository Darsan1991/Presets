using DGames.Essentials;
using UnityEngine;

namespace DGames.Presets.Components
{
    public class TextMeshTextable : ITextable
    {
        private readonly TextMesh _text;

        public string Text
        {
            get => _text.text;
            set => _text.text = value;
        }

        public TextMeshTextable(TextMesh text)
        {
            _text = text;
        }
    }
}