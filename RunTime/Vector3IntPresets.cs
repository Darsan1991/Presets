using DGames.Essentials.Attributes;
using UnityEditor;
using UnityEngine;

namespace DGames.Presets
{
    [DashboardResourceItem(tabPath:"Presets/Vector/Vector3Int",subFolderPath:DEFAULT_FOLDER_PATH,displayName:"Root - Key Item")]

    [CreateAssetMenu]
    public class Vector3IntPresets : DirectPresets<Vector3Int>
    {
#if UNITY_EDITOR
        [MenuItem("MyGames/Presets/Vector3Ints")]
        public static void Open()
        {
            Editor.ScriptableEditorUtils.OpenOrCreateDefault<Vector3IntPresets>(childrenPath: DEFAULT_FOLDER_PATH);
        }
#endif
    }
}