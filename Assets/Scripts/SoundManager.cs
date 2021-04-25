using UnityEngine.Audio;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    private AudioSource[] suoni;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        suoni = gameObject.GetComponents<AudioSource>();
    }

    public void Play(string name, float delay)
    {
        foreach (AudioSource xsuono in suoni)
        {
            if (xsuono.clip.name == name)
            {
                    xsuono.PlayDelayed(delay);
            }
        }
    }

}
