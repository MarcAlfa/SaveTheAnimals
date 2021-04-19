using System.IO;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public Enemy[] Enemies;

    private float TimeStart;
    private float TimeCurrent;

    // Start is called before the first frame update
    void Start()
    {
        TimeStart = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        TimeCurrent = Time.time;

        if (Enemies.Length > 0)
        {
            foreach (Enemy xenemy in Enemies)
            {
                // WEED
                if (xenemy.EnemyOggetto.gameObject.name == "WeedSimple")
                {
                    if (TimeCurrent - TimeStart > xenemy.EnemyFrequenza)
                    {
                        Instantiate(xenemy.EnemyOggetto.gameObject);
                        TimeStart = TimeCurrent;
                    }
                    //Instantiate(xenemy.EnemyOggetto.gameObject);
                    //Debug.Log("Enemy= " + xenemy.EnemyOggetto.gameObject.name);
                }
            }
        }
    }
}
