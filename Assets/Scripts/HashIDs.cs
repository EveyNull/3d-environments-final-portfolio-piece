using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HashIDs : MonoBehaviour {

    public int speedFloat;
    public int horizontalState;
    public int strafeState;
    public int catapultControlState;
    public int onBoatState;

    // Use this for initialization
    void Start () {
        speedFloat = Animator.StringToHash("Speed");
        horizontalState = Animator.StringToHash("Horizontal");
        strafeState = Animator.StringToHash("Strafing");
        catapultControlState = Animator.StringToHash("CatapultControl");
        onBoatState = Animator.StringToHash("OnBoat");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
