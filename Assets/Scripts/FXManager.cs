using System;
using System.Collections;
using UnityEngine;

public class FXManager : MonoBehaviour
{
    public Effetto[] Effetti;     // prefab

    private const int SIGN_MASK = ~Int32.MinValue;


    private ParticleSystem[] particles;
    //private ParticleSystem xFX;

    //private float TimeStart;
    private float TimeCurrent;
    private float TimeDif;
    private int TimeDec;


    private void Start()
    {
        //TimeStart = Time.time;
    }

    private void FixedUpdate()
    {
        TimeCurrent = Time.time;
        if (Effetti.Length > 0)
        {
            foreach (Effetto xeffetto in Effetti)
            {
                if (TimeCurrent >= xeffetto.FXStartDelay)
                {
                    TimeDif = TimeCurrent - xeffetto.FXStartDelay;
                    TimeDec = GetDecimal(TimeCurrent / xeffetto.FXRepeatTime);
                    if (TimeDif == 0f || ( TimeDec == 0 && xeffetto.FXRepeat) )
                    {
                        //Debug.Log("instanza -> " + xeffetto.FXPrefab.name);
                        Instantiate(xeffetto.FXPrefab);
                    }
                }
            }
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


    // duplicato, da decidere dove metterlo... c e anche in AnimalManager
    //[HideInInspector]
    private int GetDecimal(float fvalue)
    {
        double dplaces;

        try
        {
            decimal dvalue = Convert.ToDecimal(fvalue);

            dplaces = (double)((Decimal.GetBits(dvalue)[3] & SIGN_MASK) >> 16);

            return (int)((dvalue - Math.Truncate(dvalue)) * (int)Math.Pow(10d, dplaces));
        }
        catch (Exception ex)
        {
            throw new TypeInitializationException(@"{fvalue} cannot be converted", ex);
        }
    }


}
