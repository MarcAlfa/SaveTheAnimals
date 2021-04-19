using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeedCollider : MonoBehaviour
{

    /*
    public MeshCollider Collider_Mesh;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("weed-coll-enter: " + Collider_Mesh.gameObject.name);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    */

    private ParticleSystem ps;
    private ParticleSystem.CollisionModule coll;



    void Start()
    {
        ps = GetComponent<ParticleSystem>();
        coll = ps.collision;
        coll.enabled = true;
        //coll.bounce = 0.5f;
    }

    private void Update()
    {
        //Debug.Log("WeedController-> " + ps.particleCount + coll);
        //ps.pa

    }

    void OnParticleCollision(GameObject other)
    {
        Debug.Log("OnParticleCollision-> " + other.name);
        //if (other.name == "Player")
        //{
        //    Destroy()
        //}
    }

    private void OnParticleTrigger()
    {
        Debug.Log("OnParticleTrigger-> ");
    }


    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("weed-coll-enter: " + collision.collider.name);
    }
    void OnCollisionStay(Collision collision)
    {
        Debug.Log("weed-coll-stay: " + collision.collider.name);
    }
    void OnCollisionExit(Collision collision)
    {
        Debug.Log("weed-coll-exit: " + collision.collider.name);
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("weed-trig-enter: " + other.name);
    }
    void OnTriggerStay(Collider other)
    {
        Debug.Log("weed-trig-stay: " + other.name);
    }
    void OnTriggerExit(Collider other)
    {
        Debug.Log("weed-trig-exit: " + other.name);
    }



}
