using DGames.Essentials.Attributes;
using UnityEditor;
using UnityEngine;

namespace DGames.Presets
{
    [DashboardResourceItem(tabPath:"Presets/Gradient",subFolderPath:DEFAULT_FOLDER_PATH,displayName:"Root - Key Item")]
    [CreateAssetMenu]
    public class GradientPresets : DirectPresets<Gradient>
    {
        

#if UNITY_EDITOR
        [MenuItem("MyGames/Presets/Gradient")]
        public static void Open()
        {
            Editor.ScriptableEditorUtils.OpenOrCreateDefault<GradientPresets>(childrenPath: DEFAULT_FOLDER_PATH);
        }
#endif
    }
}