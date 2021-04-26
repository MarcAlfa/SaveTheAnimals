using UnityEngine;

[System.Serializable]
public class Animal
{
    public GameObject AnimalPrefab;

    [Range(1, 10)]
    [Tooltip("quanti (1..10) - pausa fissa tra uno e l'altro")]
    public int AnimalCount;

    [Range(1, 120)]
    [Tooltip("primio invio - ritardo dall'inizio, in secondi - max 120")]
    public float AnimalStartDelay;

    [Range(1, 120)]
    [Tooltip("ulteriori invii - ogni quanto , in secondi - max 120")]
    public int AnimalRate;
 
    [Range(1, 240)]
    [Tooltip("velocita (1..240)")]
    public int AnimalSpeed;

    [Range(0, 480)]
    [Tooltip("fa pausa dopo quanti secondi dall inizio (0..480)")]
    public int AnimalPausaDelay;

    [Range(0, 10)]
    [Tooltip("pausa per quanti secondi (1..10)")]
    public int AnimalPausaTime;



}