using DGames.Essentials.Attributes;
using UnityEngine;

namespace DGames.Presets.Scriptable
{
    [DashboardType(tabPath:"Presets/Sprite",displayName:"Sprite")]
    public class SpriteItem : ScriptableObject
    {
        [SerializeField] private UnityEngine.Sprite _value;

        public UnityEngine.Sprite Value => _value;

        public static implicit operator UnityEngine.Sprite(SpriteItem item) => item.Value;
    }
}