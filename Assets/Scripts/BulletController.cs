using UnityEngine;

public class BulletController : MonoBehaviour
{

    private Rigidbody body;
    private ParticleSystem part;

    private float TimeStart;
    private float TimeCurrent;


    // Start is called before the first frame update
    void Start()
    {
        TimeStart = Time.time;
        body = GetComponent<Rigidbody>();
        body.AddForce(this.transform.forward * 3000);
        part = GetComponent<ParticleSystem>();
        part.Play();
    }

    void Update()
    {
        TimeCurrent = Time.time;
        if (TimeCurrent - TimeStart > 0.8f)   // se modificato time va modificata anche animazione del particle system
        {
            Destroy(this.gameObject);
        }
    }

    void FixedUpdate()
    {
        body.AddForce(this.transform.forward * 3000 * Time.deltaTime);  // se modificata "forza", va modificato anche Particle system
    }

    void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("BULLET-coll-enter-> " + collision.collider.name);
    }
    void OnCollisionStay(Collision collision)
    {
        //Debug.Log("BULLET-coll-stay-> " + collision.collider.name);
    }
    void OnCollisionExit(Collision collision)
    {
        //Debug.Log("BULLET-coll-exit-> " + collision.collider.name);
    }

    void OnTriggerEnter(Collider other)
    {
        //Debug.Log("bullet-trig-enter: " + other.name);
    }
    void OnTriggerStay(Collider other)
    {
        //Debug.Log("bullet-trig-stay: " + other.name);
    }
    void OnTriggerExit(Collider other)
    {
        //Debug.Log("bullet-trig-exit: " + other.name);
    }


    void OnParticleCollision(GameObject other)
    {
        Debug.Log("BULLET.OnParticleCollision-> " + other.name);
    }


}
