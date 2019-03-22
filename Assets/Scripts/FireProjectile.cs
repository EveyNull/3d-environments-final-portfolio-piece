using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireProjectile : MonoBehaviour {

    public GameObject projectile;
    public float fireVelocity = 10;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
        if(Input.GetButtonDown("Interact"))
        {

           GameObject missile = (GameObject)Instantiate(projectile, transform.position, transform.rotation);
            missile.GetComponent<Rigidbody>().AddForce(missile.transform.forward*fireVelocity,ForceMode.Impulse);
        }

	}
}
