using UnityEngine;

public class ExplosionController : MonoBehaviour
{
    public float Durata = 10f;

    private float TimeStart;
    private float TimeCurrent;

    private AudioSource xAudioFiamme;

    void Start()
    {
        TimeStart = Time.time;
        xAudioFiamme = GetComponent<AudioSource>();
        xAudioFiamme.PlayDelayed(1f);
    }

    void Update()
    {
        TimeCurrent = Time.time;
        if (TimeCurrent - TimeStart > Durata)
        {
            Destroy(this.gameObject);
        }
    }

    void FixedUpdate()
    {
        float spostamento = (1.5f * Time.deltaTime);
        Vector3 newpos = new Vector3(this.transform.position.x - spostamento, this.transform.position.y, this.transform.position.z);
        this.transform.position = newpos;
    }

    /*
    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Explosion.Coll-enter-> " + collision.collider.name);
    }

    void OnParticleCollision(GameObject other)
    {
        Debug.Log("Explosion.PColl-> " + other.name);
    }
    */


}
