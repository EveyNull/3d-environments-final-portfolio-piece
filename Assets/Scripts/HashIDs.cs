using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HashIDs : MonoBehaviour {

    public int speedFloat;
    public int horizontalState;
    public int strafeState;

    // Use this for initialization
    void Start () {
        speedFloat = Animator.StringToHash("Speed");
        horizontalState = Animator.StringToHash("Horizontal");
        strafeState = Animator.StringToHash("Strafing");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
