using DGames.Essentials.Attributes;
using UnityEngine;

namespace DGames.Presets
{
    
    [DashboardResourceItem(tabPath:"Presets/Value",subFolderPath:DEFAULT_FOLDER_PATH,displayName:"Root - Key Item")]

        [CreateAssetMenu]

    public class ValuePresets : DirectPresets<float>
    {

        public int GetInt(string key, int def = 0)
        {
            return (int)Get(key, def);
        }

#if UNITY_EDITOR
        [UnityEditor.MenuItem("MyGames/Presets/ValuePresets")]
        public static void Open()
        {
            Editor.ScriptableEditorUtils.OpenOrCreateDefault<ValuePresets>(childrenPath:DEFAULT_FOLDER_PATH);
        }
#endif
    }
}