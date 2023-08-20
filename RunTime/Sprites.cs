using System.Collections;
using System.Collections.Generic;
using DGames.Essentials.Attributes;
using UnityEngine;

namespace DGames.Presets.Scriptable
{
    [DashboardType(tabPath:"Presets/Sprite",displayName:"Sprite Groups")]
    public class Sprites : ScriptableObject,IEnumerable<UnityEngine.Sprite>
    {
        [SerializeField] private List<UnityEngine.Sprite> _items = new();

        public IEnumerator<UnityEngine.Sprite> GetEnumerator()
        {
            return _items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}