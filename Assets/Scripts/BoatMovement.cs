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
            if(transform.position == destinations[currentDestination].position
                && currentDestination < destinations.Length-1)
            {
                currentDestination++;
            }
            
        }
        if(transform.position == destinations[destinations.Length-1].position)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag.Equals("Player"))
        {
            foreach(GameObject box in boxes)
            {
                Debug.Log(box.GetComponent<BoxesHitBehaviour>().GetBeenHit());
                if(!box.GetComponent<BoxesHitBehaviour>().GetBeenHit())
                {
                    SetSail();
                    return;
                }
            }
        }
    }

    void SetSail()
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

        Camera.main.transform.parent = null;
        Camera.main.transform.position = new Vector3(0, 20, 0);
        Camera.main.GetComponent<OverShoulderCam>().cameraStatic = true;
    }
}
