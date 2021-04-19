using UnityEngine;

[System.Serializable]

public class Enemy
{
    public GameObject EnemyOggetto;
    public float EnemyFrequenza = 2f;
    [Range(0, 100)]
    public int EnemyForza = 20;

    //[HideInInspector]
}