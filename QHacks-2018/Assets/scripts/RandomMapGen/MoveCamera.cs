using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{

    public float speed = 4f;

    private Vector3 startPos;
    private bool moving;

    private void FixedUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        { 
            startPos = Input.mousePosition;
            moving = true;
        }

        if (Input.GetMouseButtonUp(0) && moving)
            moving = false;

        if (moving)
        {
            Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - startPos);
            Vector3 move = new Vector3(pos.x * speed, pos.y * speed, 0);
            transform.Translate(move, Space.Self);
        }
    }
}
