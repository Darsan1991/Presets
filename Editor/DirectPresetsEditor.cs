using System.Linq;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace DGames.Presets.Editor
{
    [CustomEditor(typeof(DirectPresets<>), true)]
    public partial class DirectPresetsEditor : Essentials.Editor.Editor
    {
        private SerializedProperty _childrenField;
        private string _newChildName;
        private bool _childrenFold;
        private GUIStyle _titleStyle;
        private ReorderableList _keyValuePairs;

        private void OnEnable()
        {
            _childrenField = serializedObject.FindProperty("childPresets");
            _keyValuePairs = new ReorderableList(serializedObject,
                serializedObject.FindProperty(DirectPresets<int>.KEY_VALUE_FIELD),true,true,true,true)
            {
                multiSelect = true
            };

            _keyValuePairs.drawElementCallback = (rect, index, active, focused) =>
            {
                UnityEditor.EditorGUI.PropertyField(rect,
                    _keyValuePairs.serializedProperty.GetArrayElementAtIndex(index), true);
            };

            _keyValuePairs.elementHeightCallback = index =>
                UnityEditor.EditorGUI.GetPropertyHeight(
                    _keyValuePairs.serializedProperty.GetArrayElementAtIndex(index));

            _keyValuePairs.drawHeaderCallback = rect => UnityEditor.EditorGUI.LabelField(rect, "Key Vs Values");

        }


        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            UnityEditor.EditorGUI.BeginChangeCheck();

            CacheIfNeeded();
            EditorGUILayout.BeginVertical(GUI.skin.box);
            EditorGUILayout.BeginHorizontal();
            _childrenFold = UnityEditor.EditorGUI.Foldout(EditorGUILayout.GetControlRect(GUILayout.MaxWidth(13)),
                _childrenFold, "");
            EditorGUILayout.LabelField(GetTitleWithDashes("CHILDREN SECTION", 120),_titleStyle);
            EditorGUILayout.EndHorizontal();
            

            var notifiedChanged = false;
            if (_childrenFold)
            {
                UnityEditor.EditorGUI.indentLevel++;
                _childrenField.isExpanded = true;
                EditorGUILayout.PropertyField(_childrenField);

                EditorGUILayout.BeginVertical(GUI.skin.box);

                _newChildName = EditorGUILayout.TextField(_newChildName);

                UnityEditor.EditorGUI.BeginDisabledGroup(string.IsNullOrEmpty(_newChildName));
                EditorGUILayout.Space();
                if (GUILayout.Button("Add Child"))
                {
                    var values = CreateInstance(target.GetType());
                    values.name = _newChildName;
                    _childrenField.InsertArrayElementAtIndex(_childrenField.arraySize);
                    var property = _childrenField.GetArrayElementAtIndex(_childrenField.arraySize - 1);

                    AssetDatabase.AddObjectToAsset(values, target);
                    AssetDatabase.SaveAssetIfDirty(target);
                    property.objectReferenceValue = values;
                    _newChildName = "";
                    notifiedChanged = true;
                }

                UnityEditor.EditorGUI.EndDisabledGroup();

                EditorGUILayout.EndVertical();

                UnityEditor.EditorGUI.indentLevel--;
                
            }

            EditorGUILayout.EndVertical();
            EditorGUILayout.Space();
            

            _keyValuePairs.DoLayoutList();


            if (UnityEditor.EditorGUI.EndChangeCheck())
                serializedObject.ApplyModifiedProperties();


            if (notifiedChanged)
            {
                NotifyChanged?.Invoke(this);
            }
        }

        private void CacheIfNeeded()
        {
            if (_titleStyle == null)
            {
                var style = EditorStyles.label;
                _titleStyle = new GUIStyle(style) { alignment = TextAnchor.MiddleCenter };
            }
        }

        public static string GetTitleWithDashes(string title, int countPerSide = 70)
        {
            var dashes = string.Join("", Enumerable.Repeat("-", countPerSide));

            return $"{dashes}{title}{dashes}";
        }
    }
}