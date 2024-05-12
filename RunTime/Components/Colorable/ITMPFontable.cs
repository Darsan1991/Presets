#if TMP
using TMPro;
using UnityEngine;

namespace DGames.Presets.Components
{
    public interface ITMPFontable
    {
        TMP_FontAsset Font { get; set; }    
    }
}
#endif