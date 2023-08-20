using DGames.Essentials.Attributes;
using UnityEditor;
using UnityEngine;

namespace DGames.Presets
{
    [DashboardResourceItem(tabPath:"Presets/Color",subFolderPath:DEFAULT_FOLDER_PATH,displayName:"Root - Key Array")]
    [CreateAssetMenu]
    public class ColorsPresets : DirectPresets<Color[]>
    {
        

#if UNITY_EDITOR
        [MenuItem("MyGames/Presets/ColorsPresets")]
        public static void Open()
        {
            Editor.ScriptableEditorUtils.OpenOrCreateDefault<ColorsPresets>(childrenPath: DEFAULT_FOLDER_PATH);
        }
#endif
    }
}