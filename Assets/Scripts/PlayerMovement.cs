using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public float speedDampTime = 0.01f;
    public float cameraMoveSpeed = 10.0f;

    public Transform overShoulderCameraDest;
    public Transform cameraTarget;

    private Animator anim;
    private HashIDs hash;

    private Camera mainCamera;

    // Use this for initialization
    void Awake ()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        anim = GetComponent<Animator>();
        hash = GameObject.FindGameObjectWithTag("GameController").GetComponent<HashIDs>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");

        MovementManager(vertical, horizontal);

        float step = cameraMoveSpeed * Time.deltaTime;
        
        
        mainCamera.transform.position = Vector3.MoveTowards(mainCamera.transform.position, overShoulderCameraDest.transform.position, step);
        mainCamera.transform.rotation = Quaternion.RotateTowards(mainCamera.transform.rotation, overShoulderCameraDest.rotation, step);

        RaycastHit hit; 
        Vector3 heading = mainCamera.transform.position - cameraTarget.position;
        float distance = heading.magnitude;
        Vector3 direction = heading / distance;

        if (Physics.Raycast(mainCamera.transform.position, direction, out hit))
        {
            mainCamera.transform.position = hit.transform.position;
        }

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
