using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace DGames.Presets
{
    public abstract class PropertyPreset<T> : Preset<T>
    {
        protected readonly Dictionary<PropertyFindInfo,PropertyInfo> propertyCache = new();
        
        [SerializeField] protected string propertyPath;
        [SerializeField] protected Component target;
        protected override void OnSetValue(T value)
        {
            var findInfo = new PropertyFindInfo
            {
                path = propertyPath,
                type = target.GetType()
            };
            
            if (!propertyCache.ContainsKey(findInfo))
            {
                propertyCache[findInfo] = FindProperty(target, propertyPath, value);
            }
            
            propertyCache[findInfo].SetValue(target,value);
        }
        
        public static  PropertyInfo FindProperty(object targetObject, string property, object setTo)
        {
            var parts = property.Split('.');
            var prop = targetObject.GetType().GetProperty(parts[0],BindingFlags.DeclaredOnly | 
                                                                   BindingFlags.Public | 
                                                                   BindingFlags.Instance);
            if (parts.Length == 1)
            {
                // last property
                if (prop != null)
                {
                    return prop;
                }
            }
            else
            {
                // Not at the end, go recursive
                if (prop != null)
                {
                    var value = prop.GetValue(targetObject);
                    return FindProperty(value, string.Join(".", parts.Skip(1)), setTo);
                }
            }

            return null;
        }
        
        public struct PropertyFindInfo
        {
            public string path;
            public Type type;

         
            public bool Equals(PropertyFindInfo other)
            {
                return path == other.path && type == other.type;
            }

            public override bool Equals(object obj)
            {
                return obj is PropertyFindInfo other && Equals(other);
            }

            public override int GetHashCode()
            {
                return HashCode.Combine(path, type);
            }
        }
    }
}