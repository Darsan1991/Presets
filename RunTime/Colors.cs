using System.Collections;
using System.Collections.Generic;
using DGames.Essentials.Attributes;
using UnityEngine;

namespace DGames.Presets.Scriptable
{
    [DashboardType(tabPath:"Presets/Color",displayName:"Color Groups")]
    public class Colors : ScriptableObject,IEnumerable<UnityEngine.Color>
    {
        [SerializeField] private List<UnityEngine.Color> _items = new();

        public IEnumerator<UnityEngine.Color> GetEnumerator()
        {
            return _items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}