using DGames.Essentials.Attributes;
using UnityEditor;
using UnityEngine;
using UnityEngine.TextCore.Text;

namespace DGames.Presets
{
    [DashboardResourceItem(tabPath:"Presets/Fonts",subFolderPath:DEFAULT_FOLDER_PATH,displayName:"Root - Key Item")]
    [CreateAssetMenu]
    public class FontPresets : DirectPresets<Font>
    {

#if UNITY_EDITOR
        [MenuItem("MyGames/Presets/FontPresets")]
        public static void Open()
        {
            Editor.ScriptableEditorUtils.OpenOrCreateDefault<FontPresets>(childrenPath:DEFAULT_FOLDER_PATH);
        }
#endif
    }
}