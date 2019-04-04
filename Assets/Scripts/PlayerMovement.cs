using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public float speedDampTime = 0.01f;

    private Animator anim;
    private HashIDs hash;

    // Use this for initialization
    void Awake ()
    {
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
        bool mustStop = true;
        if (vertical>0)
        {
            if (Physics.Raycast(transform.position + transform.up + (transform.forward*1.5f), Vector3.down, 8f))
            {
                mustStop = false;
                float setSpeed = 1.5f;
                if (Input.GetAxis("Sprint") > 0)
                {
                    setSpeed = 2.5f;
                }
                anim.SetFloat(hash.speedFloat, setSpeed, speedDampTime, Time.deltaTime);
            }
        }

        if(vertical<0)
        {
            if(Physics.Raycast(transform.position + transform.up - transform.forward, Vector3.down, 8f))
            {
                mustStop = false;
                anim.SetFloat(hash.speedFloat, -1.5f, speedDampTime, Time.deltaTime);
                if (Physics.Raycast(transform.position + transform.up + (transform.right * horizontal), Vector3.down, 8f))
                {
                    anim.SetFloat(hash.horizontalState, horizontal);
                }
                else
                {
                    anim.SetFloat(hash.horizontalState, 0f);
                }
                return;
            }
        }

        if (mustStop)
        {
            anim.SetFloat(hash.speedFloat, 0);
        }

        Rigidbody rb = this.GetComponent<Rigidbody>();

        Quaternion deltaRotation = Quaternion.Euler(0f, horizontal, 0f);
        rb.MoveRotation(rb.rotation * deltaRotation);
    }
}
