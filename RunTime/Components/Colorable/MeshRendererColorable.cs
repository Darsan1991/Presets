using UnityEngine;

namespace DGames.Presets.Components
{
    public class MeshRendererColorable : IColorable
    {
        private readonly MeshRenderer _renderer;

        public Color Color
        {
            get => Material.color;
            set => Material.color = value;
        }

        private Material Material
        {
            get
            {
                #if UNITY_EDITOR
               return UnityEditor.PrefabUtility.IsPartOfAnyPrefab(_renderer.gameObject) || !Application.isPlaying? _renderer.sharedMaterial : _renderer.material;

#else
   return _renderer.material;
#endif
            }
        }

        public MeshRendererColorable(MeshRenderer renderer)
        {
            _renderer = renderer;
        }
    }
}