using System.Collections;
using System.Collections.Generic;
using DGames.Essentials.Attributes;
using UnityEngine;

namespace DGames.Presets.Scriptable
{
    [DashboardType(tabPath:"Presets/Texts",displayName:"Text Groups")]
    public class Strings : ScriptableObject,IEnumerable<string>
    {
        [SerializeField] private List<string> _items = new();

        public IEnumerator<string> GetEnumerator()
        {
            return _items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}