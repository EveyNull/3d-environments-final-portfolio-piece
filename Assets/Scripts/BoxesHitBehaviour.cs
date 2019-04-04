using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxesHitBehaviour : MonoBehaviour {

    private bool beenHit = false;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag.Equals("Projectile"))
        {
            beenHit = true;
        }
    }

    public bool GetBeenHit()
    {
        return beenHit;
    }
}
