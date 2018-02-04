using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChairMovement : MonoBehaviour
{

    public float chairSpeed = 0.5f;
    public Vector3 chairPos;

    // Update is called once per frame
    void Update()
    {
        float yPos = gameObject.transform.position.y +  (Random.Range(-50, 50) * chairSpeed);
        float xPos = gameObject.transform.position.x + (Random.Range(-50, 50) * chairSpeed);
        chairPos = new Vector3(Mathf.Clamp(xPos, -40, 40), Mathf.Clamp(yPos, -40, 40), 0);
        gameObject.transform.position = chairPos;
    }
}

