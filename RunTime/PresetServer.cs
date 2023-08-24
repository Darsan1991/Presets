using System;
using System.Collections.Generic;
using DGames.Presets.Utils;
using UnityEngine;

namespace DGames.Presets
{
    public static class PresetServer
    {
        private static readonly Dictionary<Type, IPresets> _presets = new();

        // ReSharper disable once MethodNameNotMeaningful
        public static IPresets<T> Get<T>()
        {
#if DGAMES_SERVICES
               return new Essentials.LazyLoadFromService<IPresets<T>>().Value;
#else

            if (!_presets.ContainsKey(typeof(IPresets<T>)))
            {
                _presets.Add(typeof(IPresets<T>),ScriptableUtils.GetDefault<IPresets<T>>());
            }

            return (IPresets<T>)_presets[typeof(IPresets<T>)];

#endif
        }

        // ReSharper disable once MethodNameNotMeaningful
        public static IPresets Get(Type type)
        {
#if DGAMES_SERVICES
            return Essentials.Services.Get(typeof(IPresets<>).MakeGenericType(type)) as IPresets;
#else
            var presetType = typeof(IPresets<>).MakeGenericType(type);
            if (!_presets.ContainsKey(presetType))
            {
                _presets.Add(presetType,(IPresets)ScriptableUtils.GetDefault(presetType));
            }
            return _presets[presetType];
#endif
        }
        
#if UNITY_EDITOR

        [UnityEditor.MenuItem("MyGames/Refresh/PresetScriptableCache")]
        public static void RefreshPresetScriptableCache()
        {
            Debug.Log("Presets:"+_presets.Count);
            _presets.Clear();
            Debug.Log("Presets:"+_presets.Count);
        }
#endif
        
    }
}