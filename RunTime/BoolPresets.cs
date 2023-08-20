using DGames.Essentials.Attributes;
using DGames.Essentials.Extensions;
using UnityEditor;
using UnityEngine;

namespace DGames.Presets
{
    [DashboardResourceItem(tabPath:"Presets/Bools",subFolderPath:DEFAULT_FOLDER_PATH,displayName:"Root - Key Item")]
    [CreateAssetMenu]
    public class BoolPresets : DirectPresets<bool>
    {
        [ContextMenu("Force Update")]
        public void ForceUpdate()
        {
            FindObjectsOfType<Preset<bool>>().ForEach(p=>p.gameObject.SetActive(true));
        }
        

#if UNITY_EDITOR
        [MenuItem("MyGames/Presets/BoolPresets")]
        public static void Open()
        {
            Editor.ScriptableEditorUtils.OpenOrCreateDefault<BoolPresets>(childrenPath:DEFAULT_FOLDER_PATH);
        }
#endif
    }
    
    


}



