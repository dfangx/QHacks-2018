using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationTest : MonoBehaviour {
    Animator animator;

	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey(KeyCode.A))
            animator.SetInteger("AnimState", 1);
        else
            animator.SetInteger("AnimState", 0);
    }
}
