using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightInteractable : MonoBehaviour {

    public float displayDistance = 5.0f;

    private GameObject player;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
		if(Vector3.Distance(transform.position, player.transform.position) > displayDistance)
        {
            GetComponent<ParticleSystem>().Stop();
        }
        else
        {
            GetComponent<ParticleSystem>().Play();
        }
	}
}
