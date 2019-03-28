using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour {
    
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
                Destroy(transform.GetChild(0).gameObject, 10);
                transform.GetChild(0).transform.rotation = Quaternion.Euler(new Vector3(90,90,90));
                transform.GetChild(0).SetParent(null);
                Destroy(gameObject, 1);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(!dying)
        {
            if(other.tag.Equals("WaterCollider"))
            {
                Destroy(gameObject);
            }
        }
    }
}
