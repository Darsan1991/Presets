using DGames.Essentials.Attributes;
using UnityEditor;
using UnityEngine;

namespace DGames.Presets
{
    [DashboardResourceItem(tabPath:"Presets/Audios",subFolderPath:DEFAULT_FOLDER_PATH,displayName:"Root - Key Item")]

    [CreateAssetMenu]
    public class AudioClipPresets : DirectPresets<AudioClip>
    {
#if UNITY_EDITOR
        [MenuItem("MyGames/Presets/AudioClipPresets")]
        public static void Open()
        {
            Editor.ScriptableEditorUtils.OpenOrCreateDefault<AudioClipPresets>(childrenPath: DEFAULT_FOLDER_PATH);
        }
#endif
    }
}