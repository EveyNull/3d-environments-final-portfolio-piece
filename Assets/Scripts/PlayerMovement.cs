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
        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");

        MovementManager(vertical, horizontal);

	}

    void MovementManager(float vertical, float horizontal)
    {
        if (vertical>0)
        {
            float setSpeed = 1.5f;
            if(Input.GetAxis("Sprint")>0)
            {
                setSpeed = 2.5f;
            }
            anim.SetFloat(hash.speedFloat, setSpeed, speedDampTime, Time.deltaTime);
        }
        else if(vertical<0)
        {
            anim.SetFloat(hash.speedFloat, -1.5f, speedDampTime, Time.deltaTime);
            anim.SetFloat(hash.horizontalState, horizontal);
            return;
        }
        else
        {
            anim.SetFloat(hash.speedFloat, 0);
        }
        
        Rigidbody rb = this.GetComponent<Rigidbody>();

        Quaternion deltaRotation = Quaternion.Euler(0f, horizontal, 0f);
        rb.MoveRotation(rb.rotation * deltaRotation);
    }
}
