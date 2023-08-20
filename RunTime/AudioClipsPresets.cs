using DGames.Essentials.Attributes;
using UnityEditor;
using UnityEngine;

namespace DGames.Presets
{
    [DashboardResourceItem(tabPath:"Presets/Audios",subFolderPath:DEFAULT_FOLDER_PATH,displayName:"Root - Key Array")]

    [CreateAssetMenu]
    public class AudioClipsPresets : DirectPresets<AudioClip[]>
    {
#if UNITY_EDITOR
        [MenuItem("MyGames/Presets/AudioClipsPresets")]
        public static void Open()
        {
            Editor.ScriptableEditorUtils.OpenOrCreateDefault<AudioClipsPresets>(childrenPath: DEFAULT_FOLDER_PATH);
        }
#endif
    }
}