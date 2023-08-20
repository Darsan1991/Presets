using DGames.Essentials.Attributes;
using UnityEditor;
using UnityEngine;

namespace DGames.Presets
{
    [DashboardResourceItem(tabPath:"Presets/Animations",subFolderPath:DEFAULT_FOLDER_PATH,displayName:"Root - Key Array")]

    [CreateAssetMenu]
    public class AnimationClipsPresets : DirectPresets<AnimationClip[]>
    {
#if UNITY_EDITOR
        [MenuItem("MyGames/Presets/Animation Clips")]
        public static void Open()
        {
            Editor.ScriptableEditorUtils.OpenOrCreateDefault<AnimationClipsPresets>(childrenPath: DEFAULT_FOLDER_PATH);
        }
#endif
    }
}