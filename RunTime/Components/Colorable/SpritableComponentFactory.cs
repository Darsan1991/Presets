using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace DGames.Presets.Components
{
    public static class SpritableComponentFactory
    {
        public static ISpritable Create(Component go)
        {
          
            return ComponentToSpritable(go) ?? ComponentToSpritable(
                new[] { typeof(SpriteRenderer), typeof(Image) }
                    .Select(t =>
                    {
                        var component = go.GetComponent((Type)t);
                        return component;
                    }).FirstOrDefault(t=>t));


        }

        public static ISpritable ComponentToSpritable(Component c)
        {
            return c switch
            {
                SpriteRenderer spriteRenderer => new SpriteSpritable(spriteRenderer),
                Image image => new ImageSpritable(image),
                _ => null
            };
        }
    }
}