using SpriteMan3D.Templates;
using System;

namespace SpriteMan3D.Demo.Scripts
{
    /// <summary>
    /// Inventory item.
    /// </summary>
    [Serializable]
    public class RpgInventoryItem
    {
        /// <summary>
        /// key to find an enventory item by.
        /// </summary>
        public string key;
        /// <summary>
        /// the SpriteMan state of this item.
        /// </summary>
        public SpriteManagerState state;
    }
}
