using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public float speedDampTime = 0.01f;
    public float sensitivityX = 1.0f;

    private Animator anim;
    private HashIDs hash;
    
	// Use this for initialization
	void Awake () {
        anim = GetComponent<Animator>();
        hash = GameObject.FindGameObjectWithTag("GameController").GetComponent<HashIDs>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        float v = Input.GetAxis("Vertical");
        //float mouseX = Input.GetAxis("MouseX");

        //Rotating(mouseX);
        MovementManager(v);
	}

    void MovementManager(float vertical)
    {
        if(vertical>0)
        {
            anim.SetFloat(hash.speedFloat, 1.5f, speedDampTime, Time.deltaTime);
        }
        else
        {
            anim.SetFloat(hash.speedFloat, 0);
        }
    }

    void Rotating(float mouseXInput)
    {

    }
}
