using DGames.Essentials.Attributes;
using UnityEditor;
using UnityEngine;

namespace DGames.Presets
{
    [DashboardResourceItem(tabPath:"Presets/Texts",subFolderPath:DEFAULT_FOLDER_PATH,displayName:"Root - Key Array")]
    [CreateAssetMenu]
    public class TextsPresets : DirectPresets<string[]>
    {

#if UNITY_EDITOR
        [MenuItem("MyGames/Presets/TextsPresets")]
        public static void Open()
        {
            Editor.ScriptableEditorUtils.OpenOrCreateDefault<TextsPresets>(childrenPath:DEFAULT_FOLDER_PATH);
        }
#endif
    }
}