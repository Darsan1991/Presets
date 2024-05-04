using System;
using System.Collections.Generic;
using System.Linq;
using DGames.Essentials.Attributes;
#if UNITY_EDITOR
using DGames.Essentials.Editor;

using System.IO;
using UnityEditor;
using UnityEditor.Presets;
#endif
using UnityEngine;

namespace DGames.Presets
{
    public partial class DirectPresets<TJ> : Presets<TJ>
    {
        [SerializeField] [SpriteFold(propertyPath: "value")] [Inline]
        protected List<DirectKeyAndValue> keyAndValues = new();

        public override IEnumerable<IKeyAndValue<TJ>> KeyAndValues => keyAndValues;
        
       
        

#if UNITY_EDITOR
        public override void Add(string key, object value)
        {
            var item = new DirectKeyAndValue
            {
                key = key,
                value = (TJ)value
            };
            keyAndValues.Add(item);
            Debug.Log(JsonUtility.ToJson(item));
            EditorUtility.SetDirty(this);
            RefreshDictionary();
            NotifyParentAsUpdatedIfCan();
            CallUpdateEvent();
        }

        protected override void ProcessUpdateItem(string keyPropertyStringValue, object defValue)
        {
            var index = keyAndValues.FindIndex(p => p.key == keyPropertyStringValue);

            if (index == -1)
                throw new Exception();

            var keyAndValue = keyAndValues[index];
            keyAndValue.value = defValue is TJ tj ? tj : default;
            keyAndValues[index] = keyAndValue;
            EditorUtility.SetDirty(this);
            RefreshDictionary();
            NotifyParentAsUpdatedIfCan();
            CallUpdateEvent();
        }

        public override bool CanUpdate(string key)
        {
            return HasSelfContained(key) && keyAndValues.Any(p => p.key == key && !p.useAnotherPreset)
                   || childPresets.Any(c => c.CanUpdate(key));
        }

        public override void Restore(string path)
        {
            var allPresets = GetAllPreset(this).ToList();
            
            var itemAndPresets = Directory.GetFiles(path, "*.preset", SearchOption.AllDirectories)
                .Select(FilePathToAssetPath)
                .Select(AssetDatabase.LoadAllAssetsAtPath).SelectMany(a => a)
                .OfType<Preset>()
                .Select(p => (allPresets.FirstOrDefault(item => item.name == p.name), p))
                .Where(p => p.Item1).ToList();
            
            itemAndPresets
                .ForEach(p => RestorePreset(p.Item1, p.Item2));
        }

        private static string FilePathToAssetPath(string path)
        {
            path = path.Replace("/", Path.DirectorySeparatorChar.ToString());
            path = path.Replace("\\", Path.DirectorySeparatorChar.ToString());


            
            var items = path.Split(Path.DirectorySeparatorChar).ToList();
            var index = items.FindIndex(item=>item == "Assets");
            
            return string.Join(Path.DirectorySeparatorChar, items.Skip(index));
        }

        public override void SaveTo(string folderPath)
        {
            var allPresets = GetAllPreset(this).ToList();
            allPresets.ForEach(p =>
            {
                var preset = new Preset(p)
                {
                    excludedProperties = new[] { nameof(childPresets) }
                };

                var assetPath = FilePathToAssetPath(folderPath);
                AssetDatabase.CreateAsset(preset, assetPath+ $"{Path.DirectorySeparatorChar}{p.name}.preset");
            });
        }

        protected static void RestorePreset(Presets item, Preset preset)
        {
            preset.ApplyTo(item, new[] { nameof(keyAndValues) });
        }

        protected static IEnumerable<Presets> GetAllPreset(Presets preset)
        {
            return new[] { preset }.Concat(preset.Children.SelectMany(GetAllPreset));
        }

        public void Load()
        {
            Restore(EditorUtility.OpenFolderPanel("Load Presets", "", ""));
        }

        public void Save()
        {
            SaveTo(EditorUtility.OpenFolderPanel("Save Presets", "", ""));
        }
   

#endif
        [Serializable]
        public class DirectKeyAndValue : KeyAndValue<TJ, TJ>
        {
            public override TJ ConvertToValue(TJ v)
            {
                return v;
            }
        }
    }


    public partial class DirectPresets<TJ>
    {
        public const string KEY_VALUE_FIELD = nameof(keyAndValues);
    }
    
    
  #if UNITY_EDITOR
    public partial class DirectPresets<TJ>: IToolButtons
    {
        public IEnumerable<ButtonArgs> ToolButtons=> parent == null ? new[]
        {
           
            new ButtonArgs
            {
                name = nameof(Load),
                action = Load
            }, new ButtonArgs
            {
                name = nameof(Save),
                action = Save
            },
        } : Array.Empty<ButtonArgs>();
        
    }
    #endif
    
}