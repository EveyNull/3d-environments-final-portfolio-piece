using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileDeath : MonoBehaviour {

    public GameObject smokeEmitter;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.GetChild(0).transform.rotation = Quaternion.LookRotation(GetComponent<Rigidbody>().velocity);
    }

    private void OnCollisionEnter(Collision collision)
    {
        smokeEmitter.GetComponent<ParticleSystem>().Stop();
        transform.GetChild(0).SetParent(null);
        Destroy(gameObject, 1);
    }
}
