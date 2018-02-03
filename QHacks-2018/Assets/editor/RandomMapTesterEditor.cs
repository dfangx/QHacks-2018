using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(RandomMapTester))]
public class RandomMapTesterEditor : Editor {

    public override void OnInspectorGUI()
    {
            DrawDefaultInspector();
            var script = (RandomMapTester) target;

            if (GUILayout.Button("Generate Island"))
            {
                if (Application.isPlaying)
                    script.MakeMap();
            }
    }
}
