using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FXManager : MonoBehaviour
{
    public GameObject WeedObject;   // prefab

    private ParticleSystem[] particles;
    //private ParticleSystem xFX;

    private float TimeStart;
    private float TimeCurrent;


    private void Start()
    {
        TimeStart = Time.time;
    }

    private void Update()
    {
        TimeCurrent = Time.time;
        if (TimeCurrent - TimeStart > 2.7f)
        {
            GameObject Weed = Instantiate(WeedObject);
            TimeStart = Time.time;
        }
    }

    public ParticleSystem FindFXParticle(string name)
    {
        particles = FindObjectsOfType<ParticleSystem>();
        foreach (ParticleSystem part in particles)
        {
            if (part.name == name)
            {
                return part;
            }
        }
        return null;
    }

}
