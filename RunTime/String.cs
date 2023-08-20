using DGames.Essentials.Attributes;
using UnityEngine;

namespace DGames.Presets.Scriptable
{
    [DashboardType(tabPath:"Presets/Texts",displayName:"Text")]
    public class String : ScriptableObject
    {
        [SerializeField] private string _value;

        public string Value => _value;

        public override string ToString()
        {
            return _value;
        }
    }
}