using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewGame : MonoBehaviour {

    private void OnMouseDown()
    {
        transform.localScale *= 0.9f;
    }
    private void OnMouseUp()
    {
        Application.LoadLevel(1);
    }
}
