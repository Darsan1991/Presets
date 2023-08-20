using DGames.Essentials.Attributes;
using UnityEditor;
using UnityEngine;

namespace DGames.Presets
{
    [DashboardResourceItem(tabPath:"Presets/Sprite",subFolderPath:DEFAULT_FOLDER_PATH,displayName:"Root - Key Item")]

    [CreateAssetMenu]
    public class SpritePresets : DirectPresets<Sprite>
    {
#if UNITY_EDITOR
        [MenuItem("MyGames/Presets/SpritePresets")]
        public static void Open()
        {
            Editor.ScriptableEditorUtils.OpenOrCreateDefault<SpritePresets>(childrenPath: DEFAULT_FOLDER_PATH);
        }
#endif
    }
}