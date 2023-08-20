using DGames.Essentials.Attributes;
using UnityEditor;
using UnityEngine;

namespace DGames.Presets
{
    [DashboardResourceItem(tabPath:"Presets/Curves",subFolderPath:DEFAULT_FOLDER_PATH,displayName:"Root - Key Item")]
    [CreateAssetMenu]
    public class CurvePresets : DirectPresets<AnimationCurve>
    {
#if UNITY_EDITOR
        [MenuItem("MyGames/Presets/CurvePresets")]
        public static void Open()
        {
            Editor.ScriptableEditorUtils.OpenOrCreateDefault<CurvePresets>(childrenPath: DEFAULT_FOLDER_PATH);
        }
#endif
    }
}