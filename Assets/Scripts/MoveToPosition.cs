using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToPosition : MonoBehaviour {
    
    public Transform destPosition;

    public float speed = 3.0f;
    public float delayTime = 0.0f;

    public GameObject parentAnimator;

    private float elapsedTime = 0;
    private bool moving = false;
    private bool finishedMoving = false;
    private bool countdown = false;
    private MoveToPosition parentAnimatorScript;

	// Use this for initialization
	void Start () {
        if(parentAnimator)
        {
            parentAnimatorScript = parentAnimator.GetComponent<MoveToPosition>();
        }
	}

    // Update is called once per frame
    void FixedUpdate () {
        if(parentAnimatorScript)
        {
            if(parentAnimatorScript.GetMoving())
            {
                countdown = true;
            }
        }
        else
        {
            countdown = true;
        }

        if(countdown)
        {
            elapsedTime += Time.deltaTime;
        }

        if (elapsedTime > delayTime
            && !finishedMoving)
        {
            moving = true;
            elapsedTime = 0;
        }

        if(moving)
        {
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, destPosition.position, step);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, destPosition.rotation, step*10);
            transform.localScale = Vector3.MoveTowards(transform.localScale, destPosition.localScale, step*0.5f);
        }

        if(transform.position == destPosition.position
            && transform.rotation == destPosition.rotation
            && transform.localScale == destPosition.localScale)
        {
            finishedMoving = true;
            moving = false;
        }
	}

    public bool GetMoving()
    {
        return moving;
    }
}
