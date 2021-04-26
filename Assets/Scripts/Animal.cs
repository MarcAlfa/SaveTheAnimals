using UnityEngine;

[System.Serializable]
public class Animal
{
    public GameObject AnimalPrefab;

    [Range(1, 120)]
    public float AnimalRate;

    [Range(1, 100)]
    public float AnimalStartDelay;

    [Range(1, 200)]
    public int AnimalSpeed;
}