using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DGames.Essentials.Attributes;
using DGames.Presets;
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


        public override void Restore(string path)
        {
            var allPresets = GetAllPreset(this).ToList();
            var itemAndPresets = Directory.GetFiles(path, "*.json", SearchOption.AllDirectories)
                .Select(p => new { name = Path.GetFileNameWithoutExtension(p), text = File.ReadAllText(p) })
                .Select(p => (allPresets.FirstOrDefault(item => $"{item.GetType().Name}-{item.name}" == p.name), p))
                .Where(p => p.Item1).ToList();

            itemAndPresets
                .ForEach(p => RestoreFromJson((DirectPresets<TJ>)p.Item1, p.Item2.text));
            RefreshDictionary();
        }

        [Serializable]
        public class DirectKeyAndValue : KeyAndValue<TJ, TJ>
        {
            public override TJ ConvertToValue(TJ v)
            {
                return v;
            }
        }
    }


#if UNITY_EDITOR

    public partial class DirectPresets<TJ>
    {
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
    }
    public partial class DirectPresets<TJ>
    {
        public const string KEY_VALUE_FIELD = nameof(keyAndValues);
    }

    public partial class DirectPresets<TJ>
    {
        public static string LastFolderPath
        {
            get => EditorPrefs.GetString(Application.productName + "PresetPath");
            private set => EditorPrefs.SetString(Application.productName + "PresetPath", value);
        }


        private static string FilePathToAssetPath(string path)
        {
            path = path.Replace("/", Path.DirectorySeparatorChar.ToString());
            path = path.Replace("\\", Path.DirectorySeparatorChar.ToString());


            var items = path.Split(Path.DirectorySeparatorChar).ToList();
            var index = items.FindIndex(item => item == "Assets");

            return string.Join(Path.DirectorySeparatorChar, items.Skip(index));
        }

        public override void SaveTo(string folderPath)
        {
            var allPresets = GetAllPreset(this).ToList();
            // allPresets.ForEach(p =>
            // {
            //     var preset = new Preset(p)
            //     {
            //         excludedProperties = new[] { nameof(childPresets) }
            //     };
            //
            //
            //     var assetPath = FilePathToAssetPath(folderPath);
            //     AssetDatabase.CreateAsset(preset, assetPath+ $"{Path.DirectorySeparatorChar}{p.GetType().Name}-{p.name}.preset");
            // });


            allPresets.ForEach(p =>
            {
                var json = JsonUtility.ToJson(DirectPresetSave<TJ>.Create(p.keyAndValues.ToList()), true);
                Debug.Log(json);
                p.keyAndValues.ForEach(pa => Debug.Log(pa.key));

                File.WriteAllText(folderPath + $"{Path.DirectorySeparatorChar}{p.GetType().Name}-{p.name}.json", json);
            });
            AssetDatabase.Refresh();
        }

        protected static void RestorePreset(Presets item, Preset preset)
        {
            preset.ApplyTo(item, new[] { nameof(keyAndValues) });
        }

        protected static void RestoreFromJson(DirectPresets<TJ> item, string json)
        {
            var directKeyAndValues = JsonUtility.FromJson<DirectPresetSave<TJ>>(json).keyAndValues.ToList();
            item.keyAndValues = directKeyAndValues
                .Concat(item.keyAndValues.Where(k => directKeyAndValues.All(d => d.key != k.key)).ToList()).ToList();
            item.keyAndValues.ToList().ForEach(p=> item.ProcessUpdateItem(p.key,p.value));
        }

        protected static IEnumerable<DirectPresets<TJ>> GetAllPreset(Presets preset)
        {
            return new[] { preset }.Concat(preset.Children.SelectMany(GetAllPreset)).Cast<DirectPresets<TJ>>();
        }

        public void Load()
        {
            var path = EditorUtility.OpenFolderPanel("Load Presets", LastFolderPath, "");
            if (string.IsNullOrEmpty(path))
            {
                return;
            }

            LastFolderPath = new DirectoryInfo(path).Parent!.FullName;
            Restore(path);
        }


        public void Save()
        {
            var path = EditorUtility.OpenFolderPanel("Save Presets", LastFolderPath, "");
            if (string.IsNullOrEmpty(path))
            {
                return;
            }

            LastFolderPath = new DirectoryInfo(path).Parent!.FullName;
            SaveTo(path);
        }
    }


    public partial class DirectPresets<TJ> : IToolButtons
    {
        public IEnumerable<ButtonArgs> ToolButtons => parent == null
            ? new[]
            {
                new ButtonArgs
                {
                    name = nameof(Load),
                    action = Load
                },
                new ButtonArgs
                {
                    name = nameof(Save),
                    action = Save
                },
            }
            : Array.Empty<ButtonArgs>();
    }
#endif

}

[Serializable]
public struct DirectPresetSave<T> : IEnumerable<DirectPresets<T>.DirectKeyAndValue>
{
    public List<DirectPresets<T>.DirectKeyAndValue> keyAndValues;

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public IEnumerator<DirectPresets<T>.DirectKeyAndValue> GetEnumerator()
    {
        return keyAndValues?.GetEnumerator() ?? new List<DirectPresets<T>.DirectKeyAndValue>().GetEnumerator();
    }

    public static DirectPresetSave<T> Create(IEnumerable<DirectPresets<T>.DirectKeyAndValue> keyAndValues)
    {
        return new DirectPresetSave<T>
        {
            keyAndValues = keyAndValues.ToList()
        };
    }
}