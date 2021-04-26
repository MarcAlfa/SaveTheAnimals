using System;
using UnityEngine;

public class AnimalManager : MonoBehaviour
{
    public Animal[] Animals;


    private const int SIGN_MASK = ~Int32.MinValue;

    private AnimalController AnimalContr;
    private float TimeCurrent;
    private int TimeDec;

    // Start is called before the first frame update
    void Start()
    {
    }

    void FixedUpdate()
    {
        TimeCurrent = Time.time;

        if (Animals.Length > 0)
        {
            foreach (Animal xanimal in Animals)
            {
                TimeDec = GetDecimal( (TimeCurrent + xanimal.AnimalStartDelay) / xanimal.AnimalRate);
                if (TimeDec == 0 || TimeCurrent == xanimal.AnimalStartDelay)
                {
                    Debug.Log("AVVIO -->  " + xanimal.AnimalPrefab.name);
                    AnimalContr = xanimal.AnimalPrefab.GetComponent<AnimalController>();
                    AnimalContr.Speed = xanimal.AnimalSpeed;
                    Instantiate(xanimal.AnimalPrefab, this.transform);
                }
            }
        }

    }


    //[HideInInspector]
    public int GetDecimal(float fvalue)
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
