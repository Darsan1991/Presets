using System.Linq;
using DGames.Essentials;
using UnityEngine;
using UnityEngine.UI;

namespace DGames.Presets.Components
{
    public static class ColorableComponentFactory
    {
        public static IColorable Create(Component go)
        {
          
            return ComponentToColorable(go) ?? ComponentToColorable(
                new[] { typeof(SpriteRenderer), typeof(Image), typeof(Text), typeof(MeshRenderer), typeof(Camera) }
                    .Select(t =>
                    {
                        var component = go.GetComponent(t);
                        return component;
                    }).FirstOrDefault(t=>t));


        }

        public static IColorable ComponentToColorable(Component c)
        {
            return c switch
            {
                SpriteRenderer spriteRenderer => new SpriteColorable(spriteRenderer),
                Image image => new ImageColorable(image),
                Text text => new TextColorable(text),
                MeshRenderer meshRenderer => new MeshRendererColorable(meshRenderer),
                Camera camera => new CameraColorable(camera),
                _ => null
            };
        }
    }
}