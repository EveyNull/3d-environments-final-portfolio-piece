using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeCutsceneCamera : MonoBehaviour {

    public Transform bridgeCutsceneDest;
    public Transform overShoulderDest;
    public GameObject playerCameraParent;
    public float speed = 3f;
    public float pauseTime = 1f;

    private bool cutscenePlaying = false;
    private bool movingToCutsceneDest = true;
    private float timeElapsed = 0f;
    private float pauseTimer = 0f;

    // Use this for initialization
    void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if(cutscenePlaying)
        {
            timeElapsed += Time.deltaTime;
            float step = speed * Time.deltaTime;
            if (movingToCutsceneDest)
            {
                transform.position = Vector3.MoveTowards(transform.position, bridgeCutsceneDest.position, step);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, bridgeCutsceneDest.rotation, step * 3.5f);
                if (transform.position == bridgeCutsceneDest.position
                    && transform.rotation == bridgeCutsceneDest.rotation)
                {
                    movingToCutsceneDest = false;
                }
            }
            else
            {
                pauseTimer += Time.deltaTime;
                if (pauseTimer > pauseTime)
                {
                    transform.position = Vector3.MoveTowards(transform.position, overShoulderDest.position, step);
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, overShoulderDest.rotation, step*3.5f);
                    if (transform.position == overShoulderDest.position
                        && transform.rotation == overShoulderDest.rotation)
                    {
                        cutscenePlaying = false;
                        transform.parent = playerCameraParent.transform;
                        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<OverShoulderCam>().enabled = true;
                        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().enabled = true;
                        GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().enabled = true;
                    }
                }
            }
        }
	}

    public void StartCutscene()
    {
        cutscenePlaying = true;
        transform.parent = null;
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<OverShoulderCam>().enabled = false;
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().enabled = false;
        GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().enabled = false;
    }
}
