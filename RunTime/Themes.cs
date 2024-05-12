using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using DGames.Essentials.Attributes;
using DGames.Essentials.EditorHelpers;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace DGames.Presets
{
    [DashboardResourceItem(displayName:"Themes",tabPath:"Games/Config")]
    public class Themes:ScriptableObject, IEnumerable<Themes.Theme>
    {
        [SerializeField]private List<Theme> _themes = new();
        
        
        public IEnumerator<Theme> GetEnumerator()
        {
            return _themes.GetEnumerator();
        }
        
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }


        public void LoadTheme(Theme theme)
        {
            Debug.Log("Load Theme"+theme.name);
            var path = DirectoryForTheme(theme);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            theme.presets.ForEach(preset =>
            {
                preset.Restore(path);
            });
        }
        
        public void SaveTheme(Theme theme)
        {
            var path = DirectoryForTheme(theme);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            Debug.Log("Save Theme"+theme.name);
            theme.presets.ForEach(preset =>
            {
                preset.SaveTo(path);
            });
        }

        public static string DirectoryForTheme(Theme theme)
        {
            return Application.dataPath + "/Resources/Themes/Theme_"+theme.id;
        }
        
        
        
        [Serializable]
        public struct Theme
        {
           public string name;
            public string id;
            public List<Presets> presets;
        }
        
#if UNITY_EDITOR
        [MenuItem("MyGames/Presets/Themes")]
        public static void Open()
        {
            Editor.ScriptableEditorUtils.OpenOrCreateDefault<Themes>();
        }
#endif
    }
    
    
    #if UNITY_EDITOR
    [CustomPropertyDrawer(typeof(Themes.Theme))]
    public class ThemePropertyDrawer : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUI.GetPropertyHeight(property,label,true);
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            property.isExpanded = true;
            position.height = GetPropertyHeight(property, label);
            EditorGUI.BeginProperty(position, label, property);
            var id = property.FindPropertyRelative("id");
            var name = property.FindPropertyRelative("name");
            var presets = property.FindPropertyRelative("presets");
        
            var rect = position;
            rect.height = EditorGUIUtility.singleLineHeight;
            // EditorGUI.LabelField(rect, name.stringValue);
            // rect.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
            var topRect = rect;

            OnGUITopBar(property, topRect, name);
            rect.y += EditorGUIUtility.singleLineHeight+ EditorGUIUtility.standardVerticalSpacing;
            EditorGUI.PropertyField(rect, id);
            rect.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
            EditorGUI.PropertyField(rect, presets);
            EditorGUI.EndProperty();
            
        }

        private static void OnGUITopBar(SerializedProperty property, Rect topRect, SerializedProperty name)
        {
            const float buttonWidth = 60;
            const float spacing = 5;
            topRect.height = EditorGUIUtility.singleLineHeight;
            topRect.width -= buttonWidth*2+2*spacing;
            EditorGUI.PropertyField(topRect, name);
        
            topRect.x += topRect.width +  spacing;
           
            topRect.width = buttonWidth;
            if (GUI.Button(topRect, "Save"))
            {
                var themes = (Themes)property.serializedObject.targetObject;
                themes.SaveTheme(property.ToObjectValue<Themes.Theme>());
            }
            topRect.x += buttonWidth+spacing;
            if (GUI.Button(topRect, "Load"))
            {
                var themes = (Themes)property.serializedObject.targetObject;
                themes.LoadTheme(property.ToObjectValue<Themes.Theme>());
            }
        }
    }
    
    #endif
    
    
}