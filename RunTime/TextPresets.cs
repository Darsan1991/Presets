using DGames.Essentials.Attributes;
using UnityEditor;
using UnityEngine;

namespace DGames.Presets
{
    [DashboardResourceItem(tabPath:"Presets/Texts",subFolderPath:DEFAULT_FOLDER_PATH,displayName:"Root - Key Item")]
    [CreateAssetMenu]
    public class TextPresets : DirectPresets<string>
    {

#if UNITY_EDITOR
        [MenuItem("MyGames/Presets/TextPresets")]
        public static void Open()
        {
            Editor.ScriptableEditorUtils.OpenOrCreateDefault<TextPresets>(childrenPath:DEFAULT_FOLDER_PATH);
        }
#endif
    }
}