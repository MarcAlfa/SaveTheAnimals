using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireAltoController : MonoBehaviour
{

    public float Durata = 5f;

    private float TimeStart;
    private float TimeCurrent;

    private SoundManager xAudioManager;

    void Start()
    {
        TimeStart = Time.time;
        xAudioManager = FindObjectOfType<SoundManager>();
        xAudioManager.Play("ROCKET", 0.8f);
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
        Debug.Log("FireAlto.Coll-enter-> " + collision.collider.name);
    }

    void OnParticleCollision(GameObject other)
    {
        Debug.Log("FireAlto.PColl-> " + other.name);
    }

}




