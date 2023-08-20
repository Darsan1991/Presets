using System;
using System.Collections.Generic;
using System.Linq;
using DGames.Essentials.Attributes;
using DGames.Essentials.Extensions;
using UnityEditor;
using UnityEngine;

namespace DGames.Presets
{
    public partial class DirectPresets<TJ> : Presets<TJ>
    {
        [SerializeField][Inline] protected List<DirectKeyAndValue> keyAndValues = new();

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
            var index = keyAndValues.FindIndex(p=>p.key == keyPropertyStringValue);

            if (index == -1)
                throw new Exception();

            var keyAndValue = keyAndValues[index];
            keyAndValue.value = defValue is TJ tj?tj : default;
            keyAndValues[index] = keyAndValue;
            EditorUtility.SetDirty(this);
            RefreshDictionary();
            NotifyParentAsUpdatedIfCan();
            CallUpdateEvent();

        }

        public override bool CanUpdate(string key)
        {
            return HasSelfContained(key) && keyAndValues.Any(p => p.key == key && !p.useAnotherPreset) 
                   || childPresets.Any(c=>c.CanUpdate(key));
        }

#endif
        [Serializable]
        public class DirectKeyAndValue : KeyAndValue<TJ,TJ>
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
    
}