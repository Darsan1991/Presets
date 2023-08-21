using DGames.Essentials.Attributes;
using UnityEditor;
using UnityEngine;

namespace DGames.Presets
{
    [DashboardResourceItem(tabPath:"Presets/Vector/Vector2Int",subFolderPath:DEFAULT_FOLDER_PATH,displayName:"Root - Key Item")]

    [CreateAssetMenu]
    public class Vector2IntPresets : DirectPresets<Vector2Int>
    {
#if UNITY_EDITOR
        [MenuItem("MyGames/Presets/Vector2Ints")]
        public static void Open()
        {
            Editor.ScriptableEditorUtils.OpenOrCreateDefault<Vector2IntPresets>(childrenPath: DEFAULT_FOLDER_PATH);
        }
#endif
    }
}