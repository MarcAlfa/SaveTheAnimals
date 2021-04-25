using UnityEngine;

public class HitEffectController : MonoBehaviour
{

    public float Durata = 1f;
    public GameObject ExplosionObject;   // prefab explosion, dopo firealto


    private float TimeStart;
    private float TimeCurrent;

    private SoundManager xAudioManager;
    private GameObject ExplosionInstance; // instanza del prefab explosion


    void Start()
    {
        TimeStart = Time.time;
        this.transform.parent = null;  // una volta che parte questa esplosioni, si stacca dal parent (player)
        xAudioManager = FindObjectOfType<SoundManager>();
        xAudioManager.Play("EXPLOSION", 0f);
        ExplosionInstance = Instantiate(ExplosionObject);
    }

    void Update()
    {
        TimeCurrent = Time.time;
        if (TimeCurrent - TimeStart > Durata)
        {
            Destroy(this.gameObject);
        }
    }


    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("HitEf.Coll-enter-> " + collision.collider.name);
    }

    void OnParticleCollision(GameObject other)
    {
        Debug.Log("HitEf.PColl-> " + other.name);
    }



}
