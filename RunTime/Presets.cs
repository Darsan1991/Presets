using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DGames.Essentials.Attributes;
using UnityEngine;
using DGames.Presets.Extensions;


namespace DGames.Presets
{
    public interface IPresets
    {
        event Action Updated;
        object GetValue(string key, object def = default);
        bool HasKey(string key);
    }

    public interface IPresets<T> : IPresets
    {
         T this[string key] { get; }

         T Get(string key, T def = default);
    }

    [TreeBasedResourceItem("childPresets")]
    [FooterLogo]
    public abstract class Presets : ScriptableObject,IPresets
    {
        public event Action Updated;

        public const string DEFAULT_FOLDER_PATH = "Presets";
        
        public abstract IEnumerable<Presets> Children { get; }

        public abstract object GetValue(string key, object def = default);

        public abstract bool HasKey(string key);

        protected void CallUpdateEvent() => Updated?.Invoke();

        public abstract void Restore(string path);

#if UNITY_EDITOR

        public abstract void Add(string key, object value);

        public abstract void UpdateItem(string keyPropertyStringValue, object defValue);

        public abstract Presets CreateNewChild(string childName);

        public abstract bool CanUpdate(string key);


        public abstract void SaveTo(string folderPath);
        
#endif
        public abstract bool HasSelfContained(string key);
    }

    public abstract partial class Presets<TJ> : Presets,IPresets<TJ>
    {
        
        [NoLabel][SerializeField] protected List<Presets<TJ>> childPresets = new();
        [HideInInspector] [SerializeField] protected Presets<TJ> parent;

        public Presets<TJ> Parent
        {
            get => parent;
            set
            {
                if (value == parent)
                    return;

                if (parent)
                {
                    parent.childPresets.Remove(this);
                }

                parent = value;

#if UNITY_EDITOR
                UnityEditor.EditorUtility.SetDirty(this);
#endif
            }
        }

        protected readonly Dictionary<string, TJ> keyVsValues = new();

        private IDictionary<string, TJ> KeyVsValues
        {
            get
            {
                if (!keyVsValues.Any())
                {
                    RefreshDictionary();
                }

                return keyVsValues;
            }
        }
        
        public abstract IEnumerable<IKeyAndValue<TJ>> KeyAndValues { get; }

        public override IEnumerable<Presets> Children => childPresets;

        protected void RefreshDictionary()
        {
            keyVsValues.Clear();
            this.ForEach(p =>
            {
                keyVsValues.TryAdd(p.Key, p.GetValue(this));
            });
        }

        public TJ this[string key] => KeyVsValues.GetOrDefault(key);

        
        // ReSharper disable once MethodNameNotMeaningful
        public TJ Get(string key, TJ def = default)
        {
            return KeyVsValues.GetOrDefault(key, def);
        }

        public override bool HasKey(string key) => KeyVsValues.ContainsKey(key);
        
        
        public override bool HasSelfContained(string key)=> HasKey(key) && childPresets.All(c=>!c.HasKey(key));
#if UNITY_EDITOR

        public override void UpdateItem(string keyPropertyStringValue, object defValue)
        {
            if (!HasSelfContained(keyPropertyStringValue))
            {
                childPresets.Where(c=>c.HasKey(keyPropertyStringValue)).ForEach(p=>p.UpdateItem(keyPropertyStringValue,defValue));
                return;
            }

            ProcessUpdateItem(keyPropertyStringValue, defValue);
        }

        public override Presets CreateNewChild(string childName)
        {
            var instance = (Presets<TJ>)CreateInstance(this.GetType());
            instance.name = childName;
            childPresets.Add((Presets<TJ>)instance);
            instance.parent = this;
            UnityEditor.EditorUtility.SetDirty(this);
            UnityEditor.AssetDatabase.AddObjectToAsset(instance,this);
            
            UnityEditor.AssetDatabase.SaveAssets();
            instance.RefreshDictionary();
            instance.NotifyParentAsUpdatedIfCan();
            instance.CallUpdateEvent();
            
            return (Presets)instance;
        }
        protected abstract void ProcessUpdateItem(string keyPropertyStringValue, object defValue);
#endif

        public override object GetValue(string key, object def = default) => Get(key, (TJ)def);


        public void OnChildUpdated(Presets<TJ> presets)
        {
            RefreshDictionary();
            NotifyParentAsUpdatedIfCan();
            CallUpdateEvent();
        }
    }

    public partial class Presets<TJ>
    {
      
        
       
    }
    
    public interface IKeyAndValue<TJ>
    {
        string Key { get; }
        TJ GetValue(IEnumerable<IKeyAndValue<TJ>> values, HashSet<string> keys = null);
    }


    [Serializable]
    public abstract class KeyAndValue<TValue,TRealValue>:IKeyAndValue<TRealValue>
    {
        public string key;
        public bool useAnotherPreset;

        [Condition(nameof(useAnotherPreset), false)]
        [Inline()][PlayAudio][ShortLabel()]public TValue value;

        [Condition(nameof(useAnotherPreset), true)]
        public string sourcePresetKey;

        public string Key => key;

        public TRealValue GetValue(IEnumerable<IKeyAndValue<TRealValue>> values, HashSet<string> keys = null)
        {
            keys ??= new HashSet<string>();
            var valueList = values.ToList();
            if (!useAnotherPreset)
            {
                return string.IsNullOrEmpty(key) ? default : ConvertToValue(value);
            }

            if (keys.Contains(sourcePresetKey))
            {
                throw new Exception($"Cycling keys:{sourcePresetKey}");
            }

            keys.Add(sourcePresetKey);
            var presetKey = sourcePresetKey;

            var keyAndValue = valueList.FirstOrDefault(c => c.Key == presetKey);
            return keyAndValue!=null ? keyAndValue.GetValue(valueList, keys) : default;
        }

        public abstract TRealValue ConvertToValue(TValue v);
    }

    public partial class Presets<TJ> : IEnumerable<IKeyAndValue<TJ>>
    {
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<IKeyAndValue<TJ>> GetEnumerator()
        {
            return childPresets.SelectMany(p => p).Concat(KeyAndValues).GetEnumerator();
        }
    }

    public partial class Presets<TJ>
    {
        [HideInInspector] [SerializeField] protected List<Presets<TJ>> lastChildPresets = new();


        protected virtual void OnValidate()
        {
            HandleParentValueForChildren();
            RefreshDictionary();
            NotifyParentAsUpdatedIfCan();
            CallUpdateEvent();
        }


        private void HandleParentValueForChildren()
        {
            RemoveTheParentIfNotInChildren();
            UpdateParentValueForChildren();
        }

        private void OnDestroy()
        {
            if(Parent)
                Parent.childPresets.Remove(this);

        }

        private void UpdateParentValueForChildren()
        {
            childPresets.RemoveAll(p => !p || p.Parent && p.Parent != this);
            lastChildPresets.Except(childPresets).Where(p => p.Parent == this).ForEach(p => p.Parent = null);
            childPresets.Where(p => p.Parent != this).ForEach(p => p.Parent = this);
            lastChildPresets.Clear();
            lastChildPresets.AddRange(childPresets);
        }

        protected void NotifyParentAsUpdatedIfCan()
        {
            if (Parent)
                Parent.OnChildUpdated(this);
        }

        private void RemoveTheParentIfNotInChildren()
        {
            if (!Parent) return;
            if (Parent.childPresets.All(p => p != this))
            {
                Parent = null;
            }
        }
    }
}