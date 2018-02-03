using UnityEngine;
using SpriteMan3D.Templates;

namespace SpriteMan3D.Demo.Scripts
{
    /// <summary>
    /// Simple rpg character that has customizable parts.
    /// </summary>
    public class RpgCharacter : MonoBehaviour
    {
        public SpriteManager body;
        public SpriteManager head;
        public SpriteManager torso;
        public SpriteManager legs;
        public SpriteManager feet;
        public SpriteManager weapons;

        public CharacterInventory inventory;

        public void ChangeHead(string key)
        {
            ChangeState(head, key);
        }

        public void ChangeTorso(string key)
        {
            ChangeState(torso, key);
        }

        public void ChangeLegs(string key)
        {
            ChangeState(legs, key);
        }

        public void ChangeFeet(string key)
        {
            ChangeState(feet, key);
        }

        public void ChangeWeapon(string key)
        {
            ChangeState(weapons, key);
        }

        /// <summary>
        /// swaps a given SpriteManager's state from inventory for a given key.
        /// </summary>
        /// <param name="manager"></param>
        /// <param name="key"></param>
        private void ChangeState(SpriteManager manager, string key)
        {
            // hide the game object if key set to 'none'
            if (key == "none")
            {
                manager.gameObject.SetActive(false);
            }
            else
            {
                // find the item and load it
                var item = inventory.GetInventoryItem(key);
                if (item != null && manager != null)
                {
                    manager.CopyFrom(item.state);

                    // display the object if it's hidden
                    if (!manager.gameObject.activeSelf)
                    {
                        manager.gameObject.SetActive(true);
                    }
                }
            }
        }
    }
}
