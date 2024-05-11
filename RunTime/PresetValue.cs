using System;
using UnityEngine;

namespace DGames.Presets
{
    [Serializable]
    public struct PresetInfo<T>
    {
        public string key;
        public T def;
        // private PresetValue<T> _presetValue;
        //
        // public PresetValue<T> PresetValue => _presetValue ??= new PresetValue<T>(key, def,!Application.isEditor);
        //
        // public T Value => PresetValue.Value;

        public PresetInfo(string key)
        {
            this.key = key;
            def = default;
            // _presetValue = null;
        }



    }
    [Serializable]
    public class PresetValue<T> : IDisposable
    {
        [SerializeField] private PresetInfo<T> _info;
       
        
        public bool Cache { get; set; } = !Application.isEditor;
        
        private T _value;
        private bool _hasCache;
        private IPresets<T> _presetInternal;
        private Binder<T> _binder;

        protected IPresets<T> PresetInternal
        {
            get => _presetInternal;
            private set
            {
                if (_presetInternal != null)
                {
                    _presetInternal.Updated -= OnPresetUpdated;
                }

                _presetInternal = value;
                
                if (_presetInternal != null)
                {
                    _presetInternal.Updated += OnPresetUpdated;
                }
            }
        }

        public IPresets Presets => (PresetInternal ??= PresetServer.Get<T>());

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

        public Binder<T> Binder => _binder ??= new Binder<T>();
        
        

        public PresetValue(string key, T def = default, bool? cache = null)
        {
            _info = new PresetInfo<T> { key = key, def = def };
            Cache = cache ?? !Application.isEditor;
        }
   

        public void Dispose()
        {
            PresetInternal = null;
        }

        ~PresetValue()
        {
            Dispose();
        }
        

        
        private void OnPresetUpdated()
        {
            if(Cache)
                return;
            if(Presets.GetValue(this._info.key, this._info.def).Equals(_value))
                return;
            
            Binder.Raised(Value);
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