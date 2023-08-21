using DGames.Essentials.Attributes;
using UnityEditor;
using UnityEngine;

namespace DGames.Presets
{
    [DashboardResourceItem(tabPath:"Presets/Vector/Vector3",subFolderPath:DEFAULT_FOLDER_PATH,displayName:"Root - Key Item")]

    [CreateAssetMenu]
    public class Vector3Presets : DirectPresets<Vector3>
    {
#if UNITY_EDITOR
        [MenuItem("MyGames/Presets/Vector3")]
        public static void Open()
        {
            Editor.ScriptableEditorUtils.OpenOrCreateDefault<Vector3Presets>(childrenPath: DEFAULT_FOLDER_PATH);
        }
#endif
    }
}