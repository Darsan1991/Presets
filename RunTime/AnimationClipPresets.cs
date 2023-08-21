using System;
using DGames.Essentials.Attributes;
using UnityEditor;
using UnityEngine;

namespace DGames.Presets
{
    [DashboardResourceItem(tabPath:"Presets/Animations",subFolderPath:DEFAULT_FOLDER_PATH,displayName:"Root - Key Item")]

    [CreateAssetMenu]
    public class AnimationClipPresets : DirectPresets<AnimationClip>
    {
#if UNITY_EDITOR
        [MenuItem("MyGames/Presets/Animation Clip")]
        public static void Open()
        {
            Editor.ScriptableEditorUtils.OpenOrCreateDefault<AnimationClipPresets>(childrenPath: DEFAULT_FOLDER_PATH);
        }
#endif
    }
}