using DGames.Essentials.Attributes;
using UnityEngine;

namespace DGames.Presets
{
    [ExecuteInEditMode]
    public abstract class Preset<T, TJ> : Preset<T>
    {
        [SerializeField] protected bool manualTarget;
        [Condition(nameof(manualTarget),true)][SerializeField] protected Component target;

        private TJ _target;

        public TJ Target => _target ??= GetTarget(manualTarget? target:this);
        
        protected abstract TJ GetTarget(Component component);
    }
    
    [ExecuteInEditMode]
    public abstract class Preset<T> : MonoBehaviour
    {
        [SerializeField] protected PresetInfo<T> info;
       


        private IPresets<T> _presets;
        public bool Active
        {
            get => enabled;
            set => enabled = value;
        }

        public IPresets<T> Presets =>

            _presets ??= PresetServer.Get<T>();
        
        
        public T Value { get; protected set; }



        protected virtual void Start()
        {
            UpdateValue();
            
            // Debug.Log(ScriptableUtils.GetDefault<IPresets<T>>());
            if (ErrorIfNoPreset())
                return;

            Presets.Updated += UpdateValue;
        }

        protected virtual void OnDestroy()
        {
            if (ErrorIfNoPreset())
                return;
            Presets.Updated -= UpdateValue;
        }

        protected bool ErrorIfNoPreset()
        {
            if (Presets != null) return false;
            Debug.LogError("Presets Not Registered");
            return true;

        }
        

        [ContextMenu(nameof(UpdateValue))]
        protected void UpdateValue()
        {
            if (!Active)
            {
                return;
            }
            if (string.IsNullOrEmpty(info.key)) return;
            if (ErrorIfNoPreset())
                return;
            // Debug.Log($"{info.key}"+Presets.HasKey(info.key));
            Value = Presets.Get(info.key, info.def);
            OnSetValue(Value);
        }

        protected abstract void OnSetValue(T value);
        


        protected virtual void OnValidate()
        {
            UpdateValue();
            
        }
    }
}