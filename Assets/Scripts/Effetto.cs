using UnityEngine;

[System.Serializable]
public class Effetto
{
    public GameObject FXPrefab;

    [Range(1, 240)]
    [Tooltip("secondi dalla partenza")]
    public float FXStartDelay;

    [Tooltip("repeat o no")]
    public bool FXRepeat;

    [Range(1, 120)]
    [Tooltip("ulteriori invii - ogni quanto , in secondi - max 120")]
    public int FXRepeatTime;

}