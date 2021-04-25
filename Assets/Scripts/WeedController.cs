using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeedController : MonoBehaviour
{

    void OnParticleCollision(GameObject other)
    {
        //Debug.Log("WEED.PColl-> " + other.name);

        if (other.tag == "Player" || other.tag == "WEAPON" || other.tag == "FX" || other.tag == "DEATH")
        {
            //Debug.Log("WEED.COLPITOOOOOOOOOOOOO");
            Destroy(this.gameObject);
        }
    }


        void OnCollisionEnter(Collision collision)
        {
            Debug.Log("WEED.coll-enter-> " + collision.collider.name);
        }

        void OnCollisionStay(Collision collision)
        {
            Debug.Log("WEED.coll-stay-> " + collision.collider.name);
        }

        void OnCollisionExit(Collision collision)
        {
            Debug.Log("WEED.coll-exit-> " + collision.collider.name);
        }




        void OnTriggerEnter(Collider other)
        {
            //Debug.Log("weed-trig-enter: " + other.name);
        }
        void OnTriggerStay(Collider other)
        {
            //Debug.Log("weed-trig-stay: " + other.name);
        }
        void OnTriggerExit(Collider other)
        {
            //Debug.Log("weed-trig-exit: " + other.name);
        }

}
