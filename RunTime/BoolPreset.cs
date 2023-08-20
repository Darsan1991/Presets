using DGames.Essentials.Attributes;
using UnityEngine;

namespace DGames.Presets
{
    public class BoolPreset : Preset<bool>
    {
        [SerializeField] private Target _target;

        [Condition(nameof(_target), Target.Behavior)] [SerializeField]
        private Behaviour _behaviour;
        
        protected override void OnSetValue(bool value)
        {
            if (_target == Target.GameObject)
            {
                gameObject.SetActive(value);
            }
            else if (_target == Target.Behavior && _behaviour)
            {
                _behaviour.enabled = value;
            }
        }
        
        public enum Target
        {
            GameObject,Behavior
        }
    }
}