using System;
using DGames.Essentials.Attributes;
using UnityEditor;
using UnityEngine;

namespace DGames.Presets
{
    [DashboardResourceItem(tabPath:"Presets/Tween",subFolderPath:DEFAULT_FOLDER_PATH,displayName:"Root - Key Item")]

    [CreateAssetMenu]
    public class TweenAnimationPresets : DirectPresets<TweenInfo>
    {
#if UNITY_EDITOR
        [MenuItem("MyGames/Presets/Animation Clip")]
        public static void Open()
        {
            Editor.ScriptableEditorUtils.OpenOrCreateDefault<TweenAnimationPresets>(childrenPath: DEFAULT_FOLDER_PATH);
        }
#endif
    }
    
    
    [Serializable]
    public struct TweenInfo
    {
        public AnimationCurve curve;
        public float time;
    }
}