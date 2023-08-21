using DGames.Essentials.Attributes;
using UnityEditor;
using UnityEngine;

namespace DGames.Presets
{
    [DashboardResourceItem(tabPath:"Presets/Vector/Vector2",subFolderPath:DEFAULT_FOLDER_PATH,displayName:"Root - Key Item")]

    [CreateAssetMenu]
    public class Vector2Presets : DirectPresets<Vector2>
    {
#if UNITY_EDITOR
        [MenuItem("MyGames/Presets/Vector2")]
        public static void Open()
        {
            Editor.ScriptableEditorUtils.OpenOrCreateDefault<Vector2Presets>(childrenPath: DEFAULT_FOLDER_PATH);
        }
#endif
    }
}