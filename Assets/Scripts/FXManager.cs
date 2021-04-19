using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FXManager : MonoBehaviour
{
    private ParticleSystem[] particles;

    private ParticleSystem xWeedFX;

    public ParticleSystem FindFXParticle(string name)
    {
        particles = FindObjectsOfType<ParticleSystem>();
        foreach (ParticleSystem part in particles)
        {
            //Debug.Log("FX= " + part.name);
            if (part.name == name)
            {
                return part;
            }
        }
        return null;
    }

    private void Start()
    {
        //xWeedFX = this.FindFXParticle("FX_TumbleWeed");
        //xWeedFX.Play();
    }
}
