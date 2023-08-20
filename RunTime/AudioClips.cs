using System.Collections;
using System.Collections.Generic;
using DGames.Essentials.Attributes;
using UnityEngine;

namespace DGames.Presets.Scriptable
{
    [DashboardType(tabPath:"Presets/Audios",displayName:"Clip Groups")]
    public class AudioClips : ScriptableObject,IEnumerable<UnityEngine.AudioClip>
    {
        [SerializeField] private List<UnityEngine.AudioClip> _items = new();

        public IEnumerator<UnityEngine.AudioClip> GetEnumerator()
        {
            return _items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}