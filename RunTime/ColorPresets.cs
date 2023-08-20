using DGames.Essentials.Attributes;
using UnityEditor;
using UnityEngine;

namespace DGames.Presets
{
    [DashboardTabMessage("You can set color presets here that used game wide.")]
    [DashboardResourceItem(tabPath:"Presets/Color",subFolderPath:DEFAULT_FOLDER_PATH,displayName:"Root - Key Item")]
    [CreateAssetMenu]
    public class ColorPresets : DirectPresets<Color>
    {
        

#if UNITY_EDITOR
        [MenuItem("MyGames/Presets/ColorPresets")]
        public static void Open()
        {
            Editor.ScriptableEditorUtils.OpenOrCreateDefault<ColorPresets>(childrenPath: DEFAULT_FOLDER_PATH);
        }
#endif
    }
}