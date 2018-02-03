using System.Linq;
using UnityEngine;

namespace SpriteMan3D.Demo.Scripts
{
    /// <summary>
    /// Simple inventory.
    /// </summary>
    public class CharacterInventory : MonoBehaviour
    {
        /// <summary>
        /// Array of inventory items.
        /// </summary>
        public RpgInventoryItem[] items;

        /// <summary>
        /// Returns an inventory item if found.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public RpgInventoryItem GetInventoryItem(string key)
        {
            return items.FirstOrDefault(o => o.key == key);
        }
    }
}
