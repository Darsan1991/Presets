using DGames.Essentials.Attributes;
using UnityEngine;

namespace DGames.Presets.Scriptable
{
    [DashboardType(tabPath:"Presets/Color",displayName:"Color")]
    public class Color : ScriptableObject
    {
        [SerializeField] private UnityEngine.Color _value;

        public UnityEngine.Color Value => _value;

        public static implicit operator UnityEngine.Color(Color color) => color.Value;
    }
}