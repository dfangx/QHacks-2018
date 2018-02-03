using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {

    // Use this for initialization
    public float carSpeed = 1;
    public Vector3 playerPos;
	// Update is called once per frame
	void Update () {
        float yPos = gameObject.transform.position.y +(Input.GetAxis("Vertical")*carSpeed);
        float xPos = gameObject.transform.position.x + (Input.GetAxis("Horizontal") * carSpeed);
        playerPos = new Vector3(Mathf.Clamp(xPos, -5, 5), Mathf.Clamp(yPos,-4,4),0);
        gameObject.transform.position = playerPos; 
    }
}
