using UnityEngine;

namespace SpriteMan3D.Demo
{
    public class SceneSetup : MonoBehaviour
    {
        public float gravity = -200f;
        void Start()
        {
            Physics.gravity = new Vector3(0f, gravity, 0f);
        }
    }
}
