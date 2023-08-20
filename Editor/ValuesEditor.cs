using System;
using DGames.Essentials.Editor;

namespace DGames.Presets.Editor
{
    public partial class DirectPresetsEditor : IWindowContent
    {
        public event Action<Essentials.Editor.Editor> NotifyChanged;
    }
}