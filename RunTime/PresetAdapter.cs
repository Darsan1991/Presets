using System;

namespace DGames.Presets
{
    public class PresetAdapter<T,TJ>:IPresets<T>
    {
        public event Action Updated;

        private readonly IPresets<TJ> _fromPreset;

        public PresetAdapter(IPresets<TJ> fromPreset)
        {
            _fromPreset = fromPreset;
            fromPreset.Updated += FromPresetOnUpdated;
        }

        ~PresetAdapter()
        {
            _fromPreset.Updated -= FromPresetOnUpdated;
        }
        
        private void FromPresetOnUpdated()
        {
            Updated?.Invoke();
        }


        public object GetValue(string key, object def = default)
        {
            def = Convert.ChangeType(def,typeof(TJ));
            return Convert.ChangeType(_fromPreset.GetValue(key, def), typeof(T));
        }

        public bool HasKey(string key)
        {
            return _fromPreset.HasKey(key);
        }

        public T this[string key] => Get(key,def:default);

        public T Get(string key, T def = default)
        {
            return (T)GetValue(key, def);
        }
    }
}