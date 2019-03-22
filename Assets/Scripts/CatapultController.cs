﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatapultController : MonoBehaviour {

    // The time to wait after player changes control state, before accepting another change in control.
    // This is to stop the player immediately snapping to the catapult again after trying to leave
    // (Or vice versa)
    public float stateChangeBufferTime = 0.5f;
    public float cameraMoveSpeed;

    public Transform catapultCameraDest;
    public Transform overShoulderCameraDest;

    private Camera mainCamera;

    private bool playerInControl;
    private float elapsedTime = 0f;
    bool canChangeState = true;

    private GameObject player;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
	}

    private void Update()
    {
        if(!canChangeState)
        {
            elapsedTime += Time.deltaTime;
            if(elapsedTime > stateChangeBufferTime)
            {
                canChangeState = true;
                elapsedTime = 0f;
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate () {
		if(Input.GetButtonDown("Catapult")
            && playerInControl
            && canChangeState)
        {
            playerInControl = false;
            player.GetComponent<PlayerMovement>().enabled = true;
            player.GetComponent<Animator>().SetBool(
            GameObject.FindGameObjectWithTag("GameController").GetComponent<HashIDs>().catapultControlState
            , false);
            player.transform.SetParent(null);
            player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
            player.GetComponent<CapsuleCollider>().enabled = true;
            canChangeState = false;

            mainCamera.transform.SetParent(overShoulderCameraDest.transform.parent.transform);

        }

        if(playerInControl)
        {
            transform.GetComponentInParent<Rigidbody>().AddForce(
            -transform.forward*Input.GetAxis("Vertical")*15f
            , ForceMode.Acceleration);
            Rigidbody parent = GetComponentInParent<Rigidbody>();
            transform.parent.transform.RotateAround(
                transform.position, transform.up, Input.GetAxis("Horizontal")*0.3f);
            player.transform.localPosition = new Vector3(0, 0, 0);

            float step = cameraMoveSpeed * Time.deltaTime;
            mainCamera.transform.position = Vector3.MoveTowards(mainCamera.transform.position, catapultCameraDest.position, step);
            mainCamera.transform.rotation = Quaternion.RotateTowards(mainCamera.transform.rotation, catapultCameraDest.rotation, step*2);
        }
	}

    private void OnTriggerStay(Collider other)
    {
        if (other.tag.Equals("InteractionCollider")
            && Input.GetButtonDown("Catapult")
            && canChangeState)
        {
            if(!playerInControl)
            {
                playerInControl = true;
                player.GetComponent<PlayerMovement>().enabled = false;
                player.GetComponent<Animator>().SetBool(
                GameObject.FindGameObjectWithTag("GameController").GetComponent<HashIDs>().catapultControlState
                , true);
                player.transform.SetParent(transform);
                player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionY | 
                    RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
                player.GetComponent<CapsuleCollider>().enabled = false;
                canChangeState = false;

                mainCamera.transform.SetParent(null);
            }
        }
    }
}
