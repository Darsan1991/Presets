using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace DGames.Presets.Components
{
    public static class FontableComponentFactory
    {
        public static IFontable Create(Component c)
        {
            return ComponentToTextable(c) ?? ComponentToTextable(
                new[] { typeof(Text),typeof(TextMesh) }
                    .Select(t =>
                    {
                        var component = c.GetComponent((Type)t);
                        return component;
                    }).FirstOrDefault(t=>t));
            
        }
        
        public static IFontable ComponentToTextable(Component c)
        {
            return c switch
            {
                Text text => new TextLegacyFontable(text),
                TextMesh mesh => new TextMeshFontable(mesh),
                _ => null
            };
        } 
    }
}