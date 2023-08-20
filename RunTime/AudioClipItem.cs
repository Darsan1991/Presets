using DGames.Essentials.Attributes;
using UnityEngine;

namespace DGames.Presets.Scriptable
{
    [DashboardType(tabPath:"Presets/Audios",displayName:"Clip")]
    public class AudioClipItem : ScriptableObject
    {
        [SerializeField] private UnityEngine.AudioClip _value;

        public UnityEngine.AudioClip Value => _value;

        public static implicit operator UnityEngine.AudioClip(AudioClipItem color) => color.Value;
    }
}