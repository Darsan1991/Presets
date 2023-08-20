using System.Linq;
using DGames.Presets;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

[InitializeOnLoad]
public static class BoolPresetEditorHandler
{
    static BoolPresetEditorHandler()
    {
        EditorSceneManager.sceneOpened += OnSceneOpened;
        ActivateAllBoolPresets();

    }
    private static void OnSceneOpened(Scene scene, OpenSceneMode mode)
    {
        ActivateAllBoolPresets();
    }

    private static void ActivateAllBoolPresets()
    {
        Object.FindObjectsOfType<Preset<bool>>(true).ToList().ForEach(p => p.gameObject.SetActive(true));
    }
}
