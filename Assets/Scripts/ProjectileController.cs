﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour {

    public GameObject smokeEmitter;

    private bool dying = false;

	// Use this for initialization
	void Start () {
        Destroy(gameObject, 20);
	}
	
	// Update is called once per frame
	void Update () {
        if (!dying)
        {
            transform.GetChild(0).transform.rotation = Quaternion.LookRotation(GetComponent<Rigidbody>().velocity);
        }
        if(transform.position.y<-10.0f)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!dying)
        {
            if (collision.collider.tag.Equals("GroundCollider"))
            {
                dying = true;
                smokeEmitter.GetComponent<ParticleSystem>().Stop();
                Destroy(transform.GetChild(0).gameObject, 10);
                transform.GetChild(0).SetParent(null);
                Destroy(gameObject, 1);
            }
            else if (collision.collider.tag.Equals("WaterCollider"))
            {
                dying = true;
                Destroy(gameObject);
            }
        }
    }
}
