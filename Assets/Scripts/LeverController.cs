using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverController : MonoBehaviour {

    public GameObject leverBase;
    public GameObject bridge;

    private bool triggered = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

	}

    private void OnTriggerStay(Collider other)
    {
        if (other.tag.Equals("InteractionCollider")
            && Input.GetAxis("Interact") == 1
            && !triggered)
        {
            triggered = true;
            MoveToPosition[] bridgeComponents = bridge.GetComponentsInChildren<MoveToPosition>();
            foreach (MoveToPosition component in bridgeComponents)
            {
                component.enabled = true;
            }
            leverBase.GetComponent<MoveToPosition>().enabled = true;
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<BridgeCutsceneCamera>().StartCutscene();
        }
    }
}
