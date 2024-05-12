#if TMP
using System;
using System.Linq;
using TMPro;
using UnityEngine;

namespace DGames.Presets.Components
{
    public static class TMPFontableComponentFactory
    {
        public static ITMPFontable Create(Component c)
        {
            return ComponentToTextable(c) ?? ComponentToTextable(
                new[] { typeof(TMP_Text) }
                    .Select(t =>
                    {
                        var component = c.GetComponent((Type)t);
                        return component;
                    }).FirstOrDefault(t=>t));
            
        }
        
        public static ITMPFontable ComponentToTextable(Component c)
        {
            return c switch
            {
                TMP_Text text => new TMPFontable(text),
                _ => null
            };
        }
    }
}
#endif