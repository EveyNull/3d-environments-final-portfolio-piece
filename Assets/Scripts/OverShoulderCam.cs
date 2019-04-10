using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverShoulderCam : MonoBehaviour {

    public float cameraMoveSpeed;
    public bool cameraStatic = false;

    public Transform overShoulderCameraDest;
    public Transform cameraTarget;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {


        float step = cameraMoveSpeed * Time.deltaTime;

        if (!cameraStatic)
        {
            Vector3 desiredPosition = Vector3.MoveTowards(transform.position, overShoulderCameraDest.position, step);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, overShoulderCameraDest.rotation, step);

            RaycastHit hit;
            Ray ray = new Ray(cameraTarget.transform.position, desiredPosition - cameraTarget.position);


            if (Physics.Raycast(ray, out hit,
                Vector3.Distance(cameraTarget.transform.position, desiredPosition)))
            {
                Debug.DrawRay(cameraTarget.transform.position, desiredPosition - cameraTarget.position, Color.red);
                transform.position = hit.point;
            }
            else
            {
                transform.position = desiredPosition;
                Debug.DrawRay(cameraTarget.transform.position, transform.position - cameraTarget.position, Color.green);
            }
        }
        else
        {
            transform.LookAt(cameraTarget);
        }
    }
}
