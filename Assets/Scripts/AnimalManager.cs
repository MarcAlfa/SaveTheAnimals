using System;
using System.Collections;
using UnityEngine;

public class AnimalManager : MonoBehaviour
{
    public Animal[] Animals;


    private const int SIGN_MASK = ~Int32.MinValue;

    private AnimalController AnimalContr;
    private float TimeCurrent;
    private float TimeDif;
    private int TimeDec;

    // Start is called before the first frame update
    void Start()
    {
    }

    void FixedUpdate()
    {
        TimeCurrent = Time.time;
        //Debug.Log("TimeCurrent -> " + TimeCurrent);
        if (Animals.Length > 0)
        {
            foreach (Animal xanimal in Animals)
            {
                if ( (TimeCurrent >= xanimal.AnimalStartDelay) && (xanimal.AnimalCount > 0) )
                {
                    TimeDif = TimeCurrent - xanimal.AnimalStartDelay;
                    TimeDec = GetDecimal(TimeCurrent / xanimal.AnimalRate);
                    if (TimeDif == 0f || TimeDec == 0) 
                    {
                        AnimalContr = xanimal.AnimalPrefab.GetComponent<AnimalController>();
                        AnimalContr.Speed = xanimal.AnimalSpeed;
                        AnimalContr.PausaDelay = xanimal.AnimalPausaDelay;
                        AnimalContr.PausaTime = xanimal.AnimalPausaTime;
                        StartCoroutine(IstanziaAnimale(xanimal,3));
                    }
                }
            }
        }
    }

    IEnumerator IstanziaAnimale(Animal animale, int intervallo)
    {
        // instanzio il primo subito
        //Debug.Log("AVVIO -->  0" + animale.AnimalPrefab.name);
        Instantiate(animale.AnimalPrefab, this.transform);

        for (int i = 1; i < animale.AnimalCount; i++)
        {
            // pausa 
            yield return new WaitForSecondsRealtime(intervallo);
            //Debug.Log("AVVIO -->  " + i + animale.AnimalPrefab.name);
            animale.AnimalPrefab.GetComponent<AnimalController>().PausaDelay -= i + intervallo;            
            Instantiate(animale.AnimalPrefab, this.transform);
        }
    }



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
