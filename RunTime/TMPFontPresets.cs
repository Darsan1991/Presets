#if TMP
using DGames.Essentials.Attributes;
using TMPro;

using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace DGames.Presets
{
    [DashboardResourceItem(tabPath:"Presets/TMPFonts",subFolderPath:DEFAULT_FOLDER_PATH,displayName:"Root - Key Item")]
    [CreateAssetMenu]
    public class TMPFontPresets : DirectPresets<TMP_FontAsset>
    {

#if UNITY_EDITOR
        [MenuItem("MyGames/Presets/TMPFontPresets")]
        public static void Open()
        {
            Editor.ScriptableEditorUtils.OpenOrCreateDefault<TMPFontPresets>(childrenPath:DEFAULT_FOLDER_PATH);
        }
#endif
    }
}
#endif