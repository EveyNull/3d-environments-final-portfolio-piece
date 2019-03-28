using System.Collections;
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
    public Transform firePosition;
    public Transform torsionUpDest;
    public Transform torsionDownDest;

    public GameObject torsion;
    public GameObject projectile;
    public GameObject placeHolderProjectile;

    public float minimumVelocity = 20.0f;
    public float maximumVelocity = 40.0f;
    public float alterVelocityPerFrame = 0.3f;

    private bool maxVelocityReached = false;

    private Camera mainCamera;

    private bool playerInControl;
    private float elapsedTime = 0f;
    bool canChangeState = true;

    private bool firing = false;
    private float fireVelocity = 0.0f;

    private GameObject player;

    private float initHeightAtDist;

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
        if (playerInControl)
        {

            if (Input.GetButtonUp("Interact"))
            {
                if (fireVelocity > minimumVelocity)
                {
                    GameObject missile = (GameObject)Instantiate(projectile, firePosition.position, firePosition.rotation);
                    missile.GetComponent<Rigidbody>().AddForce(missile.transform.forward * fireVelocity, ForceMode.Impulse);
                }
                placeHolderProjectile.GetComponent<MeshRenderer>().enabled = false;
                foreach (ParticleSystem particle in placeHolderProjectile.GetComponentsInChildren<ParticleSystem>())
                {
                    particle.Clear();
                    particle.Stop();
                }
                placeHolderProjectile.GetComponentInChildren<Light>().enabled = false;
                fireVelocity = 0.0f;
                maxVelocityReached = false;
                firing = false;
            }

            if (fireVelocity > minimumVelocity)
            {
                placeHolderProjectile.GetComponent<MeshRenderer>().enabled = true;
                foreach (ParticleSystem particle in placeHolderProjectile.GetComponentsInChildren<ParticleSystem>())
                {
                    if (!particle.isPlaying)
                    {
                        particle.Play();
                    }
                }
                placeHolderProjectile.GetComponentInChildren<Light>().enabled = true;
            }
            
            if(Input.GetButtonDown("Interact"))
            {
                firing = true;
            }

            if (Input.GetButton("Interact")
                && playerInControl)
            {
                fireVelocity = Mathf.Clamp(fireVelocity + alterVelocityPerFrame, 0.0f, maximumVelocity);
                if (fireVelocity >= maximumVelocity)
                {
                    maxVelocityReached = true;
                }
                if (torsion.transform.eulerAngles.x < 350.0f)
                {
                    torsion.transform.rotation = Quaternion.RotateTowards(torsion.transform.rotation, torsionDownDest.rotation, Time.deltaTime * 75.0f);
                }
            }

            if(!firing)
            {
                torsion.transform.rotation = Quaternion.RotateTowards(torsion.transform.rotation, torsionUpDest.rotation, Time.deltaTime*500.0f);
            }
            
        }
    }

    // Update is called once per frame
    void FixedUpdate () {
		if(Input.GetButtonDown("Catapult")
            && playerInControl
            && canChangeState
            && !firing)
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
            
            player.transform.localPosition = new Vector3(0, 0, 0);

            float step = cameraMoveSpeed * Time.deltaTime;
            if(firing)
            {
                if (!maxVelocityReached)
                {
                    mainCamera.transform.Translate(Vector3.back * step*0.5f);
                    mainCamera.fieldOfView = Mathf.Clamp(mainCamera.fieldOfView + step * 3, 60, 130);
                }
            }
            else
            {

                mainCamera.fieldOfView = Mathf.Clamp(mainCamera.fieldOfView - step * 10, 60, 130);
                mainCamera.transform.position = Vector3.MoveTowards(mainCamera.transform.position, catapultCameraDest.position, step*3);
                mainCamera.transform.rotation = Quaternion.RotateTowards(mainCamera.transform.rotation, catapultCameraDest.rotation, step * 2);
                transform.GetComponentInParent<Rigidbody>().AddForce(
                    -transform.forward * Input.GetAxis("Vertical") * 15f
                    , ForceMode.Acceleration);
                Rigidbody parent = GetComponentInParent<Rigidbody>();
                transform.parent.transform.RotateAround(
                    transform.position, transform.up, Input.GetAxis("Horizontal") * 0.3f);
            }
        }
	}

    // Calculate the frustum height at a given distance from the camera.
    float FrustumHeightAtDistance(float distance)
    {
        return 2.0f * distance * Mathf.Tan(mainCamera.fieldOfView * 0.5f * Mathf.Deg2Rad);
    }

    // Calculate the FOV needed to get a given frustum height at a given distance.
    float FOVForHeightAndDistance(float height, float distance)
    {
        return 2.0f * Mathf.Atan(height * 0.5f / distance) * Mathf.Rad2Deg;
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
