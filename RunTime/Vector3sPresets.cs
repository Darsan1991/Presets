using DGames.Essentials.Attributes;
using UnityEditor;
using UnityEngine;

namespace DGames.Presets
{
    [DashboardResourceItem(tabPath:"Presets/Vector/Vector3",subFolderPath:DEFAULT_FOLDER_PATH,displayName:"Root - Key Array")]
    
    [CreateAssetMenu]
    // ReSharper disable once InconsistentNaming
    public class Vector3sPresets : DirectPresets<Vector3[]>
    {
#if UNITY_EDITOR
        [MenuItem("MyGames/Presets/Vector3s")]
        public static void Open()
        {
            Editor.ScriptableEditorUtils.OpenOrCreateDefault<Vector3sPresets>(childrenPath: DEFAULT_FOLDER_PATH);
        }
#endif
    }
}