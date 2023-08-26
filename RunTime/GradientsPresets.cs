using DGames.Essentials.Attributes;
using UnityEditor;
using UnityEngine;

namespace DGames.Presets
{
    [DashboardResourceItem(tabPath:"Presets/Gradient",subFolderPath:DEFAULT_FOLDER_PATH,displayName:"Root - Key Array")]
    [CreateAssetMenu]
    public class GradientsPresets : DirectPresets<Gradient[]>
    {
        

#if UNITY_EDITOR
        [MenuItem("MyGames/Presets/Gradients")]
        public static void Open()
        {
            Editor.ScriptableEditorUtils.OpenOrCreateDefault<GradientsPresets>(childrenPath: DEFAULT_FOLDER_PATH);
        }
#endif
    }
}