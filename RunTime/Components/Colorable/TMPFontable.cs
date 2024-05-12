#if TMP
using TMPro;

namespace DGames.Presets.Components
{
    public class TMPFontable : ITMPFontable
    {
        private readonly TMP_Text _text;
        
        public TMP_FontAsset Font
        {
            get => _text.font;
            set => _text.font = value;
        }

        public TMPFontable(TMP_Text text)
        {
            _text = text;
        }
    }
}
#endif