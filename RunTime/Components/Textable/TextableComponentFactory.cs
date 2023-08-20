using System.Linq;
using DGames.Essentials;
using UnityEngine;
using UnityEngine.UI;

namespace DGames.Presets.Components
{
    public static class TextableComponentFactory
    {
        public static ITextable Create(Component go)
        {
            return ComponentToTextable(go) ?? ComponentToTextable(
                new[] { typeof(Text),typeof(TextMesh) }
                    .Select(t =>
                    {
                        var component = go.GetComponent(t);
                        return component;
                    }).FirstOrDefault(t=>t));
            
        }
        
        public static ITextable ComponentToTextable(Component c)
        {
            return c switch
            {
                Text text => new TextLegacyTextable(text),
                TextMesh mesh => new TextMeshTextable(mesh),
                _ => null
            };
        } 
    }
}