using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace DGames.Presets.Editor
{
    [CustomPropertyDrawer(typeof(PresetInfo<>), true)]
    public class PresetInfoPropertyDrawer : PropertyDrawer
    {
        private Presets _presets;
        private SerializedProperty _keyProperty;
        private SerializedProperty _defProperty;
        private bool _canEditPreset;
        private bool _isFoldSupported;
        private GUIStyle _titleStyle;
        private Type _defPropertyType;
        private bool _complexField;
        private Presets _targetPresetToAdd;
        private string _addPresetText;


        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return UnityEditor.EditorGUI.GetPropertyHeight(property, label, _complexField) +
                   (!_complexField
                       ? EditorGUIUtility.standardVerticalSpacing * 6f + (property.isExpanded && _isFoldSupported
                           ? 2 * EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing * 2
                           : 0)
                       : 0);
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            CacheFieldsIfNeeded(property);

            if (_complexField)
            {
                // Debug.Log("Complex Field:" + property.name);
                UnityEditor.EditorGUI.PropertyField(position, property,
                    label != GUIContent.none ? new GUIContent(property.displayName) : GUIContent.none, true);

                return;
            }



            position.height = GetPropertyHeight(property, label);
            position = DrawBackgroundColorBoxIfNeeded(position);
            var totalWidth = position.width;
            DrawPrimary(ref position, property, label);

            // Debug.Log(EditorGUIUtility.currentViewWidth);
            //
            DrawOriginalSection(position, property, totalWidth);
        }

        private void DrawPrimary(ref Rect position, SerializedProperty property, GUIContent label)
        {
            position.height = EditorGUIUtility.singleLineHeight;

            UnityEditor.EditorGUI.BeginProperty(position, label, property);

            var totalWidth = position.width;
            var leftWidth = position.width;

            if (_isFoldSupported)
                property.isExpanded = UnityEditor.EditorGUI.Foldout(position, property.isExpanded, label);
            else
            {
                UnityEditor.EditorGUI.LabelField(position, label);
            }

            var startX = position.position.x;
            position.position += Vector2.right * 40;
            leftWidth -= 40;

            GUI.color = GUI.color.WithAlpha(0.3f);
            UnityEditor.EditorGUI.LabelField(position, "K");
            position.position += Vector2.right * 15;
            leftWidth -= 15;
            GUI.color = GUI.color.WithAlpha(1);

            position = DrawDropDownsForPreset(position, ref leftWidth);
            //
            var keyFieldWidth = totalWidth / 3;
            position.width = keyFieldWidth;
            _keyProperty.stringValue = UnityEditor.EditorGUI.TextField(position, _keyProperty.stringValue);
            position.position += Vector2.right * (keyFieldWidth + 10);
            leftWidth -= keyFieldWidth + 10;
            position.width = 10;
            leftWidth -= 15;
            GUI.color = GUI.color.WithAlpha(0.3f);
            UnityEditor.EditorGUI.LabelField(position, "D");
            position.position += Vector2.right * 15;
            position.width = leftWidth;
            GUI.color = GUI.color.WithAlpha(1);
            UnityEditor.EditorGUI.PropertyField(position, _defProperty, GUIContent.none);
            UnityEditor.EditorGUI.EndProperty();
            position.width = totalWidth;
            position.position = new Vector2(startX, position.position.y);
        }

        private void CacheFieldsIfNeeded(SerializedProperty property)
        {
            _keyProperty ??= property.FindPropertyRelative("key");
            _defProperty ??= property.FindPropertyRelative("def");
            _defPropertyType ??= _defProperty.GetValueType();
            _complexField = !_defProperty.propertyType.IsBuildInSerializableField();
            _isFoldSupported = true; //|| EditorGUIExtra.CanDrawBuildInFieldEditorGUI(_defPropertyType);
            if (_titleStyle == null)
            {
                var style = GUI.skin.GetStyle("Label");
                _titleStyle = new GUIStyle(style) { alignment = TextAnchor.MiddleCenter };
            }
        }

        private void DrawOriginalSection(Rect position, SerializedProperty property, float totalWidth)
        {
            if (!property.isExpanded || !_isFoldSupported)
            {
                return;
            }


            position.position +=
                Vector2.up * (EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing);

            if (!_presets)
            {
                DrawNoPresetsFound(position);
            }
            else if (_presets.HasKey(_keyProperty.stringValue))
            {
                DrawOriginalGroup(position, totalWidth);
            }

            else if (_targetPresetToAdd != null)
            {
                DrawAddGroup(position);
            }
            else
            {
                DrawMissingGroup(position);
            }
        }


        private void DrawMissingGroup(Rect position)
        {
            //236 - 38

            UnityEditor.EditorGUI.LabelField(position,
                GetTitleWithDashes("MISSING"), _titleStyle);
            // Debug.Log(position.width);
            position.position +=
                Vector2.up * (EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing);
            UnityEditor.EditorGUI.BeginDisabledGroup(string.IsNullOrEmpty(_keyProperty.stringValue));
            if (GUI.Button(position, "Add To Preset"))
            {
                ShowAddMenu(GetDefValue());
            }

            UnityEditor.EditorGUI.EndDisabledGroup();
        }

        private void DrawNoPresetsFound(Rect position)
        {
            //236 - 38

            UnityEditor.EditorGUI.LabelField(position,
                GetTitleWithDashes("MISSING PRESETS"), _titleStyle);
            // Debug.Log(position.width);
            position.position +=
                Vector2.up * (EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing);
            UnityEditor.EditorGUI.BeginDisabledGroup(string.IsNullOrEmpty(_keyProperty.stringValue));
            UnityEditor.EditorGUI.LabelField(position,
                "Presets For This type not Found. Please Create Presets For This Type");

            UnityEditor.EditorGUI.EndDisabledGroup();
        }

        private void DrawAddGroup(Rect position)
        {
            //236 - 38

            UnityEditor.EditorGUI.LabelField(position,
                GetTitleWithDashes("CREATE NEW"), _titleStyle);
            // Debug.Log(position.width);
            position.position +=
                Vector2.up * (EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing);


            position.width -= 100;
            _addPresetText = UnityEditor.EditorGUI.TextField(position, _addPresetText);

            position.x += position.width + 5;

            position.width = 45;


            if (GUI.Button(position, "Cancel"))
            {
                _targetPresetToAdd = null;
                _addPresetText = "";
                return;
            }


            position.x += position.width + 5;
            UnityEditor.EditorGUI.BeginDisabledGroup(string.IsNullOrEmpty(_addPresetText));

            if (GUI.Button(position, "Create"))
            {
                _targetPresetToAdd.CreateNewChild(_addPresetText).Add(_keyProperty.stringValue,
                    _defProperty.ToObjectValue(_defPropertyType));
                _targetPresetToAdd = null;
                _addPresetText = "";
            }

            UnityEditor.EditorGUI.EndDisabledGroup();
        }

        private void DrawOriginalGroup(Rect position, float totalWidth)
        {
            UnityEditor.EditorGUI.LabelField(position,
                GetTitleWithDashes("ORIGINAL"), _titleStyle);
            position.position +=
                Vector2.up * (EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing);
            position.width = 45;

            var canUpdate = _presets.CanUpdate(_keyProperty.stringValue);

            UnityEditor.EditorGUI.BeginDisabledGroup(!canUpdate);
            if (GUI.Button(position, _canEditPreset ? "Close" : "Edit"))
            {
                _canEditPreset = !_canEditPreset;
            }

            UnityEditor.EditorGUI.EndDisabledGroup();
            position.position += Vector2.right * (position.width + 5);
            position.width = totalWidth - position.width;
            UnityEditor.EditorGUI.BeginDisabledGroup(!canUpdate || !_canEditPreset);
            UnityEditor.EditorGUI.BeginChangeCheck();

            var value = EditorGUI.BuildInFieldEditorGUI(position,
                _presets.GetValue(_keyProperty.stringValue, GetDefValue()));
            if (UnityEditor.EditorGUI.EndChangeCheck())
            {
                _presets.UpdateItem(_keyProperty.stringValue, value);
            }

            UnityEditor.EditorGUI.EndDisabledGroup();
        }

        public static string GetTitleWithDashes(string title, int countPerSide = 70)
        {
            var dashes = string.Join("", Enumerable.Repeat("-", countPerSide));

            return $"{dashes}{title}{dashes}";
        }


        private Rect DrawBackgroundColorBoxIfNeeded(Rect position)
        {
            // if (!Attribute.HasColor) return position;
            position.height -= EditorGUIUtility.standardVerticalSpacing * 2;
            UnityEditor.EditorGUI.DrawRect(position, new Color(42f / 255, 42f / 255, 42f / 255, 0.5f));
            // GUI.skin.box.Draw(position,true,true,true,true);
            position.position += Vector2.up * (EditorGUIUtility.standardVerticalSpacing * 2);
            position.position += Vector2.right * (EditorGUIUtility.standardVerticalSpacing * 10);
            position.width -= EditorGUIUtility.standardVerticalSpacing * 11;

            return position;
        }

        private Rect DrawDropDownsForPreset(Rect position, ref float leftWidth)
        {
            _presets = PresetServer.Get(_defPropertyType) as Presets;
            if (!_presets) return position;


            var defValue = GetDefValue();

            if (!_presets.HasKey(_keyProperty.stringValue))
            {
                var newPosition = DrawDropDownToAdd(position, defValue);
                leftWidth -= newPosition.x - position.x;
                position = newPosition;
            }
            else if (!string.IsNullOrEmpty(_keyProperty.stringValue) &&
                     !Equals(_presets.GetValue(_keyProperty.stringValue, defValue),
                         defValue))
            {
                var newPosition = DrawDropDownForUpdateItem(position, defValue);
                leftWidth -= newPosition.x - position.x;
                position = newPosition;
            }


            return position;
        }

        private object GetDefValue()
        {
            return _defProperty.ToObjectValue(_defPropertyType);
        }

        private Rect DrawDropDownToAdd(Rect position, object defValue)
        {
            UnityEditor.EditorGUI.BeginDisabledGroup(string.IsNullOrEmpty(_keyProperty.stringValue));
            position.width = position.height;
            if (UnityEditor.EditorGUI.DropdownButton(position, new GUIContent("+"), FocusType.Passive,
                    new GUIStyle { border = new RectOffset(1, 1, 1, 1) }))
            {
                ShowAddMenu(defValue);
            }

            UnityEditor.EditorGUI.EndDisabledGroup();
            position.position += Vector2.right * position.width;
            return position;
        }

        private void ShowAddMenu(object defValue, string startPath = null)
        {
            var menu = new GenericMenu();
            foreach (var tuple in GetAllPresets(_presets))
            {
                menu.AddItem(new GUIContent((string.IsNullOrEmpty(startPath) ? "" : "Add To/") + tuple.Item1), false,
                    () =>
                    {
                        if (tuple.Item1.EndsWith("Create New"))
                        {
                            _targetPresetToAdd = tuple.Item2;
                        }
                        else
                        {
                            tuple.Item2.Add(_keyProperty.stringValue, defValue
                            );
                        }
                    });
            }

            menu.ShowAsContext();
        }

        private Rect DrawDropDownForUpdateItem(Rect position, object defValue)
        {
            position.width = position.height;
            if (UnityEditor.EditorGUI.DropdownButton(position, new GUIContent("$"), FocusType.Passive,
                    new GUIStyle { border = new RectOffset(1, 1, 1, 1) }))
            {
                var menu = new GenericMenu();
                menu.AddItem(new GUIContent("Update Value to Default"), false,
                    () => { _presets.UpdateItem(_keyProperty.stringValue, defValue); });
                menu.ShowAsContext();
            }

            position.position += Vector2.right * position.width;

            return position;
        }

        private static IEnumerable<(string, Presets)> GetAllPresets(Presets presets, string path = "")
        {
            Debug.Log((path + (string.IsNullOrEmpty(path) ? presets.name : "/" + presets.name)));
            yield return (path + (string.IsNullOrEmpty(path) ? presets.name : "/" + presets.name), presets);

            var subPath = path + (string.IsNullOrEmpty(path) ? "Sub" : $"/Sub");

            foreach (var child in presets.Children)
            {
                foreach (var t in GetAllPresets(child, subPath))
                {
                    // Debug.Log(t.Item1);
                    yield return t;
                }
            }

            var createPath = path + (string.IsNullOrEmpty(path) ? "Create New" : $"/Create New");
            Debug.Log(createPath);
            yield return (createPath, presets);
        }
    }

}