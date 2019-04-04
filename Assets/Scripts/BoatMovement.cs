using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatMovement : MonoBehaviour {

    public GameObject[] boxes;

    public Transform[] destinations;

    public float boatMoveSpeed = 10f;

    private GameObject player;
    private int currentDestination = 0;
    private bool sailing = false;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (sailing)
        {

            float step = Time.deltaTime * boatMoveSpeed;
            transform.position = Vector3.MoveTowards(transform.position, destinations[currentDestination].position, step);
            Debug.Log(destinations.Length);
            Debug.Log(currentDestination);
            if(transform.position == destinations[currentDestination].position
                && currentDestination < destinations.Length-1)
            {
                currentDestination++;
            }
            
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.collider);
        if(collision.collider.tag.Equals("Player"))
        {
            foreach(GameObject box in boxes)
            {
                if(!box.GetComponent<BoxesHitBehaviour>().GetBeenHit())
                {
                    sailing = true;
                    player = GameObject.FindGameObjectWithTag("Player");
                    player.transform.SetParent(transform);
                    player.GetComponent<PlayerMovement>().enabled = false;
                    player.GetComponent<Animator>().SetBool(
                    GameObject.FindGameObjectWithTag("GameController").GetComponent<HashIDs>().onBoatState
                    , true);
                    player.transform.localPosition = new Vector3(0, 0, 0);
                    player.transform.rotation = Quaternion.Euler(0, 90, 0);
                    player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionY |
                        RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
                    player.GetComponent<CapsuleCollider>().enabled = false;
                    return;
                }
            }
        }
    }
}
