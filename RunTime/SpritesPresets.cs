using DGames.Essentials.Attributes;
using UnityEditor;
using UnityEngine;

namespace DGames.Presets
{
    [DashboardResourceItem(tabPath:"Presets/Sprite",subFolderPath:DEFAULT_FOLDER_PATH,displayName:"Root - Key Array")]

    [CreateAssetMenu]
    public class SpritesPresets : DirectPresets<Sprite[]>
    {
#if UNITY_EDITOR
        [MenuItem("MyGames/Presets/SpritesPresets")]
        public static void Open()
        {
            Editor.ScriptableEditorUtils.OpenOrCreateDefault<SpritesPresets>(childrenPath: DEFAULT_FOLDER_PATH);
        }
#endif
    }
}