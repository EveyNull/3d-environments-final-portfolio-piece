using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HashIDs : MonoBehaviour {

    public int speedFloat;
    public int walkState;

    // Use this for initialization
    void Start () {
        speedFloat = Animator.StringToHash("Speed");
        walkState = Animator.StringToHash("Walk");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
