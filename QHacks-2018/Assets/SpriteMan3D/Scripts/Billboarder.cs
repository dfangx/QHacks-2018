using UnityEngine;

namespace SpriteMan3D
{
    /// <summary>
    /// Billboards rendered objects - usually sprites.
    /// </summary>
    [ExecuteInEditMode]
    public class Billboarder : MonoBehaviour
    {
        /// <summary>
        /// Billboard the vertical axis.
        /// </summary>
        public bool billboardV = true;

        void Start()
        {
        }

        /// <summary>
        /// NOTE: This only gets called if there's a Renderer attached to the GameObject.
        /// </summary>
        void OnWillRenderObject()
        {
            if (gameObject.activeInHierarchy)
            {
                var parent = gameObject.transform.parent;

                if (parent != null)
                {
                    var cameraTransform = Camera.current.transform;

                    Billboard(cameraTransform, parent);
                }
                else if (!name.EndsWith("(Clone)")) // rely on root manager parent error - prefabs don't have rootManager.parent
                {
                    Debug.LogErrorFormat("GameObject '{0}' requires a parent object for a SpriteBillboarder to work!",
                        gameObject.name);
                }
            }
        }

        /// <summary>
        /// Billboards this sprite.
        /// </summary>
        /// <param name="cameraTransform"></param>
        /// <param name="parent"></param>
        private void Billboard(Transform cameraTransform, Transform parent)
        {
            var camForward = cameraTransform.forward;
            var lookAtPosition = transform.position + camForward;

            if (!billboardV)
            {
                lookAtPosition = transform.position + new Vector3(camForward.x, 0, camForward.z);
            }

            transform.LookAt(lookAtPosition, parent.up);
        }
    }
}
