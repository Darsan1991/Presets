using System;
using DGames.Essentials;
using DGames.Essentials.Attributes;
using UnityEngine;

namespace DGames.Presets
{
    [Serializable]
    public struct PresetInfo<T>
    {
        public string key;
        public T def;
    }
    [Serializable]
    public class PresetValue<T>
    {
        [SerializeField] private PresetInfo<T> _info;
       
        
        public bool Cache { get; set; } = true;
        
        private T _value;
        private bool _hasCache;

        private LazyLoadValue<IPresets<T>> _presets;

        public IPresets Presets => (_presets ??= new LazyLoadFromService<IPresets<T>>()).Value;

        public T Value
        {
            get
            {

                if (Presets == null) return _info.def;
                
                if (Cache)
                {
                    var value = _hasCache ? _value : (_value = (T)Presets.GetValue(_info.key, _info.def));
                    _hasCache = true;
                    return value;
                }

                return (T)Presets.GetValue(_info.key, _info.def);

            }
        }

        public PresetValue(string key, T def = default, bool cache = true)
        {
            _info = new PresetInfo<T> { key = key, def = def };
            Cache = cache;
        }

        public PresetValue()
        {
            
        }

        public void ForceUpdate()
        {
            if (!Cache || Presets == null)
                return;

            _value = (T)Presets.GetValue(_info.key, _info.def);
        }
        //
        // public IPresets GetPresetsForType()
        // {
        //     if (typeof(T) == typeof(int))
        //     {
        //         return GlobalServices.GetOrDefault<IPresets<float>>();
        //     }
        //
        //
        //     return GlobalServices.GetOrDefault<IPresets<T>>();
        // }

        public static implicit operator T(PresetValue<T> preset) => preset.Value;
    }
}