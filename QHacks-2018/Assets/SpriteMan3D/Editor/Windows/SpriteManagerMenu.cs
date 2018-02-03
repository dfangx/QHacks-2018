using SpriteMan3D.UnityEditor.Data;
using SpriteMan3D.Templates;
using UnityEditor;
using UnityEngine;

namespace SpriteMan3D.UnityEditor
{
    /// <summary>
    /// Contains menu items for the Tools\SpriteMan 3D menu.
    /// </summary>
    internal class SpriteManagerMenu : MonoBehaviour
    {
        /// <summary>
        /// Opens the SpriteMan tools window.
        /// </summary>
        [MenuItem("Tools/SpriteMan 3D/SpriteMan Tools Window", priority = 12)]
        static void OpenSpriteManToolsWindow()
        {
            var window = EditorWindow.GetWindow<SpriteManToolsWindow>(false, "SpriteMan");
            window.Show();
        }
    }
}
